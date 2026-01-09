using QuanLiTapHoa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanliTapHoaPro
{
    public partial class Categories : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public Categories()
        {
            InitializeComponent();
            displayData();

        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    displayData();
                });
            }
            else
            {
                displayData();
            }
        }
        public void displayData()
        {
            showDateCategories uData = new showDateCategories();
            List<showDateCategories> listData = uData.AllData();
            dataGridView1.DataSource = listData;

        }
        private int getID = 0;

       

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (categoryName.Text == "")
            {
                MessageBox.Show("Please fill up empty space", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();
                        string checkCategory = "SELECT * FROM Categories WHERE CategoryName = @categoryname";
                        using (SqlCommand cmd = new SqlCommand(checkCategory, connect))
                        {
                            cmd.Parameters.AddWithValue("@categoryname", categoryName.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show(categoryName.Text + " is alredy in database", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                string insertData = "INSERT INTO Categories(CategoryName)" + " VALUES(@categoryname)";
                                using (SqlCommand insertD = new SqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@categoryname", categoryName.Text.Trim());

                                    insertD.ExecuteNonQuery();
                                    ClearData();
                                    displayData();
                                    MessageBox.Show("Add new Category successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
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

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (categoryName.Text == "")
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


                            string updateData = "UPDATE Categories SET CategoryName = @categoryname" + " WHERE CategoryID = @categoryid";
                            using (SqlCommand updateD = new SqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@categoryname", categoryName.Text.Trim());

                                updateD.Parameters.AddWithValue("@categoryid", getID);

                                updateD.ExecuteNonQuery();
                                ClearData();
                                displayData();
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

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user (ID: " + getID + ")?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();
                        string deleteQuery = "DELETE FROM Categories WHERE CategoryID = @categoryid";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connect))
                        {
                            deleteCmd.Parameters.AddWithValue("@categoryid", getID);

                            int rows = deleteCmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("User deleted successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearData();
                                displayData();
                                getID = 0;
                            }
                            else
                            {
                                MessageBox.Show("No record deleted (check UserID).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Delete failed: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearData();

        }

        public bool checkConnection()
        {
            if (connect.State == ConnectionState.Closed || connect.State == ConnectionState.Broken)
            {
                return true;
            }
            else { return false; }
        }

        public void ClearData()
        {
            categoryName.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int id = (int)row.Cells["CategoryID"].Value;
                getID = id;
                string categoryname = row.Cells[1].Value.ToString();
                string image = row.Cells[2].ToString();
                string date = row.Cells[3].ToString();
                categoryName.Text = categoryname;


            }
        }
    }
}
