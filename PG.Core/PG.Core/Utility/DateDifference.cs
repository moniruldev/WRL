using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.Utility
{
    public class DateDifference
    {
        /// <summary>
        /// this three variable for output representation..
        /// </summary>
        private int year = 0;
        private int month = 0;
        private int day = 0;
        private int totDay = 0;

        /// <summary>
        /// returns year month day in text.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return base.ToString();

            string strDiff = string.Empty;
            string strYear = string.Empty;
            string strMonth = string.Empty;
            string strDay = string.Empty;

            string yGap = string.Empty;
            string mGap = string.Empty;

            if (this.year > 0)
            {
                strYear = this.year.ToString() + " year" + (this.year == 1 ? string.Empty : "s");
                yGap = " ";
            }
            if (this.month > 0)
            {
                strMonth = this.month.ToString() + " month" + (this.month == 1 ? string.Empty : "s");
                mGap = " ";
            }
            if (this.day > 0)
            {
                strDay = this.day.ToString() + " day" + (this.day == 1 ? string.Empty : "s");
            }

            strDiff = strYear + yGap + strMonth + mGap + strDay;
            return strDiff;
        }

        public int Years
        {
            get
            {
                return this.year;
            }
        }

        public int Months
        {
            get
            {
                return this.month;
            }
        }

        public int TotalMonths
        {
            get
            {
                int totMonths = (year * 12) + month;
                return totMonths;
            }
        }


        public int Days
        {
            get
            {
                return this.day;
            }
        }

        public int TotalDays
        {
            get
            {
                return totDay;
            }
        }


        public static DateDifference GetDateDiff(DateTime d1, DateTime d2)
        {
            DateTime fromDate;
            DateTime toDate;

            /// defining Number of days in month; index 0=> january and 11=> December
            /// february contain either 28 or 29 days, that's why here value is -1
            /// which wil be calculate later.
           int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };


            DateDifference diff = new DateDifference();
            int increment;

            if (d1 > d2)
            {
                fromDate = d2;
                toDate = d1;
            }
            else
            {
                fromDate = d1;
                toDate = d2;
            }

            /// 
            /// Day Calculation
            /// 
            increment = 0;

            if (fromDate.Day > toDate.Day)
            {
                increment = monthDay[fromDate.Month - 1];

            }
            /// if it is february month
            /// if it's to day is less then from day
            if (increment == -1)
            {
                if (DateTime.IsLeapYear(fromDate.Year))
                {
                    // leap year february contain 29 days
                    increment = 29;
                }
                else
                {
                    increment = 28;
                }
            }
            if (increment != 0)
            {
                diff.day = (toDate.Day + increment) - fromDate.Day;
                increment = 1;
            }
            else
            {
                diff.day = toDate.Day - fromDate.Day;
            }

            ///
            ///month calculation
            ///
            if ((fromDate.Month + increment) > toDate.Month)
            {
                diff.month = (toDate.Month + 12) - (fromDate.Month + increment);
                increment = 1;
            }
            else
            {
                diff.month = (toDate.Month) - (fromDate.Month + increment);
                increment = 0;
            }

            ///
            /// year calculation
            ///
            diff.year = toDate.Year - (fromDate.Year + increment);


            diff.totDay = Convert.ToInt32((toDate - fromDate).TotalDays);

            return diff;
        }
        public static DateTime FirstDayOfMonth()
        {
            DateTime dateTime = DateTime.Now;
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }
    }
}
