using FileReader.Common.Data;
using FileReader.Common.Exceptions;
using FileReader.Interfaces.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Services.Words
{
    internal class FileASCIICharsProvider : ICharsProvider
    {
        private readonly string _fileName;

        private const int BufferSize = 10000;
        private const int BytesInKByte = 1024;

        public FileASCIICharsProvider(string fileName)
        {
            _fileName = fileName;
        }

        public async IAsyncEnumerable<char> GetChars(IProgress<ProcessingState> progress, [EnumeratorCancellation] CancellationToken token)
        {
            FileStream fs;
            try
            {
                fs = File.Open(_fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (fs.Length == 0)
                    yield break;
            }
            catch (DirectoryNotFoundException de)
            {
                throw new CharsProviderException("Directory not found", de);
            }
            catch (PathTooLongException pe)
            {
                throw new CharsProviderException("File name is too long", pe);
            }
            catch (FileNotFoundException fe)
            {
                throw new CharsProviderException("File not found", fe);
            }
            catch (UnauthorizedAccessException ae)
            {
                throw new CharsProviderException("File access unauthorized", ae);
            }
            catch (IOException ioe)
            {
                throw new CharsProviderException("Input/Output Error", ioe);
            }
            catch (Exception e)
            {
                throw new CharsProviderException("Unexpected error", e);
            }

            var buffer = new byte[BufferSize];

            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested)
                        yield break;

                    var length = await fs.ReadAsync(buffer, 0, BufferSize, token).ConfigureAwait(false);
                    if (length == 0)
                        break;
                }
                catch (Exception e)
                {
                    throw new CharsProviderException("Unexpected reading exception", e);
                }

                var chars = Encoding.ASCII.GetChars(buffer);
                foreach (var c in chars)
                    yield return c;

                progress?.Report(new ProcessingState((double)fs.Position / fs.Length, $"{fs.Position / BytesInKByte}/{fs.Length / BytesInKByte}Kb processed"));
            }
        }
    }
}
