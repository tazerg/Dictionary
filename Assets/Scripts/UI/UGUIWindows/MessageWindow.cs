#if !UI_ELEMENTS
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class MessageWindow : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _message;
        
        public override WindowType WindowType => WindowType.FullScreen;

        public void Open(string message)
        {
            Open();

            _message.text = message;
        }

        protected override void Awake()
        {
            base.Awake();
            
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
        }
    }
}
#endif