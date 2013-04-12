using System;
using NHibernate;
using System.Reflection;
using NHibernate.Context;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// A session manager which gets binded and unbinded sessions.
    /// </summary>
    /// <typeparam name="TBinder">type of binder which uses this session manager.</typeparam>
    public class SessionBinderLayer<TBinder>
        : SessionManager
        where TBinder : CurrentSessionContext
    {

        private Action<ISession> bindAction;
        private Func<ISessionFactory, ISession> unBindAction;
        private Func<ISessionFactory, bool> hasBind;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionFactory"></param>
        public SessionBinderLayer(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
            this.OnInit();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnInit()
        {
            var flags = BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
            var type = typeof(TBinder);
            bindAction = (Action<ISession>)Delegate.CreateDelegate(typeof(Action<ISession>), null, type.GetMethod("Bind", flags));
            unBindAction = (Func<ISessionFactory, ISession>)Delegate.CreateDelegate(typeof(Func<ISessionFactory, ISession>), null, type.GetMethod("Unbind", flags));
            hasBind = (Func<ISessionFactory, bool>)Delegate.CreateDelegate(typeof(Func<ISessionFactory, bool>), null, type.GetMethod("HasBind", flags));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ISession OpenSession()
        {
            try
            {
                return this.SessionFactory.OpenSession();
            }
            catch (Exception ex)
            {
                throw new SessionNotOpenedException("Error on trying to open a new session, verify if the current database connection is available.", "OpenSession", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="mode"></param>
        public void BindSession(ISession session, FlushMode mode)
        {
            if (session != null)
            {
                if (!session.IsOpen)
                {
                    throw new InvalidSessionException("The session to bind is closed, so It's required to open a new session.", "BindSession");
                }
            }
            else
            {
                throw new SessionNotAvailableException("There's no available session to bind into current context, It's require to open a new session.", "BindSession");
            }
            session.FlushMode = mode;
            this.bindAction(session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasSessionBinded()
        {
            return this.hasBind(this.SessionFactory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ISession UnbindSession()
        {
            return this.unBindAction(this.SessionFactory);
        }

        /// <summary>
        /// Makes a rollback the current transaction.
        /// </summary>
        /// <remarks>
        /// When a inner transaction rollbacks, It will be thrown a InnerRollBackException.
        /// After making rollback the transaction, the current session will be closed, then the current provider opens a new session in order to bind it into current context.
        /// </remarks>
        public override void RollbackTransaction()
        {
            try
            {
                base.RollbackTransaction();
            }
            finally
            {
                #region
                // indipendente dalla modalità di uscita dell'istruzione del blocco try
                // occorre eseguire questo blocco di istruzioni.
                // Possibili eccezioni:
                // - InnerRollBackException     // avvenuto un RollBack in una Inner transaction.
                // - RollbackFailedException    // chiamata al RollBack fallisce.
                #endregion

                if (this.HasSessionBinded())
                {
                    ISession lastSession = this.UnbindSession();
                    this.BindSession(this.OpenSession(), lastSession.FlushMode);

                    if (lastSession != null && lastSession.IsOpen)
                    {
                        lastSession.Close();
                    }
                }
            }
        }
    }
}
