using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using FrikinCore.Achievement;

namespace FrikinCore.Save
{
    public static class SaveManager
    {
        public static PlayerData playerData = null;

        public static void SavePlayer(PersistentDataManager player)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream =
                new FileStream(Application.persistentDataPath + "/FrikinLaserShark.sav", FileMode.Create);

            PlayerData data = new PlayerData(player);

            bf.Serialize(stream, data);
            stream.Close();
        }

        public static void LoadGameData()
        {
            if (File.Exists(Application.persistentDataPath + "/FrikinLaserShark.sav"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/FrikinLaserShark.sav",
                    FileMode.Open);

                PlayerData data = bf.Deserialize(stream) as PlayerData;

                stream.Close();
                playerData = data;
            }
            else
            {
                playerData = new PlayerData();
            }
        }

        public static void DeleteGameData()
        {
            if (File.Exists(Application.persistentDataPath + "/FrikinLaserShark.sav"))
            {
                File.Delete(Application.persistentDataPath + "/FrikinLaserShark.sav");
                playerData = new PlayerData();
            }
        }
    }

    [Serializable]
    public class PlayerData
    {
        //This is for PersistentDataManager
        public int[] SavedInts = new int[PersistentDataManager.PersistentIntCount];
        public int[][] SavedIntArrays = new int[PersistentDataManager.PersistentIntArrayCount][];
        public bool[] SavedBools = new bool[PersistentDataManager.PersistentIntCount];
        public bool[][] SavedBoolArrays = new bool[PersistentDataManager.PersistentIntCount][];

        //This constructor contains default information for a base load of the this class
        public PlayerData()
        {

        }

        public PlayerData(PersistentDataManager persistentData)
        {
            //Ints
            for (int i = 0; i < PersistentDataManager.DataIntDictionary.Count; i++)
            {
                SavedInts[i] = PersistentDataManager.DataIntDictionary[(PersistentDataManager.DataInts)i];
            }

            for (int i = PersistentDataManager.AchievementIntDictionary.Count - 1; i >= 0; i--)
            {
                SavedInts[i + PersistentDataManager.DataIntDictionary.Count] =
                    PersistentDataManager.AchievementIntDictionary[(PersistentDataManager.AchievementInts)i];
            }

            //Bools
            for (int i = 0; i < PersistentDataManager.DataBoolDictionary.Count; i++)
            {
                SavedBools[i] = PersistentDataManager.DataBoolDictionary[(PersistentDataManager.DataBools)i];
            }

            for (int i = PersistentDataManager.AchievementIntDictionary.Count - 1; i >= 0; i--)
            {
                SavedBools[i + PersistentDataManager.DataBoolDictionary.Count] =
                    PersistentDataManager.AchievementBoolDictionary[(Achievements)i];
            }

            //Int[]
            for (int i = 0; i < PersistentDataManager.DataIntArrayDictionary.Count; i++)
            {
                SavedIntArrays[i] =
                    PersistentDataManager.DataIntArrayDictionary[(PersistentDataManager.DataIntArrays)i].ToArray();
            }

            for (int i = 0; i < PersistentDataManager.AchievementIntArrayDictionary.Count; i++)
            {
                SavedIntArrays[i + PersistentDataManager.DataIntArrayDictionary.Count] =
                    PersistentDataManager
                        .AchievementIntArrayDictionary[(PersistentDataManager.AchievementIntArrays)i]
                        .ToArray();
            }

            //bool[]
            for (int i = 0; i < PersistentDataManager.DataBoolArrayDictionary.Count; i++)
            {
                SavedBoolArrays[i] =
                    PersistentDataManager
                        .DataBoolArrayDictionary[(PersistentDataManager.DataBoolArrays)i].ToArray();
            }

            for (int i = 0; i < PersistentDataManager.AchievementBoolArrayDictionary.Count; i++)
            {
                SavedBoolArrays[i + PersistentDataManager.DataBoolArrayDictionary.Count] =
                    PersistentDataManager.AchievementBoolArrayDictionary[(
                        PersistentDataManager.AchievementBoolArrays)i].ToArray();
            }
        }
    }
}
