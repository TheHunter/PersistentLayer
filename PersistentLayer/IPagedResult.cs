using System.Collections.Generic;

namespace PersistentLayer
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
