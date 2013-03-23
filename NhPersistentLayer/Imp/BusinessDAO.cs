using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Imp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="VKey"></typeparam>
    public class BusinessDAO<TEntity, VKey>
        : EntityDAO<TEntity, VKey>, ISessionDAO<TEntity, VKey>
        where TEntity : class
    {
        private SessionInfo sessionInfo = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionProvider"></param>
        public BusinessDAO(ISessionProvider sessionProvider)
        {
            sessionInfo = new SessionInfo(sessionProvider);
        }

        /// <summary>
        /// 
        /// </summary>
        public override SessionInfo SessionInfo
        {
            get { return this.sessionInfo; }
        }

    }
}
