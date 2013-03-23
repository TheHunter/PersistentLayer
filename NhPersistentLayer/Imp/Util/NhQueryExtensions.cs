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

        ///// <summary>
        ///// Transforms the criteria object into typed safe IQueryable object.
        ///// </summary>
        ///// <typeparam name="TEntity">the type object for IQueryable object.</typeparam>
        ///// <param name="sourceDAO">a busisness DAO that supplies transforming the criteria object.</param>
        ///// <param name="criteria">a detached criteria to transform.</param>
        ///// <returns>returns a typed safe IQueryable object.</returns>
        //public static IQueryable<TEntity> ToIQueryable<TEntity>
        //    (this ISessionContext sourceDAO, DetachedCriteria criteria)
        //    where TEntity : class
        //{
        //    ISession session = sourceDAO.SessionInfo.CurrentSession;
        //    return session.
        //            Linq<TEntity>(criteria.GetExecutableCriteria(session));
        //}

        //public static IQueryable<TEntity> ToIQueryable<TEntity, VKey>
        //    (this ISessionDAO<TEntity, VKey> sourceDAO, DetachedCriteria criteria)
        //    where TEntity : class
        //{
        //    ISession session = sourceDAO.SessionInfo.CurrentSession;
        //    return session.
        //            Linq<TEntity>(criteria.GetExecutableCriteria(session));
        //}


        //public static IQueryable<TEntity> ToIQueryable<TEntity>
        //    (this ISessionDAO sourceDAO, DetachedCriteria criteria)
        //    where TEntity : class
        //{
        //    ISession session = sourceDAO.SessionInfo.CurrentSession;
        //    return session.
        //            Linq<TEntity>(criteria.GetExecutableCriteria(session));
        //}

        ///// <summary>
        ///// Transforms the criteria object into typed safe IQueryable object.
        ///// </summary>
        ///// <typeparam name="TEntity">the type object for IQueryable object.</typeparam>
        ///// <param name="sourceDAO">a business DAO that supplies tranforming the QueryOver object.</param>
        ///// <param name="query">a typed safe QueryOver to tranform</param>
        ///// <returns>returns a typed safe IQueryable object.</returns>
        //public static IQueryable<TEntity> ToIQueryable<TEntity>
        //    (this ISessionContext sourceDAO, QueryOver<TEntity> query)
        //    where TEntity : class
        //{
        //    ISession session = sourceDAO.SessionInfo.CurrentSession;
        //    return session.
        //            Linq<TEntity>(query.GetExecutableQueryOver(session).UnderlyingCriteria);
        //}

        //public static IQueryable<TEntity> ToIQueryable<TEntity, VKey>
        //    (this ISessionDAO<TEntity, VKey> sourceDAO, QueryOver<TEntity> query)
        //    where TEntity : class
        //{
        //    ISession session = sourceDAO.SessionInfo.CurrentSession;
        //    return session.
        //            Linq<TEntity>(query.GetExecutableQueryOver(session).UnderlyingCriteria);
        //}

        
        //public static IQueryable<TEntity> ToIQueryable<TEntity>
        //    (this ISessionDAO sourceDAO, QueryOver<TEntity> query)
        //    where TEntity : class
        //{
        //    ISession session = sourceDAO.SessionInfo.CurrentSession;
        //    return session.
        //            Linq<TEntity>(query.GetExecutableQueryOver(session).UnderlyingCriteria);
        //}

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
            return session.Merge<TEntity>(instance);
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
            if (instance != null && sourceDAO.IsCached<TEntity>(instance))
            {
                try
                {
                    sourceDAO.SessionInfo.CurrentSession.Evict(instance);
                }
                catch (Exception)
                {
                    // questa eccezione potrebbe essere sollevata perché
                    // l'identifier è nullo.
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
            if (query == null)
                throw new NullReferenceException("Query to execute cannot be null.");

            return sourceDAO.TransformResult<TResult>(query.DetachedCriteria); // verificare se solleva un'eccezione.
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
            if (criteria == null)
                throw new NullReferenceException("Query to execute cannot be null.");
            
            ICriteria execCriteria = criteria.GetExecutableCriteria(sourceDAO.SessionInfo.CurrentSession);
            return sourceDAO.TransformResult<TResult>(Transformers.AliasToBean<TResult>(), execCriteria);
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
            if (query == null)
                throw new NullReferenceException("Query to execute cannot be null.");

            return sourceDAO.TransformResult<TResult>(transformer, query.DetachedCriteria); // verificare se solleva un'eccezione.;
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
            if (criteria == null)
                throw new NullReferenceException("Query to execute cannot be null.");

            if (transformer == null)
                throw new NullReferenceException("Transformer cannot be null.");

            ICriteria execCriteria = criteria.GetExecutableCriteria(sourceDAO.SessionInfo.CurrentSession);
            return sourceDAO.TransformResult<TResult>(transformer, execCriteria);
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
            try
            {
                if (string.IsNullOrEmpty(query))
                    throw new ArgumentException("Native SQL query to make cannot be empty or null", "query");

                return sourceDAO.SessionInfo.CurrentSession.CreateSQLQuery(query);
            }
            catch (Exception ex)
            {
                throw new QueryFormatException(ex.Message, ex);
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
            try
            {
                if (string.IsNullOrEmpty(query))
                    throw new ArgumentException("HQL query to make cannot be empty or null", "query");

                return sourceDAO.SessionInfo.CurrentSession.CreateQuery(query);
            }
            catch (Exception ex)
            {
                throw new QueryFormatException(ex.Message, ex);
            }
        }
    }
}
