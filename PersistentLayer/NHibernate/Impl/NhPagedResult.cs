using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using PersistentLayer.Exceptions;

namespace PersistentLayer.NHibernate.Impl
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class NhPagedResult<TEntity>
        : IPagedResult<TEntity>
        where TEntity : class
    {
        private int startIndex;
        private int size;
        private int counter;
        private IEnumerable<TEntity> result;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <exception cref="ExecutionQueryException"></exception>
        /// <exception cref="QueryArgumentException"></exception>
        public NhPagedResult(int startIndex, int pageSize, ICriteria criteria)
        {
            result = null;
            if (criteria == null)
                throw new QueryArgumentException("The given criteria using for paging cannot be null.", "NhPagedResult", "criteria");
            
            if (pageSize < 0)
                throw new QueryArgumentException("The given pageSize for PagedResult instance cannot be less than zero.", "NhPagedResult", "pageSize");

            if (startIndex < 0)
                throw new QueryArgumentException("The given start index cannot be less than zero.", "NhPagedResult", "startIndex");

            try
            {
                IFutureValue<int> counter = CriteriaTransformer.TransformToRowCount(criteria).FutureValue<int>();
                IEnumerable<TEntity> futureInstances = criteria.SetFirstResult(startIndex).SetMaxResults(pageSize).Future<TEntity>();

                this.StartIndex = startIndex;
                this.Size = pageSize;
                this.result = futureInstances;
                this.counter = counter.Value;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException("Error on calling the constructor when the given criteria instance is executed.", "NhPagedResult", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <exception cref="ExecutionQueryException"></exception>
        /// <exception cref="QueryArgumentException"></exception>
        public NhPagedResult(int startIndex, int pageSize, IQueryable<TEntity> query)
        {
            result = null;
            if (query == null)
                throw new QueryArgumentException("The given IQueryable instance using for paging cannot be null.", "NhPagedResult", "query");

            if (pageSize < 0)
                throw new QueryArgumentException("The given pageSize for PagedResult instance cannot be less than zero.", "NhPagedResult", "pageSize");

            if (startIndex < 0)
                throw new QueryArgumentException("The given start index cannot be less than zero.", "NhPagedResult", "startIndex");

            try
            {
                this.StartIndex = startIndex;
                this.Size = pageSize;

                IFutureValue<int> counter = query.ToFutureValue(n => n.Count());
                this.result = query.Skip(startIndex).Take(pageSize).ToFuture();

                this.counter = counter.Value;
            }
            catch (Exception ex)
            {
                throw new ExecutionQueryException("Error on calling the constructor when the given IQueryable instance is executed.", "NhPagedResult", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartIndex
        {
            protected set { this.startIndex = value; }
            get { return this.startIndex; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            protected set { this.size = value; }
            get { return this.size; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Counter
        {
            get { return this.counter; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetResult()
        {
            return this.result.ToList();
        }

        /// <summary>
        /// resets all fields values
        /// </summary>
        private void Reset()
        {
            this.counter = 0;
            this.result = null;
            this.size = 0;
            this.startIndex = 0;
        }
    }
}
