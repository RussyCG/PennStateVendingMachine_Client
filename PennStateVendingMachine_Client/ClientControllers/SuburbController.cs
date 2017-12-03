using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientControllers
{
    public class SuburbController
    {
        public static List<String> getSuburbs()
        {
            return DataAccess.SelectController.getSuburbs();
        }
    }
}
