using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrignoV2.Business.Entities;
using NHibernate.DAL.NhPersistentLayer.Imp.Util;
using NHibernate.DAL.NhPersistentLayer.Imp;
using NHibernate.DAL.NhPersistentLayer;
using NhPersistentLayer_tst.Wrappers;
using NHibernate.Transform;
using NHibernate.Criterion;
using NHibernate;

namespace WFA_NHibernate.DaoExt
{

    public class ConsultantDAO
        : BusinessPagedDAO<Consultant, long>
    {

        public ConsultantDAO(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        public IEnumerable<Consultant> GetInstancesWhithIDBiggerThan(long ID)
        {
            return this.CurrentSession.GetNamedQuery("InstancesByID").SetParameter("ID", ID).List<Consultant>();
        }

        public IEnumerable<Consultant> GetInstancesBiggerThan(long code)
        {
            return this.CurrentSession.GetNamedQuery("ConsultantsQueryByCode").SetParameter("code", code).List<Consultant>();
        }

        public IEnumerable<Consultant> GetConsByDataRif_func(DateTime datarif)
        {
            return this.CurrentSession.GetNamedQuery("GetConsByDataFunc")
                .SetDateTime("datarif", datarif)
                .List<Consultant>();
        }

        public IEnumerable<Consultant> SetConsultantByName(string name)
        {
            return this.CurrentSession.GetNamedQuery("SetConsultantByName").SetParameter("name", name).List<Consultant>();
        }

        public IEnumerable<Consultant> SPSetConsultantByName(string name)
        {
            return this.CurrentSession.GetNamedQuery("SPSetConsultantByName").SetParameter("name", name).List<Consultant>();
        }

        public IEnumerable<ReportConsultant> GetRepConsultant()
        {
            return this.CurrentSession.GetNamedQuery("RepConsultant").SetResultTransformer(Transformers.AliasToBean<ReportConsultant>()).List<ReportConsultant>();
        }

        public IEnumerable<ReportConsultant> GetReportWithLinq()
        {
            var res = this.ToIQueryable().Select(n => new ReportConsultant { ID = n.ID.Value, Name = n.Name, Surname = n.Surname, NumSubAgents = n.Agents.Count });
            return res.ToList();
        }

        public IEnumerable<Consultant> GetConsultantsByExample(Consultant instance)
        {
            return this.CurrentSession.CreateCriteria<Consultant>()
                .Add(Example.Create(instance))
                .List<Consultant>();
        }

        public IQueryOver<Consultant> GetQueryOverByExample(Consultant consultant)
        {
            IQueryOver<Consultant> query = QueryOver.Of<Consultant>().And(Example.Create(consultant));
            return query;
        }


    }
}
