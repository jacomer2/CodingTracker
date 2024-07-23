using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Helpers
{
    class Conversions
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

        static public DateTime stringtoDateTime(string timeStr)
        {
            DateTime.TryParse(timeStr, out DateTime result);

            return result;
        }

        static public int calculateDuration(string startTimeStr, string endTimeStr) {

            DateTime startTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;

            if (validateDateTime(startTimeStr))
            {
                 startTime = stringtoDateTime(startTimeStr);
            }

            if (validateDateTime(endTimeStr))
            {
                 endTime = stringtoDateTime(endTimeStr);
            }

            int duration = (int) endTime.Subtract(startTime).TotalMinutes;


            return duration;
        }
    }
}
