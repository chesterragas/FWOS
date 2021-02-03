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
    public class WorkLocationsController : Controller
    {
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        // GET: MasterModule/WorkLocations
        public ActionResult WorkLocations()
        {
            return View();
        }


        #region FOR Work Classification
        public ActionResult GetWorkClassificationList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];
                
                List<MasterGET_WorkClassificationList> list = new List<MasterGET_WorkClassificationList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_WorkClassificationList";

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
                        MasterGET_WorkClassificationList getter = new MasterGET_WorkClassificationList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);//.ToString();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.WorkClassificationName = rdr["WorkClassificationName"].ToString();
                        
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

        public ActionResult CreateClassification(M_WorkClassification data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_WorkClassification]" +
                              "     ([WorkClassificationName]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.WorkClassificationName + "'," +
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

        public ActionResult UpdateClassification(M_WorkClassification data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_WorkClassification] SET " +
                              "      [WorkClassificationName] =  '" + data.WorkClassificationName + "'" +
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

        public ActionResult DeleteClassification(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_WorkClassification] SET " +

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

        #region FOR Work Request
        public ActionResult GetWorkrequestList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_WorkRequestList> list = new List<MasterGET_WorkRequestList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_WorkRequestList";

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
                        MasterGET_WorkRequestList getter = new MasterGET_WorkRequestList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);//.ToString();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.WorkRequestName = rdr["WorkRequestName"].ToString();

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

        public ActionResult Createrequest(M_WorkRequest data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_WorkRequest]" +
                              "     ([WorkRequestName]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.WorkRequestName + "'," +
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

        public ActionResult Updaterequest(M_WorkRequest data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_WorkRequest] SET " +
                              "      [WorkrequestName] =  '" + data.WorkRequestName + "'" +
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

        public ActionResult Deleterequest(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_WorkRequest] SET " +

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


        #region FOR Building
        public ActionResult GetBuildingList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_BuildingList> list = new List<MasterGET_BuildingList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_BuildingList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = user.DivisionID;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        MasterGET_BuildingList getter = new MasterGET_BuildingList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);
                        getter.DivisionID = Convert.ToInt64(rdr["DivisionID"]);
                        getter.BuildingName = rdr["BuildingName"].ToString();

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

        public ActionResult CreateBuilding(M_Building data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_Building]" +
                              "     ([BuildingName]" +
                              "     ,[DivisionID]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.BuildingName + "'," +
                              "     '" + user.DivisionID + "'," +
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

        public ActionResult UpdateBuilding(M_Building data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Building] SET " +
                              "      [BuildingName] =  '" + data.BuildingName + "'" +
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

        public ActionResult DeleteBuilding(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_Building] SET " +

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


        #region FOR Floor
        public ActionResult GetFloorList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_FloorList> list = new List<MasterGET_FloorList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_FloorList";

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
                        MasterGET_FloorList getter = new MasterGET_FloorList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);
                        getter.BuildingID = Convert.ToInt64(rdr["BuildingID"]);
                        getter.BuildingName = rdr["BuildingName"].ToString();
                        getter.FloorName = rdr["FloorName"].ToString();

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

        public ActionResult CreateFloor(M_Floor data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_Floor]" +
                              "     ([FloorName]" +
                              "     ,[BuildingID]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.FloorName + "'," +
                              "     '" + data.BuildingID + "'," +
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

        public ActionResult UpdateFloor(M_Floor data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Floor] SET " +
                              "      [FloorName] =  '" + data.FloorName + "'," +
                              "      [BuildingID] =  '" + data.BuildingID + "'" +
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

        public ActionResult DeleteFloor(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_Floor] SET " +

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


        #region FOR ProcessArea
        public ActionResult GetProcessAreaList()
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_ProcessAreaList> list = new List<MasterGET_ProcessAreaList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_ProcessAreaList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = user.DivisionID;

                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        MasterGET_ProcessAreaList getter = new MasterGET_ProcessAreaList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);
                        getter.DivisionID = rdr["DivisionID"].ToString();
                        getter.ProcessAreaName = rdr["ProcessAreaName"].ToString();

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

        public ActionResult CreateProcessArea(M_ProcessArea data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_ProcessArea]" +
                              "     ([ProcessAreaName]" +
                              "     ,[DivisionID]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.ProcessAreaName + "'," +
                              "     '" + user.DivisionID + "'," +
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

        public ActionResult UpdateProcessArea(M_ProcessArea data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_ProcessArea] SET " +
                              "      [ProcessAreaName] =  '" + data.ProcessAreaName + "'" +


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

        public ActionResult DeleteProcessArea(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_ProcessArea] SET " +

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