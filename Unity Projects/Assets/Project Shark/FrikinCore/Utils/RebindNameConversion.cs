using System.Collections.Generic;

namespace FrikinCore.Utils
{
    public static class RebindNameConversion
    {
        static Dictionary<string, string> ActionNameDictionary = new()
        {
            { "OpenMenu", "Menu" },
        };

        static Dictionary<string, string> BindingNameDictionary = new()
        {
            { "Position", "Mouse" },
            {"Num 6", "Up Arrow"} 
        };

        public static string ActionNameConvert(string actionName)
        {
            return ActionNameDictionary.ContainsKey(actionName) == false ? actionName 
                : ActionNameDictionary[actionName];
        }

        public static string BindingNameConvert(string binding)
        {
            return BindingNameDictionary.ContainsKey(binding) == false ? binding 
                : BindingNameDictionary[binding];
        }
    }
}