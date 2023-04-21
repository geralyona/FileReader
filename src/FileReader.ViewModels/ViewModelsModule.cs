using FileReader.ViewModels.Operation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.ViewModels
{
    public class ViewModelsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<FileSlectorVM>().To<FileSlectorVM>();
        }
    }
}
