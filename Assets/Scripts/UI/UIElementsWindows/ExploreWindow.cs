#if UI_ELEMENTS
using System;
using System.Threading.Tasks;
using JHI.Dict.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace JHI.Dict.UI.UIElements
{
    public class ExploreWindow : BaseWindow
    {
        private Label _wordLabel;
        private Button _firstButton;
        private Button _secondButton;
        private Button _thirdButton;
        private Button _fourthButton;
        private Button _closeButton;
        
        private IWindowService _windowService;
        private ExploreWordsService _exploreWordsService;
        private RandomIndexService _randomIndexService;

        private bool _checking;
        private int _correctButtonIndex;
        private int _currentExploringWordsCount;

        private StyleColor _normalColor;
        private readonly StyleColor _correctColor = new(new Color(0f, 1f, 0f));
        private readonly StyleColor _incorrectColor = new(new Color(1f, 0f, 0f));
        
        public override WindowType WindowType => WindowType.FullScreen;
        
        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _exploreWordsService = ServiceLocator.GetService<ExploreWordsService>();
            _randomIndexService = ServiceLocator.GetService<RandomIndexService>();
            
            base.Awake();
        }

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _wordLabel = root.Q<Label>("WordLabel");
            _firstButton = root.Q<Button>("FirstButton");
            _secondButton = root.Q<Button>("SecondButton");
            _thirdButton = root.Q<Button>("ThirdButton");
            _fourthButton = root.Q<Button>("FourthButton");
            _closeButton = root.Q<Button>("CloseButton");
            
            _firstButton.RegisterCallback<ClickEvent>(OnFirstButtonClick);
            _secondButton.RegisterCallback<ClickEvent>(OnSecondButtonClick);
            _thirdButton.RegisterCallback<ClickEvent>(OnThirdButtonClick);
            _fourthButton.RegisterCallback<ClickEvent>(OnFourthButtonClick);
            _closeButton.RegisterCallback<ClickEvent>(OnCloseClick);

            _normalColor = _firstButton.style.color;

            _exploreWordsService.Setup();

            SelectWord();
        }

        private void OnDisable()
        {
            _firstButton?.UnregisterCallback<ClickEvent>(OnFirstButtonClick);
            _secondButton?.UnregisterCallback<ClickEvent>(OnSecondButtonClick);
            _thirdButton?.UnregisterCallback<ClickEvent>(OnThirdButtonClick);
            _fourthButton?.UnregisterCallback<ClickEvent>(OnFourthButtonClick);
            _closeButton?.UnregisterCallback<ClickEvent>(OnCloseClick);
        }

        private void SelectWord()
        {
            _randomIndexService.Setup(4);
            _exploreWordsService.GetWord(out var currentWord, out var randomWords);
            
            _wordLabel.text = currentWord.Original;
            _correctButtonIndex = _randomIndexService.GetRandomIndex();
            var button = GetButton(_correctButtonIndex);
            button.text = currentWord.Translate;

            for (var i = 0; i < randomWords.Length; i++)
            {
                var randomWord = randomWords[i];
                var randomIndex = _randomIndexService.GetRandomIndex();
                var randomButton = GetButton(randomIndex);
                randomButton.text = randomWord.Translate;
            }
        }

        private void CheckAnswer(int index)
        {
            _currentExploringWordsCount++;
            
            if (_checking)
                return;
            
            _checking = true;

            var correctButton = GetButton(_correctButtonIndex);
            correctButton.style.color = _correctColor;
            
            if (_correctButtonIndex == index)
            {
                _exploreWordsService.UpdateProgressCurrent(true);
                _ = WaitAndSelectNext();
                return;
            }

            var incorrectButton = GetButton(index);
            incorrectButton.style.color = _incorrectColor;
            _exploreWordsService.UpdateProgressCurrent(false);
            _ = WaitAndSelectNext();
        }

        private async Task WaitAndSelectNext()
        {
            await Task.Delay(TimeSpan.FromSeconds(Consts.WAIT_FOR_NEXT_WORD_TIME_SEC));
            
            _firstButton.style.color = _normalColor;
            _secondButton.style.color = _normalColor;
            _thirdButton.style.color = _normalColor;
            _fourthButton.style.color = _normalColor;
            
            _checking = false;
            
            if (_currentExploringWordsCount == Consts.MAX_WORDS_IN_SESSION)
                OnCloseClick(null);
            
            SelectWord();
        }

        private Button GetButton(int index)
        {
            switch (index)
            {
                case 0:
                    return _firstButton;
                case 1:
                    return _secondButton;
                case 2:
                    return _thirdButton;
                case 3:
                    return _fourthButton;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnFirstButtonClick(ClickEvent evt)
        {
            CheckAnswer(0);
        }

        private void OnSecondButtonClick(ClickEvent evt)
        {
            CheckAnswer(1);
        }

        private void OnThirdButtonClick(ClickEvent evt)
        {
            CheckAnswer(2);
        }

        private void OnFourthButtonClick(ClickEvent evt)
        {
            CheckAnswer(3);
        }

        private void OnCloseClick(ClickEvent evt)
        {
            _windowService.OpenWindow<MainWindow>();
        }
    }
}
#endif