using ROHM_WOS.Areas.MasterModule.Models;
using ROHM_WOS.Controllers;
using ROHM_WOS.Models.MasterEntities;
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
    public class HolidaysController : Controller
    {
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        // GET: MasterModule/Holidays
        public ActionResult Holidays()
        {
            return View();
        }

        #region FOR holidays
        public ActionResult GetholidaysList(string Type)
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_HolidayList> list = new List<MasterGET_HolidayList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_HolidayList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@TypeHoliday", SqlDbType.NVarChar).Value = Type;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        MasterGET_HolidayList getter = new MasterGET_HolidayList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);//.ToString();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.HolidayName = rdr["HolidayName"].ToString();
                        getter.HolidayDate = Convert.ToDateTime(rdr["HolidayDate"]);

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

        public ActionResult Createholidays(M_Holidays data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
                string Query = "";
                Query += "INSERT INTO [dbo].[M_Holidays]" +
                              "     ([HolidayName]" +
                              "     ,[HolidayDate]" +
                              "     ,[Type]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.HolidayName + "'," +
                              "     '" + data.HolidayDate + "'," +
                              "     '" + data.Type + "'," +
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

        public ActionResult Updateholidays(M_Holidays data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Holidays] SET " +
                              "      [HolidayName] =  '" + data.HolidayName + "'" +
                              "      ,[HolidayDate]=  '" + data.HolidayDate + "'" +
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

        public ActionResult Deleteholidays(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_Holidays] SET " +

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