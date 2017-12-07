using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class SelectController
    {
        private static string server = "localhost";
        private static string database = "vendingmachine";
        private static string uid = "root";
        private static string password = "";
        private static string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        private static MySqlConnection connection = new MySqlConnection(connectionString);
        private static MySqlConnection schedulerConn = new MySqlConnection(connectionString);
        public SelectController()
        {
            MySqlConnection.ClearAllPools();
        }
        public static List<string> getCountries()
        {
            List<String> data = new List<string>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM tblcountry";
                if (connection.State.ToString() == "Open")
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    
                    while (dataReader.Read())
                    {
                        data.Add(dataReader["CountryName"].ToString());
                    }
                    
                    dataReader.Close();

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }
        public static List<string> getProvinces()
        {
            List<String> data = new List<string>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM tblprovince";
                if (connection.State.ToString() == "Open")
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(dataReader["ProvinceName"].ToString());
                    }

                    dataReader.Close();

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }
        public static List<string> getCities()
        {
            List<String> data = new List<string>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM tblcity";
                if (connection.State.ToString() == "Open")
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(dataReader["CityName"].ToString());
                    }

                    dataReader.Close();

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }
        public static List<string> getSuburbs()
        {
            List<String> data = new List<string>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM tblsuburb";
                if (connection.State.ToString() == "Open")
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        data.Add(dataReader["SuburbName"].ToString());
                    }

                    dataReader.Close();

                    connection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }
        public static int getProductIDbyName(string productName)
        {
            try
            {
                if (connection.State.ToString()!="Open")
                {
                    connection.Open();
                }
                string query = "SELECT * FROM  `tblproduct` WHERE  `ProductName` =  '"+productName+"'";
                if (connection.State.ToString() == "Open")
                {
                    int result = 0;
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                result = Convert.ToInt16(dataReader["ID"].ToString());
                            }

                            dataReader.Close();
                        }
                    }
                    connection.Close();
                    MySqlConnection.ClearAllPools();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return 0;
        }
        public static List<VendingMachineModels.VendingMachinePurchaseDTO> getSales()
        {
            List<VendingMachineModels.VendingMachinePurchaseDTO> data = new List<VendingMachineModels.VendingMachinePurchaseDTO>();
            try
            {
                schedulerConn.Open();
                string query = "SELECT * FROM tblpurchase";
                if (schedulerConn.State.ToString() == "Open")
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, schedulerConn))
                    {
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                VendingMachineModels.VendingMachinePurchaseDTO val = new VendingMachineModels.VendingMachinePurchaseDTO();
                                val.ID = Convert.ToInt16(dataReader["ID"].ToString());
                                val.Product = new VendingMachineModels.ProductDTO(Convert.ToInt16(dataReader["ProductID"].ToString()), null, null);
                                val.Cost = Convert.ToDouble(dataReader["Cost"].ToString());
                                val.Quantity = Convert.ToInt16(dataReader["Quantity"].ToString());
                                val.DateTimePurchased = DateTime.Now;
                                System.Threading.Thread.Sleep(50);
                                val.VendingMachine = new VendingMachineModels.VendingMachineDTO(Convert.ToInt16(dataReader["VendingMachineID"].ToString()), null, null, null);
                                data.Add(val);
                            }

                            dataReader.Close();
                        }
                    }
                    schedulerConn.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return data;
        }
        //public static VendingMachineModels.VendingMachineDTO getVendingMachineByID(int ID)
        //{
        //    try
        //    {
        //        if (connection.State.ToString() != "Open")
        //        {
        //            connection.Open();
        //        }
        //        string query = "SELECT * FROM  `tblvendingmachine` WHERE  `ID` = "+ID;
        //        VendingMachineModels.VendingMachineDTO result = new VendingMachineModels.VendingMachineDTO();
        //        if (connection.State.ToString() == "Open")
        //        {
        //            using (MySqlCommand cmd = new MySqlCommand(query, connection))
        //            {
        //                using (var dataReader = cmd.ExecuteReader())
        //                {
        //                    while (dataReader.Read())
        //                    {
        //                        result = Convert.ToInt16(dataReader["ID"].ToString());
        //                    }

        //                    dataReader.Close();
        //                }
        //            }
        //            connection.Close();
        //            MySqlConnection.ClearAllPools();
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return 0;
        //}
    }
}
