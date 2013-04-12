using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessLayerException
        : Exception
    {

        private string invokerName = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BusinessLayerException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        public BusinessLayerException(string message, string invokerName)
            : base(message)
        {
            this.InvokerName = invokerName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessLayerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="invokerName"></param>
        /// <param name="innerException"></param>
        public BusinessLayerException(string message, string invokerName, Exception innerException)
            : base(message, innerException)
        {
            this.invokerName = invokerName;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string InvokerName
        {
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value", "The method name cannot be empty or null.");
                this.invokerName = value;
            }
            get { return this.invokerName; }
        }
    }
}
