using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterProcedure
{
    public class UserPageAccess
    {
        public long ID { get; set; }
        public string PageIndex { get; set; }
        public string PageName { get; set; }
        public string PageModule { get; set; }
        public Nullable<bool> AccessType { get; set; }
    }
}