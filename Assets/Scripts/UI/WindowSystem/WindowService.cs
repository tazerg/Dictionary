using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JHI.Dict.UI
{
    public class WindowService : MonoBehaviour, IWindowService
    {
        private readonly List<IWindow> _windows = new();

        private IWindow _currentOpenedWindow;

        public void OpenWindow<T>() where T : BaseWindow
        {
            if (!TryFindWindow<T>(out var window))
            {
                Debug.LogError($"Can't find window with type {nameof(T)}");
                return;
            }
            
            _currentOpenedWindow?.Close();
            _currentOpenedWindow = window;
            _currentOpenedWindow.Open();
        }

        private bool TryFindWindow<T>(out IWindow window) where T : BaseWindow
        {
            window = _windows.FirstOrDefault(x => x is T);
            if (window != null)
                return true;

            window = FindFirstObjectByType<T>(FindObjectsInactive.Include);
            if (window == null)
                return false;
            
            _windows.Add(window);
            return true;
        }

        private void Awake()
        {
            var allWindows = FindObjectsByType<BaseWindow>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _windows.AddRange(allWindows);
            
            foreach (var window in _windows)
            {
                window.Close();
            }
        }
    }
}