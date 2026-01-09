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
    class showDateCategories
    {

        public int CategoryID { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Date { get; set; }


        public List<showDateCategories> AllData()
        {
            List<showDateCategories> listData = new List<showDateCategories>();
            SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

            try
            {


                connect.Open();
                string selectData = "SELECT * FROM Categories ";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showDateCategories uData = new showDateCategories();
                        uData.CategoryID = (int)reader["CategoryID"];
                        uData.Name = reader["CategoryName"].ToString();
                        uData.Date = reader["CreatedDate"].ToString();



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