using System;
using System.Linq;
using System.Linq.Expressions;

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
        /// <param name="predicate"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);

        
        //IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, IQueryable<TEntity> query);

        
        //IPagedResult<TEntity> GetIndexPagedResult(int pageIndex, int pageSize, IQueryable<TEntity> query);
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
        /// <param name="predicate"></param>
        /// <returns></returns>
        IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;
        

        
        //IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, IQueryable<TEntity> query) where TEntity : class;


        //IPagedResult<TEntity> GetIndexPagedResult<TEntity>(int pageIndex, int pageSize, IQueryable<TEntity> query) where TEntity : class;
    }
}
