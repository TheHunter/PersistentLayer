using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersistentLayer.Domain;

namespace PersistentLayer.Test.Wrappers
{
    /// <summary>
    /// 
    /// </summary>
    public class SalesmanDetails
        : SalesmanPrj
    {
        public ICollection<TradeContract> OwnContracts { get; set; }
    }
}
