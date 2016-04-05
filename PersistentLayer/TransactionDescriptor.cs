using System.Data;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionDescriptor
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the isolation.
        /// </summary>
        /// <value>
        /// The isolation.
        /// </value>
        public IsolationLevel? Isolation { get; set; }
    }
}
