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
    public interface ISessionContext
    {
        /// <summary>
        /// 
        /// </summary>
        SessionInfo SessionInfo { get; }

    }
}
