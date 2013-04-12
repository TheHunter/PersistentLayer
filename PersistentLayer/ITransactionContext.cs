namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITransactionProvider GetTransactionProvider();
    }
}
