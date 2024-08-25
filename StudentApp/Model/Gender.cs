using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Model
{
    public enum Gender
    {
        Male, Female, Unknown
    }

    public static class GenderConvert
    {
        public static Gender CharToGender(string character)
        {
            switch (character)
            {
                case "M": return Gender.Male;
                case "F": return Gender.Female;
                default: return Gender.Unknown;
            }
        }
    }
}
