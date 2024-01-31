#if !UI_ELEMENTS
using System.Collections.Generic;
using System.Globalization;
using JHI.Dict.Providers;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class WordsListWindow : BaseWindow
    {
        [SerializeField] private WordPanel _wordPanelPrefab;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _wordsParent;
        
        private IWindowService _windowService;
        private SortedByUnexploredWordsProvider _wordsProvider;

        private List<WordPanel> _wordInstances = new();
        
        public override WindowType WindowType => WindowType.FullScreen;
        
        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _wordsProvider = ServiceLocator.GetService<SortedByUnexploredWordsProvider>();
            
            base.Awake();
            
            _closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnEnable()
        {
            foreach (var word in _wordsProvider.GetWords())
            {
                var wordInstance = Instantiate(_wordPanelPrefab, _wordsParent);
                var progress = word.NumberOfCorrect / 100f;
                wordInstance.Setup(word.Original, word.Translate, progress.ToString(CultureInfo.InvariantCulture));
                _wordInstances.Add(wordInstance);
            }
        }

        private void OnDisable()
        {
            foreach (var wordInstance in _wordInstances)
            {
                Destroy(wordInstance.gameObject);
            }
            
            _wordInstances.Clear();
        }

        private void OnCloseClick()
        {
            _windowService.OpenWindow<MainWindow>();
        }
    }
}
#endif