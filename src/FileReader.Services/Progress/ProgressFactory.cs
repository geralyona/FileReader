using FileReader.Common.Data;
using FileReader.Interfaces.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Services.Progress
{
    public class ProgressFactory : IProgressFactory
    {
        public IProgress<ProcessingState> CreateProgress(Action<ProcessingState> handler) => new Progress<ProcessingState>(handler);
    }
}
