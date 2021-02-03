﻿using ROHM_WOS.Controllers;
using ROHM_WOS.Models.MasterEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ROHM_WOS.Areas.WorkOrderApproval.Models;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace ROHM_WOS.Areas.WorkOrderApproval.Controllers
{
    public class WorkOrderApprovalController : Controller
    {
        private SmtpClient smtpClient;
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        [Session]
        // GET: WorkOrderApproval/WorkOrderApproval
        public ActionResult WorkOrderApproval()
        {
            //SendMail();
            return View();
        }

        public ActionResult GetWorkOrderApprovalList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<P_WorkOrderApprovalList> list = new List<P_WorkOrderApprovalList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.WorkOrderApprovalGET_WorkOrderApprovalList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@EmployeeNo", SqlDbType.NVarChar).Value = user.EmployeeNo;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        P_WorkOrderApprovalList getter = new P_WorkOrderApprovalList();
                        getter.ID = Convert.ToInt32(rdr["ID"]);
                        getter.Division = rdr["DivisionName"].ToString();
                        getter.Department = rdr["DepartmentName"].ToString();
                        getter.Section = rdr["SectionName"].ToString();
                        getter.Building = rdr["BuildingName"].ToString();
                        getter.Floor = rdr["FloorName"].ToString();
                        getter.ProcessArea = rdr["ProcessName"].ToString();
                        getter.ReferenceNo = rdr["ReferenceNo"].ToString();
                        getter.WorkOrderNo = rdr["WorkOrderNo"].ToString();
                        getter.Requestor = rdr["Requestor"].ToString();
                        getter.RequestAge = rdr["RequestAge"].ToString();
                        getter.Rejected = Convert.ToBoolean(rdr["Rejected"]);
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

        public ActionResult GetWorkOrderApprovalList_approved()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<P_WorkOrderApprovalList> list = new List<P_WorkOrderApprovalList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.WorkOrderApprovalGET_WorkOrderApprovalList_approved";

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
                        P_WorkOrderApprovalList getter = new P_WorkOrderApprovalList();
                        getter.ID = Convert.ToInt32(rdr["ID"]);
                        getter.Division = rdr["DivisionName"].ToString();
                        getter.Department = rdr["DepartmentName"].ToString();
                        getter.Section = rdr["SectionName"].ToString();
                        getter.Building = rdr["BuildingName"].ToString();
                        getter.Floor = rdr["FloorName"].ToString();
                        getter.ProcessArea = rdr["ProcessName"].ToString();
                        getter.ReferenceNo = rdr["ReferenceNo"].ToString();
                        getter.WorkOrderNo = rdr["WorkOrderNo"].ToString();
                        getter.Requestor = rdr["Requestor"].ToString();
                        getter.RequestAge = rdr["RequestAge"].ToString();
                        getter.Rejected = Convert.ToBoolean(rdr["Rejected"]);
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

        public ActionResult GetWorkOrderApprovalList_ongoing()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<P_WorkOrderApprovalList> list = new List<P_WorkOrderApprovalList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.WorkOrderApprovalGET_WorkOrderApprovalList_onGoing";

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
                        P_WorkOrderApprovalList getter = new P_WorkOrderApprovalList();
                        getter.ID = Convert.ToInt32(rdr["ID"]);
                        getter.Division = rdr["DivisionName"].ToString();
                        getter.Department = rdr["DepartmentName"].ToString();
                        getter.Section = rdr["SectionName"].ToString();
                        getter.Building = rdr["BuildingName"].ToString();
                        getter.Floor = rdr["FloorName"].ToString();
                        getter.ProcessArea = rdr["ProcessName"].ToString();
                        getter.ReferenceNo = rdr["ReferenceNo"].ToString();
                        getter.WorkOrderNo = rdr["WorkOrderNo"].ToString();
                        getter.Requestor = rdr["Requestor"].ToString();
                        getter.RequestAge = rdr["RequestAge"].ToString();
                        getter.Rejected = Convert.ToBoolean(rdr["Rejected"]);
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
        public ActionResult GetDetails(int ID)
        {
            //M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
            //string User = Session["user"].ToString();

            string buttonStatus = "";
            string buttonHideShow = "";
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.WorkOrderRequest_ApprovalLevel";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@workorderID", SqlDbType.NVarChar).Value = ID;
            cmdSql.Parameters.Add("@currentLoginID", SqlDbType.NVarChar).Value = user.EmployeeNo;
            cmdSql.Parameters.Add("@buttonStatus", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters.Add("@buttonHideShow", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters["@buttonStatus"].Direction = ParameterDirection.Output;
            cmdSql.Parameters["@buttonHideShow"].Direction = ParameterDirection.Output;
            cmdSql.CommandTimeout = 0;

            conn.Open();
            cmdSql.ExecuteNonQuery();
            buttonStatus = (cmdSql.Parameters["@buttonStatus"].Value.ToString());
            buttonHideShow = (cmdSql.Parameters["@buttonHideShow"].Value.ToString());

            conn.Close();
            return Json(new
            {
                buttonStatus = buttonStatus,
                buttonHideShow = buttonHideShow
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UpdateWOStatus(long? ID, int approvedreject, string Remarks)
        {
            if (Remarks == string.Empty || Remarks == null)
            {
                Remarks = "Approved by: " + user.EmployeeNo;
            }
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.WorkOrderRequest_UpdateLevel";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ID", SqlDbType.NVarChar).Value = ID;
            cmdSql.Parameters.Add("@EmployeeNo", SqlDbType.NVarChar).Value = user.EmployeeNo;
            cmdSql.Parameters.Add("@ApprovedReject", SqlDbType.Int).Value = approvedreject;
            cmdSql.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = Remarks;
            cmdSql.Parameters.Add("@NextApproverEmail", SqlDbType.NVarChar, -1).Value = 0;
            cmdSql.Parameters["@NextApproverEmail"].Direction = ParameterDirection.Output;
            cmdSql.Parameters.Add("@RequestorEmail", SqlDbType.NVarChar, -1).Value = 0;
            cmdSql.Parameters["@RequestorEmail"].Direction = ParameterDirection.Output;

            cmdSql.CommandTimeout = 0;


            conn.Open();
            cmdSql.ExecuteNonQuery();

            string nextApprover = cmdSql.Parameters["@NextApproverEmail"].Value.ToString();
            string RequestorEmail = cmdSql.Parameters["@RequestorEmail"].Value.ToString();

            conn.Close();

            SendMail(nextApprover); // Next Approver
            SendMail_Requestor(RequestorEmail); // email requestor

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }


        public void SendMail(string nextApprover)
        {
            string EmailCredentials = ConnectionString.EmailCredentials;
            string[] emaildetails = EmailCredentials.Split(';');
            this.smtpClient = new SmtpClient();
            this.smtpClient.UseDefaultCredentials = false;
            this.smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.smtpClient.Host = emaildetails[0];
            this.smtpClient.Port = Convert.ToInt32(emaildetails[1]);
            this.smtpClient.Credentials = new NetworkCredential(emaildetails[2], emaildetails[3]);
            this.smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            string msg = string.Empty;
            msg ="<table style='border-collapse: collapse; width: 100%; height: 126px;' border='1'>"+
                    "<tbody>"+
                    "<tr style='height: 18px;'>"+
                    "<td style='width: 50%; height: 18px;'>Request Reference No</td>"+
                    "<td style='width: 50%; height: 18px;'>REF20200102123456</td>"+
                    "</tr>"+
                    "<tr style='height: 18px;'>"+
                    "<td style='width: 50%; height: 18px;'>Work Order Title</td>"+
                    "<td style='width: 50%; height: 18px;'>Work Order Request 1</td>"+
                    "</tr>"+
                    "<tr style='height: 18px;'>"+
                    "<td style='width: 50%; height: 18px;'>Work Order No</td>"+
                    "<td style='width: 50%; height: 18px;'>100</td>"+
                    "</tr>"+
                    "<tr style='height: 18px;'>"+
                    "<td style='width: 50%; height: 18px;'>Division</td>"+
                    "<td style='width: 50%; height: 18px;'>TR</td>"+
                    "</tr>"+
                    "<tr style='height: 18px;'>"+
                    "<td style='width: 50%; height: 18px;'>Department</td>"+
                    "<td style='width: 50%; height: 18px;'>PD1</td>"+
                    "</tr>"+
                    "<tr style='height: 18px;'>"+
                    "<td style='width: 50%; height: 18px;'>Section</td>"+
                    "<td style='width: 50%; height: 18px;'>Planning</td>"+
                    "</tr>"+
                    "<tr style='height: 18px;'>"+
                    "<td style='width: 50%; height: 18px;'>Requestor</td>"+
                    "<td style='width: 50%; height: 18px;'>Jolibee</td>"+
                    "</tr>"+
                    "</tbody>"+
                    "</table>";

            String vhtml = String.Empty;
            StreamReader reader = new StreamReader(Server.MapPath("/EmailTemplate/email.html"));
            vhtml = reader.ReadToEnd();
            vhtml = vhtml.Replace("{{{body}}}", msg);
            /* RECIPIENT */
            mail.To.Add(nextApprover);
            mail.From = new MailAddress("FWOSMail@seiko-it.com.ph");
            mail.IsBodyHtml = true;
            mail.Body = vhtml;
            this.smtpClient.Send(mail);
        }

        public void SendMail_Requestor(string RequestorEmail)
        {
            string EmailCredentials = ConnectionString.EmailCredentials;
            string[] emaildetails = EmailCredentials.Split(';');
            this.smtpClient = new SmtpClient();
            this.smtpClient.UseDefaultCredentials = false;
            this.smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.smtpClient.Host = emaildetails[0];
            this.smtpClient.Port = Convert.ToInt32(emaildetails[1]);
            this.smtpClient.Credentials = new NetworkCredential(emaildetails[2], emaildetails[3]);
            this.smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            string msg = string.Empty;
            msg = "Request on going";

            String vhtml = String.Empty;
            StreamReader reader = new StreamReader(Server.MapPath("/EmailTemplate/email.html"));
            vhtml = reader.ReadToEnd();
            vhtml = vhtml.Replace("{{{body}}}", msg);
            /* RECIPIENT */
            mail.To.Add(RequestorEmail);
            mail.From = new MailAddress("FWOSMail@seiko-it.com.ph");
            mail.IsBodyHtml = true;
            mail.Body = vhtml;
            this.smtpClient.Send(mail);
        }



    }

}


                  
    