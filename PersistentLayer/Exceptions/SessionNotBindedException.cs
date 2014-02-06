using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionNotBindedException
        : BusinessLayerException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SessionNotBindedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        public SessionNotBindedException(string message, string invokerName)
            : base(message, invokerName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public SessionNotBindedException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        /// <param name="exception"></param>
        public SessionNotBindedException(string message, string invokerName, Exception exception)
            :base (message, invokerName, exception)
        {
        }
    }
}
