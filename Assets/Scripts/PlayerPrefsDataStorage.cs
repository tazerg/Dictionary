using UnityEngine;

namespace JHI.Dict
{
    public static class PlayerPrefsDataStorage
    {
        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key, string.Empty);
        }

        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
    }
}