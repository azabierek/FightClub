using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FightClub.Static
{
    public static class FighterValidator
    {
        static Regex nameSurnameRegex = new Regex("^[A-Za-zĄĆĘŁŃÓŚŹŻąćęłńóśźż]+$");
        static Regex yearRegex = new Regex("^(1[0-9]{3}|2[0-9]{3})$");
        static Regex weightRegex = new Regex("^(300|[1-9][0-9]{0,2})(\\.[0-9]{1,2})?$");

#nullable enable
        public static bool CheckName(string? name) => name != null && nameSurnameRegex.IsMatch(name);
        public static bool CheckSurname(string? surname) => surname != null && nameSurnameRegex.IsMatch(surname);
        public static bool CheckYear(string? year) => year != null && yearRegex.IsMatch(year) && IsYearAcceptable(year);
        public static bool CheckFirstTraining(string? year, string? firstTraining)
        {
            if (int.TryParse(year, out int yearOut))
            {
                if (int.TryParse(firstTraining, out int firstOut))
                {
                    if (firstOut >= yearOut)
                        return true;
                }
            }
            return false;

        }
    
        public static bool CheckWeight(string? weight) => weight != null && weightRegex.IsMatch(weight);

        private static bool IsYearAcceptable(string age)
        {
            if (int.TryParse(age, out int year))
            {
                DateTime now = DateTime.Now;
                if (year <= now.Year)
                    return true;
            }
            return false;
        }
    }
}
