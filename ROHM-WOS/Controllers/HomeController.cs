using ROHM_WOS.Models.MasterEntities;
using ROHM_WOS.Models.MasterProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ROHM_WOS.Controllers
{
    [Session]
    public class HomeController : Controller
    {
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        M_Users user = (M_Users)System.Web.HttpContext.Current.Session["user"];
        public ActionResult Chat()
        {
            return View();
        }
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetUserProfile()
        {
            try
            {
                MasterGET_UserList getter = new MasterGET_UserList();
                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandType = CommandType.StoredProcedure;
                cmdSql.CommandText = @"dbo.MasterGET_UserList";

                cmdSql.Parameters.Clear();
                cmdSql.Parameters.Add("@PageCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters.Add("@RowCount", SqlDbType.Int).Value = 1;
                cmdSql.Parameters.Add("@OrderBy", SqlDbType.NVarChar).Value = "ID";
                cmdSql.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = user.EmployeeNo;
                cmdSql.Parameters.Add("@RecordCount", SqlDbType.Int).Value = 0;
                cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                cmdSql.CommandTimeout = 0;

                conn.Open();
                //cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                      
                        getter.ID = Convert.ToInt64(rdr["ID"]);//.ToString();
                        getter.Rownum = Convert.ToInt32(rdr["Rownum"]);//.ToString();
                        getter.EmployeeNo = rdr["EmployeeNo"].ToString();
                        getter.FirstName = rdr["FirstName"].ToString();
                        getter.LastName = rdr["LastName"].ToString();
                        getter.Email = rdr["Email"].ToString();
                        getter.MobileNo = rdr["MobileNo"].ToString();
                        getter.LocalNo = rdr["LocalNo"].ToString();
                        getter.Password = rdr["Password"].ToString();
                        getter.UserPhoto = rdr["UserPhoto"].ToString();
                        getter.Division = rdr["DivisionID"].ToString();
                        getter.Department = rdr["DepartmentID"].ToString();
                        getter.Section = rdr["SectionID"].ToString();
                        getter.IsApproved = Convert.ToInt32(rdr["IsApproved"]);

                       
                    }
                }
                
                return Json(new { data = getter }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);

            }
        }

       
        public ActionResult UpdateProfile(M_Users data)
        {
            try
            {
                data.CreateID = user.EmployeeNo;
                data.CreateDate = DateTime.Now;
                data.UpdateID = user.EmployeeNo;
                data.UpdateDate = DateTime.Now;


                string Query = "";
                Query += "UPDATE [dbo].[M_Users] SET " +
                              "      [FirstName] =  '" + data.FirstName + "'" +
                              "     ,[LastName]=    '" + data.LastName + "'" +
                              "     ,[Email]=       '" + data.Email + "'" +
                              "     ,[MobileNo]=    '" + data.MobileNo + "'" +
                              "     ,[LocalNo]=     '" + data.LocalNo + "'" +
                              "     ,[DivisionID]=    '" + data.DivisionID + "'" +
                              "     ,[DepartmentID]=  '" + data.DepartmentID + "'" +
                              "     ,[SectionID]=     '" + data.SectionID + "'" +
                              "     ,[UpdateID]=    '" + data.UpdateID + "'" +
                              "     ,[UpdateDate]=  '" + data.UpdateDate + "'" +
                              " WHERE [EmployeeNo] =   '" + user.EmployeeNo + "'";

                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandText = Query;

                conn.Open();
                cmdSql.ExecuteNonQuery();
                conn.Close();


                M_Users userupdate = new M_Users();
                
                cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandText = "SELECT * FROM M_Users WHERE EmployeeNo =  '" + user.EmployeeNo + "'";
                cmdSql.CommandTimeout = 0;

                conn.Open();
                cmdSql.ExecuteNonQuery();

                using (SqlDataReader rdr = cmdSql.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        userupdate.ID = Convert.ToInt64(rdr["ID"]);
                        userupdate.EmployeeNo = rdr["EmployeeNo"].ToString();
                        userupdate.FirstName = rdr["FirstName"].ToString();
                        userupdate.LastName = rdr["LastName"].ToString();
                        userupdate.Email = rdr["Email"].ToString();
                        userupdate.Password = rdr["Password"].ToString();
                        userupdate.UserPhoto = rdr["UserPhoto"].ToString();
                        userupdate.DivisionID = Convert.ToInt64(rdr["DivisionID"]);//.ToString();
                        userupdate.DepartmentID = Convert.ToInt64(rdr["DepartmentID"]);
                        userupdate.SectionID = Convert.ToInt64(rdr["SectionID"]);
                    }
                }

                conn.Close();


                System.Web.HttpContext.Current.Session["user"] = userupdate;

                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception err)
            {
                return Json(new { msg = err.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        public ActionResult UpdatePic()
        {
            try
            {
                #region Save to Server
                bool isSuccess = false;
                string serverMessage = string.Empty;
                var fileOne = Request.Files[0] as HttpPostedFileBase;
                string uploadPath = Server.MapPath(@"~/PictureResources/UsersPhoto/");
                string newFileOne = Path.Combine(uploadPath, fileOne.FileName);
                fileOne.SaveAs(HttpContext.Server.MapPath("~/PictureResources/UsersPhoto/") + Path.GetFileName(Regex.Replace(fileOne.FileName, @"\s+", "")));

                string Query = "";
                Query += "UPDATE [dbo].[M_Users] SET " +
                              " [UserPhoto] =  '" + fileOne.FileName + "'" +
                              " WHERE [EmployeeNo] =   '" + user.EmployeeNo + "'";

                SqlCommand cmdSql = new SqlCommand();
                cmdSql.Connection = conn;
                cmdSql.CommandTimeout = 0;
                cmdSql.CommandText = Query;

                conn.Open();
                cmdSql.ExecuteNonQuery();
                conn.Close();


                #endregion

                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception err)
            {
                return Json(new { msg = err.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}