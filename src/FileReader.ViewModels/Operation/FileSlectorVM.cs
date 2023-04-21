using CommunityToolkit.Mvvm.Input;
using FileReader.Interfaces.UI;
using FileReader.ViewModels.Base;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileReader.ViewModels.Operation
{
    public class FileSlectorVM : ViewModelBase
    {
        [Inject] public IFileSelector FileSelector { private get; set; } = null!;

        public ICommand SelectFileNameCommand { get; set; }

        public FileSlectorVM()
        {
            SelectFileNameCommand = new RelayCommand(SelectFileName);
        }

        private string _fileName = string.Empty;

        public string FileName
        {
            get => _fileName;
            set => SetField(ref _fileName, value);
        }

        private void SelectFileName()
        {
            if (FileSelector.TryGetFileName(out var name))
            {
                FileName = name;
            }
        }
    }
}
