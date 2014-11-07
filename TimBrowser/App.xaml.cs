using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace TimBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            BuildLibraries();

            this.Activated += App_Activated;
            _spScreen = new SplashScreen(@"./Assets/Splash3.png");
        }


        private static bool _isActivated;
        private SplashScreen _spScreen;

        private void App_Activated(object sender, EventArgs e)
        {
            if (_isActivated)
                return;

            _isActivated = true;

            var worker = new System.ComponentModel.BackgroundWorker();
            worker.DoWork += (s, args) =>
            {
                System.Threading.Thread.Sleep(1500);
                _spScreen.Close(TimeSpan.FromMilliseconds(0));
                    //_spScreen.Close(TimeSpan.FromMilliseconds(500));
            };
            worker.RunWorkerAsync();     
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            BackgroundWorker spScreenWorker = new BackgroundWorker();
            _spScreen.Show(false, true);
        }


        private void BuildLibraries()
        {
            string resource1 = "TimBrowser.Caliburn.Micro.dll";
            string dll1 = "Caliburn.Micro.dll";

            string resource2 = "TimBrowser.SimpleInjector.dll";
            string dll2 = "SimpleInjector.dll";

            string resource3 = "TimBrowser.System.Windows.Interactivity.dll";
            string dll3 = "System.Windows.Interactivity.dll";


            AppDomain.CurrentDomain.AssemblyResolve +=
                new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            EmbeddedAssembly.Load(resource1, dll1);
            EmbeddedAssembly.Load(resource2, dll2);
            EmbeddedAssembly.Load(resource3, dll3);

        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
