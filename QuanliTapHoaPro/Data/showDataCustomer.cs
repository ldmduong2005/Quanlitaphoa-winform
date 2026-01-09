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
    class showDataCustomer
    {
        public int ID { get; set; }
        public string Totalprice { get; set; }
        public string Change { get; set; }
        public string OrderDate { get; set; }

        public string CustomerName { get; set; }


        public List<showDataCustomer> AllCustomerData()
        {
            List<showDataCustomer> listCustomerData = new List<showDataCustomer>();
            SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");
            try
            {
                connect.Open();
                string selectData = "SELECT * FROM Customers";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showDataCustomer uData = new showDataCustomer();
                        uData.ID = (int)reader["CustomerID"];
                        uData.Totalprice = reader["TotalPrice"].ToString();
                        uData.Change = reader["Change"].ToString();
                        uData.OrderDate = reader["OrderDate"].ToString();
                        uData.CustomerName = reader["CustomerName"].ToString();



                        listCustomerData.Add(uData);
                    }
                }







            }
            catch (Exception ex)
            {


                MessageBox.Show("Error Connection " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
            finally
            {
                connect.Close();
            }

            return listCustomerData;

        }
    }
}