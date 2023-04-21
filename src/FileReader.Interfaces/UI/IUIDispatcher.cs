using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Interfaces.UI
{
    public interface IUIDispatcher
    {
        void ScheduleOnUI(Action action);

        void RunOnUI(Action action);
    }
}
