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
    public class PatientsController : Controller
    {
        // GET: Patients
        public ActionResult Index()
        {
            if (Session["userid"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    return RedirectToAction("AdminIndex");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
        public ActionResult AdminIndex()
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "Admin" )
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
            var myPatientList = dbHandle.ConvertDataTable<Patients>(dbHandle.GetAll("patient_select").Tables[0]);
            return Json(myPatientList);
        }


        // GET: Patients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            if (Session["userid"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    return RedirectToAction("AdminCreate");
                }
                else
                    return View(); 
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult AdminCreate()
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "Admin")
            {
                ViewBag.createError = "Data Subitted";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");

            }
        }

        // POST: Patients/Create
        [HttpPost]
        public ActionResult Create(Patients patients)
        {
            bool status = false;
            string message = "";

            try
            {

                SqlCommand com = new SqlCommand("sp_patient_insert");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user_id", int.Parse(Session["userid"].ToString()));
                com.Parameters.AddWithValue("@P_First_name", patients.P_First_name);
                com.Parameters.AddWithValue("@P_Last_name", patients.P_Last_name);
                com.Parameters.AddWithValue("@P_Phone", patients.P_Phone);
                com.Parameters.AddWithValue("@P_Email", patients.P_Email);
                com.Parameters.AddWithValue("@P_Address", patients.P_Address);
                com.Parameters.AddWithValue("@Medical_condition", patients.Medical_condition);
                com.Parameters.AddWithValue("@IsFollowUp", patients.user_id);
                DBHandler dbHandler = new DBHandler();
                var result = dbHandler.DMLOperation(com);
                ViewBag.createError = result.ToString();
                //return RedirectToAction("Error");
                status = true;
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;
                ViewBag.createError = errormessage.ToString();
                message=ex.Message;
            }
            return Json(new { status = status, message = message });

        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["userid"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    return RedirectToAction("AdminEdit");
                }
                else
                {
                    try
                    {
                        SqlCommand com = new SqlCommand("sp_patient_detail");
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@patient_id", id);
                        DBHandler dBHandler = new DBHandler();
                        //var CustomerList = dBHandler.ConvertDataTable<Customer>(dBHandler.GetSingle(com));
                        //var customer = CustomerList.First();
                        var patients = dBHandler.ConvertDataTable<Patients>(dBHandler.GetSingle(com)).First<Patients>();
                        ViewBag.Patients = JsonSerializer.Serialize(patients);
                        return View();
                    }
                    catch
                    {
                        return View();
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        public ActionResult AdminEdit(int id)
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "Admin")
            {
                try
                {
                    SqlCommand com = new SqlCommand("sp_patient_detail");
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@patient_id", id);
                    DBHandler dBHandler = new DBHandler();
                    //var CustomerList = dBHandler.ConvertDataTable<Customer>(dBHandler.GetSingle(com));
                    //var customer = CustomerList.First();
                    var patients = dBHandler.ConvertDataTable<Patients>(dBHandler.GetSingle(com)).First<Patients>();
                    ViewBag.Patients = JsonSerializer.Serialize(patients);
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

        // POST: Patients/Edit/5
        [HttpPost]
        public ActionResult Edit(Patients patients)
        {
            bool status = false;
            string message = "";
            try
            {
                SqlCommand com = new SqlCommand("sp_patient_update");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@patient_id", patients.patient_id);
                com.Parameters.AddWithValue("@P_First_name", patients.P_First_name);
                com.Parameters.AddWithValue("@P_Last_name", patients.P_Last_name);
                com.Parameters.AddWithValue("@P_Phone", patients.P_Phone);
                com.Parameters.AddWithValue("@P_Email", patients.P_Email);
                com.Parameters.AddWithValue("@P_Address", patients.P_Address);
                com.Parameters.AddWithValue("@Medical_condition", patients.Medical_condition);
                com.Parameters.AddWithValue("@IsFollowUp", patients.IsFollowUp);
                DBHandler dBHandler = new DBHandler();
                var result = dBHandler.DMLOperation(com);
                status = true;
            }
            catch
            {
                return View();
            }
            return Json(new { status = status, message = message });
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int id)
        {
            SqlCommand com = new SqlCommand("sp_patient_detail");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@patient_id", id);
            DBHandler dBHandler = new DBHandler();
            var PatientList = dBHandler.ConvertDataTable<Patients>(dBHandler.GetSingle(com));
            var patients = PatientList.First();
            return View(patients);
        }
        
        public ActionResult AdminDelete(int id)
        {
            SqlCommand com = new SqlCommand("sp_patient_detail");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@patient_id", id);
            DBHandler dBHandler = new DBHandler();
            var PatientList = dBHandler.ConvertDataTable<Patients>(dBHandler.GetSingle(com));
            var patients = PatientList.First();
            return View(patients);
        }
        // POST: Patients/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_patient_delete");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@patient_id", id);
                DBHandler dBHandler = new DBHandler();
                var result = dBHandler.DMLOperation(com);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AdminDelete(int id, FormCollection collection)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_patient_delete");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@patient_id", id);
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
