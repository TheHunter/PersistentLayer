using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class CommitFailedException
        : BusinessLayerException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public CommitFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        public CommitFailedException(string message, string invokerName)
            : base(message, invokerName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CommitFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        /// <param name="innerException"></param>
        public CommitFailedException(string message, string invokerName, Exception innerException)
            : base(message, invokerName, innerException)
        {
        }
    }
}
