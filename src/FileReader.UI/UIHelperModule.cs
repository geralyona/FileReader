using FileReader.Interfaces.UI;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileReader.UI
{
    public class UIHelperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUIDispatcher>().To<UIDispatcher>().WithConstructorArgument(Application.Current.Dispatcher);

            Bind<IMessager>().To<Messager>();
            Bind<IFileSelector>().To<FileSelector>();
        }
    }
}
