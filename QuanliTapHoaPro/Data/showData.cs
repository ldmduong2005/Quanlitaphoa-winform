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
    class showData
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }




        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");
        public List<showData> AllData()
        {
            List<showData> listData = new List<showData>();
            using (SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False"))
            {
                connect.Open();
                string selectData = "SELECT * FROM Users";
                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showData uData = new showData();
                        uData.UserID = (int)reader["UserID"];
                        uData.Username = reader["Username"].ToString();
                        uData.Password = reader["Password"].ToString();
                        uData.Role = reader["Role"].ToString();
                        uData.Fullname = reader["Fullname"].ToString();



                        listData.Add(uData);
                    }
                }
                connect.Close();

            }
            return listData;

        }


        public List<showData> AllDataByEmployee()
        {
            List<showData> listEmployee = new List<showData>();
            try
            {
                connect.Open();
                string selectEmployee = "SELECT UserID, Username, Fullname FROM Users WHERE Role ='Employee'";
                using (SqlCommand cmd = new SqlCommand(selectEmployee, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        showData uData = new showData();
                        uData.UserID = (int)reader["UserID"];
                        uData.Username = reader["Username"].ToString();
                        uData.Fullname = reader["Fullname"].ToString();

                        listEmployee.Add(uData);
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
            return listEmployee;

        }
    }
}
