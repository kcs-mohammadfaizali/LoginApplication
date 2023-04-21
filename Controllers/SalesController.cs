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
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "Admin")
            {
                return View();
            }
            else if (Session["userid"] != null && Session["Role"].ToString() == "User")
            {
                return  RedirectToAction("UserIndex");
            }
            else 
            {
                return RedirectToAction("Index","Login");
            }
            
        }
        public ActionResult UserIndex()
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "Admin")
            {
                return RedirectToAction("Index");
            }
            else if (Session["userid"] != null && Session["Role"].ToString() == "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        //public  JsonResult TablesColumnDisplay()
        //{
        //    List<MedicineSales> medicineSales= new List<MedicineSales>();
        //    string constring = "Data Source=KCSLAP5204\\SQLEXPRESS;Initial Catalog=MedicalPrescriptionManagment;Integrated Security=True";
        //    using(SqlConnection sqlconn =new SqlConnection(constring))
        //    {
        //        using(SqlCommand sqlCommand= new SqlCommand("SELECT P.id,P.Prescription_id,P.sr_id,P.quantity,M.Medicine_name,M.price as price,P.price as totalprice  FROM Prescription_Details P LEFT JOIN Medicine_Management M ON  M.sr_id =P.sr_id"))
        //        {
        //            using(SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                sqlCommand.Connection = sqlconn;
        //                sqlconn.Open();
        //                sda.SelectCommand= sqlCommand;
        //                SqlDataReader sdr = sqlCommand.ExecuteReader();
        //                while (sdr.Read())
        //                {
        //                    MedicineSales msobj= new MedicineSales();
        //                    msobj.id = Convert.ToInt32(sdr["Id"]);
        //                    msobj.Prescription_id = Convert.ToInt32(sdr["Prescription_id"]);
        //                    msobj.sr_id = Convert.ToInt32(sdr["sr_id"]);
        //                    msobj.Medicine_name = sdr["Medicine_name"].ToString();
        //                    msobj.quantity = Convert.ToInt32(sdr["quantity"]);
        //                    msobj.price = Convert.ToDecimal(sdr["price"]);

        //                    msobj.totalprice = Convert.ToDecimal(sdr["totalprice"]);
        //                    medicineSales.Add(msobj);

        //                }
        //            }
        //            return Json(medicineSales);
        //        }
        //    }
        //}

        public JsonResult AjaxMethod()
        {

            SqlCommand com = new SqlCommand("sp_prescription_transaction_select");
            com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.AddWithValue("@patient_id", id);
            DBHandler dBHandler = new DBHandler();
            var SalesList = dBHandler.ConvertDataTable<MedicineSales>(dBHandler.GetsAll(com).Tables[0]);
            return Json(SalesList);
        }
        public JsonResult UserAjaxMethod()
        {

            SqlCommand com = new SqlCommand("sp_prescription_details_select");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@user_id", int.Parse(Session["userid"].ToString()));
            DBHandler dBHandler = new DBHandler();
            var SalesList = dBHandler.ConvertDataTable<MedicineSales>(dBHandler.GetsAll(com).Tables[0]);
            return Json(SalesList);
        }
        public ActionResult Order()
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult UserOrder()
        {
            if (Session["userid"] != null && Session["Role"].ToString() == "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public JsonResult getPrescription()
        {
            SqlCommand com = new SqlCommand("sp_prescription_select");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@user_id", int.Parse(Session["userid"].ToString()));
            DBHandler dBHandler = new DBHandler();
            var PrescriptionList = dBHandler.ConvertDataTable<Prescription>(dBHandler.GetsAll(com).Tables[0]);
            return Json(PrescriptionList, JsonRequestBehavior.AllowGet);

        }
        
        public JsonResult getMedicine()
        {
            SqlCommand com = new SqlCommand("sp_medicine_select");
            com.CommandType = CommandType.StoredProcedure;
            DBHandler dBHandler = new DBHandler();
            var MedicineList = dBHandler.ConvertDataTable<Medicine>(dBHandler.GetsAll(com).Tables[0]);
            return Json(MedicineList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getMedicineById(int id)
        {
            SqlCommand com = new SqlCommand("sp_medicine_select_ById");
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@sr_id", id);
            DBHandler dBHandler = new DBHandler();
            var MedicineList = dBHandler.ConvertDataTable<Medicine>(dBHandler.GetsAll(com).Tables[0]);
            return Json(MedicineList, JsonRequestBehavior.AllowGet);
        }

        // POST: Prescription/Create
        [HttpPost]
        public ActionResult Order(MedicineSales medicineSales)
        {
            bool status = false;
            string message = "";
            try
            {
                SqlCommand com = new SqlCommand("sp_prescription_details_insert");
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Prescription_id", medicineSales.Prescription_id);
                com.Parameters.AddWithValue("@sr_id", medicineSales.sr_id);
                com.Parameters.AddWithValue("@quantity", medicineSales.quantity);
                com.Parameters.AddWithValue("@price", medicineSales.price);

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

    }
}