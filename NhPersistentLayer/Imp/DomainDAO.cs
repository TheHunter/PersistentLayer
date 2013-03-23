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
    public abstract class DomainDAO
        : AbstractDAO, IDomainDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public TEntity FindBy<TEntity, VKey>(VKey id, LockMode mode) where TEntity : class
        {
            return this.CurrentSession.FindBy<TEntity, VKey>(id, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll<TEntity>() where TEntity : class
        {
            return this.CurrentSession.FindAll<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="cacheable"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll<TEntity>(bool cacheable) where TEntity : class
        {
            return this.CurrentSession.FindAll<TEntity>(cacheable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="cacheRegion"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll<TEntity>(string cacheRegion) where TEntity : class
        {
            return this.CurrentSession.FindAll<TEntity>(cacheRegion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fetchSize"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll<TEntity>(int fetchSize) where TEntity : class
        {
            return this.CurrentSession.FindAll<TEntity>(fetchSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll<TEntity>(Criterion.DetachedCriteria criteria) where TEntity : class
        {
            return this.CurrentSession.FindAll<TEntity>(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll<TEntity>(Criterion.QueryOver<TEntity> query) where TEntity : class
        {
            return this.CurrentSession.FindAll<TEntity>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAllFuture<TEntity>(Criterion.DetachedCriteria criteria) where TEntity : class
        {
            return this.CurrentSession.FindAllFuture<TEntity>(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAllFuture<TEntity>(Criterion.QueryOver<TEntity> query) where TEntity : class
        {
            return this.CurrentSession.FindAllFuture<TEntity>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IFutureValue<FutureValue> GetFutureValue<FutureValue>(Criterion.DetachedCriteria criteria)
        {
            return this.CurrentSession.GetFutureValue<FutureValue>(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IFutureValue<FutureValue> GetFutureValue<TEntity, FutureValue>(Criterion.QueryOver<TEntity> query) where TEntity : class
        {
            return this.CurrentSession.GetFutureValue<TEntity, FutureValue>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public bool Exists<TEntity, VKey>(VKey identifier) where TEntity : class
        {
            return this.CurrentSession.Exists<TEntity, VKey>(identifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        public bool Exists<TEntity, VKey>(IEnumerable<VKey> identifiers) where TEntity : class
        {
            return this.CurrentSession.Exists<TEntity, VKey>(identifiers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public bool Exists(Criterion.DetachedCriteria criteria)
        {
            return this.CurrentSession.Exists(criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool Exists<TEntity>(Criterion.QueryOver<TEntity> query) where TEntity : class
        {
            return this.CurrentSession.Exists(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity MakePersistent<TEntity>(TEntity entity) where TEntity : class
        {
            return this.CurrentSession.MakePersistent<TEntity>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="VKey"></typeparam>
        /// <param name="entity"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public TEntity MakePersistent<TEntity, VKey>(TEntity entity, VKey identifier) where TEntity : class
        {
            return this.CurrentSession.MakePersistent<TEntity, VKey>(entity, identifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> MakePersistent<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return this.CurrentSession.MakePersistent<TEntity>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void MakeTransient<TEntity>(TEntity entity) where TEntity : class
        {
            this.CurrentSession.MakeTransient<TEntity>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        public void MakeTransient<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            this.CurrentSession.MakeTransient<TEntity>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity RefreshState<TEntity>(TEntity entity) where TEntity : class
        {
            return this.CurrentSession.RefreshState<TEntity>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> RefreshState<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return this.CurrentSession.RefreshState<TEntity>(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable<TEntity>() where TEntity : class
        {
            return this.CurrentSession.ToIQueryable<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="mode"></param>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable<TEntity>(CacheMode mode) where TEntity : class
        {
            return this.CurrentSession.ToIQueryable<TEntity>(mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="region"></param>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable<TEntity>(string region) where TEntity : class
        {
            return this.CurrentSession.ToIQueryable<TEntity>(region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="mode"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public IQueryable<TEntity> ToIQueryable<TEntity>(CacheMode mode, string region) where TEntity : class
        {
            return this.CurrentSession.ToIQueryable<TEntity>(mode, region);
        }
    }
}
