using FileReader.Common.Data;
using FileReader.Interfaces.Words;

namespace FileReader.Interfaces.Logic
{
    public interface ICountsCalculator
    {
        /// <exception cref="FileReader.Common.Exceptions.CharsProviderException"/ >
        Task<IReadOnlyList<WordCount>> CalculateWordCounts( ICharsProvider charsProvider, IProgress<ProcessingState> progress,  CancellationToken token);
    }
}
