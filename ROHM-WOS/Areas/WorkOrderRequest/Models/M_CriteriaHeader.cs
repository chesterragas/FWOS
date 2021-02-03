using System.Collections.Generic;
namespace ROHM_WOS.Areas.WorkOrderRequest.Models
{
    public class M_CriteriaHeader
    {
        public int ID { get; set; }
        public string WorkCategory { get; set; }
        public string CriteriaName { get; set; }
        public int CriteriaOrder { get; set; }
        public List<M_CriteriaDetails> CriteriaDetailsList { get; set; }
    }
    public class M_CriteriaDetails
    {
        public int ID { get; set; }
        public int CriteriaHeaderID { get; set; }
        public string DetailName { get; set; }
        public int DetailPoints { get; set; }
        public int DetailOrder { get; set; }
    }
}
