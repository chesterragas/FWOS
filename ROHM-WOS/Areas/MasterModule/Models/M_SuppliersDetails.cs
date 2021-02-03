﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterEntities
{
    public class M_SuppliersDetails : BaseEntity
    {
        public long ID { get; set; }
        public string SupplierID { get; set; }
        public string ContactTitle { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactPosition { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone { get; set; }
        public string ContactCellphone { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}