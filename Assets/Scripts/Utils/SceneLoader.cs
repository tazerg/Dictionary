using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace JHI.Dict.Utils
{
    public static class SceneLoader
    {
        private const float DELAY_TIME = 0.2f;
        
        public static async Task Load(string path)
        {
            var operation = SceneManager.LoadSceneAsync(path);
            
            while (true)
            {
                if (operation.isDone)
                    break;

                await Task.Delay(TimeSpan.FromSeconds(DELAY_TIME));
            }
        }
    }
}