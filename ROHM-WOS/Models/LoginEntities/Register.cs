using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.LoginEntities
{
    public class Register
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmployeeNo { get; set; }
        public string MobileNo { get; set; }
        public string LocalNo { get; set; }
        public long DivisionID { get; set; }
        public long DepartmentID { get; set; }
        public long SectionID { get; set; }
    }
}