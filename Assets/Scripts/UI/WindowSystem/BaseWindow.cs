using UnityEngine;

namespace JHI.Dict.UI
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public abstract WindowType WindowType { get; }
        
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}