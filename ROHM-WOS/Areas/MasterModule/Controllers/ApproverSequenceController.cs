﻿using ROHM_WOS.Controllers;
using ROHM_WOS.Models.MasterEntities;
using ROHM_WOS.Models.MasterProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROHM_WOS.Areas.MasterModule.Controllers
{
    public class ApproverSequenceController : Controller
    {
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        long? SectionIDFilter = 0;
        [Session]
        // GET: MasterModule/ApproverSequence
        public ActionResult ApproverSequence()
        {
            return View();
        }

        public ActionResult GetPosition(string UserID)
        {
            string Position= string.Empty;
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandText = "SELECT TOP 1 Position FROM M_Users WHERE IsDeleted <> 1 AND EmployeeNo = '" + UserID + "'";
            cmdSql.CommandTimeout = 0;

            conn.Open();
            //cmdSql.ExecuteNonQuery();

            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Position = rdr["Position"].ToString();
                }
            }
            conn.Close();
            return Json(new { position = Position }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDivisionList()
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

                List<M_Division> list = new List<M_Division>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandText = "SELECT * FROM M_Division WHERE IsDeleted <> 1";
                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        M_Division getter = new M_Division();
                        getter.ID = Convert.ToInt32(rdr["ID"]);//.ToString();
                        getter.DivisionName = rdr["DivisionName"].ToString();
                        //getter.DepartmentList = GetDeparmentList(getter.ID);
                        list.Add(getter);
                    }
                }

                int totalCount = list.Count;
                conn.Close();

                int? totalrows = totalCount;
                int? totalrowsafterfiltering = totalCount;

                foreach(M_Division m in list)
                {
                    m.DepartmentList = GetDeparmentList(m.ID);
                }

                return Json(new {data = list }, JsonRequestBehavior.AllowGet);
                //return Json(new { data = list, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
        }

        public List<M_Department> GetDeparmentList(long DivisionID)
        {
             
                List<M_Department> list = new List<M_Department>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandText = "SELECT * FROM M_Department WHERE IsDeleted <> 1 AND DivisionID ='"+DivisionID+ "'";
                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        M_Department getter = new M_Department();
                        getter.ID = Convert.ToInt32(rdr["ID"]);//.ToString();
                        getter.DivisionID = Convert.ToInt32(rdr["DivisionID"]);//.ToString();
                        getter.DepartmentName = rdr["DepartmentName"].ToString();


                        list.Add(getter);
                    }
                }
                conn.Close();

                foreach (M_Department m in list)
                {
                    m.SectionList = GetSectionList(m.ID);
                }

            return list;
            
            
        }

        public List<M_Section> GetSectionList(long DepartmentID)
        {
            List<M_Section> list = new List<M_Section>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandText = "SELECT * FROM M_Section WHERE IsDeleted <> 1 AND DepartmentID ='" + DepartmentID + "'";
            cmdSql.CommandTimeout = 0;

            conn.Open();
            using (SqlDataReader rdr = cmdSql.ExecuteReader())
            {
                while (rdr.Read())
                {
                    M_Section getter = new M_Section();
                    getter.ID = Convert.ToInt32(rdr["ID"]);//.ToString();
                    getter.DepartmentID = Convert.ToInt32(rdr["DepartmentID"]);//.ToString();
                    getter.SectionName = rdr["SectionName"].ToString();
                    list.Add(getter);
                }
            }
            conn.Close();

            //list = list.Where(x => x.ID == SectionIDFilter || SectionIDFilter == null).ToList();

            return list;
        }
        
        public ActionResult GetApproverSequenceList(long? SectionID)
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

                List<MasterGET_ApproverSequenceList> list = new List<MasterGET_ApproverSequenceList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_ApproverSequenceList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@SectionID", SqlDbType.BigInt).Value = SectionID;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        MasterGET_ApproverSequenceList getter = new MasterGET_ApproverSequenceList();
                        getter.ID = Convert.ToInt32(rdr["ID"]);//.ToString();
                        getter.DivisionID = Convert.ToInt64(rdr["DivisionID"]);
                        getter.DivisionName = rdr["DivisionName"].ToString();
                        getter.DepartmentID = Convert.ToInt64(rdr["DepartmentID"]);
                        getter.DepartmentName = rdr["DepartmentName"].ToString();
                        getter.SectionID = Convert.ToInt64(rdr["SectionID"]);
                        getter.SectionName = rdr["SectionName"].ToString();
                       
                        getter.EmpNo = rdr["EmpNo"].ToString();
                        getter.OrderNo = Convert.ToInt32(rdr["OrderNo"]);
                        getter.MainBackupApprover = Convert.ToBoolean(rdr["MainBackupApprover"]);
                        getter.Position = rdr["Position"].ToString();



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

        public ActionResult CreateApprover(M_ApproverSequence data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;

                string Query = "";
                Query += "INSERT INTO [dbo].[M_ApproverSequence]" +
                              "     ([DivisionID]" +
                              "     ,[DepartmentID]" +
                              "     ,[SectionID]" +
                              "     ,[EmployeeNo]" +
                              "     ,[OrderNo]" +
                              "     ,[MainBackupApprover]" +
                              "     ,[Position]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.DivisionID + "'," +
                              "     '" + data.DepartmentID + "'," +
                              "     '" + data.SectionID + "'," +
                              "     '" + data.EmployeeNo + "'," +
                              "     '" + data.OrderNo + "'," +
                              "     '" + data.MainBackupApprover + "'," +
                              "     '" + data.Position + "'," +
                              "     '" + 0 + "'," +
                              "     '" + data.CreateID + "'," +
                              "     '" + data.CreateDate + "'," +
                              "     '" + data.UpdateID + "'," +
                              "     '" + data.UpdateDate + "')";

                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandText = Query;

                conn.Open();
                cmdSql.ExecuteNonQuery();
                conn.Close();

                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception err)
            {
                return Json(new { msg = err.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateApproverSequence(M_ApproverSequence data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_ApproverSequence] SET " +
                              "      [DivisionID] =   '" + data.DivisionID + "'" +
                              "     ,[DepartmentID]=         '" + data.DepartmentID + "'" +
                              "     ,[SectionID]=         '" + data.SectionID + "'" +
                              "     ,[EmployeeNo]=         '" + data.EmployeeNo + "'" +
                              "     ,[OrderNo]=         '" + data.OrderNo + "'" +
                              "     ,[MainBackupApprover]=         '" + data.MainBackupApprover + "'" +
                              "     ,[Position]=         '" + data.Position + "'" +
                              "     ,[UpdateID]=        '" + data.UpdateID + "'" +
                              "     ,[UpdateDate]=      '" + data.UpdateDate + "'" +
                              " WHERE [ID] =   '" + data.ID + "'";

                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandText = Query;

                conn.Open();
                cmdSql.ExecuteNonQuery();
                conn.Close();

                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception err)
            {
                return Json(new { msg = err.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteApproverSequence(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_ApproverSequence] SET " +

                                  "     [IsDeleted]=     '" + 1 + "'" +
                                  "     ,[UpdateID]=    '" + user.EmployeeNo + "'" +
                                  "     ,[UpdateDate]=  '" + DateTime.Now + "'" +
                                  " WHERE [ID] =   '" + data + "'";

                    SqlCommand cmdSql = new SqlCommand();
                    cmdSql.Connection = conn;
                    cmdSql.CommandTimeout = 0;
                    cmdSql.CommandText = Query;

                    conn.Open();
                    cmdSql.ExecuteNonQuery();
                    conn.Close();
                }

                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception err)
            {
                return Json(new { msg = err.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}