//using System.Collections.Generic;

namespace PersistentLayer.Test.Wrappers
{
    public class SalesmanPrj
    {
        public virtual long? ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        //public ISet<Salesman> Agents { get; set; }
    }
}
