using Newtonsoft.Json;

namespace JHI.Dict.Model
{
    public class Word
    {
        [JsonProperty("Original")]
        private string _original;
        [JsonProperty("Translate")]
        private string _translate;
        [JsonProperty("NumberOfCorrect")]
        private int _numberOfCorrect;

        [JsonIgnore]
        public string Original => _original;
        [JsonIgnore]
        public string Translate => _translate;
        [JsonIgnore]
        public int NumberOfCorrect => _numberOfCorrect;

        public Word(string original, string translate)
        {
            _original = original;
            _translate = translate;
            _numberOfCorrect = 0;
        }

        public void UpdateCorrectAnswer(bool isCorrectAnswer)
        {
            if (isCorrectAnswer)
                _numberOfCorrect += 1;
            else
                _numberOfCorrect -= 1;
        }

        public void UpdateTexts(string original, string translate)
        {
            _original = original;
            _translate = translate;
        }

        public override string ToString()
        {
            return $"Original: {_original} | Translate {_translate}";
        }
    }
}

