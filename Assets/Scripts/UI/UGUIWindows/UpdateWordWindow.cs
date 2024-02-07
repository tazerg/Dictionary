using JHI.Dict.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class UpdateWordWindow : BaseWindow
    {
        [SerializeField] private TMP_InputField _originalTextField;
        [SerializeField] private TMP_InputField _translateTextField;
        [SerializeField] private Button _updateWordButton;
        [SerializeField] private Button _closeButton;
        
        private IWindowService _windowService;

        private Word _currentWord;
        
        public override WindowType WindowType => WindowType.FullScreen;

        public void Open(Word word)
        {
            _currentWord = word;

            _originalTextField.text = word.Original;
            _translateTextField.text = word.Translate;
            
            Open();
        }
        
        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            
            base.Awake();
            
            _updateWordButton.onClick.AddListener(OnUpdateWordButtonClick);
            _closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnDestroy()
        {
            _updateWordButton.onClick.RemoveListener(OnUpdateWordButtonClick);
            _closeButton.onClick.RemoveListener(OnCloseClick);
        }

        private void OnUpdateWordButtonClick()
        {
            _currentWord.UpdateTexts(_originalTextField.text, _translateTextField.text);
            _originalTextField.text = string.Empty;
            _translateTextField.text = string.Empty;
            OnCloseClick();
        }

        private void OnCloseClick()
        {
            _currentWord = null;
            Close();
            _windowService.OpenWindow<WordsListWindow>();
        }
    }
}