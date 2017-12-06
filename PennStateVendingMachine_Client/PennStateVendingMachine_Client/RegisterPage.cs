using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PennStateVendingMachine_Client
{
    public partial class RegisterPage : Form
    {
        DateTime currentDate;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void tmrDataUpdate_Tick(object sender, EventArgs e)
        {
            currentDate = DateTime.Now;
            lblHeaderDate.Text = currentDate.DayOfWeek + " " + currentDate.ToShortDateString() + " " + currentDate.ToLongTimeString();
        }

        private void RegisterPage_Load(object sender, EventArgs e)
        {
            currentDate = DateTime.Now;
            lblHeaderDate.Text = currentDate.DayOfWeek + " " + currentDate.ToShortDateString() + " " + currentDate.ToLongTimeString();
            tmrDataUpdate.Interval = 1000;
            tmrDataUpdate.Enabled = true;
            tmrDataUpdate.Start();
        }
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.Trim();

            if (key=="")
            {
                lblRequired.Visible = true;
                return;
            }

            if (ClientControllers.StartUpController.validateCode(key)==false)
            {
                MessageBox.Show("This code is not valid", "INVALID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKey.Text = "";
                return;
            }
            else
            {
                AutoClosingMessageBox.Show("Thank you for registering your unit.", "WELCOME USER", 3000);
                ClientControllers.StartUpController.createStartUpIndex(key);
                string ID = ClientControllers.StartUpController.getID();
                //Application.Run(new HomePage(ID));
                //this.Close();
                HomePage hp = new HomePage();
                hp.FormClosing += delegate { this.Close(); };
                hp.Show();
                this.Hide();
            }

        }

        private void txtKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if (e.Handled==false)
            {
                lblRequired.Visible = false;
            }
        }
    }
}
