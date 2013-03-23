using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="VKey"></typeparam>
    public interface IEntityDAO<TEntity, VKey>
        : IQueryableDAO<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Finds the object which matches with the given key.
        /// </summary>
        /// <param name="id">The key of instance to get.</param>
        /// <param name="mode">The type of locking for the instance to get, this argument can be null.</param>
        /// <returns>return an instance related to the calling type object.</returns>
        TEntity FindBy(VKey id, LockMode mode);

        /// <summary>
        /// gets all instances from data source related to declared type instance.
        /// </summary>
        /// <returns>a set of instances from database.</returns>
        IEnumerable<TEntity> FindAll();

        /// <summary>
        /// gets all instances from data source, and cache the query.
        /// </summary>
        /// <param name="cacheable">specify if cache will be switch on.</param>
        /// <returns>a set of instances from database.</returns>
        IEnumerable<TEntity> FindAll(bool cacheable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheRegion"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(string cacheRegion);

        /// <summary>
        /// sets the pre loaded of istances from the database.
        /// </summary>
        /// <param name="fetchSize"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(int fetchSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(DetachedCriteria criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(QueryOver<TEntity> query);


        #region Future section
        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAllFuture(DetachedCriteria criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAllFuture(QueryOver<TEntity> query);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IFutureValue<FutureValue> GetFutureValue<FutureValue>(DetachedCriteria criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FutureValue"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        IFutureValue<FutureValue> GetFutureValue<FutureValue>(QueryOver<TEntity> query);

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists(VKey identifier);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        bool Exists(IEnumerable<VKey> identifiers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        bool Exists(DetachedCriteria criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        bool Exists(QueryOver<TEntity> query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity MakePersistent(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TEntity MakePersistent(TEntity entity, VKey identifier);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> MakePersistent(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void MakeTransient(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void MakeTransient(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity RefreshState(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> RefreshState(IEnumerable<TEntity> entities);
    }
}
