using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRootEntity"></typeparam>
    public interface IRootPagedDAO<in TRootEntity>
        : IRootPersisterDAO<TRootEntity>
        where TRootEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, TRootEntity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRootEntity"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRootPagedDAO<in TRootEntity, TEntity>
        : IRootPersisterDAO<TRootEntity, TEntity>
        where TRootEntity : class
        where TEntity : class, TRootEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);
    }
}
