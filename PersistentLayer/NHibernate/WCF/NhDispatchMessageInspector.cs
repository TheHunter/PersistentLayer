using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using NH = NHibernate;
using NHibernate.Context;

namespace PersistentLayer.NHibernate.WCF
{
    /// <summary>
    /// Defines a base implementation for custom inspection or modification of inbound and outbound application messages in services applications.
    /// </summary>
    public class NhDispatchMessageInspector
        : IDispatchMessageInspector
    {
        private readonly NH.ISessionFactory sessionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionFactory"></param>
        public NhDispatchMessageInspector(NH.ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public Object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            CurrentSessionContext.Bind(this.sessionFactory.OpenSession());
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref Message reply, Object correlationState)
        {
            NH.ISession session = CurrentSessionContext.Unbind(this.sessionFactory);
            session.Dispose();
        }
    }
}
