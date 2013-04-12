using System.Linq;
using NHibernate.Criterion;

namespace PersistentLayer.NHibernate.Impl
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BusinessNhPagedDao<TEntity, TKey>
        : BusinessDAO<TEntity, TKey>, INhPagedDAO<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionProvider"></param>
        public BusinessNhPagedDao(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, DetachedCriteria criteria)
        {
            return this.CurrentSession.GetPagedResult<TEntity>(startIndex, pageSize, criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, QueryOver<TEntity> query)
        {
            return this.CurrentSession.GetPagedResult<TEntity>(startIndex, pageSize, query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, IQueryable<TEntity> query)
        {
            return this.CurrentSession.GetPagedResult<TEntity>(startIndex, pageSize, query);
        }
    }
}
