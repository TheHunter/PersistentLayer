namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionInfo
    {
        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        int Index { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
    }
}
