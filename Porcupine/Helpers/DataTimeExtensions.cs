using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Porcupine.Helpers
{
    public static class dataTimeExtensions
    {
        public static DateTime addBusinessDays(this DateTime date, int workingDays)
        {
            int direction = workingDays < 0 ? -1 : 1;
            DateTime newDate = date;
            while (workingDays != 0)
            {
                newDate = newDate.AddDays(direction);
                if (!newDate.isWeekendOrHolyday())
                    workingDays -= direction;
            }
            return newDate;
        }

        public static DateTime getThisOrNextWorkday(this DateTime date)
        {
            while (date.isWeekendOrHolyday())
                date = date.AddDays(1);

            return date;
        }

        public static DateTime getThisOrPrevWorkday(this DateTime date)
        {
            while (date.isWeekendOrHolyday())
                date = date.AddDays(-1);

            return date;
        }

        public static double diffBusinessDays(DateTime startDate, DateTime endDate)
        {
            double rtn =
                1 + ((endDate - startDate).TotalDays * 5 -
                (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;

            if ((int)endDate.DayOfWeek == 6) rtn--;
            if ((int)startDate.DayOfWeek == 0) rtn--;

            return rtn;
        }

        private static bool isWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool isWeekendOrHolyday(this DateTime date)
        {
            Boolean rtn = false;

            if (date.isWeekend())
                rtn = true;
            else
            {
                DateTime easterSunday = CalculateEasterSunday(date.Year);
                if (date.Date == easterSunday.AddDays(-7).Date) // Palmesøndag
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(-3).Date) // Skærtorsdag
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(-2).Date) // Langefredag
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(0).Date) // Påskesøndag
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(1).Date) // 2. Påskedag
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(26).Date) // St. bededag
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(39).Date) // Kristi himmelfart
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(49).Date) // Pinsesøndag
                    rtn = true;
                else if (date.Date == easterSunday.AddDays(50).Date) // 2. Pinsedag
                    rtn = true;
                else if (date.Month == 1 && date.Day == 1) // Nytårsdag
                    rtn = true;
                else if (date.Month == 6 && date.Day == 5) // Grundlovsdag
                    rtn = true;
                else if (date.Month == 12 && date.Day == 24) // Juleaften
                    rtn = true;
                else if (date.Month == 12 && date.Day == 25) // 1. juledag
                    rtn = true;
                else if (date.Month == 12 && date.Day == 26) // 2. juledag
                    rtn = true;

            }


            return rtn;
        }


        public static DateTime CalculateEasterSunday(int year)
        {

            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;
            return new DateTime(year, month, day).Date;
        }
    }
}