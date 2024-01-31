#if !UI_ELEMENTS
using TMPro;
using UnityEngine;

namespace JHI.Dict.UI.UGUI
{
    public class WordPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _originalText;
        [SerializeField] private TMP_Text _translatedText;
        [SerializeField] private TMP_Text _progressText;

        public void Setup(string original, string translated, string progress)
        {
            _originalText.text = original;
            _translatedText.text = translated;
            _progressText.text = $"{progress}%";
        }
    }
}
#endif