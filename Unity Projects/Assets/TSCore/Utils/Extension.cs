
using UnityEngine;

namespace TSCore.Utils
{
    public static class Extension
    {
        public static bool Invert(this bool val) => !val;
        public static bool IsNull<T>(this T val) => val == null;
        public static bool IsNotNull<T>(this T val) => val != null;
        public static float Abs(this float val) => Mathf.Abs(val);
        public static int Abs(this int val) => Mathf.Abs(val);
    }
}
