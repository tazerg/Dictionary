#if !UI_ELEMENTS
using System;
using System.Threading.Tasks;
using JHI.Dict.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class ExploreWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _wordText;
        [SerializeField] private AnswerButton[] _answerButtons;
        [SerializeField] private Button _closeButton;
        
        private IWindowService _windowService;
        private ExploreWordsService _exploreWordsService;
        private RandomIndexService _randomIndexService;
        
        private bool _checking;
        private int _correctButtonIndex;
        private int _currentExploringWordsCount;

        private ColorBlock _normalColor;
        private ColorBlock _correctColor;
        private ColorBlock _incorrectColor;
        
        public override WindowType WindowType => WindowType.FullScreen;
        
        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _exploreWordsService = ServiceLocator.GetService<ExploreWordsService>();
            _randomIndexService = ServiceLocator.GetService<RandomIndexService>();
            
            base.Awake();
            
            _normalColor = _answerButtons[0].colors;
            var colorBlock = _answerButtons[0].colors;
            colorBlock.normalColor = new Color(0f, 1f, 0f);
            _correctColor = colorBlock;
            colorBlock.normalColor = new Color(1f, 0f, 0f);
            _incorrectColor = colorBlock;
            
            _closeButton.onClick.AddListener(OnCloseClick);
            for(var i = 0; i < _answerButtons.Length; i++)
            {
                var index = i;
                var answerButton = _answerButtons[index];
                answerButton.onClick.AddListener(() => CheckAnswer(index));
            }
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseClick);
            for(var i = 0; i < _answerButtons.Length; i++)
            {
                _answerButtons[i].onClick.RemoveAllListeners();
            }
        }

        private void OnEnable()
        {
            _exploreWordsService.Setup();
            SelectWord();
        }
        
        private void SelectWord()
        {
            _randomIndexService.Setup(4);
            _exploreWordsService.GetWord(out var currentWord, out var randomWords);
            
            _wordText.text = currentWord.Original;
            _correctButtonIndex = _randomIndexService.GetRandomIndex();
            var button = _answerButtons[_correctButtonIndex];
            button.text = currentWord.Translate;

            for (var i = 0; i < randomWords.Length; i++)
            {
                var randomWord = randomWords[i];
                var randomIndex = _randomIndexService.GetRandomIndex();
                var randomButton = _answerButtons[randomIndex];
                randomButton.text = randomWord.Translate;
            }
        }

        private void CheckAnswer(int index)
        {
            _currentExploringWordsCount++;
            
            if (_checking)
                return;
            
            var correctButton = _answerButtons[_correctButtonIndex];
            correctButton.colors = _correctColor;
            
            if (_correctButtonIndex == index)
            {
                _exploreWordsService.UpdateProgressCurrent(true);
                _ = WaitAndSelectNext();
                return;
            }

            var incorrectButton = _answerButtons[index];
            incorrectButton.colors = _incorrectColor;
            _exploreWordsService.UpdateProgressCurrent(false);
            _ = WaitAndSelectNext();
        }
        
        private async Task WaitAndSelectNext()
        {
            _checking = true;

            await Task.Delay(TimeSpan.FromSeconds(Consts.WAIT_FOR_NEXT_WORD_TIME_SEC));

            foreach (var answerButton in _answerButtons)
            {
                answerButton.colors = _normalColor;
            }
            
            _checking = false;
            
            if (_currentExploringWordsCount == Consts.MAX_WORDS_IN_SESSION)
                OnCloseClick();
            
            SelectWord();
        }
        
        private void OnCloseClick()
        {
            _windowService.OpenWindow<MainWindow>();
        }
    }
}
#endif