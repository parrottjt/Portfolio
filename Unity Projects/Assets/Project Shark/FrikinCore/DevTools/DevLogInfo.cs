using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace FrikinCore.DevelopmentTools
{
    [CreateAssetMenu(fileName = "DevLogInfo", menuName = "DevLog /Dev Log Info")]

    public class DevLogInfo : ScriptableObject
    {
        [SerializeField] List<string> devLogLines = new List<string>();

        public void WriteLineToList(string writtenBy, string message)
        {
            devLogLines.Add($"{writtenBy}: {message}");
        }

        public void ClearDevLogLines()
        {
            devLogLines.Clear();
        }

        public void WriteToFile()
        {
            File.WriteAllLines(Application.persistentDataPath + "/FrikinDevLog.text", devLogLines);
        }

        public void OpenLogFileOnComputer()
        {
            Process.Start("notepad.exe", Application.persistentDataPath + "/FrikinDevLog.text");
        }
    }
}
