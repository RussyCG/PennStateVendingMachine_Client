﻿using ClientControllers;
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
        int vendingID = 1;
        public HomePage()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            currentDate = DateTime.Now;
            lblHeaderDate.Text = currentDate.DayOfWeek + " " + currentDate.ToShortDateString() + " " + currentDate.ToLongTimeString();
            tmrDataUpdate.Interval = 1000;
            tmrDataUpdate.Enabled = true;
            tmrDataUpdate.Start();
            ClientConnection.Scheduler.startScheduler();
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
            catch (Exception ex)
            {
                MessageBox.Show("Error message: "+ex.Message+"\nPlease contact technical department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AutoClosingMessageBox.Show("Thank you for your purchase", "Success", 3000);
            lblTotal.Text = "R 0.00";
            selectedItem = "";
        }



        
    }
}