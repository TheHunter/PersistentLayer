using EntityModel;
using EntityModel.Notifiers;

namespace PersistentLayer.Domain
{
    public class NumEntity
        : ObservableEntity<long>
    {
        private string _Testo = null;

        public virtual string Testo
        {
            get { return this._Testo; }
            set { this._Testo = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is NumEntity)
            {
                return this.GetHashCode() == obj.GetHashCode();
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (this.Testo == null ? string.Empty.GetHashCode() : this.Testo.GetHashCode());
        }
    }
}
