using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using mvcwithado.Models; 

namespace mvcwithado.dal
{
    
    public class productdal
    {
        string constring = ConfigurationManager.ConnectionStrings["adoconnectionstring"].ToString();

        //get all product
        public List<product> getallitem()
        {
            List<product> listdata = new List<product>();
            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "getallITEM";
                SqlDataAdapter sqlda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                connection.Open();
                sqlda.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    listdata.Add(new product
                    {
                        id = Convert.ToInt32(dr["id"]),
                        name = dr["name"].ToString(),
                        age = Convert.ToInt32(dr["age"]),
                        address = dr["address"].ToString(),
                        phone = dr["phone"].ToString(),
                        email = dr["email"].ToString(),
                        course = dr["class"].ToString(),
                        dob = dr["DOB"].ToString(),
                        username = dr["photo"].ToString(),
                        password = dr["resume"].ToString()
                    });
                }
            }
            return listdata;
        }
        //insert item
        public bool insertitems(product product)
        {
            //if (ModelState.IsValid)
            //{
            //    if (file.ContentLength > 0)
            //    {
            //        string photo = Path.GetFileName(file.FileName);
            //        var s = Server.MapPath("~/photo");
            //        string pa = Path.Combine(s, photo);
            //        file.SaveAs(pa);
            //        var fullpath = Path.Combine("~\\photo", photo);
            //        product.photo = fullpath;
            //    }
            //}
            
            int id = 0;

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("mvcins", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@age", product.age);
                command.Parameters.AddWithValue("@address", product.address);
                command.Parameters.AddWithValue("@phone", product.phone);
                command.Parameters.AddWithValue("@email", product.email);
                command.Parameters.AddWithValue("@class", product.course); ;
                command.Parameters.AddWithValue("@DOB", product.dob);
                command.Parameters.AddWithValue("@photo", product.username);
                command.Parameters.AddWithValue("@resume", product.password);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //get product byid
        public List<product> getitembyid(int productid)
        {
            List<product> listdata = new List<product>();
            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_getitembyid";
                command.Parameters.AddWithValue("@id", productid);
                SqlDataAdapter sqlda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                connection.Open();
                sqlda.Fill(dt);
                connection.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    listdata.Add(new product
                    {
                        id = Convert.ToInt32(dr["id"]),
                        name = dr["name"].ToString(),
                        age = Convert.ToInt32(dr["age"]),
                        address = dr["address"].ToString(),
                        phone = dr["phone"].ToString(),
                        email = dr["email"].ToString(),
                        course = dr["class"].ToString(),
                        dob = dr["DOB"].ToString(),
                        username = dr["photo"].ToString(),
                        password = dr["resume"].ToString()
                    });
                }
            }
            return listdata;
        }

        //update item
        public bool update(product product)
        {

            int i = 0;

            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("sp_update", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", product.id);
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@age", product.age);
                command.Parameters.AddWithValue("@address", product.address);
                command.Parameters.AddWithValue("@phone", product.phone);
                command.Parameters.AddWithValue("@email", product.email);
                command.Parameters.AddWithValue("@class", product.course); ;
                command.Parameters.AddWithValue("@DOB", product.dob);
                command.Parameters.AddWithValue("@photo", product.username);
                command.Parameters.AddWithValue("@resume", product.password);
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();

            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //delete product
        public string deleteproduct(int id)
        {
            string result = "";
            using(SqlConnection connection=new SqlConnection(constring))
            {
                SqlCommand command = new SqlCommand("sp_DeleteData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }
    }
}