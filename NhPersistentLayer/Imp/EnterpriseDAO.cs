using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Imp
{
    /// <summary>
    /// 
    /// </summary>
    public class EnterpriseDAO
        : DomainDAO, ISessionDAO
    {
        private SessionInfo sessionInfo = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionProvider"></param>
        public EnterpriseDAO(ISessionProvider sessionProvider)
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
