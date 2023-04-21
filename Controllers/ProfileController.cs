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
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["userid"] != null)
            {
                ViewBag.User = Session["username"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");

            }
        }
        public ActionResult AdminIndex()
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "Admin")
            {
                ViewBag.User = Session["username"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");

            }
        }
        public JsonResult AjaxMethod()
        {
            string s = Session["userid"].ToString();
            DBHandler dbHandle = new DBHandler();
            SqlCommand com = new SqlCommand("sp_user_detail");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@user_id", int.Parse(s));
            var users = dbHandle.ConvertDataTable<Users>(dbHandle.GetSingle(com)).First<Users>();
            return Json(users,JsonRequestBehavior.AllowGet);
        }
    }
}