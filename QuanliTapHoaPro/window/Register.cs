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

namespace QuanliTapHoaPro
{

    public partial class Register : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public Register()
        {
            InitializeComponent();
            hidePass();
            this.AcceptButton = SignupBtn;
        }

        private void SignupBtn_Click(object sender, EventArgs e)
        {
            if (newUsername.Text == "" || newPassword.Text == "" || newPassword1.Text == "")
            {
                MessageBox.Show("Please fill up all empty fields", "Error messaga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();
                        string checkUsername = "SELECT * FROM Users WHERE Username = @username";
                        using (SqlCommand cmd = new SqlCommand(checkUsername, connect))
                        {
                            cmd.Parameters.AddWithValue("@username", newUsername.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show(newUsername.Text.Trim() + " is alredy takem", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (newPassword.Text.Length < 8)
                            {
                                MessageBox.Show("Invalid length of password, need at least 8 single character", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else if (newPassword.Text != newPassword1.Text)
                            {
                                MessageBox.Show("You need to input same password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                string insertData = "INSERT INTO Users (Username, Password, Role, Fullname)" + "VALUES(@username, @password, @role, @fullname)";
                                using (SqlCommand insertD = new SqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@username", newUsername.Text.Trim());
                                    insertD.Parameters.AddWithValue("@password", newPassword.Text.Trim());
                                    insertD.Parameters.AddWithValue("@role", "Employee");
                                    insertD.Parameters.AddWithValue("@fullname", "N/A");
                                    DateTime today = DateTime.Today;

                                    insertD.ExecuteNonQuery();

                                    MessageBox.Show("Register Successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Login loginForm = new Login();
                                    loginForm.Show();
                                    this.Hide();

                                }




                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connecttion failed" + ex, "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    finally
                    {
                        connect.Close();
                    }

                }
            }
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            newPassword.PasswordChar = showPassword.Checked ? '\0' : '*';
            newPassword1.PasswordChar = showPassword.Checked ? '\0' : '*';
        }

        private void newUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void newPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void newPassword1_TextChanged(object sender, EventArgs e)
        {

        }

        private void signBtn_Click(object sender, EventArgs e)
        {
            Login loginFrom = new Login();
            loginFrom.Show();
            this.Hide();
        }
        private void hidePass()
        {
            newPassword.PasswordChar = '*';
            newPassword1.PasswordChar = '*';
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
    }
}
