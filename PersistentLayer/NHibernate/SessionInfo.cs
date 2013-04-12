using System;
using NHibernate;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionInfo
    {
        private readonly ISessionProvider sessionProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public SessionInfo(ISessionProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("Error into assign the session provider into BussinesDAO 'cause It's null.");
            
            this.sessionProvider = provider;
        }

        /// <summary>
        /// 
        /// </summary>
        internal protected ISession CurrentSession
        {
            get { return this.sessionProvider.GetCurrentSession(); }
        }

        /// <summary>
        /// 
        /// </summary>
        internal protected ISessionProvider Provider
        {
            get { return this.sessionProvider; }
        }
    }
}
