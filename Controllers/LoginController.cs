using LoginApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LoginApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginProcess(Users users)
        {
            bool status = false;
            string message = "Error";
            try
            {
                string uid = users.Email;
                string pass = users.Password;
                SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomeEntities"].ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT user_id,First_name,Last_name,Email,Password,Role,IsActive FROM dbo.[user] where (Email='" + uid + "') and (Password='" + pass + "')", sqlConn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                bool user_status = bool.Parse(dt.Rows[0]["IsActive"].ToString());
                if (dt.Rows.Count > 0)
                {
                    string role = dt.Rows[0]["Role"].ToString();
                    Session["userid"] = dt.Rows[0]["user_id"].ToString();//accessing through column name
                    Session["username"] = dt.Rows[0]["First_name"].ToString();
                    Session["Role"] = dt.Rows[0]["Role"].ToString();
                    
                    status = true;
                    message = "Success";
                }
                else
                    message = "Login Fail";

            }
            catch (Exception ex)
            {
                message = "Error";
            }
            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DashBoard()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    return RedirectToAction("AdminDashBoard");
                }
                else if (Session["Role"].ToString() == "User")
                {
                    return RedirectToAction("UserDashBoard");

                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult UserDashBoard()
        {

            if (Session["userid"] != null)
            {
                ViewBag.User = Session["username"];
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult AdminDashBoard()
        {

            if (Session["userid"] != null)
            {
                ViewBag.User = Session["username"];
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session["userid"] = null;
            return RedirectToAction("Index");
        }

    }
}