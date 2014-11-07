using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimBrowser.Services;
using TimBrowser.DataCore.File;
using System.ComponentModel;


namespace TimBrowser.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();

            // ParametersTemplateService service = new ParametersTemplateService();
            // service.GetParametersTemplate(4000);

            //this.ResizeMode = System.Windows.ResizeMode.NoResize;

            //TimDataService timDataService = new TimDataService();
            //TimFileService tfs = new TimFileService(timDataService);
            
            /*
            timDataService.OnTimDataChanged += (s, e) =>
                {
                    string devName = timDataService.CurrentInformationModule.DeviceInfo.DeviceId.ToString();

                    IFileItem iFile = new DesktopFileItem(devName, 2.0, timDataService.CurrentFuncDownloadData);

                    tfs.SaveFile(iFile, @"c:\timtest\dbgFileItemSaved.tim");

                };
            */

            //tfs.LoadFile(@"c:\timtest\dbgFileItem.tim");

        }
    }
}
