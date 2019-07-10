using System.Collections.Generic;
using System.Linq;

namespace Wikiled.Invoices.Helpers
{
    public static class PythonDateStyleConverter
    {
        private static readonly Dictionary<string, string> replacement = new Dictionary<string, string>();

        static PythonDateStyleConverter()
        {
            //%a	Weekday as locale’s abbreviated name.	Tue
            replacement["%a"] = "ddd";

            //%A	Weekday as locale’s full name.	Tuesday
            replacement["%A"] = "dddd";

            //%w	Weekday as a decimal number, where 0 is Sunday and 6 is Saturday.	2
            replacement["%w"] = null; // not supported

            //%d	Day of the month as a zero-padded decimal number.	03
            replacement["%d"] = "dd";

            //%-d	Day of the month as a decimal number. (Platform specific)	3
            replacement["%-d"] = "d";

            //%b	Month as locale’s abbreviated name.	Sep
            replacement["%b"] = "MMM";

            //%B	Month as locale’s full name.	september
            replacement["%B"] = "MMMM";

            //%m	Month as a zero-padded decimal number.	09
            replacement["%m"] = "MM";

            //%-m	Month as a decimal number. (Platform specific)	9
            replacement["%-m"] = "M";

            //%y	Year without century as a zero-padded decimal number.	13
            replacement["%y"] = "yy";

            //%Y	Year with century as a decimal number.	2013
            replacement["%Y"] = "yyyy";

            //%H	Hour (24-hour clock) as a zero-padded decimal number.	09
            replacement["%H"] = "HH";

            //%-H	Hour (24-hour clock) as a decimal number. (Platform specific)	9
            replacement["%-H"] = "H";

            //%I	Hour (12-hour clock) as a zero-padded decimal number.	09
            replacement["%I"] = "hh";

            //%-I	Hour (12-hour clock) as a decimal number. (Platform specific)	9
            replacement["%-I"] = "h";

            //%p	Locale’s equivalent of either AM or PM.	AM
            replacement["%p"] = "tt";

            //%M	Minute as a zero-padded decimal number.	06
            replacement["%M"] = "mm";

            //%-M	Minute as a decimal number. (Platform specific)	6
            replacement["%-M"] = "m";

            //%S	Second as a zero-padded decimal number.	05
            replacement["%S"] = "ss";

            //%-S	Second as a decimal number. (Platform specific)	5
            replacement["%-S"] = "s";

            //%f	Microsecond as a decimal number, zero-padded on the left.	000000
            replacement["%f"] = "FFFFF";

            //%z	UTC offset in the form +HHMM or -HHMM (empty string if the the object is naive).	 
            replacement["%z"] = "zzz";

            //%Z	Time zone name (empty string if the object is naive).	
            replacement["%Z"] = "K";

            //%j	Day of the year as a zero-padded decimal number.	246
            replacement["%j"] = null;

            //%-j	Day of the year as a decimal number. (Platform specific)	246
            replacement["%-j"] = null;

            //%U	Week number of the year (Sunday as the first day of the week) as a zero padded decimal number. All days in a new year preceding the first Sunday are considered to be in week 0.	35
            replacement["%U"] = null;

            //%W	Week number of the year (Monday as the first day of the week) as a decimal number. All days in a new year preceding the first Monday are considered to be in week 0.	35
            replacement["%W"] = null;

            //%c	Locale’s appropriate date and time representation.	Tue Sep 3 09:06:05 2013
            replacement["%"] = "D";

            //%x	Locale’s appropriate date representation.	09/03/13
            replacement["%x"] = "d";

            //%X	Locale’s appropriate time representation.	09:06:05
            replacement["%X"] = "T";

            //%%	A literal '%' character.	%
            replacement["%"] = "%";
        }

        public static string Convert(string python)
        {
            foreach (var item in replacement.Where(item => item.Value != null))
            {
                python = python.Replace(item.Key, item.Value);
            }

            return python;
        }
    }
}
