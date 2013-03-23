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
    public class EnterprisePagedDAO
        : EnterpriseDAO, IPagedDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionProvider"></param>
        public EnterprisePagedDAO(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, ICriteria criteria) where TEntity : class
        {
            return this.CurrentSession.GetPagedResult<TEntity>(startIndex, pageSize, criteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, Criterion.DetachedCriteria criteria) where TEntity : class
        {
            return this.GetPagedResult<TEntity>(startIndex, pageSize, criteria.GetExecutableCriteria(this.SessionInfo.CurrentSession));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, Criterion.QueryOver<TEntity> query) where TEntity : class
        {
            return this.GetPagedResult<TEntity>(startIndex, pageSize, query.GetExecutableQueryOver(this.SessionInfo.CurrentSession).UnderlyingCriteria);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IPagedResult<TEntity> GetPagedResult<TEntity>(int startIndex, int pageSize, IQueryable<TEntity> query) where TEntity : class
        {
            return this.CurrentSession.GetPagedResult<TEntity>(startIndex, pageSize, query);
        }
    }
}
