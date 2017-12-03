using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineModels.DTOs
{
    public class PurchaseDTO
    {
        private int purchaseID;
        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private int vendingMachineID;
        private double cost;
        private int quantity;
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public int VendingMachineID
        {
            get { return vendingMachineID; }
            set { vendingMachineID = value; }
        }

        public int PurchaseID
        {
            get { return purchaseID; }
            set { purchaseID = value; }
        }

    }
}
