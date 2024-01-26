using UnityEngine.UIElements;

namespace JHI.Dict.UI
{
    public class MainWindow : BaseWindow
    {
        private Button _addWordsButton;
        private Button _wordsListButton;
        
        private IWindowService _windowService;
        
        public override WindowType WindowType => WindowType.Start;

        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            
            base.Awake();
        }

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _addWordsButton = root.Q<Button>("AddWordButton");
            _wordsListButton = root.Q<Button>("WordsListButton");
            
            _addWordsButton.RegisterCallback<ClickEvent>(OnAddWordButtonClick);
            _wordsListButton.RegisterCallback<ClickEvent>(OnWordsListButtonClick);
        }

        private void OnDisable()
        {
            _addWordsButton?.UnregisterCallback<ClickEvent>(OnAddWordButtonClick);
            _wordsListButton?.UnregisterCallback<ClickEvent>(OnWordsListButtonClick);
        }

        private void OnAddWordButtonClick(ClickEvent evt)
        {
            _windowService.OpenWindow<AddWordWindow>();
        }

        private void OnWordsListButtonClick(ClickEvent evt)
        {
            _windowService.OpenWindow<WordsListWindow>();
        }
    }
}