using System;
using System.Collections;
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
    public interface IRootQueryableDAO<in TRootEntity>
        where TRootEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TEntity FindBy<TEntity>(object identifier)
            where TEntity : class, TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists<TEntity>(object identifier)
            where TEntity : class, TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        bool Exists<TEntity>(ICollection identifiers)
            where TEntity : class, TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity UniqueResult<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll<TEntity>()
            where TEntity : class, TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="queryExpr"></param>
        /// <returns></returns>
        TResult ExecuteExpression<TEntity, TResult>(Expression<Func<IEnumerable<TEntity>, TResult>> queryExpr)
            where TEntity : class, TRootEntity;
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRootEntity"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRootQueryableDAO<in TRootEntity, TEntity>
        where TRootEntity : class
        where TEntity : class, TRootEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists(object identifier);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        bool Exists(ICollection identifiers);

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
        TEntity FindBy(object identifier);

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
