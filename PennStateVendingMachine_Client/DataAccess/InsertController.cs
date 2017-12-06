using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccess
{
    public class InsertController
    {
        private static string server = "localhost";
        private static string database = "vendingmachine";
        private static string uid = "root";
        private static string password = "";
        private static string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        private static MySqlConnection connection = new MySqlConnection(connectionString);
        public InsertController()
        {
            MySqlConnection.ClearAllPools();
        }
        public static void itemPurchased(string vendingMachineID, int ProductID, double cost, int quantity, DateTime date)
        {
            try
            {
                connection.Open();
                string q1 = string.Format("INSERT INTO `vendingmachine`.`tblpurchase` (`ID`, `VendingMachineID`, `ProductID`, `Cost`, `Quantity`, `PurchaseDateTime`) VALUES(NULL, '{0}', '{1}', '{2}', '{3}', '{4}');",vendingMachineID,ProductID,cost,quantity,date.ToLongDateString());
                if (connection.State.ToString() == "Open")
                {
                    using (MySqlCommand cmd = new MySqlCommand(q1, connection))
                    {
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MySqlConnection.ClearAllPools();
                    }
                }
            }
            catch (Exception)
            {
                connection.Close();
                MySqlConnection.ClearAllPools();
                throw;
            }
        }
    }
}
