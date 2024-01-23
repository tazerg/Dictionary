using UnityEngine.UIElements;

namespace JHI.Dict.UI
{
    public class MainWindow : BaseWindow
    {
        private Button _addWordsButton;
        
        private IWindowService _windowService;
        
        public override WindowType WindowType => WindowType.Start;

        private void Start()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();

            var root = GetComponent<UIDocument>().rootVisualElement;
            _addWordsButton = root.Q<Button>("AddWordButton");
            
            _addWordsButton.RegisterCallback<ClickEvent>(OnAddWordButtonClick);
        }

        private void OnDestroy()
        {
            _addWordsButton.UnregisterCallback<ClickEvent>(OnAddWordButtonClick);
        }

        private void OnAddWordButtonClick(ClickEvent evt)
        {
            _windowService.OpenWindow<AddWordWindow>();
        }
    }
}