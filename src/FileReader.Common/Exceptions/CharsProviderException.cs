using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Common.Exceptions
{
    public class CharsProviderException : Exception
    {
        public CharsProviderException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
