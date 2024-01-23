#if UNITY_EDITOR
using System.IO;
using UnityEngine;

namespace JHI.Dict.Serialization
{
    public class EditorJsonFileSaver
    {
        private const string FILE_PATH = "/Editor/JSON/";
        private const string FILE_NAME = "WordsJson.json";

        public static void SaveTestFile(string json)
        {
            if (string.IsNullOrEmpty(json))
                return;
            
            var path = $"{Application.dataPath}{FILE_PATH}";
            Debug.Log($"Save json to file {path}");
            
            if (File.Exists(path))
                File.Delete(path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path += FILE_NAME;
            using (var sv = File.CreateText(path))
            {
                sv.Write(json);
                sv.Close();
            }
        }
    }
}
#endif