using System;
using System.Linq;

namespace TSCore.Utils
{
    public static class Strings
    {
        public static string CreateRandomSeed()
        {
            //Do calc

            //Return calc
            return "";
        }

        public static string RemoveSpaceAndCapitals(string text)
        {
            var noSpace = String.Concat(text.Where(character => !Char.IsWhiteSpace(character)));
            return noSpace.ToLower();
        }
    }
}