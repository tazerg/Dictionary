#if UNITY_EDITOR
using System.IO;

namespace JHI.Dict.Serialization
{
    public class EditorJsonFileSaver
    {
        private const string FILE_PATH = "/Editor/JSON/";
        private const string FILE_NAME = "WordsJson.json";

        public static void SaveTestFile(string json)
        {
            var path = Path.Combine(FILE_PATH, FILE_NAME);
            if (File.Exists(path))
                File.Delete(path);
            
            using (var sv = File.CreateText(path))
            {
                sv.Write(json);
                sv.Close();
            }
        }
    }
}
#endif