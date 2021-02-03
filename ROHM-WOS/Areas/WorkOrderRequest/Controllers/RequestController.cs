﻿using ROHM_WOS.Areas.WorkOrderRequest.Models;
using ROHM_WOS.Controllers;
using ROHM_WOS.Models.Helper;
using ROHM_WOS.Models.MasterEntities;
using ROHM_WOS.Models.MasterProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
namespace ROHM_WOS.Areas.WorkOrderRequest.Controllers
{
    [Session]
    public class RequestController : Controller
    {
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        DataHelper dataHelper = new DataHelper();
        public ActionResult Index()
        {
            ViewBag.User = user;
            var DivisionList = GetDropdown_Division();
            ViewBag.DivisionList = DivisionList;
            ViewBag.DepartmentList = GetDropdown_Department(Convert.ToInt32(user.DivisionID));
            ViewBag.SectionList = GetDropdown_Section(Convert.ToInt32(user.DepartmentID));
            ViewBag.BuildingList = GetDropdown_Building(Convert.ToInt32(user.DivisionID));
            ViewBag.ProcessAreaList = GetDropdown_ProcessArea(Convert.ToInt32(user.DivisionID));
            ViewBag.WorkRequestList = GetDropdown_WorkRequest();
            ViewBag.WorkClassificationList = GetDropdown_WorkClassification();
            ViewBag.UserList = GetDropdown_User();
            //ViewBag.CriteriaList = GetDropdown_Criteria();
            //ViewBag.TimeFrameList = GetDropdown_TimeFrame();
            return View("Request");
        }
        public List<DropDown> GetDropdown_Division()
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_Division";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DropDown item = new DropDown();
                    item.ID = Convert.ToInt64(rdr["ID"]);
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public List<DropDown> GetDropdown_Department(long DivisionID)
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = DivisionID;
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_Department";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DropDown item = new DropDown();
                    item.ID = Convert.ToInt64(rdr["ID"]);
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public List<DropDown> GetDropdown_Section(long DepartmentID)
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = DepartmentID;
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_Section";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DropDown item = new DropDown();
                    item.ID = Convert.ToInt64(rdr["ID"]);
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public List<DropDown> GetDropdown_Building(long DivisionID)
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = DivisionID;
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_Building";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DropDown item = new DropDown();
                    item.ID = Convert.ToInt64(rdr["ID"]);
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public List<DropDown> GetDropdown_ProcessArea(long DivisionID)
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = DivisionID;
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_ProcessArea";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DropDown item = new DropDown();
                    item.ID = Convert.ToInt64(rdr["ID"]);
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public List<DropDown> GetDropdown_WorkRequest()
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_WorkRequest";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DropDown item = new DropDown();
                    item.ID = Convert.ToInt64(rdr["ID"]);
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public List<DropDown> GetDropdown_WorkClassification()
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_WorkClassification";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    DropDown item = new DropDown();
                    item.ID = Convert.ToInt64(rdr["ID"]);
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public List<UserDropdown> GetDropdown_User()
        {

            List<UserDropdown> list = new List<UserDropdown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_Users";

            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    UserDropdown item = new UserDropdown();
                    item.EmployeeNo = rdr["EmployeeNo"].ToString();
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return list;
        }
        public ActionResult GetDropdown_Criteria(string WorkCategory)
        {

            List<M_CriteriaHeader> list = new List<M_CriteriaHeader>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.WorkOrderRequest_GetCriteriaHeaderList";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@WorkCategory", SqlDbType.NVarChar).Value = WorkCategory;
            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    M_CriteriaHeader item = new M_CriteriaHeader();
                    item.WorkCategory = rdr["WorkCategory"].ToString();
                    item.CriteriaName = rdr["CriteriaName"].ToString();
                    item.ID = Convert.ToInt32(rdr["ID"]);
                    item.CriteriaOrder = Convert.ToInt32(rdr["CriteriaOrder"]);
                    list.Add(item);
                }
            }
            conn.Close();

            foreach (M_CriteriaHeader Criteria in list)
            {
                Criteria.CriteriaDetailsList = GetCriteriaDetails(Criteria.ID);
            }

            return Json(new { Criteria = list, TimeFrameList = GetDropdown_TimeFrame(WorkCategory) });
        }
        public List<M_CriteriaDetails> GetCriteriaDetails(long ID)
        {
            List<M_CriteriaDetails> DetailsList = new List<M_CriteriaDetails>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.WorkOrderRequest_GetCriteriaDetailsList";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@CriteriaHeaderID", SqlDbType.NVarChar).Value = ID;
            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    M_CriteriaDetails item = new M_CriteriaDetails();
                    item.ID = Convert.ToInt32(rdr["ID"]);
                    item.DetailName = rdr["DetailName"].ToString();
                    item.DetailPoints = Convert.ToInt32(rdr["DetailPoints"]);
                    item.DetailOrder = Convert.ToInt32(rdr["DetailOrder"]);
                    DetailsList.Add(item);
                }
            }
            conn.Close();
            return DetailsList;
        }
        public List<M_TimeFrame> GetDropdown_TimeFrame(string WorkCategory)
        {
            List<M_TimeFrame> DetailsList = new List<M_TimeFrame>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.WorkOrderRequest_GetTimeFrameList";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@WorkCategory", SqlDbType.NVarChar).Value = WorkCategory;
            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    M_TimeFrame item = new M_TimeFrame();
                    item.ID = Convert.ToInt32(rdr["ID"]);
                    item.WorkCategory = rdr["WorkCategory"].ToString();
                    item.WorkingDays = Convert.ToInt32(rdr["WorkingDays"]);
                    item.PriorityLevel = Convert.ToInt32(rdr["PriorityLevel"]);
                    item.ScoreFrom = Convert.ToInt32(rdr["ScoreFrom"]);
                    item.ScoreTo = Convert.ToInt32(rdr["ScoreTo"]);
                    item.PriorityLevelRemarks = rdr["PriorityLevelRemarks"].ToString();
                    DetailsList.Add(item);
                }
            }
            conn.Close();
            return DetailsList;
        }
        public ActionResult GetSuppliersList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"])); //Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                //ObjectParameter totalCount = new ObjectParameter("RecordCount", typeof(int));

                List<MasterGET_SuppliersList> list = new List<MasterGET_SuppliersList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.WorkOrderRequest_SupplierList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        MasterGET_SuppliersList getter = new MasterGET_SuppliersList();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.ID = Convert.ToInt32(rdr["ID"]);//.ToString();
                        getter.SupplierName = rdr["SupplierName"].ToString();
                        getter.Address = rdr["SupplierAddress"].ToString();


                        list.Add(getter);
                    }
                }

                int totalCount = Convert.ToInt32(cmdSql.Parameters["@RecordCount"].Value);
                conn.Close();

                int? totalrows = totalCount;
                int? totalrowsafterfiltering = totalCount;


                return Json(new { data = list, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetFacilitiesList(int SectionID)
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"])); //Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                //ObjectParameter totalCount = new ObjectParameter("RecordCount", typeof(int));

                List<FacilitiesList> list = new List<FacilitiesList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.WorkOrderRequest_GetFacilitiesList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@SectionID", SqlDbType.Int).Value = SectionID;
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        FacilitiesList getter = new FacilitiesList();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.ID = Convert.ToInt32(rdr["ID"]);//.ToString();
                        getter.SectionName = rdr["SectionName"].ToString();
                        getter.EmpName = rdr["EmpName"].ToString();
                        list.Add(getter);
                    }
                }

                int totalCount = Convert.ToInt32(cmdSql.Parameters["@RecordCount"].Value);
                conn.Close();

                int? totalrows = totalCount;
                int? totalrowsafterfiltering = totalCount;


                return Json(new { data = list, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetRequestList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"])); //Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                //ObjectParameter totalCount = new ObjectParameter("RecordCount", typeof(int));

                List<WO_RequestorInformation> list = new List<WO_RequestorInformation>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.WorkOrderRequest_GetRequestList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        WO_RequestorInformation getter = new WO_RequestorInformation();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.ID = Convert.ToInt32(rdr["ID"]);//.ToString();
                        getter.DateRequest = rdr["DateRequest"].ToString();
                        getter.WorkOrderTitle = rdr["WorkOrderTitle"].ToString();
                        getter.ReferenceNo = rdr["ReferenceNo"].ToString();
                        getter.WorkOrderNo = rdr["WorkOrderNo"].ToString();
                        getter.RequestorEmployeeNo = rdr["RequestorEmployeeNo"].ToString();
                        getter.EmployeeName = rdr["EmployeeName"].ToString();
                        getter.DivisionName = rdr["DivisionName"].ToString();
                        getter.DivisionID = Convert.ToInt32(rdr["DivisionID"]);
                        getter.DepartmentName = rdr["DepartmentName"].ToString();
                        getter.DepartmentID = Convert.ToInt32(rdr["DepartmentID"]);
                        getter.SectionName = rdr["SectionName"].ToString();
                        getter.SectionID = Convert.ToInt32(rdr["SectionID"]);
                        getter.IsSubmitted = Convert.ToBoolean(rdr["IsSubmitted"].ToString());
                        getter.Approved = rdr["Approved"].ToString();
                        getter.ForApproval = rdr["ForApproval"].ToString();
                        getter.Rejected = rdr["Rejected"].ToString();
                        getter.RevisionNo = rdr["RevisionNo"].ToString();
                        list.Add(getter);
                    }
                }

                int totalCount = Convert.ToInt32(cmdSql.Parameters["@RecordCount"].Value);
                conn.Close();

                int? totalrows = totalCount;
                int? totalrowsafterfiltering = totalCount;


                return Json(new { data = list, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult SaveWorkOrderRequestData(WO_RequestorInformation WorkOrderRequestData)
        {
            bool error = false;
            int RetRequestorInformationID = 0;
            string RetEndMsg = "";
            string path = Server.MapPath("~/Areas/WorkOrderRequest/Uploads/Request/Drawing/");
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
                {
                    conn.Open();
                    SqlTransaction transaction;
                    transaction = conn.BeginTransaction("RequestTRX");
                    #region Saving Header
                    using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                    {
                        cmdSqlHeader.Connection = conn;
                        cmdSqlHeader.Transaction = transaction;
                        cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                        cmdSqlHeader.CommandText = "WorkOrderRequest_InsertUpdateHeader";

                        cmdSqlHeader.Parameters.Clear();
                        cmdSqlHeader.Parameters.AddWithValue("@ID", WorkOrderRequestData.ID);
                        cmdSqlHeader.Parameters.AddWithValue("@DateRequest", WorkOrderRequestData.DateRequest == null ? "" : WorkOrderRequestData.DateRequest);
                        cmdSqlHeader.Parameters.AddWithValue("@WorkOrderTitle", WorkOrderRequestData.WorkOrderTitle == null ? "" : WorkOrderRequestData.WorkOrderTitle);
                        cmdSqlHeader.Parameters.AddWithValue("@RequestorEmployeeNo", WorkOrderRequestData.RequestorEmployeeNo == null ? "" : WorkOrderRequestData.RequestorEmployeeNo);
                        cmdSqlHeader.Parameters.AddWithValue("@DivisionID", WorkOrderRequestData.DivisionID);
                        cmdSqlHeader.Parameters.AddWithValue("@DepartmentID", WorkOrderRequestData.DepartmentID);
                        cmdSqlHeader.Parameters.AddWithValue("@SectionID", WorkOrderRequestData.SectionID);
                        cmdSqlHeader.Parameters.AddWithValue("@ForApproval", WorkOrderRequestData.ForApproval == null ? "0" : WorkOrderRequestData.ForApproval);
                        cmdSqlHeader.Parameters.AddWithValue("@IsSubmitted", WorkOrderRequestData.IsSubmitted);

                        cmdSqlHeader.Parameters.AddWithValue("@BuildingID", WorkOrderRequestData.WO_WorkDescriptionDetailsData.BuildingID);
                        cmdSqlHeader.Parameters.AddWithValue("@BuildingOther", WorkOrderRequestData.WO_WorkDescriptionDetailsData.BuildingOther == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.BuildingOther);
                        cmdSqlHeader.Parameters.AddWithValue("@FloorID", WorkOrderRequestData.WO_WorkDescriptionDetailsData.FloorID);
                        cmdSqlHeader.Parameters.AddWithValue("@FloorOther", WorkOrderRequestData.WO_WorkDescriptionDetailsData.FloorOther == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.FloorOther);
                        cmdSqlHeader.Parameters.AddWithValue("@ProcessAreaID", WorkOrderRequestData.WO_WorkDescriptionDetailsData.ProcessAreaID);
                        cmdSqlHeader.Parameters.AddWithValue("@ProcessAreaOther", WorkOrderRequestData.WO_WorkDescriptionDetailsData.ProcessAreaOther == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.ProcessAreaOther);
                        cmdSqlHeader.Parameters.AddWithValue("@Details", WorkOrderRequestData.WO_WorkDescriptionDetailsData.Details == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.Details);

                        cmdSqlHeader.Parameters.AddWithValue("@ReceivedBy", WorkOrderRequestData.WO_FacilitiesData.ReceivedBy == null ? "" : WorkOrderRequestData.WO_FacilitiesData.ReceivedBy);
                        cmdSqlHeader.Parameters.AddWithValue("@ReceivedDate", WorkOrderRequestData.WO_FacilitiesData.ReceivedDate == null ? "" : WorkOrderRequestData.WO_FacilitiesData.ReceivedDate);
                        cmdSqlHeader.Parameters.AddWithValue("@WorkCategory", WorkOrderRequestData.WO_FacilitiesData.WorkCategory == null ? "" : WorkOrderRequestData.WO_FacilitiesData.WorkCategory);
                        cmdSqlHeader.Parameters.AddWithValue("@RequestAssigned", WorkOrderRequestData.WO_FacilitiesData.RequestAssigned == null ? "" : WorkOrderRequestData.WO_FacilitiesData.RequestAssigned);
                        cmdSqlHeader.Parameters.AddWithValue("@Notes", WorkOrderRequestData.WO_FacilitiesData.Notes == null ? "" : WorkOrderRequestData.WO_FacilitiesData.Notes);
                        cmdSqlHeader.Parameters.AddWithValue("@PriorityLevel", WorkOrderRequestData.WO_FacilitiesData.PriorityLevel);
                        cmdSqlHeader.Parameters.AddWithValue("@DeadlineDate", WorkOrderRequestData.WO_FacilitiesData.DeadlineDate == null ? "" : WorkOrderRequestData.WO_FacilitiesData.DeadlineDate);

                        cmdSqlHeader.Parameters.AddWithValue("@CreateID", user.EmployeeNo);

                        SqlParameter ErrorMessage = cmdSqlHeader.Parameters.Add("@ErrorMessage", SqlDbType.VarChar, 200);
                        SqlParameter EndMsg = cmdSqlHeader.Parameters.Add("@EndMsg", SqlDbType.VarChar, 200);
                        SqlParameter Error = cmdSqlHeader.Parameters.Add("@Error", SqlDbType.Bit);
                        SqlParameter HeaderID = cmdSqlHeader.Parameters.Add("@HeaderID", SqlDbType.VarChar, 20);

                        Error.Direction = ParameterDirection.Output;
                        ErrorMessage.Direction = ParameterDirection.Output;
                        EndMsg.Direction = ParameterDirection.Output;
                        HeaderID.Direction = ParameterDirection.Output;
                        cmdSqlHeader.CommandTimeout = 0;
                        cmdSqlHeader.ExecuteNonQuery();

                        error = Convert.ToBoolean(Error.Value);
                        if (error)
                        {
                            throw new System.InvalidOperationException(ErrorMessage.Value.ToString());
                        }
                        else
                        {
                            RetRequestorInformationID = Convert.ToInt32(HeaderID.Value);
                            RetEndMsg = EndMsg.Value.ToString();
                        }
                    }
                    #endregion
                    #region Work Description Details
                    //using (SqlCommand cmdSqlWorkDescriptionDetails = conn.CreateCommand())
                    //{
                    //    cmdSqlWorkDescriptionDetails.Connection = conn;
                    //    cmdSqlWorkDescriptionDetails.Transaction = transaction;
                    //    cmdSqlWorkDescriptionDetails.CommandType = CommandType.StoredProcedure;
                    //    cmdSqlWorkDescriptionDetails.CommandText = "WorkOrderRequest_InsertWorkDescriptionDetails";

                    //    cmdSqlWorkDescriptionDetails.Parameters.Clear();
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@RequestorInformationID", RetRequestorInformationID);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@BuildingID", WorkOrderRequestData.WO_WorkDescriptionDetailsData.BuildingID);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@BuildingOther", WorkOrderRequestData.WO_WorkDescriptionDetailsData.BuildingOther == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.BuildingOther);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@FloorID", WorkOrderRequestData.WO_WorkDescriptionDetailsData.FloorID);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@FloorOther", WorkOrderRequestData.WO_WorkDescriptionDetailsData.FloorOther == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.FloorOther);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@ProcessAreaID", WorkOrderRequestData.WO_WorkDescriptionDetailsData.ProcessAreaID);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@ProcessAreaOther", WorkOrderRequestData.WO_WorkDescriptionDetailsData.ProcessAreaOther == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.ProcessAreaOther);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@Details", WorkOrderRequestData.WO_WorkDescriptionDetailsData.Details == null ? "" : WorkOrderRequestData.WO_WorkDescriptionDetailsData.Details);
                    //    cmdSqlWorkDescriptionDetails.Parameters.AddWithValue("@CreateID", user.EmployeeNo);
                    //    cmdSqlWorkDescriptionDetails.CommandTimeout = 0;
                    //    cmdSqlWorkDescriptionDetails.ExecuteNonQuery();
                    //}
                    if (WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_WorkRequestList != null)
                    {
                        if (WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_WorkRequestList.Count > 0)
                        {
                            using (SqlCommand cmdSqlWO_WorkRequest = conn.CreateCommand())
                            {
                                cmdSqlWO_WorkRequest.Transaction = transaction;
                                cmdSqlWO_WorkRequest.CommandType = CommandType.Text;
                                cmdSqlWO_WorkRequest.CommandText = "DELETE FROM  [WO_WorkRequest] WHERE IsDeleted = 0 AND RequestorInformationID='" + RetRequestorInformationID + "'";
                                cmdSqlWO_WorkRequest.CommandTimeout = 0;
                                cmdSqlWO_WorkRequest.ExecuteNonQuery();
                            }
                            DataTable table = WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_WorkRequestList.AsDataTable();
                            table = dataHelper.UpdateCreateCols(table, user.EmployeeNo.ToString(), user.EmployeeNo.ToString());
                            table = dataHelper.UpdateColumnID(table, "RequestorInformationID", RetRequestorInformationID);
                            table = dataHelper.UpdateDates(table);
                            using (SqlBulkCopy sqlbcWO_WorkRequest = new SqlBulkCopy(conn, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction))
                            {
                                sqlbcWO_WorkRequest.DestinationTableName = "WO_WorkRequest";

                                sqlbcWO_WorkRequest.ColumnMappings.Add("RequestorInformationID", "RequestorInformationID");
                                sqlbcWO_WorkRequest.ColumnMappings.Add("WorkRequestID", "WorkRequestID");
                                sqlbcWO_WorkRequest.ColumnMappings.Add("WorkRequestOther", "WorkRequestOther");
                                sqlbcWO_WorkRequest.ColumnMappings.Add("CreateID", "CreateID");
                                sqlbcWO_WorkRequest.ColumnMappings.Add("UpdateID", "UpdateID");

                                sqlbcWO_WorkRequest.WriteToServer(table);
                            }
                        }
                    }
                    if (WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_WorkClassificationList != null)
                    {
                        if (WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_WorkClassificationList.Count > 0)
                        {
                            using (SqlCommand cmdSqlWO_WorkRequestClassification = conn.CreateCommand())
                            {
                                cmdSqlWO_WorkRequestClassification.Transaction = transaction;
                                cmdSqlWO_WorkRequestClassification.CommandType = CommandType.Text;
                                cmdSqlWO_WorkRequestClassification.CommandText = "DELETE FROM  [WO_WorkClassification] WHERE IsDeleted = 0 AND RequestorInformationID='" + RetRequestorInformationID + "'";
                                cmdSqlWO_WorkRequestClassification.CommandTimeout = 0;
                                cmdSqlWO_WorkRequestClassification.ExecuteNonQuery();
                            }
                            DataTable table = WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_WorkClassificationList.AsDataTable();
                            table = dataHelper.UpdateCreateCols(table, user.EmployeeNo.ToString(), user.EmployeeNo.ToString());
                            table = dataHelper.UpdateColumnID(table, "RequestorInformationID", RetRequestorInformationID);
                            table = dataHelper.UpdateDates(table);
                            using (SqlBulkCopy sqlbcWO_WorkRequestClassification = new SqlBulkCopy(conn, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction))
                            {
                                sqlbcWO_WorkRequestClassification.DestinationTableName = "WO_WorkClassification";

                                sqlbcWO_WorkRequestClassification.ColumnMappings.Add("RequestorInformationID", "RequestorInformationID");
                                sqlbcWO_WorkRequestClassification.ColumnMappings.Add("WorkClassificationID", "WorkClassificationID");
                                sqlbcWO_WorkRequestClassification.ColumnMappings.Add("WorkClassificationOther", "WorkClassificationOther");

                                sqlbcWO_WorkRequestClassification.ColumnMappings.Add("CreateID", "CreateID");
                                sqlbcWO_WorkRequestClassification.ColumnMappings.Add("UpdateID", "UpdateID");
                                sqlbcWO_WorkRequestClassification.WriteToServer(table);
                            }
                        }
                    }
                    if (WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_DrawingList != null)
                    {
                        if (WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_DrawingList.Count > 0)
                        {
                            using (SqlCommand cmdSqlWO_Drawing = conn.CreateCommand())
                            {
                                cmdSqlWO_Drawing.Transaction = transaction;
                                cmdSqlWO_Drawing.CommandType = CommandType.Text;
                                cmdSqlWO_Drawing.CommandText = "DELETE FROM  [WO_Drawing] WHERE IsDeleted = 0 AND RequestorInformationID='" + RetRequestorInformationID + "'";
                                cmdSqlWO_Drawing.CommandTimeout = 0;
                                cmdSqlWO_Drawing.ExecuteNonQuery();
                            }
                            DataTable table = WorkOrderRequestData.WO_WorkDescriptionDetailsData.WO_DrawingList.AsDataTable();
                            table = dataHelper.UpdateCreateCols(table, user.EmployeeNo.ToString(), user.EmployeeNo.ToString());
                            table = dataHelper.UpdateColumnID(table, "RequestorInformationID", RetRequestorInformationID);
                            table = dataHelper.UpdateDates(table);
                            using (SqlBulkCopy sqlbcWO_Drawing = new SqlBulkCopy(conn, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction))
                            {
                                sqlbcWO_Drawing.DestinationTableName = "WO_Drawing";

                                sqlbcWO_Drawing.ColumnMappings.Add("RequestorInformationID", "RequestorInformationID");
                                sqlbcWO_Drawing.ColumnMappings.Add("FileName", "FileName");

                                sqlbcWO_Drawing.ColumnMappings.Add("CreateID", "CreateID");
                                sqlbcWO_Drawing.ColumnMappings.Add("UpdateID", "UpdateID");
                                sqlbcWO_Drawing.WriteToServer(table);
                            }
                        }
                    }
                    #endregion
                    #region Facilities
                    //using (SqlCommand cmdSqlInsertFacilities = conn.CreateCommand())
                    //{
                    //    cmdSqlInsertFacilities.Connection = conn;
                    //    cmdSqlInsertFacilities.Transaction = transaction;
                    //    cmdSqlInsertFacilities.CommandType = CommandType.StoredProcedure;
                    //    cmdSqlInsertFacilities.CommandText = "WorkOrderRequest_InsertFacilities";

                    //    cmdSqlInsertFacilities.Parameters.Clear();
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@RequestorInformationID", RetRequestorInformationID);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@ReceivedBy", WorkOrderRequestData.WO_FacilitiesData.ReceivedBy == null ? "" : WorkOrderRequestData.WO_FacilitiesData.ReceivedBy);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@ReceivedDate", WorkOrderRequestData.WO_FacilitiesData.ReceivedDate == null ? "" : WorkOrderRequestData.WO_FacilitiesData.ReceivedDate);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@WorkCategory", WorkOrderRequestData.WO_FacilitiesData.WorkCategory == null ? "" : WorkOrderRequestData.WO_FacilitiesData.WorkCategory);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@RequestAssigned", WorkOrderRequestData.WO_FacilitiesData.RequestAssigned == null ? "" : WorkOrderRequestData.WO_FacilitiesData.RequestAssigned);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@Notes", WorkOrderRequestData.WO_FacilitiesData.Notes == null ? "" : WorkOrderRequestData.WO_FacilitiesData.Notes);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@PriorityLevel", WorkOrderRequestData.WO_FacilitiesData.PriorityLevel);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@DeadlineDate", WorkOrderRequestData.WO_FacilitiesData.DeadlineDate == null ? "" : WorkOrderRequestData.WO_FacilitiesData.DeadlineDate);
                    //    cmdSqlInsertFacilities.Parameters.AddWithValue("@CreateID", user.EmployeeNo);
                    //    cmdSqlInsertFacilities.CommandTimeout = 0;
                    //    cmdSqlInsertFacilities.ExecuteNonQuery();
                    //}
                    if (WorkOrderRequestData.WO_FacilitiesData.WO_CriteriaDetailsPointsList != null)
                    {
                        if (WorkOrderRequestData.WO_FacilitiesData.WO_CriteriaDetailsPointsList.Count > 0)
                        {
                            using (SqlCommand cmdSqlWO_CriteriaDetailsPoints = conn.CreateCommand())
                            {
                                cmdSqlWO_CriteriaDetailsPoints.Transaction = transaction;
                                cmdSqlWO_CriteriaDetailsPoints.CommandType = CommandType.Text;
                                cmdSqlWO_CriteriaDetailsPoints.CommandText = "DELETE FROM  [WO_CriteriaDetailsPoints] WHERE RequestorInformationID='" + RetRequestorInformationID + "'";
                                cmdSqlWO_CriteriaDetailsPoints.CommandTimeout = 0;
                                cmdSqlWO_CriteriaDetailsPoints.ExecuteNonQuery();
                            }
                            DataTable table = WorkOrderRequestData.WO_FacilitiesData.WO_CriteriaDetailsPointsList.AsDataTable();
                            table = dataHelper.UpdateCreateCols(table, user.EmployeeNo.ToString(), user.EmployeeNo.ToString());
                            table = dataHelper.UpdateColumnID(table, "RequestorInformationID", RetRequestorInformationID);
                            table = dataHelper.UpdateDates(table);
                            using (SqlBulkCopy sqlbcWO_CriteriaDetailsPoints = new SqlBulkCopy(conn, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction))
                            {
                                sqlbcWO_CriteriaDetailsPoints.DestinationTableName = "WO_CriteriaDetailsPoints";

                                sqlbcWO_CriteriaDetailsPoints.ColumnMappings.Add("RequestorInformationID", "RequestorInformationID");
                                sqlbcWO_CriteriaDetailsPoints.ColumnMappings.Add("CriteriaDetailsID", "CriteriaDetailsID");
                                sqlbcWO_CriteriaDetailsPoints.ColumnMappings.Add("CriteriaDetailsPoints", "CriteriaDetailsPoints");
                                sqlbcWO_CriteriaDetailsPoints.ColumnMappings.Add("CreateID", "CreateID");
                                sqlbcWO_CriteriaDetailsPoints.WriteToServer(table);
                            }
                        }
                    }
                    if (WorkOrderRequestData.WO_FacilitiesData.WO_CriteriaHeaderRemarksList != null)
                    {
                        if (WorkOrderRequestData.WO_FacilitiesData.WO_CriteriaHeaderRemarksList.Count > 0)
                        {
                            using (SqlCommand cmdSqlWO_CriteriaHeaderRemarks = conn.CreateCommand())
                            {
                                cmdSqlWO_CriteriaHeaderRemarks.Transaction = transaction;
                                cmdSqlWO_CriteriaHeaderRemarks.CommandType = CommandType.Text;
                                cmdSqlWO_CriteriaHeaderRemarks.CommandText = "DELETE FROM  [WO_CriteriaHeaderRemarks] WHERE  RequestorInformationID='" + RetRequestorInformationID + "'";
                                cmdSqlWO_CriteriaHeaderRemarks.CommandTimeout = 0;
                                cmdSqlWO_CriteriaHeaderRemarks.ExecuteNonQuery();
                            }
                            DataTable table = WorkOrderRequestData.WO_FacilitiesData.WO_CriteriaHeaderRemarksList.AsDataTable();
                            table = dataHelper.UpdateCreateCols(table, user.EmployeeNo.ToString(), user.EmployeeNo.ToString());
                            table = dataHelper.UpdateColumnID(table, "RequestorInformationID", RetRequestorInformationID);
                            table = dataHelper.UpdateDates(table);
                            using (SqlBulkCopy sqlbcWO_CriteriaHeaderRemarks = new SqlBulkCopy(conn, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction))
                            {
                                sqlbcWO_CriteriaHeaderRemarks.DestinationTableName = "WO_CriteriaHeaderRemarks";

                                sqlbcWO_CriteriaHeaderRemarks.ColumnMappings.Add("RequestorInformationID", "RequestorInformationID");
                                sqlbcWO_CriteriaHeaderRemarks.ColumnMappings.Add("CriteriaHeaderID", "CriteriaHeaderID");
                                sqlbcWO_CriteriaHeaderRemarks.ColumnMappings.Add("CriteriaPoints", "CriteriaPoints");
                                sqlbcWO_CriteriaHeaderRemarks.ColumnMappings.Add("Remarks", "Remarks");

                                sqlbcWO_CriteriaHeaderRemarks.ColumnMappings.Add("CreateID", "CreateID");
                                sqlbcWO_CriteriaHeaderRemarks.ColumnMappings.Add("UpdateID", "UpdateID");
                                sqlbcWO_CriteriaHeaderRemarks.WriteToServer(table);
                            }
                        }
                    }
                    #endregion
                    #region In-House/Contractor

                    if (WorkOrderRequestData.WO_ContractorDetailsDataList != null)
                    {
                        if (WorkOrderRequestData.WO_ContractorDetailsDataList.Count > 0)
                        {
                            using (SqlCommand cmdSqlWO_ContractorDetails = conn.CreateCommand())
                            {
                                cmdSqlWO_ContractorDetails.Transaction = transaction;
                                cmdSqlWO_ContractorDetails.CommandType = CommandType.Text;
                                cmdSqlWO_ContractorDetails.CommandText = "DELETE FROM  [WO_ContractorDetails] WHERE  RequestorInformationID='" + RetRequestorInformationID + "'";
                                cmdSqlWO_ContractorDetails.CommandTimeout = 0;
                                cmdSqlWO_ContractorDetails.ExecuteNonQuery();
                            }
                            DataTable table = WorkOrderRequestData.WO_ContractorDetailsDataList.AsDataTable();
                            table = dataHelper.UpdateCreateCols(table, user.EmployeeNo.ToString(), user.EmployeeNo.ToString());
                            table = dataHelper.UpdateColumnID(table, "RequestorInformationID", RetRequestorInformationID);
                            table = dataHelper.UpdateDates(table);
                            using (SqlBulkCopy sqlbcWO_ContractorDetails = new SqlBulkCopy(conn, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints, transaction))
                            {
                                sqlbcWO_ContractorDetails.DestinationTableName = "WO_ContractorDetails";

                                sqlbcWO_ContractorDetails.ColumnMappings.Add("RequestorInformationID", "RequestorInformationID");
                                sqlbcWO_ContractorDetails.ColumnMappings.Add("ID", "SupplierID");
                                sqlbcWO_ContractorDetails.ColumnMappings.Add("DateRequested", "DateRequested");
                                sqlbcWO_ContractorDetails.ColumnMappings.Add("SurveryDateTime", "SurveryDateTime");
                                sqlbcWO_ContractorDetails.ColumnMappings.Add("QuotationSubmission", "QuotationSubmission");
                                sqlbcWO_ContractorDetails.ColumnMappings.Add("CreateID", "CreateID");
                                sqlbcWO_ContractorDetails.ColumnMappings.Add("UpdateID", "UpdateID");
                                sqlbcWO_ContractorDetails.WriteToServer(table);
                            }
                        }
                    }
                    #endregion
                    transaction.Commit();
                    conn.Close();
                }
            }
            catch (Exception err)
            {
                string errmsg;
                if (err.InnerException != null)
                    errmsg = "Error: " + err.InnerException.ToString();
                else
                    errmsg = "Error: " + err.Message.ToString();

                return Json(new
                {
                    success = false,
                    errors = errmsg
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, msg = "Work Order Request was successfully " + RetEndMsg });
        }
        public ActionResult ValidateDrawingFiles(string FileNames)
        {
            bool hasError = false;
            string[] errFiles = new String[(FileNames.Split(',').Length)];
            var arrFileNames = FileNames.Split(',');

            try
            {
                for (int x = 0; x < arrFileNames.Length; x++)
                {
                    if (arrFileNames[x] != null)
                    {
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Areas/WorkOrderRequest/Uploads/Request/Drawing/") + arrFileNames[x]);
                        if (System.IO.File.Exists(ServerSavePath))
                        {
                            if (arrFileNames[x].Trim() != "")
                            {
                                hasError = true;
                                errFiles[x] = arrFileNames[x];
                            }
                            //throw new InvalidOperationException("The File \"" + InputFileName + "\" already exists. Please select a different file or rename the file.");
                        }
                    }
                }
            }
            catch (Exception err)
            {
                string errmsg;
                if (err.InnerException != null)
                    errmsg = "Error: " + err.InnerException.ToString();
                else
                    errmsg = "Error: " + err.Message.ToString();

                return Json(new { success = false, errors = errmsg }, JsonRequestBehavior.AllowGet);
            }
            if (hasError)
                return Json(new { success = false, errors = "The File/s " + String.Join(", ", errFiles) + " already exists. Please select a different file or rename the file." });
            else
            {
                return Json(new { success = true, data = new { valid = true } });
            }
        }
        public ActionResult GetRequestDetails(int ID)
        {
            WO_RequestorInformation WorkOrderRequestData = new WO_RequestorInformation();
            int DivisionID = 0;
            int DepartmentID = 0;
            bool hasError = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
                {
                    conn.Open();
                    #region Get Header
                    using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                    {
                        cmdSqlHeader.Connection = conn;
                        cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                        cmdSqlHeader.CommandText = "WorkOrderRequest_GetRequestHeader";

                        cmdSqlHeader.Parameters.Clear();
                        cmdSqlHeader.Parameters.AddWithValue("@ID", ID);
                        cmdSqlHeader.ExecuteNonQuery();
                        using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                WorkOrderRequestData.ID = Convert.ToInt32(rdr["ID"].ToString());
                                WorkOrderRequestData.DateRequest = rdr["DateRequest"].ToString();
                                WorkOrderRequestData.WorkOrderTitle = rdr["WorkOrderTitle"].ToString();
                                WorkOrderRequestData.RequestorEmployeeNo = rdr["RequestorEmployeeNo"].ToString();
                                WorkOrderRequestData.EmployeeName = rdr["EmployeeName"].ToString();
                                WorkOrderRequestData.MobileNo = rdr["MobileNo"].ToString();
                                WorkOrderRequestData.LocalNo = rdr["LocalNo"].ToString();
                                WorkOrderRequestData.Email = rdr["Email"].ToString();
                                WorkOrderRequestData.DivisionID = Convert.ToInt32(rdr["DivisionID"].ToString());
                                WorkOrderRequestData.DepartmentID = Convert.ToInt32(rdr["DepartmentID"].ToString());
                                WorkOrderRequestData.SectionID = Convert.ToInt32(rdr["SectionID"].ToString());
                                WorkOrderRequestData.ReferenceNo = rdr["ReferenceNo"].ToString();
                                WorkOrderRequestData.WorkOrderNo = rdr["WorkOrderNo"].ToString();
                                WorkOrderRequestData.RevisionNo = rdr["RevisionNo"].ToString();
                                WorkOrderRequestData.IsSubmitted = Convert.ToBoolean(rdr["IsSubmitted"].ToString());
                                WorkOrderRequestData.Approved = rdr["Approved"].ToString();
                                WorkOrderRequestData.ForApproval = rdr["ForApproval"].ToString();
                                WorkOrderRequestData.Rejected = rdr["Rejected"].ToString();
                                WorkOrderRequestData.RevisionNo = rdr["RevisionNo"].ToString();

                                WO_WorkDescriptionDetails WO_WorkDescriptionDetailsData = new WO_WorkDescriptionDetails();
                                WO_WorkDescriptionDetailsData.BuildingID = Convert.ToInt32(rdr["BuildingID"].ToString());
                                WO_WorkDescriptionDetailsData.BuildingOther = rdr["BuildingOther"].ToString();
                                WO_WorkDescriptionDetailsData.FloorID = Convert.ToInt32(rdr["FloorID"].ToString());
                                WO_WorkDescriptionDetailsData.FloorOther = rdr["FloorOther"].ToString();
                                WO_WorkDescriptionDetailsData.ProcessAreaID = Convert.ToInt32(rdr["ProcessAreaID"].ToString());
                                WO_WorkDescriptionDetailsData.ProcessAreaOther = rdr["ProcessAreaOther"].ToString();
                                WO_WorkDescriptionDetailsData.Details = rdr["Details"].ToString();
                                WO_WorkDescriptionDetailsData.WO_DrawingList = GetWO_DrawingList(ID);
                                WO_WorkDescriptionDetailsData.WO_WorkRequestList = GetWO_WorkRequestList(ID);
                                WO_WorkDescriptionDetailsData.WO_WorkClassificationList = GetWO_WorkClassificationList(ID);
                                WorkOrderRequestData.WO_WorkDescriptionDetailsData = WO_WorkDescriptionDetailsData;

                                WO_Facilities WO_FacilitiesData = new WO_Facilities();
                                WO_FacilitiesData.ReceivedBy = rdr["ReceivedBy"].ToString();
                                WO_FacilitiesData.ReceivedDate = rdr["ReceivedDate"].ToString();
                                WO_FacilitiesData.WorkCategory = rdr["WorkCategory"].ToString();
                                WO_FacilitiesData.RequestAssigned = rdr["RequestAssigned"].ToString();
                                WO_FacilitiesData.Notes = rdr["Notes"].ToString();
                                WO_FacilitiesData.PriorityLevel = Convert.ToInt32(rdr["PriorityLevel"].ToString());
                                WO_FacilitiesData.DeadlineDate = rdr["DeadlineDate"].ToString();
                                WO_FacilitiesData.WO_CriteriaDetailsPointsList = GetWO_CriteriaDetailsPointsList(ID);
                                WO_FacilitiesData.WO_CriteriaHeaderRemarksList = GetWO_CriteriaHeaderRemarksList(ID);
                                WorkOrderRequestData.WO_FacilitiesData = WO_FacilitiesData;

                                WorkOrderRequestData.WO_ContractorDetailsDataList = GetWO_ContractorDetailsDataList(ID, WO_FacilitiesData.WorkCategory);

                            }
                        }

                    }
                    #endregion
                    if (WorkOrderRequestData.WO_FacilitiesData.WorkCategory == "In-House")
                    {
                        if (WorkOrderRequestData.WO_ContractorDetailsDataList.Count > 0)
                        {
                            using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                            {
                                cmdSqlHeader.Connection = conn;
                                cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                                cmdSqlHeader.CommandText = "WorkOrderRequest_GetWO_ContractorDetailsListFacilitiesFilter";

                                cmdSqlHeader.Parameters.Clear();
                                cmdSqlHeader.Parameters.AddWithValue("@SectionID", WorkOrderRequestData.WO_ContractorDetailsDataList[0].SectionID);
                                cmdSqlHeader.ExecuteNonQuery();
                                using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                                {
                                    if (rdr.Read())
                                    {
                                        DepartmentID = Convert.ToInt32(rdr["DepartmentID"]);
                                        DivisionID = Convert.ToInt32(rdr["DivisionID"]);
                                    }
                                }

                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception err)
            {
                string errmsg;
                if (err.InnerException != null)
                    errmsg = "Error: " + err.InnerException.ToString();
                else
                    errmsg = "Error: " + err.Message.ToString();

                return Json(new { success = false, errors = errmsg }, JsonRequestBehavior.AllowGet);
            }
            if (hasError)
                return Json(new { success = false, errors = "" });
            else
            {
                return Json(new { success = true, WorkOrderRequestData = WorkOrderRequestData, DepartmentID, DivisionID });
            }
        }
        public List<WO_Drawing> GetWO_DrawingList(int ID)
        {
            List<WO_Drawing> WO_DrawingList = new List<WO_Drawing>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
            {
                conn.Open();
                using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                {
                    cmdSqlHeader.Connection = conn;
                    cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                    cmdSqlHeader.CommandText = "WorkOrderRequest_GetWO_DrawingList";

                    cmdSqlHeader.Parameters.Clear();
                    cmdSqlHeader.Parameters.AddWithValue("@ID", ID);
                    cmdSqlHeader.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WO_DrawingList.Add(new WO_Drawing
                            {
                                FileName = rdr["FileName"].ToString(),
                            });
                        }
                    }

                }
                conn.Close();
            }
            return WO_DrawingList;
        }
        public List<WO_WorkRequest> GetWO_WorkRequestList(int ID)
        {
            List<WO_WorkRequest> WO_WorkRequestList = new List<WO_WorkRequest>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
            {
                conn.Open();
                using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                {
                    cmdSqlHeader.Connection = conn;
                    cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                    cmdSqlHeader.CommandText = "WorkOrderRequest_GetWO_WorkRequestList";

                    cmdSqlHeader.Parameters.Clear();
                    cmdSqlHeader.Parameters.AddWithValue("@ID", ID);
                    cmdSqlHeader.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WO_WorkRequestList.Add(new WO_WorkRequest
                            {
                                WorkRequestID = Convert.ToInt32(rdr["WorkRequestID"].ToString()),
                                WorkRequestOther = rdr["WorkRequestOther"].ToString(),
                            });
                        }
                    }

                }
                conn.Close();
            }
            return WO_WorkRequestList;
        }
        public List<WO_WorkClassification> GetWO_WorkClassificationList(int ID)
        {
            List<WO_WorkClassification> WO_WorkClassificationList = new List<WO_WorkClassification>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
            {
                conn.Open();
                using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                {
                    cmdSqlHeader.Connection = conn;
                    cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                    cmdSqlHeader.CommandText = "WorkOrderRequest_GetWO_WorkClassificationList";

                    cmdSqlHeader.Parameters.Clear();
                    cmdSqlHeader.Parameters.AddWithValue("@ID", ID);
                    cmdSqlHeader.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WO_WorkClassificationList.Add(new WO_WorkClassification
                            {
                                WorkClassificationID = Convert.ToInt32(rdr["WorkClassificationID"].ToString()),
                                WorkClassificationOther = rdr["WorkClassificationOther"].ToString(),
                            });
                        }
                    }

                }
                conn.Close();
            }
            return WO_WorkClassificationList;
        }
        public List<WO_CriteriaDetailsPoints> GetWO_CriteriaDetailsPointsList(int ID)
        {
            List<WO_CriteriaDetailsPoints> WO_CriteriaDetailsPointsList = new List<WO_CriteriaDetailsPoints>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
            {
                conn.Open();
                using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                {
                    cmdSqlHeader.Connection = conn;
                    cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                    cmdSqlHeader.CommandText = "WorkOrderRequest_GetWO_CriteriaDetailsPointsList";

                    cmdSqlHeader.Parameters.Clear();
                    cmdSqlHeader.Parameters.AddWithValue("@ID", ID);
                    cmdSqlHeader.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WO_CriteriaDetailsPointsList.Add(new WO_CriteriaDetailsPoints
                            {
                                CriteriaDetailsID = Convert.ToInt32(rdr["CriteriaDetailsID"].ToString()),
                                CriteriaDetailsPoints = Convert.ToInt32(rdr["CriteriaDetailsPoints"].ToString()),
                            });
                        }
                    }

                }
                conn.Close();
            }
            return WO_CriteriaDetailsPointsList;
        }
        public List<WO_CriteriaHeaderRemarks> GetWO_CriteriaHeaderRemarksList(int ID)
        {
            List<WO_CriteriaHeaderRemarks> WO_CriteriaHeaderRemarksList = new List<WO_CriteriaHeaderRemarks>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
            {
                conn.Open();
                using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                {
                    cmdSqlHeader.Connection = conn;
                    cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                    cmdSqlHeader.CommandText = "WorkOrderRequest_GetWO_CriteriaHeaderRemarksList";

                    cmdSqlHeader.Parameters.Clear();
                    cmdSqlHeader.Parameters.AddWithValue("@ID", ID);
                    cmdSqlHeader.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WO_CriteriaHeaderRemarksList.Add(new WO_CriteriaHeaderRemarks
                            {
                                CriteriaHeaderID = Convert.ToInt32(rdr["CriteriaHeaderID"].ToString()),
                                CriteriaPoints = Convert.ToInt32(rdr["CriteriaPoints"].ToString()),
                                Remarks = rdr["Remarks"].ToString(),
                            });
                        }
                    }

                }
                conn.Close();
            }
            return WO_CriteriaHeaderRemarksList;
        }
        public List<WO_ContractorDetails> GetWO_ContractorDetailsDataList(int ID, string WorkCategory)
        {
            List<WO_ContractorDetails> WO_ContractorDetailsList = new List<WO_ContractorDetails>();
            using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
            {
                conn.Open();
                using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                {
                    cmdSqlHeader.Connection = conn;
                    cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                    cmdSqlHeader.CommandText = "WorkOrderRequest_GetWO_ContractorDetailsList";

                    cmdSqlHeader.Parameters.Clear();
                    cmdSqlHeader.Parameters.AddWithValue("@ID", ID);
                    cmdSqlHeader.Parameters.AddWithValue("@WorkCategory", WorkCategory);
                    cmdSqlHeader.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            WO_ContractorDetailsList.Add(new WO_ContractorDetails
                            {
                                ID = Convert.ToInt32(rdr["SupplierID"].ToString()),
                                SectionID = Convert.ToInt32(rdr["SectionID"].ToString()),
                                SupplierID = Convert.ToInt32(rdr["SupplierID"].ToString()),
                                DateRequested = rdr["DateRequested"].ToString(),
                                EmpName = rdr["EmpName"].ToString(),
                                SectionName = rdr["SectionName"].ToString(),
                                SupplierName = rdr["SupplierName"].ToString(),
                                SupplierAddress = rdr["SupplierAddress"].ToString(),
                                SurveryDateTime = rdr["SurveryDateTime"].ToString(),
                                QuotationSubmission = rdr["QuotationSubmission"].ToString(),
                            });
                        }
                    }

                }
                conn.Close();
            }
            return WO_ContractorDetailsList;
        }
        public ActionResult DeleteDrawingFiles(int ID, string Files)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
                {
                    conn.Open();
                    using (SqlCommand cmdSql = conn.CreateCommand())
                    {
                        cmdSql.CommandType = CommandType.StoredProcedure;
                        cmdSql.CommandText = "WorkOrderRequest_DeleteDrawingFiles";
                        cmdSql.Parameters.Clear();
                        cmdSql.Parameters.AddWithValue("@ID", ID);
                        cmdSql.ExecuteNonQuery();

                        string[] arrFields = Files.Split(',');
                        foreach (string Filename in arrFields)
                        {
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Areas/WorkOrderRequest/Uploads/Request/Drawing/") + Filename.Trim());
                            if (System.IO.File.Exists(ServerSavePath))
                            {
                                System.IO.File.Delete(ServerSavePath);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception err)
            {
                string errmsg;
                if (err.InnerException != null)
                    errmsg = "Error: " + err.InnerException.ToString();
                else
                    errmsg = "Error: " + err.Message.ToString();

                return Json(new { success = false, errors = errmsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, msg = "Files were deleted successfully." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteRequest(int ID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
                {
                    conn.Open();
                    using (SqlCommand cmdSql = conn.CreateCommand())
                    {
                        cmdSql.CommandType = CommandType.StoredProcedure;
                        cmdSql.CommandText = "WorkOrderRequest_DeleteRequest";
                        cmdSql.Parameters.Clear();
                        cmdSql.Parameters.AddWithValue("@ID", ID);
                        cmdSql.Parameters.AddWithValue("@CreateID", user.EmployeeNo);
                        cmdSql.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception err)
            {
                string errmsg;
                if (err.InnerException != null)
                    errmsg = "Error: " + err.InnerException.ToString();
                else
                    errmsg = "Error: " + err.Message.ToString();

                return Json(new { success = false, errors = errmsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, msg = "Request was deleted successfully." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDrawingFiles()
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    foreach (string str in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Areas/WorkOrderRequest/Uploads/Request/Drawing/") + InputFileName);
                        file.SaveAs(ServerSavePath);
                    }
                }
            }
            catch (Exception err)
            {
                string errmsg;
                if (err.InnerException != null)
                    errmsg = "Error: " + err.InnerException.ToString();
                else
                    errmsg = "Error: " + err.Message.ToString();

                return Json(new { success = false, errors = errmsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true });
        }

        public ActionResult GetUserDetails(string EmployeeNo)
        {
            WO_RequestorInformation WorkOrderRequestData = new WO_RequestorInformation();
            int DivisionID = 0;
            int DepartmentID = 0;
            bool hasError = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString.WOSDB.ToString()))
                {
                    conn.Open();
                    #region Get Header
                    using (SqlCommand cmdSqlHeader = conn.CreateCommand())
                    {
                        cmdSqlHeader.Connection = conn;
                        cmdSqlHeader.CommandType = CommandType.StoredProcedure;
                        cmdSqlHeader.CommandText = "WorkOrderRequest_GetUserDetails";

                        cmdSqlHeader.Parameters.Clear();
                        cmdSqlHeader.Parameters.AddWithValue("@EmployeeNo", EmployeeNo);
                        cmdSqlHeader.ExecuteNonQuery();
                        using (SqlDataReader rdr = cmdSqlHeader.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                WorkOrderRequestData.RequestorEmployeeNo = rdr["RequestorEmployeeNo"].ToString();
                                WorkOrderRequestData.EmployeeName = rdr["EmployeeName"].ToString();
                                WorkOrderRequestData.MobileNo = rdr["MobileNo"].ToString();
                                WorkOrderRequestData.LocalNo = rdr["LocalNo"].ToString();
                                WorkOrderRequestData.Email = rdr["Email"].ToString();
                                WorkOrderRequestData.DivisionID = Convert.ToInt32(rdr["DivisionID"].ToString());
                                WorkOrderRequestData.DepartmentID = Convert.ToInt32(rdr["DepartmentID"].ToString());
                                WorkOrderRequestData.SectionID = Convert.ToInt32(rdr["SectionID"].ToString());
                            }
                        }

                    }
                    #endregion
                    conn.Close();
                }
            }
            catch (Exception err)
            {
                string errmsg;
                if (err.InnerException != null)
                    errmsg = "Error: " + err.InnerException.ToString();
                else
                    errmsg = "Error: " + err.Message.ToString();

                return Json(new { success = false, errors = errmsg }, JsonRequestBehavior.AllowGet);
            }
            if (hasError)
                return Json(new { success = false, errors = "" });
            else
            {
                return Json(new { success = true, WorkOrderRequestData = WorkOrderRequestData, DepartmentID, DivisionID });
            }
        }
    }
}