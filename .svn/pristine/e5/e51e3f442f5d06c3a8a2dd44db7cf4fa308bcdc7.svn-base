using ROHM_WOS.Controllers;
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
    [Session]
    public class DivisionController : Controller
    {
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        // GET: MasterModule/Division
        public ActionResult Division()
        {
            return View();
        }


        #region FOR Division
        public ActionResult GetDivisionList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_DivisionList> list = new List<MasterGET_DivisionList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_DivisionList";

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
                        MasterGET_DivisionList getter = new MasterGET_DivisionList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);//.ToString();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.DivisionName = rdr["DivisionName"].ToString();

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

        public ActionResult CreateDivision(M_Division data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_Division]" +
                              "     ([DivisionName]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.DivisionName + "'," +
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

        public ActionResult UpdateDivision(M_Division data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Division] SET " +
                              "      [DivisionName] =  '" + data.DivisionName + "'" +
                              "      ,[UpdateID]=  '" + data.UpdateID + "'" +
                              "      ,[UpdateDate]=  '" + data.UpdateDate + "'" +
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

        public ActionResult DeleteDivision(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_Division] SET " +

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

        #endregion


        #region FOR Department
        public ActionResult GetDepartmentList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_DepartmentList> list = new List<MasterGET_DepartmentList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_DepartmentList";

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
                        MasterGET_DepartmentList getter = new MasterGET_DepartmentList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);
                        getter.DivisionID = Convert.ToInt32(rdr["DivisionID"]);
                        getter.DivisionName = rdr["DivisionName"].ToString();
                        getter.DepartmentName = rdr["DepartmentName"].ToString();

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

        public ActionResult CreateDepartment(M_Department data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_Department]" +
                              "     ([DepartmentName]" +
                              "     ,[DivisionID]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.DepartmentName + "'," +
                              "     '" + data.DivisionID + "'," +
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

        public ActionResult UpdateDepartment(M_Department data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Department] SET " +
                              "      [DepartmentName] =  '" + data.DepartmentName + "'" +
                              "      ,[DivisionID] =  '" + data.DivisionID + "'" +
                              "      ,[UpdateID]=  '" + data.UpdateID + "'" +
                              "      ,[UpdateDate]=  '" + data.UpdateDate + "'" +
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

        public ActionResult DeleteDepartment(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_Department] SET " +

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

        #endregion


        #region FOR Section
        public ActionResult GetSectionList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_SectionList> list = new List<MasterGET_SectionList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_SectionList";

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
                        MasterGET_SectionList getter = new MasterGET_SectionList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);
                        getter.DepartmentID = Convert.ToInt32(rdr["DepartmentID"]);
                        getter.DivisionID = Convert.ToInt32(rdr["DivisionID"]);
                        getter.DivisionName = rdr["DivisionName"].ToString();
                        getter.DepartmentName = rdr["DepartmentName"].ToString();
                        getter.SectionName = rdr["SectionName"].ToString();

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

        public ActionResult CreateSection(M_Section data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_Section]" +
                              "     ([SectionName]" +
                              "     ,[DepartmentID]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.SectionName + "'," +
                              "     '" + data.DepartmentID + "'," +
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

        public ActionResult UpdateSection(M_Section data)
        {
            try
            {
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Section] SET " +
                              "      [SectionName] =  '" + data.SectionName + "'" +
                              "      ,[DepartmentID] =  '" + data.DepartmentID + "'" +
                              "      ,[UpdateID]=  '" + data.UpdateID + "'" +
                              "      ,[UpdateDate]=  '" + data.UpdateDate + "'" +
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

        public ActionResult DeleteSection(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_Section] SET " +

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

        #endregion
    }
}