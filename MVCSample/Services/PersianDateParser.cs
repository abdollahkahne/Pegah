using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Services
{
    public static class PersianDateParser
    {
        // This return a DateTime object if you pass a persian date as 1392/09/19
        public static DateTime? Parse(string shamsi) {
            var year=shamsi[0..3];
            var month=shamsi[5..6];
            var day=shamsi[8..9];
            var calendar=new PersianCalendar();
            try
            {
                return calendar.ToDateTime(int.Parse(year),int.Parse(month),int.Parse(day),0,0,0,0);
            }
            catch (System.Exception)
            {
                
                return null;
            }
            
        }
        public static DateTime? ParseUsingCulture(string persianDate) {
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            try
            {
                return DateTime.ParseExact(persianDate, "yyyy/MM/dd", persianCulture);
            }
            catch (System.Exception)
            {
                
                return null;
            }
            
        }
        public static string? FormatUsingCulture(DateTime? dateTime) {
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            if (dateTime is not null)
            return dateTime?.ToString("yyyy/MM/dd",persianCulture); else return "";
        }
    }
}