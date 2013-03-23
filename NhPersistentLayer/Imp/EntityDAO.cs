using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate.DAL.NhPersistentLayer.Imp.Util;

namespace NHibernate.DAL.NhPersistentLayer.Imp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="VKey"></typeparam>
    public abstract class EntityDAO<TEntity, VKey>
        : AbstractDAO, IEntityDAO<TEntity, VKey>
        //: IEntityDAO<TEntity, VKey>, AbstractDAO
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public TEntity FindBy(VKey id, LockMode mode)
        {
            return this.CurrentSession.FindBy<TEntity, VKey>(id, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll()
        {
            return this.CurrentSession.FindAll<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheable"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(bool cacheable)
        {
            return this.CurrentSession.FindAll<TEntity>(cacheable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheRegion"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(string cacheRegion)
        {
            return this.CurrentSession.FindAll<TEntity>(cacheRegion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fetchSize"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(int fetchSize)
        {
            return this.CurrentSession.FindAll<TEntity>(fetchSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(DetachedCriteria criteria)
        {
            return this.CurrentSession.FindAll<TEntity>(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(QueryOver<TEntity> query)
        {
            return this.CurrentSession.FindAll<TEntity>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAllFuture(DetachedCriteria criteria)
        {
            return this.CurrentSession.FindAllFuture<TEntity>(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAllFuture(QueryOver<TEntity> query)
        {
            return this.CurrentSession.FindAllFuture<TEntity>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IFutureValue<FutureValue> GetFutureValue<FutureValue>(DetachedCriteria criteria)
        {
            return this.CurrentSession.GetFutureValue<FutureValue>(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IFutureValue<FutureValue> GetFutureValue<FutureValue>(QueryOver<TEntity> query)
        {
            return this.CurrentSession.GetFutureValue<TEntity, FutureValue>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public bool Exists(VKey identifier)
        {
            return this.CurrentSession.Exists<TEntity, VKey>(identifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        public bool Exists(IEnumerable<VKey> identifiers)
        {
            return this.CurrentSession.Exists<TEntity, VKey>(identifiers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public bool Exists(DetachedCriteria criteria)
        {
            return this.CurrentSession.Exists(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool Exists(QueryOver<TEntity> query)
        {
            return this.CurrentSession.Exists(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity MakePersistent(TEntity entity)
        {
            return this.CurrentSession.MakePersistent<TEntity>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public TEntity MakePersistent(TEntity entity, VKey identifier)
        {
            return this.CurrentSession.MakePersistent<TEntity, VKey>(entity, identifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> MakePersistent(IEnumerable<TEntity> entities)
        {
            return this.CurrentSession.MakePersistent<TEntity>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void MakeTransient(TEntity entity)
        {
            this.CurrentSession.MakeTransient<TEntity>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void MakeTransient(IEnumerable<TEntity> entities)
        {
            this.CurrentSession.MakeTransient<TEntity>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity RefreshState(TEntity entity)
        {
            return this.CurrentSession.RefreshState<TEntity>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> RefreshState(IEnumerable<TEntity> entities)
        {
            return this.CurrentSession.RefreshState<TEntity>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable()
        {
            return this.CurrentSession.ToIQueryable<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable(CacheMode mode)
        {
            return this.CurrentSession.ToIQueryable<TEntity>(mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable(string region)
        {
            return this.CurrentSession.ToIQueryable<TEntity>(region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable(CacheMode mode, string region)
        {
            return this.CurrentSession.ToIQueryable<TEntity>(mode, region);
        }
    }
}
