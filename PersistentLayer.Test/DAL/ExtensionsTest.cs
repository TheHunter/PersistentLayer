using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PersistentLayer.Domain;
using PersistentLayer.Exceptions;
using PersistentLayer.NHibernate.Impl;

namespace PersistentLayer.Test.DAL
{
    public class ExtensionsTest
        : CurrentTester
    {

        [Test]
        [Category("Extensions")]
        public void GetMetadataInfo()
        {
            var info = CurrentPagedDAO.GetPeristentClassInfo(typeof (Salesman));
            Assert.IsNotNull(info);
        }

        [Test]
        [Category("Extensions")]
        [ExpectedException(typeof(BusinessLayerException))]
        public void FailedGetMetadaInfo()
        {
            CurrentPagedDAO.GetPeristentClassInfo(null);
        }

        [Test]
        [Category("Extensions")]
        public void GetNullMetadataInfo()
        {
            var info = CurrentPagedDAO.GetPeristentClassInfo(typeof(StringBuilder));
            Assert.IsNull(info);
        }


    }
}
