using System.Collections;
using System.Collections.Generic;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPagedResult<TEntity>
        : IPagedResult
        where TEntity : class
    {
        /// <summary>
        /// returns a new typed iterator which contains instances of paged result.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetResult();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPagedResult
    {
        /// <summary>
        /// The start index for taking instances for result paging.
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// The page size.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// returns the total row count of executed query.
        /// </summary>
        int Counter { get; }

        /// <summary>
        /// return the result from paged result.
        /// </summary>
        /// <returns></returns>
        IEnumerable Result { get; }
    }
}
