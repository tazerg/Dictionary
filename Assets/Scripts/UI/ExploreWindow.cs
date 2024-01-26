using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHI.Dict.Extensions;
using JHI.Dict.Model;
using JHI.Dict.Providers;
using UnityEngine;
using UnityEngine.UIElements;

namespace JHI.Dict.UI
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
        private UnexploredWordsProvider _wordsProvider;

        private bool _checking;
        private int _correctButtonIndex;
        private int _currentExploringWordsCount;
        private Word _currentWord;

        private StyleColor _normalColor;
        private readonly StyleColor _correctColor = new(new Color(0f, 1f, 0f));
        private readonly StyleColor _incorrectColor = new(new Color(1f, 0f, 0f));
        
        private readonly HashSet<int> _indexes = new();
        private readonly HashSet<Word> _currentExplored = new();
        private readonly HashSet<Word> _currentExcluded = new();
        
        public override WindowType WindowType => WindowType.FullScreen;
        
        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _wordsProvider = ServiceLocator.GetService<UnexploredWordsProvider>();
            
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
            _currentExcluded.Clear();
            for (var i = 0; i < 4; i++)
            {
                _indexes.Add(i);
            }
            
            _currentWord = GetRandomNotExploredWord();
            _wordLabel.text = _currentWord.Original;
            _correctButtonIndex = GetRandomIndex();
            var button = GetButton(_correctButtonIndex);
            button.text = _currentWord.Translate;

            for (var i = 0; i < 3; i++)
            {
                var randomWord = GetRandomWord();
                var randomIndex = GetRandomIndex();
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
                _currentWord.NumberOfCorrect++;
                _ = WaitAndSelectNext();
                return;
            }

            var incorrectButton = GetButton(index);
            incorrectButton.style.color = _incorrectColor;
            _currentWord.NumberOfCorrect--;
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

        private Word GetRandomNotExploredWord()
        {
            var word = _wordsProvider.GetWords().RandomElement();
            if (!_currentExplored.Add(word))
                return GetRandomNotExploredWord();

            _currentExcluded.Add(word);
            return word;
        }

        private Word GetRandomWord()
        {
            var word = _wordsProvider.GetWords().RandomElement();
            if (!_currentExcluded.Add(word))
                return GetRandomWord();

            return word;
        }

        private int GetRandomIndex()
        {
            var index = _indexes.RandomElement();
            _indexes.Remove(index);
            return index;
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