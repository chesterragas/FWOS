using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterEntities
{
    public class M_Suppliers:BaseEntity
    {
        public long ID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        
        
    }
}