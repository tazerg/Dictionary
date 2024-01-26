using UnityEngine.UIElements;

namespace JHI.Dict.UI
{
    public class MessageWindow : BaseWindow
    {
        private Button _closeButton;
        private Label _message;
        
        public override WindowType WindowType => WindowType.FullScreen;

        public void Open(string message)
        {
            Open();

            _message.text = message;
        }

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            _closeButton = root.Q<Button>("CloseButton");
            _message = root.Q<Label>("Message");
            
            _closeButton.RegisterCallback<ClickEvent>(OnCloseClick);
        }

        private void OnDisable()
        {
            _closeButton?.UnregisterCallback<ClickEvent>(OnCloseClick);
        }

        private void OnCloseClick(ClickEvent evt)
        {
            Close();
        }
    }
}