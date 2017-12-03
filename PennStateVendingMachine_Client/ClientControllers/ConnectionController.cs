using ClientConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientControllers
{
    public class ConnectionController
    {
        public static string TestConnection(string dataToSend)
        {
            return new ConnectionManager().write(dataToSend);
        }
    }
}
