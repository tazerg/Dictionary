using System.Linq;

namespace JHI.Dict.Providers
{
    public class WordsCountProvider
    {
        private readonly AllWordsProvider _allWordsProvider;

        public WordsCountProvider(AllWordsProvider allWordsProvider)
        {
            _allWordsProvider = allWordsProvider;
        }

        public int GetCount()
        {
            return _allWordsProvider.GetWords().Count();
        }
    }
}