using System.Collections.Generic;
using System.Linq;
using JHI.Dict.Model;

namespace JHI.Dict.Providers
{
    public class SortedByUnexploredWordsProvider
    {
        private readonly AllWordsProvider _allWordsProvider;

        public SortedByUnexploredWordsProvider(AllWordsProvider allWordsProvider)
        {
            _allWordsProvider = allWordsProvider;
        }

        public IEnumerable<Word> GetWords()
        {
            return _allWordsProvider.GetWords().OrderBy(x => x.NumberOfCorrect);
        }
    }
}