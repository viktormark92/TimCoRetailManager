using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //On startup I want to launch our shellViewModel as our base view.
            //it is kind like App.xaml -> StartupUri="ShellView.xaml"
            //Notice we are not starting the view, we are starting the viewModel, because viewModel will launch the view based upon caliber micro.

            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
