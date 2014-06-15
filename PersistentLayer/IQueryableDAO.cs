using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueryableDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists<TEntity, TKey>(TKey identifier)
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        bool Exists<TEntity, TKey>(IEnumerable<TKey> identifiers)
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TEntity FindBy<TEntity, TKey>(TKey identifier)
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity UniqueResult<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll<TEntity>()
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="queryExpr"></param>
        /// <returns></returns>
        TResult ExecuteExpression<TEntity, TResult>(Expression<Func<IEnumerable<TEntity>, TResult>> queryExpr)
            where TEntity : class;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IQueryableDAO<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists(TKey identifier);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        bool Exists(IEnumerable<TKey> identifiers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TEntity FindBy(TKey identifier);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity UniqueResult(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="queryExpr"></param>
        /// <returns></returns>
        TResult ExecuteExpression<TResult>(Expression<Func<IEnumerable<TEntity>, TResult>> queryExpr);
    }
}
