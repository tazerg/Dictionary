using UnityEngine;

namespace JHI.Dict.Utils
{
    [DefaultExecutionOrder(-100)]
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}