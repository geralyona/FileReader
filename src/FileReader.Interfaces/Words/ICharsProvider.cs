using FileReader.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Interfaces.Words
{
    public interface ICharsProvider
    {
        /// <summary>
        /// Provides chars from soume source in async way
        /// </summary>
        /// <exception cref="FileReader.Common.Exceptions.CharsProviderException"/ >
        IAsyncEnumerable<char> GetChars(IProgress<ProcessingState> progress, CancellationToken token);
    }
}
