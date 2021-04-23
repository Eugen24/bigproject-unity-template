using System;
using System.Globalization;
using UnityEngine;

namespace Template.Scripts.Utils
{
    public static class StringUtils
    {
        
        private static readonly CultureInfo _culture = new CultureInfo("en-US");
        public static string ToKMB(this float num)
        {
            if (num > 99999999999999999999999999999999999f || num < -99999999999999999999999999999999999f)
            {
                var s = num.ToString("$0,,,,,,,,,,,,.0W", _culture);
                return s;
            }else 
            if (num > 99999999999999999999999999999999f || num < -99999999999999999999999999999999f)
            {
                var s = num.ToString("$0,,,,,,,,,,,.0E", _culture);
                return s;
            }else 
            if (num > 99999999999999999999999999999f || num < -99999999999999999999999999999f)
            {
                var s = num.ToString("$0,,,,,,,,,,.0Y", _culture);
                return s;
            }else 
            if (num > 99999999999999999999999999f || num < -99999999999999999999999999f)
            {
                var s = num.ToString("$0,,,,,,,,,.0Z", _culture);
                return s;
            }else 
            if (num > 99999999999999999999999f || num < -99999999999999999999999f)
            {
                var s = num.ToString("$0,,,,,,,,.0L", _culture);
                return s;
            }else 
            if (num > 99999999999999999999f || num < -99999999999999999999f)
            {
                var s = num.ToString("$0,,,,,,,.0A", _culture);
                return s;
            }else 
            if (num > 99999999999999999f || num < -99999999999999999f)
            {
                var s = num.ToString("$0,,,,,,.0P", _culture);
                return s;
            }else 
            if (num > 99999999999999f || num < -99999999999999f)
            {
                var s = num.ToString("$0,,,,,.0Q", _culture);
                return s;
            }else 
            if (num > 99999999999f || num < -99999999999f)
            {
                var s = num.ToString("$0,,,,.0T", _culture);
                return s;
            }else 
            if (num > 999999999f || num < -999999999f )
            {
                var s = num.ToString("$0,,,.0B", _culture);
                return s;
            }
            else
            if (num > 999999f || num < -999999f )
            {
                var s = num.ToString("C0",_culture);
                s = s.Remove(s.Length - 6);
                return s + "M";
            }
            else if (num > 999f || num < -999f)
            {
                var s = num.ToString("C0", _culture);
                s = s.Remove(s.Length - 2);
                return s + "K";
            }
            else
            {
                return "$" + Mathf.FloorToInt(num).ToString();
            }
        }
        
        public static string ToKMB(this int num)
        {
            
            if (num > 99999999999 || num < -99999999999)
            {
                var s = num.ToString("$0,,,,.0T", _culture);
                return s;
            }else 
            if (num > 999999999 || num < -999999999 )
            {
                var s = num.ToString("$0,,,.0B", _culture);
                return s;
            }
            else
            if (num > 999999 || num < -999999 )
            {
                var s = num.ToString("C0",_culture);
                s = s.Remove(s.Length - 6);
                return s + "M";
            }
            else if (num > 999 || num < -999)
            {
                var s = num.ToString("C0",_culture);
                s = s.Remove(s.Length - 2);
                return s + "K";
            }
            else
            {
                return "$" + Mathf.FloorToInt(num).ToString();
            }
        }

        public static string ToDateFromSecondsOneLine(this float num)
        {
            var d = TimeSpan.FromSeconds(num);
            return $"{d.Hours:00}:{d.Minutes:00}";
        }

        public static string ToDateHourMinutes_Format1(this float num)
        {
            var d = TimeSpan.FromSeconds(num);
            return $"{d.Hours:00}H:{d.Minutes:00}m";
        }
        
        public static string ToDateMinutes_Format1(this float num)
        {
            var d = TimeSpan.FromSeconds(num);
            if (d.TotalMinutes < 1 && d.TotalMinutes > 0)
            {
                return $"1 minute";
            }
            return $"{d.TotalMinutes} minutes";
        }
        
        public static string ToDateFromSecondsOneLineWithSeconds(this float num)
        {
            var d = TimeSpan.FromSeconds(num);
            return $"{d.Hours:00}:{d.Minutes:00}:{d.Seconds:00}";
        }

        
        public static string ToDateFromSecondsTwoLines(this float num)
        {
            var d = TimeSpan.FromSeconds(num);
            return $"{d.Hours:00} Hours \n {d.Minutes:00} Minutes";
        }
    }
}
