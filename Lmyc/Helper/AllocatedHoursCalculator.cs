using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc.Helper
{
    public static class AllocatedHoursCalculator
    {
        public static int CalculateAllocatedHours(DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime > endDateTime)
            {
                return 0;
            }

            if (startDateTime >= DateTime.Now && startDateTime < DateTime.Now.AddDays(1))
            {
                return 0;
            }

            int allocatedHours = 0;
            DateTime tomorrow = startDateTime;
            var day = startDateTime;
            int max = 10;
            int totalDays = startDateTime.Date.Subtract(endDateTime.Date).Duration().Days + 1;
            for (int i = 0; i < totalDays; i++)
            {
                max = (day.DayOfWeek.Equals(DayOfWeek.Saturday) || day.DayOfWeek.Equals(DayOfWeek.Sunday)) ? 15 : 10;
                if (i == totalDays - 1)
                {
                    var hourDiff = endDateTime.Subtract(tomorrow).Hours;
                    allocatedHours += (hourDiff >= max || hourDiff == 0) ? max : hourDiff;
                }
                else
                {
                    tomorrow = day.AddDays(1).AddHours(-day.Hour);
                    var hourDiff = (tomorrow.Subtract(day)).Hours;
                    day = tomorrow;
                    allocatedHours += (hourDiff >= max || hourDiff == 0) ? max : hourDiff;
                }
            }

            return allocatedHours;
        }
    }
}
