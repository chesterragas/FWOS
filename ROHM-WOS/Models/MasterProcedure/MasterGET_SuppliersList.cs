﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterProcedure
{
    public class MasterGET_SuppliersList
    {
        public int Rownum { get; set; }
        public int ID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
    }
}