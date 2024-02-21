using UnityEngine;

namespace TSCore.Utils
{
    public static class DebugScript
    {
        public static void LogError<T>(T typeOfScript, string message, string color = "black")
        {
            Debug.LogError($"<color={color}>[{typeOfScript.ToString()}] {message}</color>");
        }

        public static void LogWarning<T>(T typeOfScript, string message, string color = "yellow")
        {
            Debug.LogWarning($"<color={color}>[{typeOfScript.ToString()}] {message}</color>");
        }
        public static void Log<T>(T script, string message, string color = "black")
        {
            Debug.Log($"<color={color}>[{script.ToString()}] {message}</color>");
        }
        
        public static void LogRedText<T>(T script, string message) => Log(script,message, "red");

        public static void LogYellowText<T>(T script, string message) => Log(script,message, "yellow");

        public static void LogGreenText<T>(T script, string message) => Log(script,message, "green");

        public static void LogWhiteText<T>(T script, string message) => Log(script,message, "white");

        public static void LogBlueText<T>(T script, string message) => Log(script,message, "blue");
        
        public static void LogEnemyInspectorError<T>(T typeOfScript,string expectedEnemyName,string foundEnemyName, string color = "black")
        {
            LogError(typeOfScript,$"All Default Components Do Not Match {expectedEnemyName}, Bad Naming Convention," +
                           $" {foundEnemyName} Was Found And Is Wrong", color);
        }
        public static void LogIncorrectTagError(GameObject gameObject,string expectedTag, string color = "black")
        {
            if (gameObject.CompareTag(expectedTag)) return;
            var foundTag = gameObject.tag;
            Debug.LogError($"<color={color}>{gameObject} Found Tag Did Not Match {expectedTag}, {foundTag} Was Found And Is Wrong.</color>");
        }
        public static void LogIncorrectLayerError(GameObject gameObject,string expectedLayer, string color = "black")
        {
            if (gameObject.CompareTag(expectedLayer)) return;
            var foundLayer = gameObject.layer;
            Debug.LogError($"<color={color}>{gameObject} Found Layer Did Not Match {expectedLayer}, {foundLayer} Was Found And Is Wrong.</color>");
        }
        public static void Log_QuickTest<T>(T script) => Log(script,"Quick Test");
    }
}
