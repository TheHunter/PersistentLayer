using System;

namespace PersistentLayer.Domain
{
    [Serializable]
    public class Agency
    {
        private long id = 0;
        private string name = null;
        private long idManager = 0;

        public virtual long ID
        {
            set { this.id = value; }
            get { return this.id; }
        }

        public virtual string Name
        {
            set { this.name = value; }
            get { return this.name; }
        }

        public virtual long IDManager
        {
            set { this.idManager = value; }
            get { return this.idManager; }
        }
    }
}
