using FileReader.Common.Data;
using FileReader.Interfaces.Logic;
using FileReader.Interfaces.Words;

namespace FileReader.Services.Logic
{
    public class CountsCalculator : ICountsCalculator
    {
        private const int ExpectedMaxWordLength = 100;

        public async Task<IReadOnlyList<WordCount>> CalculateWordCounts(ICharsProvider charsProvider, IProgress<ProcessingState> progress, CancellationToken token)
        {
            var wordsCounts = new Dictionary<string, int>();
            var wordChars = new List<char>(ExpectedMaxWordLength);
            await foreach (var c in charsProvider.GetChars(progress, token))
            {
                if (char.IsWhiteSpace(c))
                {
                    if (wordChars.Count == 0)
                        continue;

                    AppendWord(wordsCounts, wordChars);
                    wordChars.Clear();
                }
                else
                {
                    wordChars.Add(c);
                }
            }

            if (wordChars.Count != 0)
                AppendWord(wordsCounts, wordChars);

            return wordsCounts.Select(t => new WordCount(t.Key, t.Value)).ToList();
        }

        private static void AppendWord(Dictionary<string, int> wordsCounts, List<char> wordChars)
        {
            var word = string.Join("", wordChars);
            if (wordsCounts.TryGetValue(word, out var count))
                wordsCounts[word] = count + 1;
            else
                wordsCounts.Add(word, 1);
        }
    }
}
