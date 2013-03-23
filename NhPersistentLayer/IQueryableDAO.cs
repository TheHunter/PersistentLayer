using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueryableDAO<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable(CacheMode mode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable(string region);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable(CacheMode mode, string region);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IQueryableDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable<TEntity>() where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable<TEntity>(CacheMode mode) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable<TEntity>(string region) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        IQueryable<TEntity> ToIQueryable<TEntity>(CacheMode mode, string region) where TEntity : class;
    }
}
