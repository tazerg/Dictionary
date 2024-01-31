#if UI_ELEMENTS
using System.Globalization;
using JHI.Dict.Providers;
using UnityEngine;
using UnityEngine.UIElements;

namespace JHI.Dict.UI.UIElements
{
    public class WordsListWindow : BaseWindow
    {
        [SerializeField] private VisualTreeAsset _wordPanelTemplate;
        
        private Button _closeButton;
        private ScrollView _wordsScroll;
        
        private IWindowService _windowService;
        private SortedByUnexploredWordsProvider _wordsProvider;

        public override WindowType WindowType => WindowType.FullScreen;

        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _wordsProvider = ServiceLocator.GetService<SortedByUnexploredWordsProvider>();
            
            base.Awake();
        }

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _closeButton = root.Q<Button>("CloseButton");
            _wordsScroll = root.Q<ScrollView>("WordsScroll");
            
            _closeButton.RegisterCallback<ClickEvent>(OnCloseClick);
            
            foreach (var word in _wordsProvider.GetWords())
            {
                var wordInstance = _wordPanelTemplate.Instantiate();
                wordInstance.Q<Label>("OriginalValueLabel").text = word.Original;
                wordInstance.Q<Label>("TranslatedValueLabel").text = word.Translate;
                wordInstance.Q<Label>("ProgressValueLabel").text = $"{(word.NumberOfCorrect / 100f).ToString(CultureInfo.InvariantCulture)}%";
                
                _wordsScroll.Add(wordInstance);
            }
        }

        private void OnDisable()
        {
            _wordsScroll?.Clear();
            
            _closeButton?.RegisterCallback<ClickEvent>(OnCloseClick);
        }

        private void OnCloseClick(ClickEvent evt)
        {
            _windowService.OpenWindow<MainWindow>();
        }
    }
}
#endif