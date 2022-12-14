using static System.Reflection.Assembly;
using UnityEngine;
using UnityEditor;

namespace MultiPlay
{
    public class Utils
    {
        public static int GetCurrentClientIndex()
        {
            int clientIndex = 0;
            int dirDepth = Application.dataPath.Split('/').Length;
            string appFolderName = Application.dataPath.Split('/')[dirDepth - 2];
            bool isClient;

            isClient = appFolderName.EndsWith("___Client");
            if (isClient)
            {
                clientIndex = 1;

                if (appFolderName.IndexOf('[') > 0)
                {
                    int.TryParse(appFolderName.Substring(
                    appFolderName.IndexOf('[') + 1, 1), out clientIndex);
                }
            }
            return clientIndex;
        }

        public static void ClearConsole()
        {
            var assembly = GetAssembly(typeof(SceneView));
            var logEntries = assembly.GetType("UnityEditor.LogEntries");
            logEntries.GetMethod("Clear")?.Invoke(new object(), null);
        }
    }
}