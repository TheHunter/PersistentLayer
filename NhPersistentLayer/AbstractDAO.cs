using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.DAL.NhPersistentLayer.Imp;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractDAO
        : ISessionContext, ITransactionContext
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract SessionInfo SessionInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        protected ISession CurrentSession
        {
            get { return this.SessionInfo.CurrentSession; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITransactionProvider GetTransactionProvider()
        {
            return this.SessionInfo.Provider;
        }
    }
}
