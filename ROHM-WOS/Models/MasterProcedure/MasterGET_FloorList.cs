using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterProcedure
{
    public class MasterGET_FloorList
    {
        public long Rownum { get; set; }
        public long ID { get; set; }
        public long BuildingID { get; set; }
        public string BuildingName { get; set; }
        public string FloorName { get; set; }
        
    }
}