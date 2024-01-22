namespace JHI.Dict.Model
{
    public class Word
    {
        public readonly string Original;
        public readonly string Translate;
        public int NumberOfCorrect;

        public Word(string original, string translate)
        {
            Original = original;
            Translate = translate;
            NumberOfCorrect = 0;
        }

        public override string ToString()
        {
            return $"Original: {Original} | Translate {Translate}";
        }
    }
}

