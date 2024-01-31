using System.Collections.Generic;
using JHI.Dict.Extensions;

namespace JHI.Dict.Services
{
    public class RandomIndexService
    {
        private readonly HashSet<int> _indexes = new();

        public void Setup(int indexesCount)
        {
            for (var i = 0; i < indexesCount; i++)
            {
                _indexes.Add(i);
            }
        }

        public int GetRandomIndex()
        {
            var index = _indexes.RandomElement();
            _indexes.Remove(index);
            return index;
        }
    }
}