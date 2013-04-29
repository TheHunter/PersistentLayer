using System;
using EntityModel;
using Iesi.Collections.Generic;

namespace PersistentLayer.Domain
{
    [Serializable]
    public class Salesman
        : VersionedEntity<long?>
    {
        private string name;
        private string surname;
        private string email;
        private int? identityCode;
        private ISet<Salesman> agents;
        private ISet<Agency> agencies;
        private ISet<TradeContract> contracts;

        public Salesman() { }

        public Salesman(bool isTransient)
        {
            this.contracts = new HashedSet<TradeContract>();
            this.agents = new HashedSet<Salesman>();
            this.agencies = new HashedSet<Agency>();
        }

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public virtual string Email
        {
            get { return email; }
            set { this.email = value; }
        }

        public virtual int? IdentityCode
        {
            get { return this.identityCode; }
            set { this.identityCode = value; }
        }

        public virtual ISet<Salesman> Agents
        {
            get { return agents; }
            protected set { agents = value; }
        }

        public virtual ISet<Agency> Agencies
        {
            get { return agencies; }
            protected set { agencies = value; }
        }

        public virtual ISet<TradeContract> Contracts
        {
            get { return contracts; }
            protected set { this.contracts = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Salesman)
                return this.GetHashCode() == obj.GetHashCode();
            return false;
        }

        public override int GetHashCode()
        {
            return (13 * (this.IdentityCode == null ? 0 : this.IdentityCode.Value));
        }

        public override string ToString()
        {
            return string.Format("IDCode: {0}, Name: {1}, Surname: {2}, SubAgents: {3}", this.IdentityCode, this.Name, this.Surname, this.Agents == null ? 0 : this.Agents.Count);
        }

        public virtual void UpdateVersion(Salesman cons)
        {
            this.Version = cons.Version;
        }

        public virtual void UpdateId(Salesman cons)
        {
            this.ID = cons.ID;
        }
    }
}
