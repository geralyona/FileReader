using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Common.Data
{
    public class ProcessingState
    {
        public double Progres { get; }

        public string? Message { get; }

        public ProcessingState(double progres, string? message)
        {
            Progres = progres;
            Message = message;
        }
    }
}
