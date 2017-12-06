using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PennStateVendingMachine_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                if (ClientControllers.StartUpController.ISfirstStartup()==true)
                {
                    Application.Run(new RegisterPage());
                }
                else
                {
                    Application.Run(new HomePage());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error message: "+e.Message+"\nPlease Call Tech Support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            //string ID = ClientControllers.StartUpController.getID();
            //Application.Run(new HomePage(ID));
        }
    }
}
