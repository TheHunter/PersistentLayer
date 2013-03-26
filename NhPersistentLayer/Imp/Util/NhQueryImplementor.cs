using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.DAL.NhPersistentLayer.Exceptions;

namespace NHibernate.DAL.NhPersistentLayer.Imp.Util
{
    /// <summary>
    /// A basic class which implements all business DAO methods.
    /// </summary>
    internal static class NhQueryImplementor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="session"></param>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        internal static TEntity FindBy<TEntity, VKey>(this ISession session, VKey id, LockMode mode) where TEntity : class
        {
            try
            {
                if (mode == null)
                {
                    mode = LockMode.None;
                }
                return session.Load<TEntity>(id, mode);
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on loading the persistent instance (type of <{0}>) with the given identity (type of <1>).", typeof(TEntity).Name, typeof(VKey).Name), "FindBy", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAll<TEntity>(this ISession session) where TEntity : class
        {
            try
            {
                return DetachedCriteria.For<TEntity>()
                    .GetExecutableCriteria(session)
                    .List<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent instances (collection type of <{0}>) from data store.", typeof(TEntity).Name), "FindAll", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="cacheable"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAll<TEntity>(this ISession session, bool cacheable) where TEntity : class
        {
            try
            {
                return DetachedCriteria.For<TEntity>()
                    .SetCacheable(cacheable)
                    .GetExecutableCriteria(session)
                    .List<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent instances (collection type of <{0}>) from data store.", typeof(TEntity).Name), "FindAll", ex);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="cacheRegion"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAll<TEntity>(this ISession session, string cacheRegion) where TEntity : class
        {
            try
            {
                if (string.IsNullOrEmpty(cacheRegion))
                    return session.FindAll<TEntity>();
                else
                    return DetachedCriteria.For<TEntity>()
                            .SetCacheable(true)
                            .SetCacheRegion(cacheRegion)
                            .GetExecutableCriteria(session)
                            .List<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent instances (collection type of <{0}>) from data store.", typeof(TEntity).Name), "FindAll", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="fetchSize"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAll<TEntity>(this ISession session, int fetchSize) where TEntity : class
        {
            try
            {
                return DetachedCriteria.For<TEntity>()
                    .GetExecutableCriteria(session)
                    .SetFetchSize(fetchSize)
                    .List<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent instances (collection type of <{0}>) from data store with the given fetchsize.", typeof(TEntity).Name), "FindAll", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAll<TEntity>(this ISession session, DetachedCriteria criteria) where TEntity : class
        {
            if (criteria == null)
                throw new QueryArgumentException("The DetachedCriteria to execute cannot be null.", "FindAll", "criteria");

            try
            {
                return criteria.GetExecutableCriteria(session)
                        .List<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent instances (collection type of <{0}>) from data store when the given DetachedCriteria is executed.", typeof(TEntity).Name), "FindAll", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAll<TEntity>(this ISession session, QueryOver<TEntity> query) where TEntity : class
        {
            if (query == null)
                throw new QueryArgumentException(string.Format("The QueryOver (of <{0}>) instance to execute cannot be null.", typeof(TEntity).Name), "FindAll", "query");

            try
            {
                return query.GetExecutableQueryOver(session)
                    .List<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent instances (collection type of <{0}>) from data store when the given QueryOver instance is executed.", typeof(TEntity).Name), "FindAll", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAllFuture<TEntity>(this ISession session, DetachedCriteria criteria) where TEntity : class
        {
            if (criteria == null)
                throw new QueryArgumentException("The DetachedCriteria object to execute cannot be null.", "FindAllFuture", "criteria");

            try
            {
                return criteria.GetExecutableCriteria(session)
                    .Future<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent future instances (collection type of <{0}>) from data store when the given DetachedCriteria instance is executed.", typeof(TEntity).Name), "FindAllFuture", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAllFuture<TEntity>(this ISession session, QueryOver<TEntity> query) where TEntity : class
        {
            if (query == null)
                throw new QueryArgumentException(string.Format("The QueryOver object (of <{0}>) to execute cannot be null.", typeof(TEntity).Name), "FindAllFuture", "query");

            try
            {
                return query.GetExecutableQueryOver(session)
                    .Future();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting all persistent future instances (collection type of <{0}>) from data store when the given QueryOver instance is executed.", typeof(TEntity).Name), "FindAllFuture", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="session"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        internal static IFutureValue<FutureValue> GetFutureValue<FutureValue>(this ISession session, DetachedCriteria criteria)
        {
            if (criteria == null)
                throw new QueryArgumentException("The DetachedCriteria instance cannot be null.", "GetFutureValue", "criteria");

            try
            {
                return criteria.GetExecutableCriteria(session)
                    .FutureValue<FutureValue>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting future persistent instance (type of <{0}>) from data store when the given DetachedCriteria is executed.", typeof(FutureValue).Name), "GetFutureValue", ex);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static IFutureValue<FutureValue> GetFutureValue<TEntity, FutureValue>(this ISession session, QueryOver<TEntity> query) where TEntity : class
        {
            if (query == null)
                throw new QueryArgumentException("The QueryOver instance cannot be null.", "GetFutureValue", "query");

            try
            {
                return query.GetExecutableQueryOver(session)
                    .FutureValue<FutureValue>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting future persistent instance (type of <{0}>) from data store when the given QueryOver is executed.", typeof(FutureValue).Name), "GetFutureValue", ex);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="session"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        internal static bool Exists<TEntity, VKey>(this ISession session, VKey identifier) where TEntity : class
        {
            try
            {
                return
                (session.CreateCriteria<TEntity>()
                .Add(Restrictions.IdEq(identifier))
                .SetProjection(Projections.RowCountInt64())
                .UniqueResult<long>()
                ) > 0;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on executing the \"Exists\" query when It tries to find a persistent instance (type of <{0}>) with the given identifier (type of <{1}>).", typeof(TEntity).Name, typeof(VKey).Name), "Exists", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="session"></param>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        internal static bool Exists<TEntity, VKey>(this ISession session, IEnumerable<VKey> identifiers) where TEntity : class
        {
            try
            {
                string property = session.GetIdentifierName<TEntity>();
                if (identifiers != null && identifiers.Count() > 0)
                {
                    long total = identifiers.LongCount();
                    long totalFounded = session.CreateCriteria<TEntity>()
                                        .Add(Restrictions.InG(property, identifiers))
                                        .SetProjection(Projections.RowCountInt64())
                                        .UniqueResult<long>();

                    return total == totalFounded;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on executing the \"Exists\" query when It tries to find all persistent instances (type of <{0}>) with the given indentifiers (type of <{1}>).", typeof(TEntity).Name, typeof(VKey).Name), "Exists", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        internal static bool Exists(this ISession session, DetachedCriteria criteria)
        {
            try
            {
                long counter = criteria.GetExecutableCriteria(session)
                            .SetProjection(Projections.RowCountInt64())
                            .UniqueResult<long>();

                return counter > 0;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException("Error on executing the \"Exists\" query when It tries to find all persistent instances when the given DetachedCriteria instance is executed.", "Exists", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static bool Exists<TEntity>(this ISession session, QueryOver<TEntity> query) where TEntity : class
        {
            try
            {
                long counter = query.GetExecutableQueryOver(session)
                           .ToRowCountInt64Query()
                           .UnderlyingCriteria
                           .UniqueResult<long>();

                return counter > 0;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException("Error on executing the \"Exists\" query when It tries to find all persistent instances when the given QueryOver instance is executed.", "Exists", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static IQueryable<TEntity> ToIQueryable<TEntity>(this ISession session) where TEntity : class
        {
            try
            {
                return session.Query<TEntity>();
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting the IQueryable object of <{0}>", typeof(TEntity).Name), "ToIQueryable", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        internal static IQueryable<TEntity> ToIQueryable<TEntity>(this ISession session, CacheMode mode) where TEntity : class
        {
            try
            {
                return session.ToIQueryable<TEntity>().Cacheable().CacheMode(mode);
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting the IQueryable object of <{0}> using a cache mode strategy.", typeof(TEntity).Name), "ToIQueryable", ex);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        internal static IQueryable<TEntity> ToIQueryable<TEntity>(this ISession session, string region) where TEntity : class
        {
            IQueryable<TEntity> query = session.ToIQueryable<TEntity>();
            try
            {
                if (!string.IsNullOrEmpty(region))
                    query = query.CacheRegion(region);

                return query;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on getting the IQueryable object of <{0}> using a cache region strategy.", typeof(TEntity).Name), "ToIQueryable", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="mode"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        internal static IQueryable<TEntity> ToIQueryable<TEntity>(this ISession session, CacheMode mode, string region) where TEntity : class
        {
            return session.ToIQueryable<TEntity>(region).Cacheable().CacheMode(mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static TEntity MakePersistent<TEntity>(this ISession session, TEntity entity) where TEntity : class
        {
            try
            {
                session.SaveOrUpdate(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new BusinessPersistentException(string.Format("Error on making persistent the given instance (type of <{0}>)", typeof(TEntity).Name), "MakePersistent", ex);
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        internal static TEntity MakePersistent<TEntity, VKey>(this ISession session, TEntity entity, VKey identifier) where TEntity : class
        {
            try
            {
                session.Update(entity, identifier);
                return entity;
            }
            catch (Exception ex)
            {
                throw new BusinessPersistentException(string.Format("Error on saving/updating the given instance (type of <{0}>) associated at the given identifier (value={1}, type of <{2}>)", typeof(TEntity).Name, identifier == null ? "null" : identifier.ToString(), typeof(VKey).Name), "MakePersistent", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> MakePersistent<TEntity>(this ISession session, IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities == null)
                throw new QueryArgumentException("The collection of entities to save/update cannot be null.", "MakePersistent", "entities");

            try
            {
                int counter = entities.Count();
                if (counter > 0)
                {
                    int size = counter > 10 ? 10 : counter;
                    session = session.SetBatchSize(size);
                    entities.All
                        (
                            delegate(TEntity entity)
                            {
                                session.SaveOrUpdate(entity);
                                return true;
                            }
                        );
                }
                return entities;
            }
            catch (Exception ex)
            {
                throw new BusinessPersistentException(string.Format("Error on saving/updating the given instances (type of <{0}>).", typeof(TEntity).Name), "MakePersistent", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        internal static void MakeTransient<TEntity>(this ISession session, TEntity entity) where TEntity : class
        {
            try
            {
                session.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessPersistentException(string.Format("Error on deleting the given instance (type of {0}).", typeof(TEntity).Name), "MakeTransient", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entities"></param>
        internal static void MakeTransient<TEntity>(this ISession session, IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities == null)
                throw new QueryArgumentException("The collection of entities to delete cannot be null.", "MakeTransient", "entities");

            try
            {
                int counter = entities.Count();
                if (counter > 0)
                {
                    int size = counter > 10 ? 10 : counter;
                    session = session.SetBatchSize(size);
                    entities.All
                        (
                            delegate(TEntity entity)
                            {
                                session.Delete(entity);
                                return true;
                            }
                        );
                }
            }
            catch (Exception ex)
            {
                throw new BusinessPersistentException(string.Format("Error on deleting the given instances (type of {0}).", typeof(TEntity).Name), "MakeTransient", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static TEntity RefreshState<TEntity>(this ISession session, TEntity entity) where TEntity : class
        {
            try
            {
                session.Refresh(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on refreshing the given instance (type of {0}) with the current state of underlying data store.", typeof(TEntity).Name), "RefreshState", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> RefreshState<TEntity>(this ISession session, IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                entities.All
                (
                    delegate(TEntity entity)
                    {
                        session.Refresh(entity);
                        return true;
                    }
                );
                return entities;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException(string.Format("Error on refreshing the given instances (type of {0}) with the current state of underlying data store.", typeof(TEntity).Name), "RefreshState", ex);
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        internal static IPagedResult<TEntity> GetPagedResult<TEntity>(this ISession session, int startIndex, int pageSize, ICriteria criteria) where TEntity : class
        {
            return new PagedResult<TEntity>(startIndex, pageSize, criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static IPagedResult<TEntity> GetPagedResult<TEntity>(this ISession session, int startIndex, int pageSize, IQueryable<TEntity> query) where TEntity : class
        {
            return new PagedResult<TEntity>(startIndex, pageSize, query);
        }

        #region Helper methods.

        /// <summary>
        /// This method serves for looking for indentifier name of any persistent class.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static string GetIdentifierName<TEntity>(this ISession session)
            where TEntity : class
        {
            var classmap = session.SessionFactory.GetClassMetadata(typeof(TEntity));
            if (classmap != null)
            {
                return classmap.IdentifierPropertyName;
            }
            else
            {
                throw new TypeLoadException(string.Format("there's no persitent class with the current object type, class name: {0}", typeof(TEntity).Name));
            }
        }

        #endregion
    }
}
