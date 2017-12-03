using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            catch (IOException e)
            {
                return true;
            }
            
        }
        public static void createStartUpIndex()
        {
            using (StreamWriter file = new StreamWriter("firstStart"))
            {
                file.WriteLine("First started on: " + DateTime.Now.ToLongDateString());
            }
        }
    }
}
