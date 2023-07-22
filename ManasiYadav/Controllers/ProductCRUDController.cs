using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ManasiYadav.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCRUDController : ControllerBase
    {
        public string Str = "";
        SqlConnection con;
        IConfiguration configuration;
        public ProductCRUDController(IConfiguration c)
        {
            configuration = c;
            Str = c.GetConnectionString("ConStr");
            con = new SqlConnection(Str);
        }
        [HttpGet("products")]
        public IEnumerable<Products> GetProductData()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Products";
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int ctr = 0;
            List<Products> lst = new List<Products>();
            while (dr.Read())
            {
                Products obj = new Products();
                obj.ProductId = (int)dr[0];
                obj.Name = (string)dr[1];
                obj.Description = (string)dr[2];
               // obj.Price = (floadr[3];
                obj.Category = (string)dr[4];
                lst.Add(obj);
                ctr++;
            }
            return lst;
        }
        [HttpGet("products/{id}")]
        public string GetRecord(int ProductId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Products where ProductId=@p1";
            cmd.Parameters.Add(new SqlParameter("p1", System.Data.SqlDbType.Int));
            cmd.Parameters["p1"].Value = ProductId;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            Products obj = null;
           
            List<Products> lst = new List<Products>();         
            if (dr.Read())
            {
                obj = new Products();
                obj.ProductId = (int)dr[0];
                obj.Name = (string)dr[1];
                obj.Description = (string)dr[2];
                obj.Price = (float)dr[3];
                obj.Category = (string)dr[4];
                lst.Add(obj);
              
            }
            if(obj == null)
            {
                return "Not Found";
            }
            else
            {
                return lst.ToString();
            }
        }

       [HttpPost("products")]
        public string InsertRecord(int a,string b,string c,float d,string e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into Products values (@p1,@p2,@p3,@p4,@p5)";
            cmd.Parameters.Add(new SqlParameter("p1", System.Data.SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("p2", System.Data.SqlDbType.VarChar,250));
            cmd.Parameters.Add(new SqlParameter("p3", System.Data.SqlDbType.VarChar,250));
            cmd.Parameters.Add(new SqlParameter("p4", System.Data.SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("p5", System.Data.SqlDbType.VarChar,250));
            cmd.Parameters["p1"].Value = a;
            cmd.Parameters["p2"].Value = b;
            cmd.Parameters["p3"].Value = c;
            cmd.Parameters["p4"].Value = d;
            cmd.Parameters["p5"].Value = e;
            int ans =cmd.ExecuteNonQuery();
            return ans.ToString() + "Record Inserted";
        }
        [HttpPut("UpdateRecord/{id}")]
        public string UpdateRecord(int ProductId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "update from Products where ProductId=@p1";
            cmd.Parameters.Add(new SqlParameter("p1", System.Data.SqlDbType.Int));
            cmd.Parameters["p1"].Value = ProductId;
            cmd.Connection.Open();
            int ans = cmd.ExecuteNonQuery();
            return ans.ToString() + " Record Updated";
        }
        [HttpDelete]
        public string DeleteRecord(int ProdcutId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Products where ProductId=@p1";
            cmd.Parameters.Add(new SqlParameter("p1", System.Data.SqlDbType.Int));
            cmd.Parameters["p1"].Value = ProdcutId;
            cmd.Connection.Open();
            int ans = cmd.ExecuteNonQuery();
            return ans.ToString() + "Record Deleted";
        }

    }
}
