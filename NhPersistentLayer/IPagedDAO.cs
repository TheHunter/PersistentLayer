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
    public interface IPagedDAO<TEntity, VKey>
        : ISessionDAO<TEntity, VKey>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, DetachedCriteria criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, QueryOver<TEntity> query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, IQueryable<TEntity> query);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPagedDAO
        : ISessionDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, DetachedCriteria criteria)
             where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, QueryOver<TEntity> query)
             where TEntity : class;

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
    }
}
