﻿using System.Linq;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IPagedDAO<TEntity, TKey>
        : IPersisterDAO<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, IQueryable<TEntity> query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetIndexPagedResult(int pageIndex, int pageSize, IQueryable<TEntity> query);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPagedDAO
        : IPersisterDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, IQueryable<TEntity> query)
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetIndexPagedResult<TEntity>(int pageIndex, int pageSize, IQueryable<TEntity> query)
            where TEntity : class;
    }
}
