using FileReader.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Interfaces.Progress
{
    public interface IProgressFactory
    {
        IProgress<ProcessingState> CreateProgress(Action<ProcessingState> handler);
    }
}
