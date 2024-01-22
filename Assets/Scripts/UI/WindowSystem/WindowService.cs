using UnityEngine;

namespace JHI.Dict.UI
{
    public class WindowService : MonoBehaviour, IWindowService
    {
        public void ShowStartWindow()
        {
            throw new System.NotImplementedException();
        }

        public void OpenWindow<T>() where T : IWindow
        {
            throw new System.NotImplementedException();
        }
    }
}