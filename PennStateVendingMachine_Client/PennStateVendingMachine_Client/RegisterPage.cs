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
            cmbCountry.DataSource = ClientControllers.CountryController.getCountries();
            cmbProvince.DataSource = ClientControllers.ProvinceController.getProvinces();
            cmbCity.DataSource = ClientControllers.CityController.getCities();
            cmbSuburb.DataSource = ClientControllers.SuburbController.getSuburbs();
        }

        private void txtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
