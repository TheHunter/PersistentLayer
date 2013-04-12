using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class InnerRollBackException
        : BusinessLayerException
    {
        private int indexTransaction = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InnerRollBackException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        public InnerRollBackException(string message, string invokerName)
            : base(message, invokerName)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InnerRollBackException(string message, Exception innerException)
            :base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        /// <param name="innerException"></param>
        public InnerRollBackException(string message, string invokerName, Exception innerException)
            : base(message, invokerName, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int IndexTransaction
        {
            internal protected set { this.indexTransaction = value; }
            get { return this.indexTransaction; }
        }
    }
}
