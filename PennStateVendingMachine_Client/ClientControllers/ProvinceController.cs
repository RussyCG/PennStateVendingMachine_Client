using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientControllers
{
    public class ProvinceController
    {
        public static List<String> getProvinces()
        {
            return DataAccess.SelectController.getProvinces();
        }
    }
}
