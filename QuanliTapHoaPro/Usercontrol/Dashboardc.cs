using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLiTapHoa;

namespace QuanliTapHoaPro
{
    public partial class Dashboardc : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public Dashboardc()
        {
            InitializeComponent();
            displayCustomerData();
            displayEmployeeData();
            displayTotalUser();
            displayTotalProduct();
            displayTotalCustomer();
            displayTotalIncome();
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    displayCustomerData();
                    displayEmployeeData();
                    displayTotalUser();
                    displayTotalProduct();
                    displayTotalCustomer();
                    displayTotalIncome();
                });
            }
            else
            {
                displayCustomerData();
                displayEmployeeData();
                displayTotalUser();
                displayTotalProduct();
                displayTotalCustomer();
                displayTotalIncome();
            }
        }

        private void displayCustomerData()
        {
            showDataCustomer ulist = new showDataCustomer();
            List<showDataCustomer> listData = ulist.AllCustomerData();
            dataGridView1.DataSource = listData;

        }
        private void displayEmployeeData()
        {
            showData show = new showData();
            List<showData> listData = show.AllDataByEmployee();
            dataGridView2.DataSource = listData;
            dataGridView2.Columns["Password"].Visible = false;
            dataGridView2.Columns["Role"].Visible = false;
        }
        private bool checkConnection()
        {
            if (connect.State == ConnectionState.Closed || connect.State == ConnectionState.Broken)
            {
                return true;


            }
            else { return false; }

        }






        private void displayTotalUser()
        {
            if (checkConnection())
            {

                try
                {

                    connect.Open();
                    string selectTotalUser = "SELECT COUNT(UserID) FROM Users WHERE Role = @role";
                    using (SqlCommand cmd = new SqlCommand(selectTotalUser, connect))
                    {
                        cmd.Parameters.AddWithValue("@role", "Employee");
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            allUserCount.Text = count.ToString();


                        }



                    }



                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                finally
                {
                    connect.Close();
                }








            }
        }
        private void displayTotalProduct()
        {
            if (checkConnection())
            {

                try
                {

                    connect.Open();
                    string selectTotalUser = "SELECT COUNT(ProductID) FROM Products ";
                    using (SqlCommand cmd = new SqlCommand(selectTotalUser, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            allProductCount.Text = count.ToString();


                        }


                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                finally
                {
                    connect.Close();
                }


            }
        }
        private void displayTotalCustomer()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(CustomerID) FROM Customers";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result == DBNull.Value || result == null)
                            allCustomerCount.Text = "0";
                        else
                            allCustomerCount.Text = Convert.ToInt32(result).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors " + ex.Message, "Error Message",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void displayTotalIncome()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT SUM(TotalPrice) FROM Customers";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result == DBNull.Value || result == null)
                            allIncome.Text = "0";
                        else
                            allIncome.Text = Convert.ToDecimal(result).ToString("N0"); // Định dạng tiền tệ đẹp hơn
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errors " + ex.Message, "Error Message",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Error ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (MessageBox.Show("Are you want to remove all Customers data ?", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();
                            string deleteAll = "DELETE FROM Customers;" + " DBCC CHECKIDENT ('Customers', RESEED, 0);";
                            using (SqlCommand cmd = new SqlCommand(deleteAll, connect))
                            {


                                cmd.ExecuteNonQuery();
                                displayCustomerData();
                                displayTotalCustomer();
                                displayTotalIncome();

                            }

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Error " + ex.Message, "Errorss Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        finally
                        {
                            connect.Close();

                        }
                    }
                }




            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (updateName.Text == "")
            {
                MessageBox.Show("Please fill up all empty fields", "Error messaga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Want to update Data" + getID + " ?", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();


                            string updateData = "UPDATE Customers SET CustomerName = @customername" + " WHERE ID = @id";
                            using (SqlCommand updateD = new SqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@customername", updateName.Text.Trim());

                                updateD.Parameters.AddWithValue("@id", getID);

                                updateD.ExecuteNonQuery();
                                displayCustomerData();

                                MessageBox.Show("Update successfully ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection failed ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }




                }

            }
        }
        private int getID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if (row.Cells[0].Value == null || row.Cells[0].Value == DBNull.Value)
            {
                MessageBox.Show("Selected row is empty or invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                getID = Convert.ToInt32(row.Cells[0].Value);

                if (row.Cells.Count > 4 && row.Cells[4].Value != null)
                {
                    updateName.Text = row.Cells[4].Value.ToString();
                }
                else
                {
                    updateName.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading cell: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {

        }

        private void updateName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
