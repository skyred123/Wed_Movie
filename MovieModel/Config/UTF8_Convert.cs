using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieModel.Config
{
    public class UTF8_Convert
    {
        public static string UTF8Convert(string? s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string? temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
