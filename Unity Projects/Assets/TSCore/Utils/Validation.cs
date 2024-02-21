using UnityEngine;

namespace TSCore.Utils
{
    public static class Validation
    {
        public static bool EnsurePrefabIsAttachedAndHasComponent<T>(GameObject prefab) where T : Component
        {
            if (prefab != null)
            {
                return prefab.GetComponent<T>() != null;
            }
            return true;
        }

        public static bool CheckStringForKeyIsValid(string keyValue, string value)
        {
            return Strings.RemoveSpaceAndCapitals(keyValue) == Strings.RemoveSpaceAndCapitals(value);
        }
    }
}
