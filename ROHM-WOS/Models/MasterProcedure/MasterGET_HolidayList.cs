using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Areas.MasterModule.Models
{
    public class MasterGET_HolidayList
    {
        public long Rownum { get; set; }
        public long ID { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}