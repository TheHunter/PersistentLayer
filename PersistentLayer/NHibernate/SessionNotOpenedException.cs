using System;
using PersistentLayer.Exceptions;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionNotOpenedException
        : BusinessLayerException
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="invokerName"></param>
        public SessionNotOpenedException(string message, string invokerName, Exception innerException)
            : base(message, invokerName, innerException)
        {
        }
    }
}
