using QuanLiTapHoa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanliTapHoaPro
{
    public partial class ProductMenu : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source = localhost; Initial Catalog = Quanli; Integrated Security = True; Encrypt=False");

        public ProductMenu()
        {
            InitializeComponent();
            displayCategory();
            displaySupplierID();
            displayData();
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
                Invoke((MethodInvoker)delegate
                {
                    displayData();
                    displayCategory();
                    displaySupplierID();
                });
            }
            else
            {
                displayData();
                displayCategory();
                displaySupplierID();
            }
        }

        public void displayData()
        {
            showDataProduct showData = new showDataProduct();
            List<showDataProduct> listData = showData.ProductData();
            dataGridView1.DataSource = listData;
            dataGridView1.Columns["ImagePath"].Visible = false;
        }

        public void displayCategory()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();
                    string select = "SELECT * FROM Categories";
                    using (SqlCommand cmd = new SqlCommand(select, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        Category.Items.Clear();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Category.Items.Add(reader["CategoryName"].ToString());
                            }
                        }



                    }
                }
                catch
                {

                }
                finally
                {


                    connect.Close();
                }

            }
        }

        public void displaySupplierID()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();
                    string select = "SELECT * FROM Suppliers";
                    using (SqlCommand cmd = new SqlCommand(select, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        addSupplierID.Items.Clear();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                addSupplierID.Items.Add(reader["SupplierID"].ToString());
                            }
                        }



                    }
                }
                catch
                {

                }
                finally
                {


                    connect.Close();
                }

            }
        }

        private int getID = 0;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                getID = (int)row.Cells[0].Value;
                ProductsQR.Text = row.Cells[1].Value.ToString();
                addProductName.Text = row.Cells[2].Value.ToString();
                Category.Text = row.Cells[3].Value.ToString();
                Units.Text = row.Cells[4].Value.ToString();
                Price.Text = row.Cells[5].Value.ToString();
                addSupplierID.Text = row.Cells[6].Value.ToString();
                Status.Text = row.Cells[7].Value.ToString();


                string imagepath = row.Cells[9].Value.ToString();
                try
                {
                    if (imagepath != null)
                    {
                        ImageProduct.Image = Image.FromFile(imagepath);
                        ImageProduct.ImageLocation = imagepath;
                    }
                }
                catch
                {


                    MessageBox.Show("Error101", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }



            }
        }

        public void clearData()
        {
            ProductsQR.Text = "";
            addProductName.Text = "";
            addSupplierID.SelectedIndex = -1;
            Category.SelectedIndex = -1;
            Price.Text = "";
            Units.Text = "";
            ImageProduct.Image = null;

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (addProductName.Text == "" || ProductsQR.Text == "" || Price.Text.ToString() == "" || Units.Text.ToString() == "" || Status.SelectedIndex == -1 || Category.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill up empty space", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT * FROM Products WHERE ProductQR = @productqr";
                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@productqr", ProductsQR.Text.Trim());
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("ProductID" + ProductsQR.Text.Trim() + " is existing", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            string relativePath = Path.Combine("Product_Direct", ProductsQR.Text.Trim() + ".jpg");
                            string path = Path.Combine(baseDirectory, relativePath);
                            string directoryPath = Path.GetDirectoryName(path);

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            File.Copy(ImageProduct.ImageLocation, path, true);

                            string insertData = "INSERT INTO Products (ProductQR, ProductName, Category, Unit, Price,SupplierID, Status, DateInsert, Image) " + "VALUES(@productqr,@productname, @category, @unit, @price,@supplierid, @status, @dateinsert, @image)";
                            using (SqlCommand insertD = new SqlCommand(insertData, connect))
                            {
                                insertD.Parameters.AddWithValue("@productqr", ProductsQR.Text.Trim());
                                insertD.Parameters.AddWithValue("@productname", addProductName.Text.Trim());
                                insertD.Parameters.AddWithValue("@category", Category.SelectedItem);
                                insertD.Parameters.AddWithValue("@unit", Units.Text.Trim());
                                insertD.Parameters.AddWithValue("@price", Convert.ToDecimal(Price.Text.Trim()));
                                insertD.Parameters.AddWithValue("@supplierid", addSupplierID.SelectedItem);
                                insertD.Parameters.AddWithValue("@status", Status.SelectedItem);
                                insertD.Parameters.AddWithValue("@image", path);

                                DateTime now = DateTime.Now;
                                insertD.Parameters.AddWithValue("@dateinsert", now);
                                insertD.ExecuteNonQuery();
                                clearData();
                                displayData();
                                MessageBox.Show(" Add successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);




                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(" Add failed", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    connect.Close();
                }

            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (addProductName.Text == "" || ProductsQR.Text == "" || Price.Text.ToString() == "" || Units.Text.ToString() == "" || Status.SelectedIndex == -1 || Category.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill up empty space", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    connect.Open();


                    string updateData = "UPDATE Products SET ProductQR = @productqr, ProductName = @productname" + ", Category =@category, Unit = @unit, Price =@price, SupplierID = @supplierid, Status = @status WHERE ProductID =@id";
                    using (SqlCommand updateD = new SqlCommand(updateData, connect))
                    {
                        updateD.Parameters.AddWithValue("@productqr", ProductsQR.Text.Trim());
                        updateD.Parameters.AddWithValue("@productname", addProductName.Text.Trim());
                        updateD.Parameters.AddWithValue("@category", Category.SelectedItem);
                        updateD.Parameters.AddWithValue("@unit", Units.Text.Trim());
                        updateD.Parameters.AddWithValue("@price", Convert.ToDecimal(Price.Text.Trim()));
                        updateD.Parameters.AddWithValue("@supplierid", addSupplierID.SelectedItem);


                        updateD.Parameters.AddWithValue("@status", Status.SelectedItem);


                        updateD.Parameters.AddWithValue("@id", getID);

                        updateD.ExecuteNonQuery();
                        clearData();
                        displayData();
                        MessageBox.Show(" Update successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }



                }



                catch (Exception ex)
                {
                    MessageBox.Show(" Update failed", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user (ID: " + getID + ")?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();
                        string deleteQuery = "DELETE FROM Products WHERE ProductID = @ID";

                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connect))
                        {
                            deleteCmd.Parameters.AddWithValue("@ID", getID);

                            int rows = deleteCmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("Product deleted successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearData();
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

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            clearData();

        }

        private void ImageBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg ;*.png)|*.jpg; *.png";
                string imagePath = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    ImageProduct.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to recieve picture", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void displaybyName()
        {
            if(checkConnection())
            {
                try
                {
                    connect.Open();
                    string selectName = "SELECT * FROM Products WHERE ProductName LIKE @name";
                    if (searching.Text== "")
                    {
                        displayData();
                        return;
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand(selectName, connect))
                        {

                            cmd.Parameters.AddWithValue("@name", "%"+searching.Text.Trim()+"%");

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            dataGridView1.DataSource = table;
                            dataGridView1.Columns["Image"].Visible = false;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to recieve product" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void searching_TextChanged(object sender, EventArgs e)
        {
            displaybyName();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                getID = (int)row.Cells[0].Value;
                ProductsQR.Text = row.Cells[1].Value.ToString();
                addProductName.Text = row.Cells[2].Value.ToString();
                Category.Text = row.Cells[3].Value.ToString();
                Units.Text = row.Cells[4].Value.ToString();
                Price.Text = row.Cells[5].Value.ToString();
                addSupplierID.Text = row.Cells[6].Value.ToString();
                Status.Text = row.Cells[7].Value.ToString();


                string imagepath = row.Cells[9].Value.ToString();
                try
                {
                    if (imagepath != null)
                    {
                        ImageProduct.Image = Image.FromFile(imagepath);
                        ImageProduct.ImageLocation = imagepath;
                    }
                }
                catch
                {


                    MessageBox.Show("Error101", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }



            }
        }
    }
}
