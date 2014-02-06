using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionNotAvailableException
        : BusinessLayerException
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
        /// <param name="invokerName"></param>
        public SessionNotAvailableException(string message, string invokerName)
            : base(message, invokerName)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        /// <param name="innerException"></param>
        public SessionNotAvailableException(string message, string invokerName, Exception innerException)
            : base(message, invokerName, innerException)
        {
        }
    }
}
