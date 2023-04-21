using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LoginApplication.Models
{
    public class PatientsDB
    {
        string cs = ConfigurationManager.ConnectionStrings["CustomeEntities"].ConnectionString;

        //Return list of all Employees
        //
        public List<Patients> ListAll()
        {
            List<Patients> lst = new List<Patients>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                string s = HttpContext.Current.Session["userid"].ToString();
                con.Open();
                SqlCommand com = new SqlCommand("patient_select", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@user_id", int.Parse(s));//For adding spwith parameter in fetching
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Patients
                    {
                        patient_id = Convert.ToInt32(rdr["patient_id"]),
                        P_First_name = rdr["P_First_name"].ToString(),
                        P_Last_name = rdr["P_Last_name"].ToString(),

                    });
                }
                return lst;
            }
        }

    }
}