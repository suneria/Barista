using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Updater.ViewModels
{
    class MainViewModel : BindableBase
    {
        public ICommand UpdateCommand
        {
            get
            {
                DelegateCommand command = new DelegateCommand(() => { });
                return command;
            }
            set
            {
            }
        }
    }
}
