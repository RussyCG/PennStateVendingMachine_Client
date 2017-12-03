using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Script.Serialization;

namespace ClientConnection
{
    public class Scheduler
    {
        private static Thread runSync = new Thread(new ThreadStart(syncPurchases));
        public static void startScheduler()
        {
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
                    Thread.Sleep(600000);
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
