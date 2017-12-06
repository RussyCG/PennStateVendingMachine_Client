using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClientConnection
{
    public class Scheduler
    {
        public static void startScheduler()
        {
            Thread runSync = new Thread(syncPurchases);
            runSync.Start();
        }
        private static void syncPurchases()
        {
            try
            {
                while (true)
                {
                    List<VendingMachineModels.DTOs.PurchaseDTO> data = DataAccess.SelectController.getSales();
                    string json = new JavaScriptSerializer().Serialize(data);
                    string result = writeData(json);
                    Thread.Sleep(30000);
                }
            }
            catch (Exception)
            {
                return;
            }
           
        }
        private static string writeData(string writeData)
        {
            return new ConnectionManager().write(writeData);
        }
        
    }
}
