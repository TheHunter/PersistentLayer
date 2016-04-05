namespace PersistentLayer
{
    /// <summary>
    /// Rapppresents a status of transactions.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// The waiting for being executed.
        /// </summary>
        WaitingFor = 0,

        /// <summary>
        /// Indicates a transaction is in progress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Indicates a transaction was rolledback.
        /// </summary>
        RolledBack = 3,

        /// <summary>
        /// Indicates a transaction was committed.
        /// </summary>
        Committed = 4,
    }
}
