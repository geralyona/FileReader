using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Interfaces.UI
{
    public interface IFileSelector
    {
        bool TryGetFileName(out string name);
    }
}
