using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using System.Xml;
using System.IO;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class NhConfigurationBuilder
    {
        private Configuration _Config = null;
        private ISessionFactory _SessionFactory = null;

        /// <summary>
        /// 
        /// </summary>
        protected NhConfigurationBuilder() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileCfg"></param>
        /// <param name="dirMappingFiles"></param>
        public NhConfigurationBuilder(string fileCfg, string dirMappingFiles)
        {
            this.Config = new Configuration()
                            .Configure(fileCfg);

            this.Config.AddDirectory(new DirectoryInfo(dirMappingFiles));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileCfg"></param>
        /// <param name="mappingFiles"></param>
        public NhConfigurationBuilder(string fileCfg, IEnumerable<string> mappingFiles)
        {
            this.Config = new Configuration()
                            .Configure(fileCfg);

            mappingFiles.All
                (
                    delegate(string xmlfile)
                    {
                        this.Config.AddXmlFile(xmlfile);
                        return true;
                    }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileCfg"></param>
        /// <param name="directory"></param>
        public NhConfigurationBuilder(XmlReader fileCfg, DirectoryInfo directory)
        {
            this.Config = new Configuration()
                            .Configure(fileCfg);

            //this.Config.SetCacheConcurrencyStrategy
            //this.Config.SetCollectionCacheConcurrencyStrategy();

            this.Config.AddDirectory(directory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfg"></param>
        public NhConfigurationBuilder(Configuration cfg)
        {
            this.Config = cfg;
        }

        /// <summary>
        /// Makes a new Configuration instance from the given Fluent configuration.
        /// </summary>
        /// <param name="cfg"></param>
        public NhConfigurationBuilder(FluentConfiguration cfg)
        {
            this.Config = cfg.BuildConfiguration();
        }

        /// <summary>
        /// Sets the value of the Configuration property.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void OverrideProperty(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("property name cannot be empty and null.");

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(string.Format("property value cannot be empty or null, property name: {0}", name));

            this.Config.SetProperty(name, value);
        }

        /// <summary>
        /// Sets the value of the Configuration property.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetProperty(string name, string value)
        {
            if (this._SessionFactory == null)
            {
                this.OverrideProperty(name, value);
            }
        }

        /// <summary>
        /// Sets the values of the Configuration properties.
        /// </summary>
        /// <param name="properties"></param>
        public void SetProperties(IDictionary<string, string> properties)
        {
            if (this._SessionFactory == null && properties != null && properties.Count > 0)
            {
                properties.All
                    (
                        delegate(KeyValuePair<string, string> current)
                        {
                            this.OverrideProperty(current.Key, current.Value);
                            return true;
                        }
                    );
            }
        }

        /// <summary>
        /// Sets the default interceptor object which be used by all session created by the SessionFactory.
        /// </summary>
        /// <param name="defaultInterceptor"></param>
        public void SetInterceptor(IInterceptor defaultInterceptor)
        {
            if (defaultInterceptor != null)
                this.Config.SetInterceptor(defaultInterceptor);
        }

        /// <summary>
        /// 
        /// </summary>
        public void BuildSessionFactory()
        {
            if (this._SessionFactory == null)
                this._SessionFactory = this.Config.BuildSessionFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasClassMappings
        {
            get
            {
                return this._Config.ClassMappings.Count > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Configuration Config
        {
            get
            {
                return this._Config;
            }
            private set
            {
                this._Config = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get { return this._SessionFactory; }
        }
    }
}
