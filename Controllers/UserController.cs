using LoginApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.Json;

namespace LoginApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["userid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public JsonResult AjaxMethod()
        {
            SqlCommand com = new SqlCommand("[sp_user_select]");
            com.CommandType = CommandType.StoredProcedure;
            DBHandler dbHandle = new DBHandler();
            var UserList = dbHandle.ConvertDataTable<Users>(dbHandle.GetsAll(com).Tables[0]);
            return Json(UserList);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            if (Session["userid"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(Users users)
        {
            bool status = false;
            string message = "";
            try
            {

                SqlCommand com = new SqlCommand("sp_user_insert");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@First_name", users.First_name);
                com.Parameters.AddWithValue("@Last_name", users.Last_name);
                com.Parameters.AddWithValue("@Password", users.Password);
                com.Parameters.AddWithValue("@Email", users.Email);
                com.Parameters.AddWithValue("@Role", users.Role);
                com.Parameters.AddWithValue("@IsActive", users.IsActive);
                DBHandler dbHandler = new DBHandler();
                var result = dbHandler.DMLOperation(com);
                status = true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { status = status, message = message });

        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    SqlCommand com = new SqlCommand("sp_user_detail");
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@user_id", id);
                    DBHandler dBHandler = new DBHandler();
                    var users= dBHandler.ConvertDataTable<Users>(dBHandler.GetSingle(com)).First<Users>();
                    ViewBag.Users = JsonSerializer.Serialize(users);
                    return View();
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Users users)
        {
            bool status = false;
            string message = "";
            try
            {
                SqlCommand com = new SqlCommand("sp_user_update");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user_id",users.user_id);
                com.Parameters.AddWithValue("@First_name", users.First_name);
                com.Parameters.AddWithValue("@Last_name", users.Last_name);
                com.Parameters.AddWithValue("@Password", users.Password);
                com.Parameters.AddWithValue("@Email", users.Email);
                com.Parameters.AddWithValue("@Role", users.Role);
                com.Parameters.AddWithValue("@IsActive", users.IsActive);
                DBHandler dBHandler = new DBHandler();
                var result = dBHandler.DMLOperation(com);
                status = true;
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { status = status, message = message });

        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    SqlCommand com = new SqlCommand("sp_user_detail");
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@user_id", id);
                    DBHandler dBHandler = new DBHandler();
                    var users = dBHandler.ConvertDataTable<Users>(dBHandler.GetSingle(com)).First<Users>();
                    //ViewBag.User = JsonSerializer.Serialize(users);
                    return View(users);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Users users)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_user_delete");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user_id", id);
                DBHandler dBHandler = new DBHandler();
                var result = dBHandler.DMLOperation(com);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
