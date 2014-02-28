using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersistentLayer
{
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
        //IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, Expression<Func<TEntity, bool>> predicate)
        //    where TEntity : TRootEntity;
    }
}
