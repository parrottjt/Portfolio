using System;
using System.Linq;

namespace TSCore.Utils
{
    public static class Enums
    {
        public static T[] GetValues<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enum type");
            return (T[]) Enum.GetValues(typeof(T));
        }

        public static string[] GetValuesAsStrings<T>()
        {
            T[] values = GetValues<T>();
            string[] strArray = new string[values.Length];
            for (int index = 0; index < values.Length; ++index)
                strArray[index] = values[index].ToString();
            return strArray;
        }

        public static int GetCount<T>()
        {
            return GetValues<T>().Length;
        }

        public static T Random<T>()
        {
            T[] values = GetValues<T>();
            return values[UnityEngine.Random.Range(0, values.Length)];
        }

        public static T Parse<T>(string name)
        {
            T[] values = GetValues<T>();
            for (int index = 0; index < values.Length; ++index)
            {
                if (name == values[index].ToString())
                    return values[index];
            }

            return values[0];
        }

        public static bool TryParse<T>(string name, out T result)
        {
            T[] values = GetValues<T>();
            for (int index = 0; index < values.Length; ++index)
            {
                if (name == values[index].ToString())
                {
                    result = values[index];
                    return true;
                }
            }

            result = values[0];
            return false;
        }

        public static string ShortenNameInitials<T>(T valueToShorten)
        {
            return valueToShorten.ToString()
                .Where(c => c >= 65 && c <= 90)
                .Aggregate("", (current, c) => current + c);
        }
    }
}