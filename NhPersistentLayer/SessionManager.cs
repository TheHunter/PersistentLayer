using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.DAL.NhPersistentLayer.Exceptions;
using System.Data;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// Manages the session factory in order to open/manage Sessions.
    /// </summary>
    [Serializable]
    public class SessionManager
        : ISessionProvider
    {
        private int _TransactionCounter = 0;
        /// <summary>
        /// This is the factory which creates new sessions, and It's able to reference the current binded session
        /// made by CurrentSessionContext
        /// </summary>
        private readonly ISessionFactory _SessionFactory = null;

        #region Session factory section

        /// <summary>
        /// 
        /// </summary>
        protected SessionManager() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionFactory"></param>
        public SessionManager(ISessionFactory sessionFactory)
        {
            if (sessionFactory == null)
                throw new ArgumentNullException("the SessionFactory for SessionManager cannot be null.");

            this._SessionFactory = sessionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get { return this._SessionFactory; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TransactionCounter
        {
            get { return this._TransactionCounter; }
            private set { this._TransactionCounter = value; }
        }

        #endregion

        /// <summary>
        /// Gets the current binded session from the calling session manager.
        /// </summary>
        /// <returns>returns the current binded session</returns>
        /// <exception cref="SessionNotBindedException">
        /// Throws an exception when there's no session binded into any CurrentSessionContext.
        /// </exception>
        public ISession GetCurrentSession()
        {
            ISession session = null;
            try
            {
                session = this._SessionFactory.GetCurrentSession();
            }
            catch (Exception ex)
            {
                this._TransactionCounter = 0;
                SessionNotBindedException current = new SessionNotBindedException("There's no binded session, so first It would require to open a new session.", ex);
                current.InvokerName = "GetCurrentSession";
                throw current;
            }
            return session;
        }

        /// <summary>
        /// Begin a new transaction from the current binded session with the specified IsolationLevel.
        /// </summary>
        /// <param name="level">IsolationLevel for this transaction.</param>
        /// <exception cref="SessionNotBindedException">
        /// Throws an exception when there's no session binded into any CurrentSessionContext.
        /// </exception>
        public void BeginTransaction(IsolationLevel? level)
        {
            if (this.TransactionCounter == 0)
            {
                try
                {
                    ISession session = this.GetCurrentSession();
                    if (level == null)
                        session.BeginTransaction();
                    else
                        session.BeginTransaction(level.Value);
                }
                catch (SessionNotBindedException ex)
                {
                    ex.InvokerName = "BeginTransaction";
                    throw ex;
                }
            }
            this.TransactionCounter++;
        }

        /// <summary>
        /// Commit the current transaction and flushes the associated session.
        /// </summary>
        /// <exception cref="CommitFailedException">
        /// Throws an exception when current transaction tries to commit.
        /// </exception>
        public void CommitTransaction()
        {
            if (_TransactionCounter > 0)
            {
                _TransactionCounter--;
                if (_TransactionCounter == 0)
                {
                    ITransaction transaction = null;
                    try
                    {
                        ISession session = this.GetCurrentSession();
                        transaction = session.Transaction;
                        if (session.FlushMode == FlushMode.Never)
                        {
                            session.Flush();
                        }
                        transaction.Commit();
                    }
                    catch (SessionNotBindedException)
                    {
                        /*
                         * NOTA: questa eccezione è stata sollevata dalla chiamata
                         * a GetCurrentSession(), dovuto al fatto che non è riuscito a recuperare la 
                         * Binded session, quindi occorre verificare il perché
                         * la sessione non è stata sottoposta al binding.
                         *
                        */
                    }
                    catch (Exception ex)
                    {
                        // accettarsi che la chiamata a RollBack non generi un'eccezione
                        // in quel caso occorre gestirlo, eventualement risollervalo incapsulandolo in un'altra eccezione
                        transaction.Rollback();
                        CommitFailedException current = new CommitFailedException("Error when the current session tries to commit the current transaction.", ex);
                        current.InvokerName = "CommitTransaction";
                        throw current;
                    }
                }
            }
        }

        /// <summary>
        /// Makes a rollback into current transaction
        /// </summary>
        /// <exception cref="RollbackFailedException">
        /// Throws an exception when current transaction makes a rollback.
        /// </exception>
        /// <exception cref="InnerRollBackException">
        /// Throws an exception when an inner transaction makes a rollback.
        /// </exception>
        public virtual void RollbackTransaction()
        {
            if (this.TransactionCounter > 0)
            {
                this.TransactionCounter--;
                ITransaction transaction = null;
                ISession session = null;
                try
                {
                    session = this.GetCurrentSession();
                    transaction = session.Transaction;
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        RollbackFailedException current = new RollbackFailedException("Error on calling RollbackTransaction method", ex);
                        current.InvokerName = "RollbackTransaction";
                        throw current;
                    }

                    // significa che la chiamata a questo metodo avviene da una Inner Transaction
                    // ed in questo caso si dovrà sollevare un'eccezione per indicare che è avvenuto un rollback
                    // da una sotto transazione.
                    if (this.TransactionCounter > 0)
                    {
                        InnerRollBackException innerRollback = new InnerRollBackException("An inner rollback transaction has occurred.");
                        innerRollback.InvokerName = "RollbackTransaction";
                        innerRollback.IndexTransaction = this._TransactionCounter + 1;
                        throw innerRollback;
                    }
                }
                catch (SessionNotBindedException)
                {
                    #region
                    // eccezione sollevata dalla chiamata a GetCurrentSession()
                    // e il motivo di questa eccezione è causato dal mancato binding della sessione corrente.
                    /*
                     * NOTA:
                     * Questa eccezione non viene considerata perché significa che il codice chiamante
                     * ha tentato di iniziare una transaction senza considerare che occorre fare il binding della 
                     * sessione che si intende utilizzare, che serve per eseguire le operazioni verso il DataStore.
                     * */
                    #endregion
                }
                finally
                {
                    this.TransactionCounter = 0;
                }
            }
        }
    }
}
