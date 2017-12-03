using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientControllers
{
    public class CountryController
    {
        public static List<String> getCountries()
        {
            return DataAccess.SelectController.getCountries();
        }
    }
}
