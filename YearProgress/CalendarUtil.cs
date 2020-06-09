using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YearProgress
{
    public partial class CalendarUtil
    {
        /// <summary>
        /// 时间单位
        /// </summary>
        public enum TimeValueType
        {
            /// <summary>
            /// 秒
            /// </summary>
            SECOND,
            /// <summary>
            /// 分钟
            /// </summary>
            MINUTE,
            /// <summary>
            /// 小时
            /// </summary>
            HOUR,
            /// <summary>
            /// 天
            /// </summary>
            DAY,
            /// <summary>
            /// 月
            /// </summary>
            MONTH,
            /// <summary>
            /// 年
            /// </summary>
            YEAR
        }
        /// <summary>
        /// 时间计算的方式
        /// </summary>
        public enum TimeCalculationMode
        {
            /// <summary>
            /// 当前
            /// </summary>
            CURRENT,
            /// <summary>
            /// 总计
            /// </summary>
            TOTAL
        }
    }
    public partial class CalendarUtil
    {
        /// <summary>
        /// 获取指定年份的总天数
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetDaysOfYear(int year)
        {
            int days = 0;
            for (int i = 1; i <= 12; i++)
            {
                days += GetDaysOfMonth(year, i);
            }
            return days;
        }

        /// <summary>
        /// 获取当前时间已经过的天数
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentDays()
        {
            int days = 0;
            for (int i = 1; i < DateTime.Now.Month; i++)
            {
                days += GetDaysOfMonth(DateTime.Now.Year, i);
            }
            days += DateTime.Now.Day - 1;
            return days;
        }

        /// <summary>
        /// 获取今天已经经过的小时数
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentHourOfDay()
        {
            if (DateTime.Now.Hour == 0)
                return 0;
            return DateTime.Now.Hour;
        }
        /// <summary>
        /// 获取今天已经过去的分钟数
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentMinuteOfDay()
        {
            return GetCurrentHourOfDay() * 60 + DateTime.Now.Minute;
        }

        /// <summary>
        /// 获取今天已经过去的秒数
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentSecondOfDay()
        {
            return (GetCurrentHourOfDay() * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        }

        /// <summary>
        /// 获取指定月份的天数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetDaysOfMonth(int year, int month)
        {
            if (month > 12 || month < 1)
                return 0;

            if (IsLeapYear(year) && month == 2)
                return 29;
            else if (!IsLeapYear(year) && month == 2)
                return 28;
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                return 31;
            else if (month == 4 || month == 6 || month == 9 || month == 11)
                return 30;
            else
                return 0;
        }

        /// <summary>
        /// 判断是否是闰年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static bool IsLeapYear(int year)
        {
            if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// 计算指定的时间单位下指定的时间值
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <param name="type1">求的时间单位类型</param>
        /// <param name="type2">求值的单位</param>
        /// <returns></returns>
        public static int GetTimeValueByTimeUnit(DateTime date, TimeValueType type1, TimeValueType type2, TimeCalculationMode mode)
        {
            if(mode == TimeCalculationMode.TOTAL)
            {
                if (type1 == TimeValueType.YEAR)
                {
                    if (type2 == TimeValueType.YEAR)
                    {
                        return 1;
                    }
                    else if (type2 == TimeValueType.DAY) //一年的天数
                    {
                        return GetDaysOfYear(date.Year);
                    }
                    else if (type2 == TimeValueType.MONTH) //一年的月份数
                    {
                        return 12;
                    }
                    else if (type2 == TimeValueType.HOUR)//一年内的小时数
                    {
                        return GetDaysOfYear(date.Year) * 24;
                    }
                    else if (type2 == TimeValueType.MINUTE) //一年内的分钟数
                    {
                        return GetDaysOfYear(date.Year) * 1440;
                    }
                    else if (type2 == TimeValueType.SECOND) //一年内的秒数
                    {
                        return GetDaysOfYear(date.Year) * 86400;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (type1 == TimeValueType.MONTH) //月里面的时间值
                {
                    if (type2 == TimeValueType.MONTH)
                    {
                        return 1;
                    }
                    else if (type2 == TimeValueType.DAY)
                    {
                        return GetDaysOfMonth(date.Year, date.Month);
                    }
                    else if (type2 == TimeValueType.HOUR)
                    {
                        return GetDaysOfMonth(date.Year, date.Month) * 24;
                    }
                    else if (type2 == TimeValueType.MINUTE)
                    {
                        return GetDaysOfMonth(date.Year, date.Month) * 1440;
                    }
                    else if (type2 == TimeValueType.SECOND)
                    {
                        return GetDaysOfMonth(date.Year, date.Month) * 86400;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (type1 == TimeValueType.DAY)
                {
                    if (type2 == TimeValueType.DAY)
                    {
                        return 1;
                    }
                    else if (type2 == TimeValueType.HOUR)
                    {
                        return 24;
                    }
                    else if (type2 == TimeValueType.MINUTE)
                    {
                        return 1440;
                    }
                    else if (type2 == TimeValueType.SECOND)
                    {
                        return 86400;
                    }
                }
                else if (type1 == TimeValueType.HOUR)
                {
                    if (type2 == TimeValueType.HOUR)
                    {
                        return 1;
                    }
                    else if (type2 == TimeValueType.MINUTE)
                    {
                        return 60;
                    }
                    else if (type2 == TimeValueType.SECOND)
                    {
                        return 3600;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (type1 == TimeValueType.MINUTE)
                {
                    if (type2 == TimeValueType.MINUTE)
                    {
                        return 1;
                    }
                    else if (type2 == TimeValueType.SECOND)
                    {
                        return 60;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (type1 == TimeValueType.SECOND)
                {
                    if (type2 == TimeValueType.SECOND)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else if(mode == TimeCalculationMode.CURRENT)
            {
                if(type1 == TimeValueType.YEAR)
                {
                    if(type2 == TimeValueType.MONTH) //一年经过了多少月
                    {
                        return date.Month - 1;
                    }
                    else if(type2 == TimeValueType.DAY) //一年经过了多少天
                    {
                        return GetCurrentDays();
                    }
                    else if(type2 == TimeValueType.HOUR) //一年经过了多少小时
                    {
                        return GetCurrentDays() * 24 + (DateTime.Now.Hour);
                    }
                    else if(type2 == TimeValueType.MINUTE) //一年经过了多少分钟
                    {
                        return GetCurrentDays() * 1440 + GetCurrentMinuteOfDay();
                    }
                    else if(type2 == TimeValueType.SECOND) //一年经过了多少秒
                    {
                        return GetCurrentDays() * 86400 + GetCurrentSecondOfDay();
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if(type1 == TimeValueType.MONTH) //一个月里……
                {
                    if(type2 == TimeValueType.DAY)
                    {
                        return DateTime.Now.Day - 1;
                    }
                    else if(type2 == TimeValueType.HOUR)
                    {
                        return (DateTime.Now.Day - 1) * 24 + (DateTime.Now.Hour);
                    }
                    else if(type2 == TimeValueType.MINUTE)
                    {
                        return (DateTime.Now.Day - 1) * 1440 + GetCurrentMinuteOfDay();
                    }
                    else if(type2 == TimeValueType.SECOND)
                    {
                        return (DateTime.Now.Day - 1) * 86400 + GetCurrentSecondOfDay();
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if(type1 == TimeValueType.DAY) //一天里……
                {
                    if(type2 == TimeValueType.HOUR)
                    {
                        return DateTime.Now.Hour;
                    }
                    else if(type2 == TimeValueType.MINUTE)
                    {
                        return (DateTime.Now.Hour) * 60 + (DateTime.Now.Minute);
                    }
                    else if(type2 == TimeValueType.SECOND)
                    {
                        return (DateTime.Now.Hour) * 3600 + (DateTime.Now.Minute) * 60;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if(type1 == TimeValueType.HOUR) //一小时里……
                {
                    if(type2 == TimeValueType.MINUTE)
                    {
                        return DateTime.Now.Minute;
                    }
                    else if(type2 == TimeValueType.SECOND)
                    {
                        return (DateTime.Now.Minute) * 60 + (DateTime.Now.Second);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if(type1 == TimeValueType.MINUTE) //一分钟里
                {
                    if(type2 == TimeValueType.SECOND)
                    {
                        return DateTime.Now.Second ;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return 0;
        }

    }
}
