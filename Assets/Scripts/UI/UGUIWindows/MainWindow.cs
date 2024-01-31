#if !UI_ELEMENTS
using JHI.Dict.Providers;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class MainWindow : BaseWindow
    {
        [SerializeField] private Button _exploreButton;
        [SerializeField] private Button _addWordsButton;
        [SerializeField] private Button _wordsListButton;
        
        private IWindowService _windowService;
        private WordsCountProvider _wordsCountProvider;
        
        public override WindowType WindowType => WindowType.Start;
        
        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _wordsCountProvider = ServiceLocator.GetService<WordsCountProvider>();
            
            base.Awake();
            
            _exploreButton.onClick.AddListener(OnExploreButtonClick);
            _addWordsButton.onClick.AddListener(OnAddWordButtonClick);
            _wordsListButton.onClick.AddListener(OnWordsListButtonClick);
        }

        private void OnDestroy()
        {
            _exploreButton.onClick.RemoveListener(OnExploreButtonClick);
            _addWordsButton.onClick.RemoveListener(OnAddWordButtonClick);
            _wordsListButton.onClick.RemoveListener(OnWordsListButtonClick);
        }
        
        private void OnExploreButtonClick()
        {
            if (_wordsCountProvider.GetCount() < Consts.MIN_WORDS_COUNT)
            {
                var messageWindow = _windowService.GetWindow<MessageWindow>();
                messageWindow.Open(Messages.NOT_ENOUGH_WORDS);
                return;
            }
            
            _windowService.OpenWindow<ExploreWindow>();
        }

        private void OnAddWordButtonClick()
        {
            _windowService.OpenWindow<AddWordWindow>();
        }

        private void OnWordsListButtonClick()
        {
            _windowService.OpenWindow<WordsListWindow>();
        }
    }
}
#endif