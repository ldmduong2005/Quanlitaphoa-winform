using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiTapHoa
{
    class showDataOrder
    {
        public int ID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string CustomerID { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string Date { get; set; }
        public string TotalPrice { get; set; }



        public List<showDataOrder> OrderData()
        {
            List<showDataOrder> listOrderData = new List<showDataOrder>();
            SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");
            try
            {
                connect.Open();
                string selectData = "SELECT * FROM Orders ";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showDataOrder uData = new showDataOrder();
                        uData.ID = (int)reader["OrderID"];
                        uData.ProductID = reader["ProductQR"].ToString();
                        uData.ProductName = reader["ProductName"].ToString();
                        uData.Category = reader["Category"].ToString();
                        uData.CustomerID = reader["CustomerID"].ToString();
                        uData.Quantity = reader["Quantity"].ToString();
                        uData.UnitPrice = reader["UnitPrice"].ToString();
                        uData.Date = reader["OrderDate"].ToString();
                        uData.TotalPrice = reader["TotalPrice"].ToString();

                        listOrderData.Add(uData);


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
            return listOrderData;
        }






    }
}
