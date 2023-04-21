using FileReader.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.ViewModels.Operation
{
    public class ProgressStatusVM : ViewModelBase
    {
        private double _progress;

        public double Progress
        {
            get => _progress;
            set => SetField(ref _progress, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage { 
            
            get => _statusMessage; 
            set => SetField( ref _statusMessage,value); }

        public void SetProgressNotNotify(double progress, string? message)
        {
            _progress = progress;
            if(message != null) 
                _statusMessage = message;
        }

        public void RaiseAllPropertyChanged()
        {
            OnPropertyChanged(nameof(Progress));
            OnPropertyChanged(nameof(StatusMessage));
        }
    }
}
