using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LoginApplication.Models
{
    public class DBHandler
    {
        string constring = "";
        private SqlConnection con;
        public DBHandler()
        {
            constring = "Data Source=KCSLAP5204\\SQLEXPRESS;Initial Catalog=MedicalPrescriptionManagment;Integrated Security=True";
        }



        private void connection()
        {
            con = new SqlConnection(constring);
        }



        // Get all records
        public DataSet GetAll(string sp)
        {
            DataSet ds = new DataSet();
            try
            {
                string s=HttpContext.Current.Session["userid"].ToString();
                connection();
                SqlCommand cmd = new SqlCommand(sp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", int.Parse(s));//For adding spwith parameter in fetching
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                con.Open();
                sd.Fill(ds);
            }
            catch
            {



            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public DataTable GetSingle(SqlCommand com)
        {
            DataTable dt = new DataTable();
            try
            {
                connection();
                con.Open();
                com.Connection = con;
                SqlDataAdapter sd = new SqlDataAdapter(com);
                sd.Fill(dt);
            }
            catch (Exception ex)
            {



            }
            finally
            {
                con.Close();
            }
            return dt;
        }



        public string DMLOperation(SqlCommand com)
        {
            string status = "Success";
            try
            {
                connection();
                con.Open();
                com.Connection = con;
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                status = "error :" + ex.Message;
            }
            finally
            {



                con.Close();
            }
            return status;
        }



        /* start : Convert data table to generics list */
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }



        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        if (dr[column.ColumnName].ToString() == "" || dr[column.ColumnName].ToString() == "null")
                            pro.SetValue(obj, string.Empty, null);
                        else
                            pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        /* stop : Convert data table to generics list */
    }
}