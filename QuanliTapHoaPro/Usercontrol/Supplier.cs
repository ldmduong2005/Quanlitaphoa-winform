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

namespace QuanliTapHoaPro
{
    public partial class Supplier : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public Supplier()
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
            showSupplierData uData = new showSupplierData();
            List<showSupplierData> listData = uData.AllData();
            dataGridView1.DataSource = listData;

        }
        private int getID = 0;
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
            addSupplierID.Text = "";
            addEmail.Text = "";
            addAddress.Text = "";
            addPhonenumber.Text = "";
        }

      

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (addSupplierID.Text == "" || addEmail.Text == "" || addPhonenumber.Text == "" || addAddress.Text == "" || addSupplierName.Text == "")
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
                        string checkSupplierID = "SELECT * FROM Suppliers WHERE SupplierID = @supplierid";
                        using (SqlCommand cmd = new SqlCommand(checkSupplierID, connect))
                        {
                            cmd.Parameters.AddWithValue("@supplierid", addSupplierID.Text.Trim());
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show(addSupplierID.Text + " is already in database", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                string insertData = "INSERT INTO Suppliers (SupplierID, SupplierName, Phone, Email, Address )" + " VALUES(@supplierid, @suppliername, @phone, @email, @address)";
                                using (SqlCommand insertD = new SqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@supplierid", addSupplierID.Text.Trim());
                                    insertD.Parameters.AddWithValue("@suppliername", addSupplierName.Text.Trim());
                                    insertD.Parameters.AddWithValue("@phone", addPhonenumber.Text.Trim());
                                    insertD.Parameters.AddWithValue("@email", addEmail.Text.Trim());
                                    insertD.Parameters.AddWithValue("@address", addAddress.Text.Trim());

                                    insertD.ExecuteNonQuery();
                                    ClearData();
                                    displayData();
                                    MessageBox.Show("Add new Supplier successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Connection failed " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            if (addAddress.Text == "" || addEmail.Text == "" || addPhonenumber.Text == "" || addSupplierID.Text == "" || addSupplierName.Text == "")
            {
                MessageBox.Show("Please fill up all empty fields", "Error messaga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Want to update Data ?", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();


                            string updateData = "UPDATE Suppliers SET SupplierID = @supplierid, Phone = @phone, Email =@email, Address =@address  WHERE ID = @id";
                            using (SqlCommand updateD = new SqlCommand(updateData, connect))
                            {

                                updateD.Parameters.AddWithValue("@supplierid", addSupplierID.Text.Trim());
                                updateD.Parameters.AddWithValue("@suppliername", addSupplierName.Text.Trim());
                                updateD.Parameters.AddWithValue("@phone", addPhonenumber.Text.Trim());
                                updateD.Parameters.AddWithValue("@email", addEmail.Text.Trim());
                                updateD.Parameters.AddWithValue("@address", addAddress.Text.Trim());
                                updateD.Parameters.AddWithValue("@id", getID);



                                updateD.ExecuteNonQuery();
                                ClearData();
                                displayData();
                                MessageBox.Show("Update successfully ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Connection failed " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        string deleteQuery = "DELETE FROM Suppliers WHERE ID = @id";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connect))
                        {
                            deleteCmd.Parameters.AddWithValue("@id", getID);

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

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int id = (int)row.Cells[0].Value;
                getID = id;
                string supplierid = row.Cells[1].Value.ToString();
                string suppliername = row.Cells[2].Value.ToString();
                string phone = row.Cells[3].Value.ToString();
                string email = row.Cells[4].Value.ToString();
                string address = row.Cells[5].Value.ToString();


                addSupplierID.Text = supplierid;
                addSupplierName.Text = suppliername;
                addPhonenumber.Text = phone;
                addEmail.Text = email;
                addAddress.Text = address;



            }
        }
    }
}
