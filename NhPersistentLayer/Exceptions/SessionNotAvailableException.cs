using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionNotAvailableException
        : SessionLayerException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SessionNotAvailableException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SessionNotAvailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
