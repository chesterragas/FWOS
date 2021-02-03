using ROHM_WOS.Controllers;
using ROHM_WOS.Models;
using ROHM_WOS.Models.MasterEntities;
using ROHM_WOS.Models.MasterProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static ROHM_WOS.Controllers.Session;

namespace ROHM_WOS.Areas.MasterModule.Controllers
{
    public class SuppliersController : Controller
    {
        // GET: MasterModule/Suppliers
        //ROHM_WOSDBEntities db = new ROHM_WOSDBEntities();
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        [Session]
        public ActionResult Suppliers()
        {
            return View();
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
                cmdSql.CommandText = @"dbo.MasterGET_SupplierList";

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

        public ActionResult CreateSupplier(M_Suppliers data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;
              
                string Query = "";
                Query += "INSERT INTO [dbo].[M_Suppliers]" +
                              "     ([SupplierName]" +
                              "     ,[SupplierAddress]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.SupplierName + "'," +
                              "     '" + data.Address       + "'," +
                              "     '" + 0                  + "'," +
                              "     '" + data.CreateID      + "'," +
                              "     '" + data.CreateDate    + "'," +
                              "     '" + data.UpdateID      + "'," +
                              "     '" + data.UpdateDate    + "')";

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

        public ActionResult UpdateSuppliers(M_Suppliers data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Suppliers] SET " +
                              "      [SupplierName] =   '" + data.SupplierName + "'" +
                              "     ,[SupplierAddress]=         '" + data.Address + "'" +
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

        public ActionResult DeleteSuppliers(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_Suppliers] SET " +

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


        public ActionResult GetContactDetails(int SupplierID)
        {
            try
            {
                //Server Side Parameter
                int start = (Convert.ToInt32(Request["start"]) == 0) ? 0 : (Convert.ToInt32(Request["start"]) / Convert.ToInt32(Request["length"]));
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                List<MasterGET_Supplier_DetailList> list = new List<MasterGET_Supplier_DetailList>();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_Supplier_DetailList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = start;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = length;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = sortColumnName;
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = searchValue;
                cmdSql.Parameters.Add("@SupplierID", SqlDbType.NVarChar).Value = SupplierID;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        MasterGET_Supplier_DetailList getter = new MasterGET_Supplier_DetailList();
                        getter.ID = Convert.ToInt64(rdr["ID"]);//.ToString();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.SupplierID = Convert.ToInt32(rdr["SupplierID"]);
                        getter.ContactTitle = rdr["ContactTitle"].ToString();
                        getter.ContactFirstName = rdr["ContactFirstName"].ToString();
                        getter.ContactLastName = rdr["ContactLastName"].ToString();
                        getter.ContactPosition = rdr["ContactPosition"].ToString();
                        getter.ContactEmail = rdr["ContactEmail"].ToString();
                        getter.ContactTelephone = rdr["ContactTelephone"].ToString();
                        getter.ContactCellphone = rdr["ContactCellphone"].ToString();
                        
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

        public ActionResult CreateSupplierDetails(M_SuppliersDetails data) 
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;

                string Query = "";
                Query += "INSERT INTO [dbo].[M_SuppliersDetails]" +
                              "     ([SupplierID]" +
                              "     ,[ContactTitle]" +
                              "     ,[ContactFirstName]" +
                              "     ,[ContactLastName]" +
                              "     ,[ContactPosition]" +
                              "     ,[ContactEmail]" +
                              "     ,[ContactTelephone]" +
                              "     ,[ContactCellphone]" +
                              "     ,[IsDeleted]" +
                              "     ,[CreateID]" +
                              "     ,[CreateDate]" +
                              "     ,[UpdateID]" +
                              "     ,[UpdateDate])" +
                              "VALUES" +
                              "     ('" + data.SupplierID + "'," +
                              "     '" + data.ContactTitle + "'," +
                              "     '" + data.ContactFirstName + "'," +
                              "     '" + data.ContactLastName + "'," +
                              "     '" + data.ContactPosition + "'," +
                              "     '" + data.ContactEmail + "'," +
                              "     '" + data.ContactTelephone + "'," +
                              "     '" + data.ContactCellphone + "'," +
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
        
        public ActionResult UpdateSuppliersDetails(M_SuppliersDetails data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_SuppliersDetails] SET " +
                              "      [ContactTitle] =   '" + data.ContactTitle + "'" +
                              "     ,[ContactFirstName]= '" + data.ContactFirstName + "'" +
                              "     ,[ContactLastName]= '" + data.ContactLastName + "'" +
                              "     ,[ContactPosition]= '" + data.ContactPosition + "'" +
                              "     ,[ContactEmail]= '" + data.ContactEmail + "'" +
                              "     ,[ContactTelephone]= '" + data.ContactTelephone + "'" +
                              "     ,[ContactCellphone]= '" + data.ContactCellphone + "'" +
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

        public ActionResult DeleteSuppliersDetails(List<int> datalist)
        {
            try
            {
                foreach (int data in datalist)
                {
                    string Query = "";
                    Query += " UPDATE [dbo].[M_SuppliersDetails] SET " +

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