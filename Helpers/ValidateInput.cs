using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Helpers
{
    class ValidateInput
    {
        static public bool validateDateTime(string startTime)
        {
            DateTime.TryParse(startTime, out DateTime result);

            if (result != DateTime.MinValue)
            {
                return true;
            }

            
            return false;
        }
    }
}
