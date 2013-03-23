using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionNotOpenedException
        : SessionLayerException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SessionNotOpenedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SessionNotOpenedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
