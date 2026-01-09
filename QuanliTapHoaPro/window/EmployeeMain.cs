using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanliTapHoaPro
{
    public partial class EmployeeMain : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost;Initial Catalog=Quanli;Integrated Security=True;Encrypt=False");

        public EmployeeMain()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you want to log out?", "Please confirm it", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Login loginForm = new Login();
                loginForm.Show();
            }
            else
            {
                return;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            dashboardc2.Visible = true;
            categories2.Visible = false;
            employeeOrder2.Visible = false;
            productMenu2.Visible = false;
            supplier2.Visible = false;

            Dashboardc adForm = dashboardc2 as Dashboardc;
            if (adForm != null)
            {
                adForm.refreshData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboardc2.Visible = false;
            categories2.Visible = true;
            employeeOrder2.Visible = false;
            productMenu2.Visible = false;
            supplier2.Visible = false;
            Categories bdForm = categories2 as Categories;
            if (bdForm != null)
            {
                bdForm.refreshData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dashboardc2.Visible = false;
            categories2.Visible = false;
            employeeOrder2.Visible = false;
            productMenu2.Visible = true;
            supplier2.Visible = false;
            ProductMenu cdForm = productMenu2 as ProductMenu;
            if (cdForm != null)
            {
                cdForm.refreshData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dashboardc2.Visible = false;
            categories2.Visible = false;
            employeeOrder2.Visible = false;
            productMenu2.Visible = false;
            supplier2.Visible = true;
            Supplier ddFrom = supplier2 as Supplier;
            if (ddFrom != null)
            {
                ddFrom.refreshData();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dashboardc2.Visible = false;
            categories2.Visible = false;
            employeeOrder2.Visible = true;
            productMenu2.Visible = false;
            supplier2.Visible = false;
            EmployeeOrder edForm = employeeOrder2 as EmployeeOrder;
            if (edForm != null) { edForm.refreshData(); }
        }



       

        private void categories2_Load(object sender, EventArgs e)
        {

        }

      

        private void employeeOrder2_Load(object sender, EventArgs e)
        {

        }

        private void productMenu2_Load(object sender, EventArgs e)
        {

        }

        private void supplier2_Load(object sender, EventArgs e)
        {

        }

        private void dashboardc2_Load(object sender, EventArgs e)
        {

        }
    }
}
