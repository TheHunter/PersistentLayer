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
        private int _StartIndex = 0;
        private int _Size = 0;
        private int _Counter = 0;
        private IEnumerable<TEntity> _Result = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        public NhPagedResult(int startIndex, int pageSize, ICriteria criteria)
        {
            if (criteria == null)
                throw new QueryArgumentException("the given criteria using for paging cannot be null.", "NhPagedResult", "criteria");

            try
            {
                IFutureValue<int> counter = CriteriaTransformer.TransformToRowCount(criteria).FutureValue<int>();
                IEnumerable<TEntity> futureInstances = criteria.SetFirstResult(startIndex).SetMaxResults(pageSize).Future<TEntity>();

                this.StartIndex = startIndex;
                this.Size = pageSize;
                this._Result = futureInstances;
                this._Counter = counter.Value;
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
        public NhPagedResult(int startIndex, int pageSize, IQueryable<TEntity> query)
        {
            if (query == null)
                throw new QueryArgumentException("The given IQueryable instance using for paging cannot be null.", "NhPagedResult", "query");

            try
            {
                this.StartIndex = startIndex;
                this.Size = pageSize;

                IFutureValue<int> counter = query.ToFutureValue(n => n.Count());
                this._Result = query.Skip(startIndex).Take(pageSize).ToFuture();

                this._Counter = counter.Value;
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
            protected set { this._StartIndex = value; }
            get { return this._StartIndex; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            protected set { this._Size = value; }
            get { return this._Size; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Counter
        {
            get { return this._Counter; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetResult()
        {
            return this._Result.ToList();
        }

        /// <summary>
        /// resets all fields values
        /// </summary>
        private void Reset()
        {
            this._Counter = 0;
            this._Result = null;
            this._Size = 0;
            this._StartIndex = 0;
        }
    }
}
