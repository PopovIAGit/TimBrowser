using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using SimpleInjector;
using TimBrowser.ViewModels;
using TimBrowser.Services;
using System.Windows;


namespace TimBrowser
{
    public class AppBootstrapper : Bootstrapper<MainWindowViewModel>
    {
        // используем Simple Injector: http://www.cshandler.com/2013/03/basics-of-caliburn-micro-with-simple.html
        private Container _container;

        protected override void Configure()
        {
            try
            {
                _container = new Container();

                // регистрация сервисов и ViewModel
                _container.Register<IWindowManager, WindowManager>();
                _container.RegisterSingle<IEventAggregator, EventAggregator>();

                _container.RegisterSingle<TimDataService>();
                _container.RegisterSingle<TimErrorService>();
                _container.Register<TimFileService>();

                _container.Register<MenuViewModel>();
                _container.Register<DownloadViewModel>();
                _container.Register<DownloadInformationModuleViewModel>();
                _container.RegisterSingle<BluetoothViewModel>();
                _container.Register<BluetoothAuthViewModel>();
                _container.Register<ModBusViewModel>();

                _container.Register<LogEvChosenParametersViewModel>();
                _container.Register<LogEvEventsViewModel>();
                _container.Register<LogEvMainViewModel>();
                _container.Register<ParametersTableViewModel>();
                _container.Register<PrintPreviewViewModel>();

                _container.Verify();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }



        protected override IEnumerable<object> GetAllInstances(Type service)
        {
           return _container.GetAllInstances(service);
        }

        protected override object GetInstance(System.Type service, string key)
        {
            return _container.GetInstance(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            try
            {
                base.OnStartup(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
