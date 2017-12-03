﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientControllers
{
    public class SalesController
    {
        public static void itemPurchased(int vendingID, string itemName, string itemPrice)
        {
            int productID = DataAccess.SelectController.getProductIDbyName(itemName);
            DataAccess.InsertController.itemPurchased(vendingID,productID,Convert.ToDouble(itemPrice),1,DateTime.Now);
        }
    }
}