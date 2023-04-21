using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Interfaces.Words
{
    public interface ICharsProviderFactory
    {
        ICharsProvider CreateFileAsciiCharsProvider(string fileName);
    }
}
