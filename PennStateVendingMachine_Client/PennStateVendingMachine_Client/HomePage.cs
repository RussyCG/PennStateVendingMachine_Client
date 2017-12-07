using ClientControllers;
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
    public partial class HomePage : Form
    {
        DateTime currentDate;
        string selectedItem = "";
        string vendingID = "";
        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            currentDate = DateTime.Now;
            lblHeaderDate.Text = currentDate.DayOfWeek + " " + currentDate.ToShortDateString() + " " + currentDate.ToLongTimeString();
            tmrDataUpdate.Interval = 1000;
            tmrDataUpdate.Enabled = true;
            tmrDataUpdate.Start();
            //ClientConnection.Scheduler.startScheduler();
            string ID2 = ClientControllers.StartUpController.getID();
            vendingID = ID2;
        }

        private void tmrDataUpdate_Tick(object sender, EventArgs e)
        {
            currentDate = DateTime.Now;
            lblHeaderDate.Text = currentDate.DayOfWeek + " " + currentDate.ToShortDateString() + " " + currentDate.ToLongTimeString();
        }

        

        private void pnlR1I1_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR1I1Name.Text;
            lblTotal.Text = lblR1I1Price.Text;
        }

        private void pnlR1I2_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR1I2Name.Text;
            lblTotal.Text = lblR1I2Price.Text;
        }

        private void pnlR1I3_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR1I3Name.Text;
            lblTotal.Text = lblR1I3Price.Text;
        }

        private void pnlR1I4_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR1I4Name.Text;
            lblTotal.Text = lblR1I4Price.Text;
        }

        private void pnlR2I1_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR2I1Name.Text;
            lblTotal.Text = lblR2I1Price.Text;
        }

        private void pnlR2I2_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR2I2Name.Text;
            lblTotal.Text = lblR2I2Price.Text;
        }

        private void pnlR2I3_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR2I3Name.Text;
            lblTotal.Text = lblR2I3Price.Text;
        }

        private void pnlR2I4_MouseClick(object sender, MouseEventArgs e)
        {
            selectedItem = lblR2I4Name.Text;
            lblTotal.Text = lblR2I4Price.Text;
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (selectedItem=="")
            {
                return;
            }
            try
            {
                ClientControllers.SalesController.itemPurchased(vendingID, selectedItem, lblTotal.Text);
            }
            catch (TypeInitializationException t)
            {
                MessageBox.Show("Error message: \n" + t.InnerException.Message + "\n\nPlease contact technical department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error message: \n" + ex.Message + "\n\nPlease contact technical department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            AutoClosingMessageBox.Show("Thank you for your purchase", "Success", 1000);
            lblTotal.Text = "0,00";
            selectedItem = "";
        }



        
    }
}
