using FileReader.Interfaces.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FileReader.UI
{
    public class UIDispatcher : IUIDispatcher
    {
        private Dispatcher _uiDispatcher;

        public UIDispatcher(Dispatcher uiDispatcher)
        {
            _uiDispatcher = Dispatcher.CurrentDispatcher;
        }

        public void ScheduleOnUI(Action action)
        {
            _uiDispatcher.BeginInvoke(action, DispatcherPriority.Background);
        }

        public void RunOnUI(Action action)
        {
            _uiDispatcher.Invoke(action);
        }
    }
}
