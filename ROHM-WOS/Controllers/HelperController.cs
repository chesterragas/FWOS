﻿using ROHM_WOS.Models.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ROHM_WOS.Controllers
{
    public class HelperController : Controller
    {
        // GET: Helper
        SqlConnection conn = new SqlConnection(ConnectionString.WOSDB);
        public ActionResult Index()
        {
            return View();
        }

        public class DropDown2
        {
            public string ID { get; set; }
            public string ItemName { get; set; }
        }

        public ActionResult GetDropdown_Users()
        {
            List<DropDown2> list = new List<DropDown2>();
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
                    DropDown2 item = new DropDown2();
                    item.ID = rdr["EmployeeNo"].ToString();
                    item.ItemName = rdr["ItemName"].ToString();
                    list.Add(item);
                }
            }
            conn.Close();
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_Division()
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
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_Department(long? DivisionID)
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
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_Section(long? DepartmentID)
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
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_Building()
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = "";
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
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_Floor(long BuildingID)
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = BuildingID;
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_Floor";

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
            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_Criteria()
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_CriteriaHeader";

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

            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_Utilities()
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = "";
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_Utilities";

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

            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropdown_UtilitiesDetails(long UtilitiesID)
        {
            List<DropDown> list = new List<DropDown>();
            SqlCommand cmdSql = new SqlCommand();
            cmdSql.Connection = conn;
            cmdSql.CommandTimeout = 0;
            cmdSql.CommandType = CommandType.StoredProcedure;
            cmdSql.CommandText = @"dbo.Get_Dropdowns";
            cmdSql.Parameters.Clear();
            cmdSql.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = UtilitiesID;
            cmdSql.Parameters.Add("@Table", SqlDbType.NVarChar).Value = "M_UtilitiesCriteria";

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

            return Json(new { list = list }, JsonRequestBehavior.AllowGet);
        }
    }
}