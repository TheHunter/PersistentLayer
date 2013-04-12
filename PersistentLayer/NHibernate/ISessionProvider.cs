using NHibernate;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// Provides sessions by a higher implementation.
    /// </summary>
    public interface ISessionProvider
        : ITransactionProvider
    {
        /// <summary>
        /// Gets the current bounded session by a higher implementation level.
        /// </summary>
        /// <returns>Returns the current binded session by a higher implementation level.</returns>
        ISession GetCurrentSession();
    }
}
