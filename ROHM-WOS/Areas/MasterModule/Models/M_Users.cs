﻿namespace ROHM_WOS.Models.MasterEntities
{
    public class M_Users : BaseEntity
    {
        public long ID { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ResetPassword { get; set; }
        public string UserPhoto { get; set; }
        public bool IsDeleted { get; set; }
        public bool CanReceive { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string LocalNo { get; set; }
        public long DivisionID { get; set; }
        public long SectionID { get; set; }
        public long DepartmentID { get; set; }
    }
}
