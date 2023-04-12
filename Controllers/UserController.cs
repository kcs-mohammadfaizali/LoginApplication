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

            DBHandler dbHandle = new DBHandler();
            var UserList = dbHandle.ConvertDataTable<Users>(dbHandle.GetAll("sp_user_select").Tables[0]);
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
                return RedirectToAction("Error", errormessage);
            }
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

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Users users)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_user_update");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user_id",id);
                com.Parameters.AddWithValue("@First_name", users.First_name);
                com.Parameters.AddWithValue("@Last_name", users.Last_name);
                com.Parameters.AddWithValue("@Password", users.Password);
                com.Parameters.AddWithValue("@Email", users.Email);
                com.Parameters.AddWithValue("@Role", users.Role);
                com.Parameters.AddWithValue("@IsActive", users.IsActive);
                DBHandler dBHandler = new DBHandler();
                var result = dBHandler.DMLOperation(com);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
