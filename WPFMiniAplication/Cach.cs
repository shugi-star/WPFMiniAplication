using System.Globalization;
using DTO;

namespace WPF_chesedWroled
{
    public static class Cach
    {
        public static VolunteerDTO currentVolunteer { get; set; }

     

        public static bool IsDateValid(string dateStr)
        {
            // פורמט התאריך הרצוי: יום-חודש-שנה
            string format = "d-M-yyyy";
            DateTime tempDate;

            // מנסה לפרס את המחרוזת לפי הפורמט המדויק
            bool valid = DateTime.TryParseExact(
                dateStr,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out tempDate
            );

            return valid;
        }
    }
}
    
