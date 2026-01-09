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

namespace QuanliTapHoaPro
{
    public partial class Login : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public Login()
        {
            InitializeComponent();
            hidePass();
            admin();
            this.AcceptButton = loginBtn;
        }
        private void hidePass()
        {
            passWord.PasswordChar = '*';

        }
        private void admin()
        {
            userName.Text = "admin";
            passWord.Text = "12345678";
        }


        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            passWord.PasswordChar = showPassword.Checked ? '\0' : '*';

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (userName.Text == "" || passWord.Text == "")
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
                        string selectData = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@username", userName.Text.Trim());
                            cmd.Parameters.AddWithValue("@password", passWord.Text.Trim());


                            //                           SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            //                           DataTable dt = new DataTable();
                            //                          adapter.Fill(dt);
                            //                           if (dt.Rows.Count > 0)
                            //                           {
                            //                               MessageBox.Show("Login sucessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                               Main mainForm = new Main();
                            //                               mainForm.Show();
                            //                               this.Hide();
                            //                           }
                            //                          else
                            //                          {
                            //                              MessageBox.Show("There is a error from inputs or you dont have permissions", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //                          }

                            int row = (int)cmd.ExecuteScalar();
                            if (row > 0)
                            {
                                string selectRole = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";

                                using (SqlCommand getRole = new SqlCommand(selectRole, connect))
                                {
                                    getRole.Parameters.AddWithValue("@username", userName.Text.Trim());
                                    getRole.Parameters.AddWithValue("@password", passWord.Text.Trim());

                                    string userRole = getRole.ExecuteScalar() as string;
                                    MessageBox.Show("Login sucessfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (userRole == "Admin")
                                    {
                                        Main mainForm = new Main();
                                        mainForm.Show();
                                        this.Hide();
                                    }
                                    else if (userRole == "Employee")
                                    {
                                        EmployeeMain maiForm = new EmployeeMain();
                                        maiForm.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("You dont have permission", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("There is a error from inputs or you dont have permissions", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
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
