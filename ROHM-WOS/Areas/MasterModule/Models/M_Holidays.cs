using ROHM_WOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Areas.MasterModule.Models
{
    public class M_Holidays : BaseEntity
    {
        public long ID { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Type { get; set; }
    }
}