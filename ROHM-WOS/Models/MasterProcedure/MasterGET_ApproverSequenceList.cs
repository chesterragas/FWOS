﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterProcedure
{
    public class MasterGET_ApproverSequenceList
    {
        public long ID { get; set; }
        public long DivisionID { get; set; }
        public string DivisionName { get; set; }
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public long SectionID { get; set; }
        public string SectionName { get; set; }
        public long EmployeeNo { get; set; }
        public string EmpNo { get; set; }
        public int OrderNo { get; set; }
        public bool MainBackupApprover { get; set; }
        public string Position { get; set; }
    }
}