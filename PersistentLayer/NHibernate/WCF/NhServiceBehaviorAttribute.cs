using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using NHibernate;

namespace PersistentLayer.NHibernate.WCF
{
    /// <summary>
    /// Apply a new behavior into the calling service, adding a new IDispatchMessageInspector instance for binding / unbinding nhibernate sessions when an operation contract is called.
    /// </summary>
    public class NhServiceBehaviorAttribute
         : Attribute, IServiceBehavior
    {

        private readonly ISessionFactory sessionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionFactoyProvider">A property name which returns a session factory for this object.</param>
        /// <param name="provider"></param>
        public NhServiceBehaviorAttribute(string sessionFactoyProvider, Type provider)
        {
            if (provider == null)
                throw new ServiceArgumentException("The provider for getting ISessionFactory object cannot be null.", "provider");

            if (sessionFactoyProvider == null || string.IsNullOrEmpty(sessionFactoyProvider.Trim()))
                throw new ServiceArgumentException("The property name for getting the ISessionfactory cannot be empty or null.", "sessionFactoyProvider");

            const BindingFlags flags = BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            PropertyInfo propertyInfo = provider.GetProperty(sessionFactoyProvider, flags);

            if (propertyInfo == null)
                throw new ServiceBehaviorException("Property for getting the ISessionFactory instance hasn't found.");

            if (!typeof(ISessionFactory).IsAssignableFrom(propertyInfo.PropertyType))
                throw new ServiceBehaviorException("The property type doesn't equal to ISessionFactory");

            try
            {
                var ret = propertyInfo.GetValue(null, null);
                sessionFactory = (ISessionFactory)ret;
            }
            catch (Exception ex)
            {
                throw new WcfServiceException("Error on getting the ISessionFactory instance.", ex);
            }

            if (sessionFactory == null)
                throw new ServiceBehaviorException("No ISessionFactory reference was founded.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in channelDispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors
                        .Add(new NhDispatchMessageInspector(this.sessionFactory));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }
    }
}
