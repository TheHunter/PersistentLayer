using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer.Impl
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedResult<TEntity>
        : IPagedResult<TEntity>
        where TEntity : class
    {
        private readonly int startIndex;
        private readonly int size;
        private readonly int counter;
        private readonly IEnumerable<TEntity> result;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="size"></param>
        /// <param name="counter"></param>
        /// <param name="result"></param>
        public PagedResult(int startIndex, int size, int counter, IEnumerable<TEntity> result)
        {
            this.startIndex = startIndex;
            this.size = size;
            this.counter = counter;
            this.result = result;
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartIndex
        {
            get { return startIndex; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Counter
        {
            get { return counter; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable Result
        {
            get { return result; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetResult()
        {
            return result;
        }
    }
}
