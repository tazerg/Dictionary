#if !UI_ELEMENTS
using System;
using JHI.Dict.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JHI.Dict.UI.UGUI
{
    public class WordPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _originalText;
        [SerializeField] private TMP_Text _translatedText;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Button _updateButton;

        private Word _word;

        public event Action<Word> UpdateClicked;

        public void Setup(Word word)
        {
            _word = word;
            _originalText.text = _word.Original;
            _translatedText.text = _word.Translate;
            _progressText.text = $"{_word.NumberOfCorrect}%";
        }

        private void Awake()
        {
            _updateButton.onClick.AddListener(OnUpdateClicked);
        }

        private void OnDestroy()
        {
            _updateButton.onClick.RemoveListener(OnUpdateClicked);
        }

        private void OnUpdateClicked()
        {
            UpdateClicked?.Invoke(_word);
        }
    }
}
#endif