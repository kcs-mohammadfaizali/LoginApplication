﻿using LoginApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApplication.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {

            if (Session["Role"]!=null && Session["Role"].ToString() == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public JsonResult AjaxMethod(string action, string startDate,string stopDate)
        {

            SqlCommand com = new SqlCommand("sp_prescription_report");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", startDate);
            DBHandler dBHandler = new DBHandler();
            var SalesList = dBHandler.ConvertDataTable<Report>(dBHandler.GetsAll(com).Tables[0]);
            return Json(SalesList, JsonRequestBehavior.AllowGet);
        }
    }
}