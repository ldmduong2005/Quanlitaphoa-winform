using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using QuanLiTapHoa;

namespace QuanliTapHoaPro
{
    public partial class ControlUser : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public ControlUser()
        {
            InitializeComponent();
            displayAllData();
        }

        public bool checkConnection()
        {
            if (connect.State == ConnectionState.Closed || connect.State == ConnectionState.Broken)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;

            }
            displayAllData();




        }

        public void displayAllData()
        {
            showData uData = new showData();
            List<showData> listData = uData.AllData();
            dataGridView1.DataSource = listData;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (addUsername.Text == "" || addPassword.Text == "" || selectRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill up all empty fields", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();
                      
                        string checkUsername = "SELECT * FROM Users WHERE Username = @username ";

                        using (SqlCommand cmd = new SqlCommand(checkUsername, connect))
                        {
                            cmd.Parameters.AddWithValue("@username", addUsername.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show(addUsername.Text.Trim() + " is alredy takem", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {





                                string insertData = "INSERT INTO Users (Username, Password, Role, Fullname )" + "VALUES(@username, @password, @role, @fullname)";
                                using (SqlCommand insertD = new SqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@username", addUsername.Text.Trim());
                                    insertD.Parameters.AddWithValue("@password", addPassword.Text.Trim());
                                    insertD.Parameters.AddWithValue("@role", selectRole.Text.ToString());
                                    insertD.Parameters.AddWithValue("@fullname", addFullname.Text.Trim());
                                    


                                    insertD.ExecuteNonQuery();
                                    clearField();
                                    displayAllData();
                                    MessageBox.Show("Add Users successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (addUsername.Text == "" || addPassword.Text == "" || selectRole.SelectedIndex == -1)
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


                            string updateData = "UPDATE Users SET Username = @username," + "Password = @password, Role = @role, Fullname = @fullname  WHERE UserID = @userID";
                            using (SqlCommand updateD = new SqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@username", addUsername.Text.Trim());
                                updateD.Parameters.AddWithValue("@password", addPassword.Text.Trim());
                                updateD.Parameters.AddWithValue("@role", selectRole.Text.Trim());
                                updateD.Parameters.AddWithValue("@fullname", addFullname.Text.Trim());
                                updateD.Parameters.AddWithValue("@userID", getID);


                                updateD.ExecuteNonQuery();
                                clearField();
                                displayAllData();
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
                        string deleteQuery = "DELETE FROM Users WHERE UserID = @userID";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connect))
                        {
                            deleteCmd.Parameters.AddWithValue("@userID", getID);

                            int rows = deleteCmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("User deleted successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearField();
                                displayAllData();
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
            clearField();
        }
        private int getID = 0;

        public void clearField()
        {
            addUsername.Text = "";
            addPassword.Text = "";
            addFullname.Text = "";
            selectRole.SelectedIndex = -1;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int id = (int)row.Cells[0].Value;
                getID = id;
                string username = row.Cells[1].Value.ToString();
                string password = row.Cells[2].Value.ToString();
                string role = row.Cells[3].Value.ToString();
                string fullname = row.Cells[4].Value.ToString();


                addUsername.Text = username;
                addPassword.Text = password;
                addFullname.Text = fullname;
                selectRole.Text = role;

            }
        }
    }
}
