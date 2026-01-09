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
using System.Net.NetworkInformation;
using System.Windows.Forms.VisualStyles;
using System.Net.Sockets;
using QuanLiTapHoa;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace QuanliTapHoaPro
{

    public partial class EmployeeOrder : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public EmployeeOrder()
        {
            InitializeComponent();
            displayAvailable();
            displayCategory();
            displayOrder();
            displaytotalPrice();

        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    displayCategory();
                    displayAvailable();
                    displayOrder();
                    displaytotalPrice();
                });
            }
            else
            {
                displayCategory();
                displayAvailable();
                displayOrder();
                displaytotalPrice();
            }
        }
        public void displayAvailable()
        {
            showDataProduct showData = new showDataProduct();
            List<showDataProduct> listData = showData.allAvailableProduct();
            dataGridView1.DataSource = listData;
            dataGridView1.Columns["ImagePath"].Visible = false;
            dataGridView1.Columns["ID"].Visible = false;

            currentMode = DisplayMode.Default;


        }
        public void displayOrder()
        {
            showDataOrder showData1 = new showDataOrder();
            List<showDataOrder> listOrderData = showData1.OrderData();
            dataGridView2.DataSource = listOrderData;
            dataGridView2.Columns["ID"].Visible = false;



        }
        public bool checkConnection()
        {
            if (connect.State == ConnectionState.Closed || connect.State == ConnectionState.Broken)
            {
                return true;

            }
            else { return false; }
        }
        private void displayCategory()
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
                        selectCategory.Items.Clear();
                        selectCategory.Items.Add("None");
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                selectCategory.Items.Add(reader["CategoryName"].ToString());
                            }
                        }



                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("CAnt take item" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {


                    connect.Close();
                }

            }
        }

        private void selectCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectProductID.SelectedIndex = -1;
            selectProductID.Items.Clear();
            categoryProductName.Text = "";
            orderPrice.Text = "";
            quantity.Value = 0;
            string selectedValue = selectCategory.SelectedItem as string;

            if (selectedValue != null)
            {
                if (checkConnection())
                {
                    try
                    {
                        connect.Open();
                        string selectData = "SELECT * FROM Products WHERE Category = @category AND Status = @status";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@category", selectCategory.Text.Trim());
                            cmd.Parameters.AddWithValue("@status", "Available");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string value = reader["ProductQR"].ToString();
                                    selectProductID.Items.Add(value);
                                }

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cant take item proid" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        connect.Close();
                    }
                    if (selectCategory.Text != "None")
                    {
                        displayAvailableByCategory(selectedValue);
                    }
                    else
                    {
                        displayAvailable();
                    }
                }


            }
        }

        private void selectProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = selectProductID.SelectedItem as string;
            if (checkConnection())
            {
                if (selectedValue != null)
                {
                    try
                    {
                        connect.Open();
                        string selectData = "SELECT * FROM Products WHERE ProductQR = @productqr AND Status = @status";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@productqr", selectedValue);
                            cmd.Parameters.AddWithValue("@status", "Available");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string prodName = reader["ProductName"].ToString();
                                    decimal prodPrice = Convert.ToDecimal(reader["Price"]);
                                    string prodID = reader["ProductQR"].ToString();

                                    selectProductID.Text = prodID;
                                    categoryProductName.Text = prodName;
                                    orderPrice.Text = prodPrice.ToString("0.00");
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cant take item proid and value" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }



                }



            }


        }


        private int idGen = 0;
        public void IDGenerator()
        {
            string connectionString =
                @"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False";

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                string query = "SELECT MAX(CustomerID) FROM Customers";
                SqlCommand cmd = new SqlCommand(query, connect);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    int temp = Convert.ToInt32(result);
                    if (temp == 0)
                    {
                        idGen = 1;



                    }
                    else
                    {
                        idGen = temp + 1;
                    }
                }
                else
                {
                    idGen = 1;
                }
            }
        }

        private decimal totalPrice = 0;
        public void displaytotalPrice()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT SUM(TotalPrice) FROM Orders WHERE  CustomerID = @customerid";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {

                        cmd.Parameters.AddWithValue("@customerid", idGen);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalPrice = Convert.ToDecimal(result);
                            orderTotalPrice.Text = totalPrice.ToString();

                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private int getProdID;
        public void Clear()
        {
            selectCategory.SelectedIndex = -1;
            selectProductID.SelectedIndex = -1;
            categoryProductName.Text = "";
            quantity.Value = 0;
            orderPrice.Text = "";
            displayAvailable();

        }
        public void displayAvailableByCategory(string category)
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();
                    string query = "SELECT * FROM Products WHERE Category = @category AND Status = @status";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@status", "Available");

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dataGridView1.DataSource = table;
                    }
                    currentMode = DisplayMode.CategoryFilter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading filtered products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void addEmployeeBtn_Click(object sender, EventArgs e)
        {
            IDGenerator();

            if (selectCategory.SelectedIndex == -1 || selectProductID.SelectedIndex == -1 || quantity.Value == 0)
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
                        decimal getPrice = 0;
                        int currentUnit = 0;
                        string selectOrder = " SELECT * FROM Products WHERE ProductQR = @productqr";
                        using (SqlCommand getOrder = new SqlCommand(selectOrder, connect))
                        {
                            getOrder.Parameters.AddWithValue("@productqr", selectProductID.SelectedItem);

                            using (SqlDataReader reader = getOrder.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    object rawValue = reader["Price"];

                                    if (rawValue != DBNull.Value)
                                    {
                                        getPrice = Convert.ToDecimal(rawValue);

                                    }
                                    if (reader["Unit"] != DBNull.Value)
                                    {

                                        currentUnit = Convert.ToInt32(reader["Unit"]);
                                    }

                                }


                            }
                        }
                        int orderQuantity = (int)quantity.Value;
                        if (orderQuantity > currentUnit)
                        {
                            MessageBox.Show("Not enough units", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;

                        }
                        string insertData = "INSERT INTO Orders(ProductQR, ProductName, Category,CustomerID, Quantity, UnitPrice,  OrderDate, TotalPrice)" + " VALUES(@productqr, @productname,@category,@customerid, @quantity,@unitprice,  @orderdate, @totalprice)";
                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@customerid", idGen);
                            cmd.Parameters.AddWithValue("@productqr", selectProductID.SelectedItem);
                            cmd.Parameters.AddWithValue("@productname", categoryProductName.Text.Trim());
                            cmd.Parameters.AddWithValue("@category", selectCategory.SelectedItem);
                            cmd.Parameters.AddWithValue("@quantity", (int)quantity.Value);
                            cmd.Parameters.AddWithValue("@unitprice", getPrice);
                            decimal calculatedTotal = getPrice * (decimal)quantity.Value;
                            MessageBox.Show($"Order added successfully!\nTotal: {calculatedTotal:C2}",
                                           "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            DateTime now = DateTime.Now;
                            cmd.Parameters.AddWithValue("@orderdate", now);
                            cmd.Parameters.AddWithValue("@totalprice", calculatedTotal);
                            cmd.ExecuteNonQuery();



                        }
                        string updateData;

                        if (orderQuantity == currentUnit)
                        {

                            updateData = @"UPDATE Products 
                           SET Unit = 0, Status = 'Not Available' 
                           WHERE ProductQR = @productqr";
                            using (SqlCommand updateCmd = new SqlCommand(updateData, connect))
                            {
                                updateCmd.Parameters.AddWithValue("@orderQuantity", orderQuantity);
                                updateCmd.Parameters.AddWithValue("@productqr", selectProductID.SelectedItem);
                                updateCmd.ExecuteNonQuery();
                            }
                        }




                    }
                    catch (Exception ex)
                    {


                        MessageBox.Show("Error" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {

                        connect.Close();

                    }
                }
            }
            displayOrder();
            displaytotalPrice();
            displayAvailable();
        }

        private void removeEmployeeBtn_Click(object sender, EventArgs e)
        {
            if (getProdID == 0)
            {
                MessageBox.Show("Please choose item first ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you want to remove" + getProdID + " ?", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();
                        string deleteData = "DELETE FROM Orders WHERE OrderID = @orderid";
                        using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                        {
                            cmd.Parameters.AddWithValue("@orderid", getProdID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Remove sucessfully ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        connect.Close();
                    }




                }
            }
            displayOrder();
            displaytotalPrice();
        }

        private void removeAllOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count < 0)
            {
                MessageBox.Show("Error ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (MessageBox.Show("Are you want to remove all ?", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();
                            string deleteAll = "DELETE FROM Orders;" + " DBCC CHECKIDENT ('Orders', RESEED, 0);";
                            using (SqlCommand cmd = new SqlCommand(deleteAll, connect))
                            {


                                cmd.ExecuteNonQuery();
                                displayOrder();
                                Clear();

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
                else
                {
                    return;
                }



            }
            displaytotalPrice();
        }

        private void clearEmployeeBtn_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void orderAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    decimal getAmount = Convert.ToDecimal(orderAmount.Text);
                    decimal getChange = (getAmount - totalPrice);
                    if (getChange <= -1)
                    {
                        orderAmount.Text = "";
                        orderChange.Text = "";

                    }
                    else
                    {
                        orderChange.Text = getChange.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Errorss Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void payOrderBtn_Click(object sender, EventArgs e)
        {
            if (orderAmount.Text == "" || dataGridView2.Rows.Count < 0)
            {
                MessageBox.Show("Error ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (MessageBox.Show("Are you want to pay order" + getProdID + " ?", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (checkConnection())
                    {
                        try
                        {
                            connect.Open();
                            string insertData = "INSERT INTO Customers(CustomerID, TotalPrice, Amount, Change, OrderDate) " + " VALUES(@customerid, @totalprice, @amount, @change, @orderdate)";
                            using (SqlCommand cmd = new SqlCommand(insertData, connect))
                            {
                                cmd.Parameters.AddWithValue("@customerid", idGen);
                                cmd.Parameters.AddWithValue("@totalprice", Convert.ToDecimal(orderTotalPrice.Text.Trim()));
                                cmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(orderAmount.Text.Trim()));
                                cmd.Parameters.AddWithValue("@change", Convert.ToDecimal(orderChange.Text.Trim()));

                                DateTime now = DateTime.Now;
                                cmd.Parameters.AddWithValue("@orderdate", now);

                                cmd.ExecuteNonQuery();
                                displayOrder();
                                orderAmount.Text = "";
                                orderTotalPrice.Text = "";
                                orderChange.Text = "";
                                Clear();

                            }


                            foreach (DataGridViewRow row in dataGridView2.Rows)
                            {
                                if (row.Cells["ProductID"].Value != null && row.Cells["Quantity"].Value != null)
                                {
                                    string productQR = row.Cells["ProductID"].Value.ToString();
                                    int orderQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                                    string updateData = "UPDATE Products SET Unit = Unit - @orderQuantity" + " WHERE ProductQR = @productqr AND Status = @status";
                                    using (SqlCommand updateD = new SqlCommand(updateData, connect))
                                    {
                                        updateD.Parameters.AddWithValue("@orderQuantity", orderQuantity);
                                        updateD.Parameters.AddWithValue("@productqr", productQR);
                                        updateD.Parameters.AddWithValue("@status", "Available");

                                        updateD.ExecuteNonQuery();
                                        Clear();
                                        displayOrder();
                                        displayAvailable();

                                    }
                                }
                            }

                            string deleteAll = "DELETE FROM Orders;" + " DBCC CHECKIDENT ('Orders', RESEED, 0);";
                            using (SqlCommand cmd = new SqlCommand(deleteAll, connect))
                            {


                                cmd.ExecuteNonQuery();
                                displayOrder();
                                Clear();

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
       
        private void displaybyName()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Open();
                    string selectName = "SELECT * FROM Products WHERE ProductName LIKE @name AND Status = 'Available'" ;
                    if (searching.Text == "")
                    {
                        displayAvailable();
                    }
                    else
                    {
                       
                        currentMode = DisplayMode.SearchFilter;
                        using (SqlCommand cmd = new SqlCommand(selectName, connect))
                        {

                            cmd.Parameters.AddWithValue("@name", "%" + searching.Text.Trim() + "%");

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                       
                            dataGridView1.DataSource = table;
                            dataGridView1.CellClick -= dataGridView1_CellClick; 
                            dataGridView1.CellClick += dataGridView1_CellClick;
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



        private enum DisplayMode
        {
            Default,        
            CategoryFilter, 
            SearchFilter    
        }

        private DisplayMode currentMode = DisplayMode.Default;


        

        private void orderAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmployeeOrder_Load(object sender, EventArgs e)
        {

        }

       

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string category = "";
                string productID = "";
                string productName = "";
                string productQR = "";
                decimal price = 0;
                switch (currentMode)
                {
                    case DisplayMode.CategoryFilter:
                        category = row.Cells["Category"].Value.ToString();
                        productID = row.Cells["ProductQR"].Value.ToString();
                        productName = row.Cells["ProductName"].Value.ToString();
                        price = Convert.ToDecimal(row.Cells["Price"].Value);
                        break;
                    case DisplayMode.SearchFilter:
                        category = row.Cells["Category"].Value.ToString();
                        productID = row.Cells["ProductQR"].Value.ToString();
                        productName = row.Cells["ProductName"].Value.ToString();
                        price = Convert.ToDecimal(row.Cells["Price"].Value);
                        break;
                    case DisplayMode.Default:
                        category = row.Cells["Category"].Value.ToString();
                        productID = row.Cells["ProductID"].Value.ToString();
                        productName = row.Cells["ProductName"].Value.ToString();
                        price = Convert.ToDecimal(row.Cells["Price"].Value);
                        break;

                }

                selectCategory.Text = category;
                selectProductID.Text = productID;
                categoryProductName.Text = productName;
                orderPrice.Text = price.ToString("0.00");

            }
        }
    }
}
