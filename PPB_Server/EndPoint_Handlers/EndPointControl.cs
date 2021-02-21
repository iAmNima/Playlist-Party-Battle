using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.EndPoint_Handlers
{
    public static class EndPointControl
    {

        public static bool Autorized(string EndPointUserToken)
        {

            if (EndPointUserToken == new Settings().Token)
                return true;
            else
                return false;
        }

    }
}
