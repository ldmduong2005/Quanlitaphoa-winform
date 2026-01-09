using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiTapHoa
{
    class showDataProduct
    {
        public int ID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Stock { get; set; }
        public string Price { get; set; }
        public string SupplierID { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string ImagePath { get; set; }


        public List<showDataProduct> ProductData()
        {
            List<showDataProduct> listData = new List<showDataProduct>();
            SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");
            try
            {
                connect.Open();
                string selectData = "SELECT * FROM Products ";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showDataProduct uData = new showDataProduct();
                        uData.ID = (int)reader["ProductID"];
                        uData.ProductID = reader["ProductQR"].ToString();
                        uData.ProductName = reader["ProductName"].ToString();
                        uData.Category = reader["Category"].ToString();
                        uData.Stock = reader["Unit"].ToString();
                        uData.Price = reader["Price"].ToString();
                        uData.SupplierID = reader["SupplierID"].ToString();
                        uData.Status = reader["Status"].ToString();
                        uData.Date = reader["DateInsert"].ToString();
                        uData.ImagePath = reader["Image"].ToString();

                        listData.Add(uData);


                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                connect.Close();
            }
            return listData;
        }

        public List<showDataProduct> allAvailableProduct()
        {
            List<showDataProduct> listDataAvailable = new List<showDataProduct>();
            SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");
            try
            {
                connect.Open();
                string selectData = "SELECT * FROM Products WHERE Status = @status";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Available");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showDataProduct uData = new showDataProduct();
                        uData.ID = (int)reader["ProductID"];
                        uData.ProductID = reader["ProductQR"].ToString();
                        uData.ProductName = reader["ProductName"].ToString();
                        uData.Category = reader["Category"].ToString();
                        uData.Stock = reader["Unit"].ToString();
                        uData.Price = reader["Price"].ToString();
                        uData.SupplierID = reader["SupplierID"].ToString();
                        uData.Status = reader["Status"].ToString();
                        uData.Date = reader["DateInsert"].ToString();
                        uData.ImagePath = reader["Image"].ToString();

                        listDataAvailable.Add(uData);


                    }
                }









            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                connect.Close();
            }
            return listDataAvailable;

        }

    }
}
