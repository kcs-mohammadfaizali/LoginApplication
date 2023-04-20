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
    public class PrescriptionController : Controller
    {
        // GET: Prescription
        [HandleError]
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
            string s = Session["userid"].ToString();

            SqlCommand com = new SqlCommand("sp_prescription_select");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@user_id", int.Parse(s));//For adding spwith parameter in fetching
            DBHandler dBHandler = new DBHandler();
            var PrescriptionList = dBHandler.ConvertDataTable<Prescription>(dBHandler.GetsAll(com).Tables[0]);
            return Json(PrescriptionList);
        }

        // GET: Prescription/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Prescription/Create
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

        public JsonResult getPatient()
        {
            DBHandler dbHandle = new DBHandler();
            var myPatientList = dbHandle.ConvertDataTable<Patients>(dbHandle.GetAll("patient_select").Tables[0]);
            return Json(myPatientList, JsonRequestBehavior.AllowGet);
             
        }

        // POST: Prescription/Create
        [HttpPost]
        public ActionResult Create(Prescription prescription)
        {
            try
            {
                SqlCommand com = new SqlCommand("[sp_prescription_insert]");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@patient_id", prescription.patient_id);
                
                DBHandler dbHandler = new DBHandler();
                var result = dbHandler.DMLOperation(com);
                ViewBag.createError = result.ToString();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prescription/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_prescription_details");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Prescription_id", id);
                DBHandler dBHandler = new DBHandler();
                //var CustomerList = dBHandler.ConvertDataTable<Customer>(dBHandler.GetSingle(com));
                //var customer = CustomerList.First();
                var prescription = dBHandler.ConvertDataTable<Prescription>(dBHandler.GetSingle(com)).First<Prescription>();
                ViewBag.Prescription = JsonSerializer.Serialize(prescription);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: Prescription/Edit/5
        [HttpPost]
        public ActionResult Edit(Prescription prescription)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_prescription_update");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Prescription_id", prescription.Prescription_id);
                com.Parameters.AddWithValue("@patient_id", prescription.patient_id);
                
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
