using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientControllers
{
    public class CityController
    {
        public static List<String> getCities()
        {
            return DataAccess.SelectController.getCities();
        }
    }
}
