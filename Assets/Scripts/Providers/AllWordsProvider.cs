using System.Collections.Generic;
using JHI.Dict.Model;
using JHI.Dict.Services;
using UnityEngine;

namespace JHI.Dict.Providers
{
    public class AllWordsProvider : IAddingWordService
    {
        private readonly List<Word> _words;

        public AllWordsProvider(List<Word> words)
        {
            _words = words;
        }

        public IEnumerable<Word> GetWords()
        {
            return _words;
        }

        public void Add(Word word)
        {
            if (_words.Exists(x => x.Translate == word.Translate && x.Original == word.Original))
                Debug.LogWarning($"Word ({word}) already added");
            
            _words.Add(word);
        }
    }
}