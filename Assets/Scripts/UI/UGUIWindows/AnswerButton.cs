#if !UI_ELEMENTS
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public ColorBlock colors
        {
            get => _button.colors;
            set => _button.colors = value;
        }

        public Button.ButtonClickedEvent onClick => _button.onClick;

        public string text
        {
            set => _text.text = value;
        }
    }
}
#endif