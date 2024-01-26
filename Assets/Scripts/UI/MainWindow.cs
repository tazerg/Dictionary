using JHI.Dict.Providers;
using UnityEngine.UIElements;

namespace JHI.Dict.UI
{
    public class MainWindow : BaseWindow
    {
        private Button _exploreButton;
        private Button _addWordsButton;
        private Button _wordsListButton;
        
        private IWindowService _windowService;
        private WordsCountProvider _wordsCountProvider;
        
        public override WindowType WindowType => WindowType.Start;

        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _wordsCountProvider = ServiceLocator.GetService<WordsCountProvider>();
            
            base.Awake();
        }

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _exploreButton = root.Q<Button>("ExploreButton");
            _addWordsButton = root.Q<Button>("AddWordButton");
            _wordsListButton = root.Q<Button>("WordsListButton");
            
            _exploreButton.RegisterCallback<ClickEvent>(OnExploreButtonClick);
            _addWordsButton.RegisterCallback<ClickEvent>(OnAddWordButtonClick);
            _wordsListButton.RegisterCallback<ClickEvent>(OnWordsListButtonClick);
        }

        private void OnDisable()
        {
            _exploreButton?.UnregisterCallback<ClickEvent>(OnExploreButtonClick);
            _addWordsButton?.UnregisterCallback<ClickEvent>(OnAddWordButtonClick);
            _wordsListButton?.UnregisterCallback<ClickEvent>(OnWordsListButtonClick);
        }

        private void OnExploreButtonClick(ClickEvent evt)
        {
            if (_wordsCountProvider.GetCount() < Consts.MIN_WORDS_COUNT)
            {
                var messageWindow = _windowService.GetWindow<MessageWindow>();
                messageWindow.Open(Messages.NOT_ENOUGH_WORDS);
                return;
            }
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