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
    public class MedicineController : Controller
    {
        // GET: Medicine
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
            var MedicineList = dbHandle.ConvertDataTable<Medicine>(dbHandle.GetAll("sp_medicine_select").Tables[0]);
            return Json(MedicineList);
        }

        // GET: Medicine/Details/5
        public ActionResult Details(int id)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    SqlCommand com = new SqlCommand("sp_medicine_detail");
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@sr_id", id);
                    DBHandler dBHandler = new DBHandler();
                    //var CustomerList = dBHandler.ConvertDataTable<Customer>(dBHandler.GetSingle(com));
                    //var customer = CustomerList.First();
                    var medicines = dBHandler.ConvertDataTable<Medicine>(dBHandler.GetSingle(com)).First<Medicine>();
                    ViewBag.Patients = JsonSerializer.Serialize(medicines);
                    return View(medicines);
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

        // GET: Medicine/Create
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

        // POST: Medicine/Create
        [HttpPost]
        public ActionResult Create(Medicine medicine)
        {
            try
            {

                SqlCommand com = new SqlCommand("sp_medicine_insert");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Medicine_name", medicine.Medicine_name);
                com.Parameters.AddWithValue("@Total_stock", medicine.Total_stock);
                com.Parameters.AddWithValue("@Price", medicine.Price);
               
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

        // GET: Medicine/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["userid"] != null)
            {
                try
                {
                    SqlCommand com = new SqlCommand("sp_medicine_detail");
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@sr_id", id);
                    DBHandler dBHandler = new DBHandler();
                    //var CustomerList = dBHandler.ConvertDataTable<Customer>(dBHandler.GetSingle(com));
                    //var customer = CustomerList.First();
                    var medicines = dBHandler.ConvertDataTable<Medicine>(dBHandler.GetSingle(com)).First<Medicine>();
                    ViewBag.Patients = JsonSerializer.Serialize(medicines);
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

        // POST: Medicine/Edit/5
        [HttpPost]
        public ActionResult Edit(Medicine medicine)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_medicine_update");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@sr_id", medicine.sr_id);
                com.Parameters.AddWithValue("@Medicine_name", medicine.Medicine_name);
                com.Parameters.AddWithValue("@Total_stock", medicine.Total_stock);
                com.Parameters.AddWithValue("@Price", medicine.Price);
                DBHandler dBHandler = new DBHandler();
                var result = dBHandler.DMLOperation(com);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Medicine/Delete/5
        public ActionResult Delete(int id)
        {
            SqlCommand com = new SqlCommand("sp_medicine_detail");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@sr_id", id);
            DBHandler dBHandler = new DBHandler();
            //var CustomerList = dBHandler.ConvertDataTable<Customer>(dBHandler.GetSingle(com));
            //var customer = CustomerList.First();
            var medicines = dBHandler.ConvertDataTable<Medicine>(dBHandler.GetSingle(com)).First<Medicine>();
            ViewBag.Patients = JsonSerializer.Serialize(medicines);
            return View(medicines);
        }

        // POST: Medicine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Medicine medicine)
        {
            try
            {
                SqlCommand com = new SqlCommand("sp_medicine_delete");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@sr_id", id);
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
