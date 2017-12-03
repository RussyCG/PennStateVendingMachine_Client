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
        public static void itemPurchased(int vendingMachineID, int ProductID, double cost, int quantity, DateTime date)
        {
            try
            {
                connection.Open();
                //string query = "INSERT INTO tblpurchase() VALUES ("+ vendingMachineID + ", "+ProductID+", "+cost+", "+quantity+", '"+date.ToShortDateString()+"');";
                string q = "INSERT INTO tblpurchase VALUES('NULL', " + vendingMachineID + ", " + ProductID + ", " + cost + ", " + quantity + ", '" + date.ToLongDateString() + "'); ";
                string q1 = string.Format("INSERT INTO `vendingmachine`.`tblpurchase` (`ID`, `VendingMachineID`, `ProductID`, `Cost`, `Quantity`, `PurchaseDateTime`) VALUES(NULL, '{0}', '{1}', '{2}', '{3}', '{4}');",vendingMachineID,ProductID,quantity,date.ToLongDateString());
                if (connection.State.ToString() == "Open")
                {
                    //MessageBox.Show(q);
                    MySqlCommand cmd = new MySqlCommand(q1, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
