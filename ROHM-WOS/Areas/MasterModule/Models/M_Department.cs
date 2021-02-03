﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterEntities
{
    public class M_Department : BaseEntity
    {
        public long ID { get; set; }
        public long DivisionID { get; set; }
        public string DepartmentName { get; set; }
        public bool IsDeleted { get; set; }
        public List<M_Section> SectionList = new List<M_Section>();

    }
}