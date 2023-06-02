using System.Text.RegularExpressions;
using System.Text;

namespace Wed_Movie.Functions
{
    public class UTF8_Convert
    {
        public static string utf8Convert(string? s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string? temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
