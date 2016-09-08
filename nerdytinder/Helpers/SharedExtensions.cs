using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace nerdytinder
{
    public static partial class Extensions
    {
        public static Dictionary<string, string> ToKeyValuePair(this string querystring)
        {
            return Regex.Matches(querystring, "([^?=&]+)(=([^&]*))?").Cast<Match>().ToDictionary(x => x.Groups[1].Value, x => x.Groups[3].Value);
        }

        public static string ToOrdinal(this DateTime date)
        {
            return "{0}, {1} {2}".Fmt(date.ToString("dddd"), date.ToString("MMM"), date.Day.ToOrdinal());
        }

        public static bool ContainsNoCase(this string s, string contains)
        {
            if (s == null || contains == null)
                return false;

            return s.IndexOf(contains, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string TrimStart(this string s, string toTrim)
        {
            if (s.StartsWith(toTrim, StringComparison.CurrentCultureIgnoreCase))
                return s.Substring(toTrim.Length);

            return s;
        }

        public static string TrimEnd(this string s, string toTrim)
        {
            if (s.EndsWith(toTrim, StringComparison.CurrentCultureIgnoreCase))
                return s.Substring(0, s.Length - toTrim.Length);

            return s;
        }

        public static string Fmt(this string s, params object[] args)
        {
            return string.Format(s, args);
        }

        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static string ToOrdinal(this int num)
        {
            if (num <= 0)
                return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }
        /*
        public static void ToToast(this string message, ToastNotificationType type = ToastNotificationType.Info, string title = null)
        {
            Device.BeginInvokeOnMainThread(() => {
                var toaster = DependencyService.Get<IToastNotifier>();
                toaster?.Notify(type, title ?? type.ToString().ToUpper(), message, TimeSpan.FromSeconds(2.5f));
            });
        }
        */
    }
}
