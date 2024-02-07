using System.Collections.Generic;
using JHI.Dict.Extensions;
using JHI.Dict.Model;
using JHI.Dict.Providers;

namespace JHI.Dict.Services
{
    public class ExploreWordsService
    {
        private readonly UnexploredWordsProvider _wordsProvider;

        private int _currentWord = 0;
        
        private readonly List<Word> _currentExplored = new();
        private readonly HashSet<Word> _currentExcluded = new();

        public ExploreWordsService(UnexploredWordsProvider wordsProvider)
        {
            _wordsProvider = wordsProvider;
        }

        public void Setup()
        {
            _currentWord = 0;
            _currentExplored.Clear();
            
            for (var i = 0; i < Consts.MAX_WORDS_IN_SESSION; i++)
            {
                var randomWord = GetRandomNotExploredWord();
                _currentExplored.Add(randomWord);
            }
        }

        public void GetWord(out Word exploreWord, out Word[] randomWords)
        {
            _currentExcluded.Clear();
            
            exploreWord = _currentExplored[_currentWord];
            _currentExcluded.Add(exploreWord);
            
            randomWords = new Word[3];
            for (var i = 0; i < 3; i++)
            {
                randomWords[i] = GetRandomWord();
            }
        }

        public void UpdateProgressCurrent(bool isCorrectAnswer)
        {
            _currentExplored[_currentWord].UpdateCorrectAnswer(isCorrectAnswer);
            _currentWord++;
        }
        
        private Word GetRandomNotExploredWord()
        {
            var word = _wordsProvider.GetWords().RandomElement();
            return _currentExplored.Contains(word) 
                ? GetRandomNotExploredWord() 
                : word;
        }
        
        private Word GetRandomWord()
        {
            var word = _wordsProvider.GetWords().RandomElement();
            if (!_currentExcluded.Add(word))
                return GetRandomWord();

            return word;
        }
    }
}