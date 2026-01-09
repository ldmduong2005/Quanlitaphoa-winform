using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanliTapHoaPro
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dashboardc1.Visible = false;
            categories1.Visible = false;
            employeeOrder1.Visible = false;
            productMenu1.Visible = false;
            supplier1.Visible = false;
            controlUser1.Visible = true;
            ControlUser fdForm = controlUser1 as ControlUser;
            if (fdForm != null)
            {
                fdForm.refreshData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dashboardc1.Visible = true;
            categories1.Visible = false;
            employeeOrder1.Visible = false;
            productMenu1.Visible = false;
            supplier1.Visible = false;
            controlUser1.Visible = false;
            Dashboardc adForm = dashboardc1 as Dashboardc;
            if (adForm != null)
            {
                adForm.refreshData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboardc1.Visible = false;
            categories1.Visible = true;
            employeeOrder1.Visible = false;
            productMenu1.Visible = false;
            supplier1.Visible = false;
            controlUser1.Visible = false;
            Categories bdForm = categories1 as Categories;
            if (bdForm != null)
            {
                bdForm.refreshData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dashboardc1.Visible = false;
            categories1.Visible = false;
            employeeOrder1.Visible = false;
            productMenu1.Visible = true;
            supplier1.Visible = false;
            controlUser1.Visible = false;
            ProductMenu cdForm = productMenu1 as ProductMenu;
            if (cdForm != null)
            {
                cdForm.refreshData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dashboardc1.Visible = false;
            categories1.Visible = false;
            employeeOrder1.Visible = false;
            productMenu1.Visible = false;
            supplier1.Visible = true;
            controlUser1.Visible = false;
            Supplier ddForm = supplier1 as Supplier;
            if (ddForm != null)
            {
                ddForm.refreshData();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dashboardc1.Visible = false;
            categories1.Visible = false;
            employeeOrder1.Visible = true;
            productMenu1.Visible = false;
            supplier1.Visible = false;
            controlUser1.Visible = false;
            EmployeeOrder edForm = employeeOrder1 as EmployeeOrder;
            if (edForm != null)
            {
                edForm.refreshData();
            }
        }

        private void button7_Click(object sender, EventArgs e)
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

        private void categories1_Load(object sender, EventArgs e)
        {

        }

        private void controlUser1_Load(object sender, EventArgs e)
        {

        }

        private void dashboardc1_Load(object sender, EventArgs e)
        {

        }

        private void employeeOrder1_Load(object sender, EventArgs e)
        {

        }

        private void productMenu1_Load(object sender, EventArgs e)
        {

        }

        private void supplier1_Load(object sender, EventArgs e)
        {

        }
    }
}
