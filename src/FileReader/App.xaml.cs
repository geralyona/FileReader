using Ninject;
using System.Windows;
using Ninject.Extensions.Factory;
using FileReader.Interfaces.Logic;
using FileReader.Interfaces.Words;
using FileReader.Services.Logic;
using FileReader.Services.Words;
using FileReader.Interfaces.UI;
using FileReader.UI;
using FileReader.ViewModels;
using FileReader.ViewModels.Operation;
using FileReader.Services;
using FileReader.Interfaces;

namespace FileReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IKernel? _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _container = new StandardKernel();


            _container.Load(new UIHelperModule());
            _container.Load(new ServicesModule());
            _container.Load(new ViewModelsModule());

            
            var window = _container.Get<MainWindow>();
            window.DataContext = _container.Get<MainVM>();
            Current.MainWindow = window;

            Current.MainWindow.Show();
        }
    }
}
