using System.Collections.Generic;
using JHI.Dict.Model;

namespace JHI.Dict.Providers
{
    public class UnexploredWordsProvider
    {
        private readonly AllWordsProvider _allWordsProvider;

        public UnexploredWordsProvider(AllWordsProvider allWordsProvider)
        {
            _allWordsProvider = allWordsProvider;
        }

        public IEnumerable<Word> GetWords()
        {
            foreach (var word in _allWordsProvider.GetWords())
            {
                if (word.NumberOfCorrect == Consts.EXPLORED_WORDS_CORRECT_ANSWER_NUMBERS)
                    continue;

                yield return word;
            }
        }
    }
}