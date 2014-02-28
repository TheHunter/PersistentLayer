﻿using System;
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
        where TRootEntity: class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists<TEntity>(object identifier)
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        bool Exists<TEntity, TKey>(TKey identifier)
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        bool Exists<TEntity>(ICollection identifiers)
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        bool Exists<TEntity, TKey>(IEnumerable<TKey> identifiers)
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TEntity FindBy<TEntity>(object identifier)
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TEntity FindBy<TEntity, TKey>(TKey identifier)
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll<TEntity>()
            where TEntity : TRootEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : TRootEntity;
    }
}
