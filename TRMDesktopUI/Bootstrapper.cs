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
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            //these are the important for caliburn micro
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            //singleton we create one instance of ther class for the life of the application.

            //Get every type in the application with first line, recursive
            //Than we limit that,2nd and 3nd line
            //Take that and make a List from it.
            //take my container and register for each request with parameters. type key type/ service name and implementation
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));

            //TODO: go back here later and create interfaces to get a better unit test possibility
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //On startup I want to launch our shellViewModel as our base view.
            //it is kind like App.xaml -> StartupUri="ShellView.xaml"
            //Notice we are not starting the view, we are starting the viewModel, because viewModel will launch the view based upon caliber micro.

            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
