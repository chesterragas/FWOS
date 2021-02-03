using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROHM_WOS.Models.MasterProcedure
{
    public class MasterGET_UserList:BaseEntity
    {
        public long ID { get; set; }
        public int Rownum { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserPhoto { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string LocalNo { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public bool CanReceive { get; set; }
        public int IsApproved { get; set; }
    }
}