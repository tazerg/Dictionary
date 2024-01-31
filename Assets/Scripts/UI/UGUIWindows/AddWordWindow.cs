#if !UI_ELEMENTS
using JHI.Dict.Model;
using JHI.Dict.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class AddWordWindow : BaseWindow
    {
        [SerializeField] private TMP_InputField _originalTextField;
        [SerializeField] private TMP_InputField _translateTextField;
        [SerializeField] private Button _addWordsButton;
        [SerializeField] private Button _closeButton;
        
        private IWindowService _windowService;
        private IAddingWordService _addingWordService;
        
        public override WindowType WindowType => WindowType.FullScreen;
        
        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _addingWordService = ServiceLocator.GetService<IAddingWordService>();
            
            base.Awake();
            
            _addWordsButton.onClick.AddListener(OnAddWordButtonClick);
            _closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnDestroy()
        {
            _addWordsButton.onClick.RemoveListener(OnAddWordButtonClick);
            _closeButton.onClick.RemoveListener(OnCloseClick);
        }

        private void OnAddWordButtonClick()
        {
            var original = _originalTextField.text;
            var translate = _translateTextField.text;
            var word = new Word(original, translate);
            _addingWordService.Add(word);
        }

        private void OnCloseClick()
        {
            _windowService.OpenWindow<MainWindow>();
        }
    }
}
#endif