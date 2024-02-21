using UnityEditor;
using UnityEngine;

namespace TSCore.Utils
{
    public static class PrefabUtils
    {
        public static void DestroyPrefab(string prefabFolderPath, string prefabName)
        {
            var localPath =
                $"{prefabFolderPath}/{prefabName}.prefab";

            bool wasDeleted = AssetDatabase.DeleteAsset(localPath);
            if (wasDeleted)
            {
                Debug.Log($"{prefabName} was destroyed!");
            }
            else
            {
                Debug.LogError($"Prefab with name: {prefabName} \n" +
                               $"Could not be Destroyed! \n" +
                               $"Something went wrong!");
            }
        }

        public static GameObject CreatePrefabFromBase(string folderPath, string prefabName, GameObject basePrefab)
        {
            GameObject copyOfBasePrefab = Object.Instantiate(basePrefab);

            string localpath =
                $"{folderPath}/{prefabName}.prefab";

            GameObject createdPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(localpath);

            if (createdPrefab == null)
            {
                localpath = AssetDatabase.GenerateUniqueAssetPath(localpath);
                createdPrefab = PrefabUtility.SaveAsPrefabAsset(copyOfBasePrefab, localpath);
            }
            else
            {
                Debug.Log($"Prefab {prefabName} has already been created!!!");
            }

            Object.DestroyImmediate(copyOfBasePrefab, true);
            return createdPrefab;
        }
    }
}