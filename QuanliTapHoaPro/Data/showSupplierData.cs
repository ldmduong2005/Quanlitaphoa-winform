using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiTapHoa
{
    class showSupplierData
    {
        public int ID { get; set; }
        public string SupplierID { get; set; }

        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        public List<showSupplierData> AllData()
        {
            List<showSupplierData> listData = new List<showSupplierData>();
            SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

            try
            {


                connect.Open();
                string selectData = "SELECT * FROM Suppliers ";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showSupplierData uData = new showSupplierData();
                        uData.ID = Convert.ToInt32(reader["ID"]);
                        uData.SupplierID = reader["SupplierID"].ToString();
                        uData.SupplierName = reader["SupplierName"].ToString();
                        uData.PhoneNumber = reader["Phone"].ToString();
                        uData.Email = reader["Email"].ToString();
                        uData.Address = reader["Address"].ToString();



                        listData.Add(uData);
                    }
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Connection", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                connect.Close();
            }
            return listData;
        }

    }
}