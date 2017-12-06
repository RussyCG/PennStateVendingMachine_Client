using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConnectionManager
    {

        public static void killConnections()
        {
            MySqlConnection.ClearAllPools();
        }
    }
}
