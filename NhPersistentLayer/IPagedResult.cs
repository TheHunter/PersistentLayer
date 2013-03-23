using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPagedResult<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// 
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 
        /// </summary>
        int Counter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetResult();
    }
}
