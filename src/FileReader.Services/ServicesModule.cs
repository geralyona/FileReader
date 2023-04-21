using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader.Interfaces.Logic;
using FileReader.Interfaces.Progress;
using FileReader.Interfaces.Words;
using FileReader.Services.Logic;
using FileReader.Services.Progress;
using FileReader.Services.Words;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace FileReader.Services
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICountsCalculator>().To<CountsCalculator>().InSingletonScope();
            Bind<ICharsProvider>().To<FileASCIICharsProvider>();
            Bind<ICharsProviderFactory>().ToFactory().InSingletonScope();
            Bind<IProgressFactory>().To<ProgressFactory>().InSingletonScope();
        }
    }
}
