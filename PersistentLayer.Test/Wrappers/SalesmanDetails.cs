using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iesi = Iesi.Collections.Generic;
using PersistentLayer.Domain;

namespace PersistentLayer.Test.Wrappers
{
    /// <summary>
    /// 
    /// </summary>
    public class SalesmanDetails
        : SalesmanPrj
    {
        public iesi.ISet<TradeContract> OwnContracts { get; set; }
    }
}
