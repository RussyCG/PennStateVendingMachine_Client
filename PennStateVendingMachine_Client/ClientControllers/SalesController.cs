﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientControllers
{
    public class SalesController
    {
        public static void itemPurchased(string vendingID, string itemName, string itemPrice)
        {
            DataAccess.ConnectionManager.killConnections();
            int productID = DataAccess.SelectController.getProductIDbyName(itemName);
            int columnID = productID > 3 ? 3 : productID;
            DataAccess.InsertController.itemPurchased(vendingID,productID,Convert.ToDouble(itemPrice),1,DateTime.Now);
            GPIO_RGM.GPIO.dispenseItem(columnID);
        }
    }
}
