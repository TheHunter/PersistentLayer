using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPersisterDAO
        : IQueryableDAO, ITransactionContext
    {
        /// <summary>
        /// Saves or updates the given instance, It depends upon the indentifier value.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">the current instance which contains the state to persist.</param>
        /// <returns>returns the current persistent instance.</returns>
        TEntity MakePersistent<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Updates the persistent state associated with the given identifier.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="entity">the current instance which contains the state to persist.</param>
        /// <param name="identifier">the persistent indentifier which be updated.</param>
        /// <returns>returns the current persistent instance.</returns>
        TEntity MakePersistent<TEntity, TKey>(TEntity entity, TKey identifier) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> MakePersistent<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void MakeTransient<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        void MakeTransient<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IPersisterDAO<TEntity, TKey>
        : IQueryableDAO<TEntity, TKey>, ITransactionContext
         where TEntity : class
    {
        /// <summary>
        /// Saves or updates the given instance, It depends upon the indentifier value.
        /// </summary>
        /// <param name="entity">the current instance which contains the state to persist.</param>
        /// <returns>returns the current persistent instance.</returns>
        TEntity MakePersistent(TEntity entity);

        /// <summary>
        /// Updates the persistent state associated with the given identifier.
        /// </summary>
        /// <param name="entity">the current instance which contains the state to persist.</param>
        /// <param name="identifier">the persistent indentifier which be updated.</param>
        /// <returns>returns the current persistent instance.</returns>
        TEntity MakePersistent(TEntity entity, TKey identifier);

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
    }
}
