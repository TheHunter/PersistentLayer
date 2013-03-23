using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace NHibernate.DAL.NhPersistentLayer.Imp.Util
{
    /// <summary>
    /// 
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
            if (mode == null)
            {
                mode = LockMode.None;
            }
            return session.Load<TEntity>(id, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static IEnumerable<TEntity> FindAll<TEntity>(this ISession session) where TEntity : class
        {
            return DetachedCriteria.For<TEntity>()
                    .GetExecutableCriteria(session)
                    .List<TEntity>();
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
            return DetachedCriteria.For<TEntity>()
                    .SetCacheable(cacheable)
                    .GetExecutableCriteria(session)
                    .List<TEntity>();
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
            if (string.IsNullOrEmpty(cacheRegion))
                return session.FindAll<TEntity>();
            else
                return DetachedCriteria.For<TEntity>()
                        .SetCacheable(true)
                        .SetCacheRegion(cacheRegion)
                        .GetExecutableCriteria(session)
                        .List<TEntity>();
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
            return DetachedCriteria.For<TEntity>()
                    .GetExecutableCriteria(session)
                    .SetFetchSize(fetchSize)
                    .List<TEntity>();
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
                throw new ArgumentNullException("criteria", "the criteria used to filtering cannot be null.");

            return criteria.GetExecutableCriteria(session)
                    .List<TEntity>();
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
                throw new ArgumentNullException("QueryOver", "the queryover object cannot be null.");

            return query.GetExecutableQueryOver(session)
                    .List<TEntity>();
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
                throw new ArgumentNullException("future criteria", "the criteria used to filtering cannot be null.");

            return criteria.GetExecutableCriteria(session)
                    .Future<TEntity>();
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
                throw new ArgumentNullException("future query", "the QueryOver object cannot be null.");

            return query.GetExecutableQueryOver(session)
                    .Future();
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
                throw new ArgumentException("future criteria", "the criteria object cannot be null.");

            return criteria.GetExecutableCriteria(session)
                    .FutureValue<FutureValue>();
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
                throw new ArgumentException("future query", "the query object cannot be null.");

            return query.GetExecutableQueryOver(session)
                    .FutureValue<FutureValue>();
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
            return
                (session.CreateCriteria<TEntity>()
                .Add(Restrictions.IdEq(identifier))
                .SetProjection(Projections.RowCountInt64())
                .UniqueResult<long>()
                ) > 0;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        internal static bool Exists(this ISession session, DetachedCriteria criteria)
        {
            long counter = criteria.GetExecutableCriteria(session)
                            .SetProjection(Projections.RowCountInt64())
                            .UniqueResult<long>();

            return counter > 0;
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
            long counter = query.GetExecutableQueryOver(session)
                           .ToRowCountInt64Query()
                           .UnderlyingCriteria
                           .UniqueResult<long>();

            return counter > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        internal static IQueryable<TEntity> ToIQueryable<TEntity>(this ISession session) where TEntity : class
        {
            return session.Query<TEntity>();
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
            return session.ToIQueryable<TEntity>().Cacheable().CacheMode(mode);
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
            if (!string.IsNullOrEmpty(region))
                query = query.CacheRegion(region);

            return query;
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
            session.SaveOrUpdate(entity);
            return entity;
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
            session.Update(entity, identifier);
            return entity;
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
            if (entities != null)
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
            }
            else
            {
                throw new ArgumentNullException("entities", "the collection of estities to save/update cannot be null.");
            }
            return entities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        internal static void MakeTransient<TEntity>(this ISession session, TEntity entity) where TEntity : class
        {
            session.Delete(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="session"></param>
        /// <param name="entities"></param>
        internal static void MakeTransient<TEntity>(this ISession session, IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities != null)
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
            else
            {
                throw new ArgumentNullException("entities to delete", "The collection of entities to delete cannot be null.");
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
            session.Refresh(entity);
            return entity;
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
