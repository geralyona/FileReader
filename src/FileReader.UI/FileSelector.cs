using FileReader.Interfaces.UI;
using Microsoft.Win32;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FileReader.UI
{
    public class FileSelector : IFileSelector
    {
        [Inject] public IUIDispatcher UIDispatcher { private get; set; } = null!;

        public bool TryGetFileName(out string name)
        {
            var result = false;
            var fileName = string.Empty;

            UIDispatcher.RunOnUI(() =>
            {
                var dlg = new OpenFileDialog();
                dlg.CheckFileExists = true;

                if (dlg.ShowDialog() == true)
                {
                    fileName = dlg.FileName;
                    result = true;
                }
            });

            name = fileName;
            return result;
        }
    }
}
