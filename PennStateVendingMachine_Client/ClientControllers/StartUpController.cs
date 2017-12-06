﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace ClientControllers
{
    public class StartUpController
    {
        public static bool ISfirstStartup()
        {
            try
            {
                using (StreamReader file = new StreamReader("firstStart"))
                {
                    if (file.ReadLine() != null)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (IOException)
            {
                return true;
            }
            
        }
        public static void createStartUpIndex(string ID)
        {
            using (StreamWriter file = new StreamWriter("firstStart"))
            {
                file.WriteLine(ID);
                file.WriteLine("First started on: " + DateTime.Now.ToLongDateString());
            }
        }
        public static bool validateCode(string code)
        {
            try
            {
                string result = ConnectionController.getCodes(code);
                List<VendingMachineModels.DTOs.PurchaseDTO> data = new JavaScriptSerializer().Deserialize<List<VendingMachineModels.DTOs.PurchaseDTO>>(result);
                foreach (VendingMachineModels.DTOs.PurchaseDTO item in data)
                {
                    if (item.VendingMachineID.ToString()==code)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
        public static string getID()
        {
            string result = "";
            using (StreamReader file = new StreamReader("firstStart"))
            {
                result = file.ReadLine();
            }
            return result;
        }
    }
}
