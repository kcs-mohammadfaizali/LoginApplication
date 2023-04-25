using LoginApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult AjaxMethod()
        {
            SqlCommand com = new SqlCommand("sp_medicine_select");
            com.CommandType = CommandType.StoredProcedure;
            DBHandler dBHandler = new DBHandler();
            var MedicineList = dBHandler.ConvertDataTable<Medicine>(dBHandler.GetsAll(com).Tables[0]);
            return Json(MedicineList, JsonRequestBehavior.AllowGet);
        }
    }
}