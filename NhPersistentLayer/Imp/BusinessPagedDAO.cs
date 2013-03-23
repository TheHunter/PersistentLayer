using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate.DAL.NhPersistentLayer.Imp.Util;

namespace NHibernate.DAL.NhPersistentLayer.Imp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="VKey"></typeparam>
    public class BusinessPagedDAO<TEntity, VKey>
        : BusinessDAO<TEntity, VKey>, IPagedDAO<TEntity, VKey>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionProvider"></param>
        public BusinessPagedDAO(ISessionProvider sessionProvider)
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
        private IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, ICriteria criteria)
        {
            return this.CurrentSession.GetPagedResult<TEntity>(startIndex, pageSize, criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, Criterion.DetachedCriteria criteria)
        {
            return this.GetPagedResult(startIndex, pageSize, criteria.GetExecutableCriteria(this.SessionInfo.CurrentSession));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult(int startIndex, int pageSize, Criterion.QueryOver<TEntity> query)
        {
            return this.GetPagedResult(startIndex, pageSize, query.GetExecutableQueryOver(this.SessionInfo.CurrentSession).UnderlyingCriteria);
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
