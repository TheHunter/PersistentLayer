using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.DAL.NhPersistentLayer;
using NHibernate.DAL.NhPersistentLayer.Exceptions;

namespace NHibernate.DAL.NhPersistentLayer.Imp.Util
{
    /// <summary>
    /// Extensions methods for DAO's.
    /// </summary>
    public static class NhQueryExtensions
    {
        /// <summary>
        /// Converts lambda tree expression into IFutureValue object.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        internal static IFutureValue<TResult> ToFutureValue<TSource, TResult>
            (this IQueryable<TSource> source, Expression<Func<IQueryable<TSource>, TResult>> selector)
            where TResult : struct
        {
            var provider = (INhQueryProvider)source.Provider;
            var method = ((MethodCallExpression)selector.Body).Method;
            var expression = System.Linq.Expressions.Expression.Call(null, method, source.Expression);
            return (IFutureValue<TResult>)provider.ExecuteFuture(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static TEntity Merge<TEntity>
            (this ISessionContext sourceDAO, TEntity instance)
            where TEntity : class
        {
            ISession session = sourceDAO.SessionInfo.CurrentSession;
            try
            {
                return session.Merge<TEntity>(instance);
            }
            catch (Exception ex)
            {
                throw new BusinessPersistentException("Error on merging the instance with the current session.", ex);
            }

        }

        /// <summary>
        /// Indicates if the argument is present into current session cache.
        /// </summary>
        /// <typeparam name="TEntity">Persistence instance</typeparam>
        /// <param name="sourceDAO">DAO which is associated into the current session.</param>
        /// <param name="instance">persistence instance to check.</param>
        /// <returns>returns a boolean value indicating if persistent entity is present into current session cache.</returns>
        public static bool IsCached<TEntity>
            (this ISessionContext sourceDAO, TEntity instance)
            where TEntity : class
        {
            return sourceDAO.SessionInfo.CurrentSession.Contains(instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="KValue"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static KValue GetIdentifier<TEntity, KValue>
            (this ISessionContext sourceDAO, TEntity instance)
            where TEntity : class
        {
            return (KValue)sourceDAO.SessionInfo.CurrentSession.GetIdentifier(instance);
        }

        /// <summary>
        /// Indicates if the current session contains any changes which must be synchronized with the database.
        /// </summary>
        /// <param name="sourceDAO"></param>
        /// <returns>returns a boolean value indicating if the current session associated with the calling DAO contains any changes to persist.</returns>
        public static bool SessionWithChanges
            (ISessionContext sourceDAO)
        {
            return sourceDAO.SessionInfo.CurrentSession.IsDirty();
        }

        /// <summary>
        /// Evicts the persistent instance from session cache, if instance is cached into current session.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="instance"></param>
        public static void Evict<TEntity>
            (this ISessionContext sourceDAO, TEntity instance)
            where TEntity : class
        {
            ISession session = sourceDAO.SessionInfo.CurrentSession;
            if (instance != null && sourceDAO.IsCached<TEntity>(instance))
            {
                try
                {
                    session.Evict(instance);
                }
                catch (Exception)
                {
                    // questa eccezione potrebbe essere sollevata perché l'identifier è nullo.
                    // quindi non viene considerata..
                }
            }
        }

        /// <summary>
        /// Evicts all instances present into collection argument, if instances are cached into current session.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="instances"></param>
        public static void Evict<TEntity>
            (this ISessionContext sourceDAO, IEnumerable<TEntity> instances)
            where TEntity : class
        {
            if (instances != null && instances.Count() > 0)
            {
                instances.All
                    (
                        delegate(TEntity instance)
                        {
                            sourceDAO.Evict(instance);
                            return true;
                        }
                    );
            }
        }

        /// <summary>
        /// Evict all persistent instances from the current session cache.
        /// </summary>
        /// <param name="sourceDAO">DAO associated with session wich be clear.</param>
        public static void Evict
            (this ISessionContext sourceDAO)
        {
            sourceDAO.SessionInfo.CurrentSession.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> TransformResult<TEntity, TResult>
            (this ISessionContext sourceDAO, QueryOver<TEntity> query)
            where TEntity : class
        {
            return sourceDAO.TransformResult<TEntity, TResult>(Transformers.AliasToBean<TResult>(), query); // verificare se solleva un'eccezione.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> TransformResult<TResult>
            (this ISessionContext sourceDAO, DetachedCriteria criteria)
        {
            return sourceDAO.TransformResult<TResult>(Transformers.AliasToBean<TResult>(), criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="transformer"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> TransformResult<TEntity, TResult>
            (this ISessionContext sourceDAO, IResultTransformer transformer, QueryOver<TEntity> query)
            where TEntity : class
        {
            ISession session = sourceDAO.SessionInfo.CurrentSession;

            if (query == null)
                throw new QueryArgumentException("Query to execute cannot be null.", "TransformResult", "query");

            if (transformer == null)
                throw new QueryArgumentException("Transformer cannot be null.", "TransformResult", "transformer");

            try
            {
                return sourceDAO.TransformResult<TResult>(transformer, query.GetExecutableQueryOver(session).UnderlyingCriteria); // verificare se solleva un'eccezione.;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException("Error on executing transformer method with the given QueryOver instance.", "TransformResult", ex);
            }
        }

        /// <summary>
        /// Set a strategy for handling the query results. This transforms the query result into specific collection typed.
        /// </summary>
        /// <typeparam name="TResult">The type of result</typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="transformer">The tranformer which converts the result query into collection result typed</param>
        /// <param name="criteria">The given detached criteria to invoke to get result to transforming.</param>
        /// <returns></returns>
        public static IEnumerable<TResult> TransformResult<TResult>
            (this ISessionContext sourceDAO, IResultTransformer transformer, DetachedCriteria criteria)
        {
            ISession session = sourceDAO.SessionInfo.CurrentSession;

            if (criteria == null)
                throw new QueryArgumentException("Query to execute cannot be null.", "TransformResult", "criteria");

            if (transformer == null)
                throw new QueryArgumentException("Transformer cannot be null.", "TransformResult", "transformer");

            try
            {
                return sourceDAO.TransformResult<TResult>(transformer, criteria.GetExecutableCriteria(session));
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException("Error on executing transformer method with the given DetachedCriteria instance.", "TransformResult", ex);
            }
        }

        /// <summary>
        /// Set a strategy for handling the query results. This determines the "shape" of the query result set.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="transformer"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> TransformResult<TResult>
            (this ISessionContext sourceDAO, IResultTransformer transformer , ICriteria criteria)
        {
            return criteria.SetResultTransformer(transformer).List<TResult>();
        }

        /// <summary>
        /// Get an instance of IQuery for a named query defined into mapping file.
        /// </summary>
        /// <param name="sourceDAO"></param>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public static IQuery GetNamedQuery
            (this ISessionContext sourceDAO , string queryName)
        {
            return sourceDAO.SessionInfo.CurrentSession.GetNamedQuery(queryName);
        }

        /// <summary>
        /// Make a NHibernate.ISQLQuery instance for the given SQL query string.
        /// </summary>
        /// <param name="sourceDAO"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static ISQLQuery MakeSQLQuery
            (this ISessionContext sourceDAO, string query)
        {
            ISession session = sourceDAO.SessionInfo.CurrentSession;

            if (string.IsNullOrEmpty(query))
                throw new QueryArgumentException("Native SQL query to make cannot be empty or null", "MakeSQLQuery", "query");

            try
            {
                return session.CreateSQLQuery(query);
            }
            catch (Exception ex)
            {
                throw new QueryFormatException(ex.Message, "MakeSQLQuery", ex);
            }
        }

        /// <summary>
        /// Make a NHibernate.ISQLQuery instance for the given HQL string.
        /// </summary>
        /// <param name="sourceDAO"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQuery MakeHQLQuery
            (this ISessionContext sourceDAO, string query)
        {
            ISession session = sourceDAO.SessionInfo.CurrentSession;
            
            if (string.IsNullOrEmpty(query))
                throw new QueryArgumentException("HQL query to make cannot be empty or null", "MakeHQLQuery", "query");
            
            try
            {
                return session.CreateQuery(query);
            }
            catch (Exception ex)
            {
                throw new QueryFormatException(ex.Message, "MakeHQLQuery", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sourceDAO"></param>
        /// <param name="instances"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IQuery MakeFilter<TEntity>
            (this ISessionContext sourceDAO, IEnumerable<TEntity> instances, string filter)
        {
            ISession session = sourceDAO.SessionInfo.CurrentSession;

            if (instances == null)
                throw new QueryArgumentException("Persistent collection cannot be null.", "MakeFilter", "instances");

            try
            {
                return session.CreateFilter(instances, filter);
            }
            catch (Exception ex)
            {
                throw new QueryFormatException("It's impossible to make a filter with the given persistent collection.", "MakeFilter", ex);
            }
        }
    }
}
