using System;

namespace PersistentLayer.Domain
{
    [Serializable]
    public class Student
    {
        private Guid _Code;
        private string _Name = null;
        private string _Surname = null;
        private string _Email = null;

        public virtual Guid Code
        {
            protected set
            {
                this._Code = value;
            }
            get
            {
                return this._Code;
            }
        }

        public virtual string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public virtual string Surname
        {
            get { return this._Surname; }
            set { this._Surname = value; }
        }

        public virtual string Email
        {
            get { return this._Email; }
            set { this._Email = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Student)
            {
                return this.GetHashCode() == obj.GetHashCode();
            }
            return false;

        }

        public override int GetHashCode()
        {
            return (this.Name != null ? this.Name.GetHashCode() : 0) +
                (this.Surname != null ? this.Surname.GetHashCode() : 0);
        }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}
