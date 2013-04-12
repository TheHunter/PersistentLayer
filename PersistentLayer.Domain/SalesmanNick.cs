using System;

namespace PersistentLayer.Domain
{
    [Serializable]
    public class SalesmanNick
    {
        private string _Code = string.Empty;
        private string _Description = null;
        private Iesi.Collections.Generic.ISet<Salesman> _Consultants = null;

        public virtual string Code
        {
            protected set { this._Code = value; }
            get { return this._Code; }
        }

        public virtual string Description
        {
            protected set { this._Description = value; }
            get { return this._Description; }
        }

        public virtual Iesi.Collections.Generic.ISet<Salesman> Consultants
        {
            get { return this._Consultants; }
            set { this._Consultants = value; }

        }
    }
}
