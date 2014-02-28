using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer
{
    public interface IRootPersisterDAO<in TRootEntity>
        : IRootQueryableDAO<TRootEntity>, ITransactionContext
        where TRootEntity : class
    {
        /// <summary>
        /// Saves or updates the given instance, It depends upon the indentifier value.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">the current instance which contains the state to persist.</param>
        /// <returns>returns the current persistent instance.</returns>
        TEntity MakePersistent<TEntity>(TEntity entity) where TEntity : TRootEntity;

        /// <summary>
        /// Updates the persistent state associated with the given identifier.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">the current instance which contains the state to persist.</param>
        /// <param name="identifier">the persistent indentifier which be updated.</param>
        /// <returns>returns the current persistent instance.</returns>
        TEntity MakePersistent<TEntity>(TEntity entity, object identifier) where TEntity : TRootEntity;

        /// <summary>
        /// Updates the persistent state associated with the given identifier.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="entity">the current instance which contains the state to persist.</param>
        /// <param name="identifier">the persistent indentifier which be updated.</param>
        /// <returns>returns the current persistent instance.</returns>
        TEntity MakePersistent<TEntity, TKey>(TEntity entity, TKey identifier) where TEntity : TRootEntity;

        /// <summary>
        /// Saves or updates the given instances, It depends upon the indentifier value.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> MakePersistent<TEntity>(IEnumerable<TEntity> entities) where TEntity : TRootEntity;

        /// <summary>
        /// The given instance becomes transient.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void MakeTransient<TEntity>(TEntity entity) where TEntity : TRootEntity;

        /// <summary>
        /// The given instances become transient.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        void MakeTransient<TEntity>(IEnumerable<TEntity> entities) where TEntity : TRootEntity;
    }
}
