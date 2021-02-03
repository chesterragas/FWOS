using ROHM_WOS.Models.Helper;
using System.Collections.Generic;
namespace ROHM_WOS.Areas.WorkOrderRequest.Models
{
    public class WO_RequestorInformation
    {
        public int ID { get; set; }
        public int Rownum { get; set; }
        public string DateRequest { get; set; }
        public string WorkOrderTitle { get; set; }
        public string ReferenceNo { get; set; }
        public string WorkOrderNo { get; set; }
        public string RequestorEmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string MobileNo { get; set; }
        public string LocalNo { get; set; }
        public string Email { get; set; }
        public int DivisionID { get; set; }
        public string DivisionName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int SectionID { get; set; }
        public string SectionName { get; set; }
        public string Approved { get; set; }
        public string ForApproval { get; set; }
        public bool IsSubmitted { get; set; }
        public string Rejected { get; set; }
        public string RevisionNo { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }

        public WO_WorkDescriptionDetails WO_WorkDescriptionDetailsData { get; set; }
        public WO_Facilities WO_FacilitiesData { get; set; }
        public List<WO_ContractorDetails> WO_ContractorDetailsDataList { get; set; }
    }
    public class WO_WorkDescriptionDetails
    {
        public int ID { get; set; }
        public int RequestorInformationID { get; set; }
        public int BuildingID { get; set; }
        public string BuildingOther { get; set; }
        public int FloorID { get; set; }
        public string FloorOther { get; set; }
        public int ProcessAreaID { get; set; }
        public string ProcessAreaOther { get; set; }
        public string Details { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }

        public List<WO_Drawing> WO_DrawingList { get; set; }
        public List<WO_WorkRequest> WO_WorkRequestList { get; set; }
        public List<WO_WorkClassification> WO_WorkClassificationList { get; set; }
    }
    public class WO_WorkClassification
    {
        public int ID { get; set; }
        public int RequestorInformationID { get; set; }
        public int WorkClassificationID { get; set; }
        public string WorkClassificationOther { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
    }
    public class WO_WorkRequest
    {
        public int ID { get; set; }
        public int RequestorInformationID { get; set; }
        public int WorkRequestID { get; set; }
        public string WorkRequestOther { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
    }
    public class WO_Drawing
    {
        public int RequestorInformationID { get; set; }
        public string FileName { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
        public List<DrawingBase64> base64Drawing { get; set; }

    }
    public class DrawingBase64
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Base64File { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
    }
    public class WO_Facilities
    {
        public int ID { get; set; }
        public int RequestorInformationID { get; set; }
        public string ReceivedBy { get; set; }
        public string ReceivedDate { get; set; }
        public string WorkCategory { get; set; }
        public string RequestAssigned { get; set; }
        public string Notes { get; set; }
        public int PriorityLevel { get; set; }
        public string DeadlineDate { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
        public List<WO_CriteriaDetailsPoints> WO_CriteriaDetailsPointsList { get; set; }
        public List<WO_CriteriaHeaderRemarks> WO_CriteriaHeaderRemarksList { get; set; }
    }
    public class WO_CriteriaDetailsPoints
    {
        public int RequestorInformationID { get; set; }
        public int CriteriaDetailsID { get; set; }
        public int CriteriaDetailsPoints { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
    }
    public class WO_CriteriaHeaderRemarks
    {
        public int RequestorInformationID { get; set; }
        public int CriteriaHeaderID { get; set; }
        public int CriteriaPoints { get; set; }
        public string Remarks { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
    }
    public class WO_ContractorDetails
    {
        public int ID { get; set; }
        public int SectionID { get; set; }
        public int RequestorInformationID { get; set; }
        public int SupplierID { get; set; }
        public string EmpName { get; set; }
        public string SectionName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string DateRequested { get; set; }
        public string SurveryDateTime { get; set; }
        public string QuotationSubmission { get; set; }

        public int IsDeleted { get; set; }
        public string CreateID { get; set; }
        public string CreateDate { get; set; }
        public string UpdateID { get; set; }
        public string UpdateDate { get; set; }
    }

    public class UserDropdown : DropDown
    {
        public string EmployeeNo { get; set; }
    }

    public class FacilitiesList
    {
        public int Rownum { get; set; }
        public int ID { get; set; }
        public string SectionName { get; set; }
        public string EmpName { get; set; }
    }
}
