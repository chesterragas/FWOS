using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterEntities
{
    public class M_TimeFrame : BaseEntity
    {
        public long ID { get; set; }
        public string WorkCategory { get; set; }
        public int WorkingDays { get; set; }
        public int PriorityLevel { get; set; }
        public int ScoreFrom { get; set; }
        public int ScoreTo { get; set; }
        public string PriorityLevelRemarks { get; set; }
        public bool IsDeleted { get; set; }
    }
}