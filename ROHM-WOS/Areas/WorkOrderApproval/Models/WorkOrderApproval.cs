using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Areas.WorkOrderApproval.Models
{
    public class P_WorkOrderApprovalList
    {
        public int ID { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string ProcessArea { get; set; }
        public string ReferenceNo { get; set; }
        public string WorkOrderNo { get; set; }
        public string Requestor { get; set; }
        public string RequestAge { get; set; }
        public bool Rejected { get; set; }
    }
}