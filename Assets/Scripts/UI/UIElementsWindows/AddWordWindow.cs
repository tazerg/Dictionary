#if UI_ELEMENTS
using JHI.Dict.Model;
using JHI.Dict.Services;
using UnityEngine.UIElements;

namespace JHI.Dict.UI.UIElements
{
    public class AddWordWindow : BaseWindow
    {
        private TextField _originalTextField;
        private TextField _translateTextField;
        private Button _addWordsButton;
        private Button _closeButton;
        
        private IWindowService _windowService;
        private IAddingWordService _addingWordService;
        
        public override WindowType WindowType => WindowType.FullScreen;

        protected override void Awake()
        {
            _windowService = ServiceLocator.GetService<IWindowService>();
            _addingWordService = ServiceLocator.GetService<IAddingWordService>();
            
            base.Awake();
        }

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _originalTextField = root.Q<TextField>("OriginalText");
            _translateTextField = root.Q<TextField>("TranslatedText");
            _addWordsButton = root.Q<Button>("AddButton");
            _closeButton = root.Q<Button>("CloseButton");
            
            _addWordsButton.RegisterCallback<ClickEvent>(OnAddWordButtonClick);
            _closeButton.RegisterCallback<ClickEvent>(OnCloseClick);
        }

        private void OnDisable()
        {
            _addWordsButton?.UnregisterCallback<ClickEvent>(OnAddWordButtonClick);
            _closeButton?.UnregisterCallback<ClickEvent>(OnCloseClick);
        }

        private void OnAddWordButtonClick(ClickEvent evt)
        {
            var original = _originalTextField.value;
            var translate = _translateTextField.value;
            var word = new Word(original, translate);
            _addingWordService.Add(word);
        }

        private void OnCloseClick(ClickEvent evt)
        {
            _windowService.OpenWindow<MainWindow>();
        }
    }
}
#endif