using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Management;
using System.Windows.Threading;
using TimBrowser.Helper;
using TimBrowser.Services;
using MessageBox = System.Windows.MessageBox;
using Screen = Caliburn.Micro.Screen;

namespace TimBrowser.ViewModels
{
    public class MenuViewModel : Screen
    {
        public MenuViewModel(IWindowManager windowManager, TimFileService timFileService, 
            DownloadViewModel downloadViewModel, PrintSelectionViewModel printSelectionViewModel,
            PrintPreviewViewModel printPreviewControl)
        {
            _windowManager = windowManager;
            _timFileService = timFileService;
            _timFileService.CanSaveFileAction = OnCanSaveFileAction;

            _saveButtonEnable = false;
            _printButtonEnable = false;

            DownloadViewModel = downloadViewModel;
            PrintSelectionControl = printSelectionViewModel;
            PrintSelectionControl.PrintDocumentReadyAction = OnPrintDocumentReady;
            PrintPreviewControl = printPreviewControl;
        }

        #region Fields

        private IWindowManager _windowManager;
        private readonly TimFileService _timFileService;

        private string _menuTitleText;
        private string _blockVersionText;
        private string _typeBlock;
        private string _typeDrive;
        private string _factoryNumber;
        private bool _saveButtonEnable;
        private bool _printButtonEnable;
        private bool _onAlarm;

        #endregion

        #region Methods

        /// <summary>
        /// Метод, который вызывается, когда обновляется информация о разрешении/запрете на сохранение файла
        /// </summary>
        /// <param name="canSave"></param>
        private void OnCanSaveFileAction(bool canSave)
        {
            SaveButtonEnable = canSave;
            PrintButtonEnable = canSave;
            MenuTitleText = _timFileService.CurrentFileName;
            BlockVersionText = _timFileService.CurrentVersion;// _timFileService.CurrentFileName;
            TypeBlockText = _timFileService.CurrentTypeBlock;
            TypeDriveText = _timFileService.CurrentTypeDrive;
            FactoryNumberText = _timFileService.CurrentFactoryNumber;
        }

        // метод для обработки кнопки
        public void LoadFile()
        {
            //_timFileService.LoadFile(@"c:\timtest\test.tim");

            // запускаем диалогое окно открытия файла
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = Constants.FILE_EXT_FILTER;
            openFileDialog.Filter = Constants.FILE_FILTER;

            if (openFileDialog.ShowDialog() == true)
            {
                _timFileService.LoadFile(openFileDialog.FileName);
            }
        }

        public void OnAlarm()
        {
            if (_onAlarm == false) _onAlarm = true;
            else _onAlarm = false;
        }


        public void SaveFile()
        {
            // запускаем диалоговое окно сохранения файла
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.FileName = _timFileService.CurrentFileName;
            saveFileDialog.DefaultExt = Constants.FILE_SAVE_EXT_FILTER;
            saveFileDialog.Filter = Constants.FILE_SAVE_FILTER;

            if (saveFileDialog.ShowDialog() == true)
            {
                _timFileService.SaveFile(saveFileDialog.SafeFileName, saveFileDialog.FileName);
            }
        }

        public void Download()
        {
            // создаем динамический объект, который определяет настройки нового окна
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;
            settings.SizeToContent = SizeToContent.Manual;
            settings.ResizeMode = System.Windows.ResizeMode.NoResize;
            settings.Title = "Считывание";
            settings.Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/appicon.png"));
            settings.Width = 400;
            settings.Height = 550;
            DownloadViewModel.DownloadInformationModuleObj.TypeComm = 1;
            // открываем новое окно через WindowManager, используя динамический объект с настройками
            _windowManager.ShowDialog(DownloadViewModel, null, settings);
        }

        public void Download_RS()
        {
            // создаем динамический объект, который определяет настройки нового окна
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;
            settings.SizeToContent = SizeToContent.Manual;
            settings.ResizeMode = System.Windows.ResizeMode.NoResize;
            settings.Title = "Считывание";
            settings.Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/appicon.png"));
            settings.Width = 400;
            settings.Height = 550;
            DownloadViewModel.DownloadInformationModuleObj.TypeComm = 2;

            // открываем новое окно через WindowManager, используя динамический объект с настройками
            _windowManager.ShowDialog(DownloadViewModel, null, settings);
        }


        public void Print()
        {
            
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.SizeToContent = SizeToContent.WidthAndHeight;
            settings.ResizeMode = System.Windows.ResizeMode.NoResize;
            settings.Title = "Выбор для печати";
            settings.Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/appicon.png"));
            settings.Width = 400;
            settings.Height = 500;

            _windowManager.ShowDialog(PrintSelectionControl, null, settings);
        }

        /// <summary>
        /// Метод, вызывающийся при готовности документа для печати
        /// </summary>
        /// <param name="document">Документ для печати</param>
        private void OnPrintDocumentReady(FixedDocument document)
        {
            // закрываем View списка печати
            PrintSelectionControl.TryClose();

            // присваиваем документ для печати ViewModel, которая будет инициировать процесс печати
            PrintPreviewControl.FixedDocument = document;

            ActivatePrintPreview();
        }

        private void ActivatePrintPreview()
        {
            
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.Manual;
            settings.SizeToContent = SizeToContent.Manual;
            settings.ResizeMode = System.Windows.ResizeMode.CanResize;
            settings.WindowState = WindowState.Maximized;
            settings.Title = "Предпросмотр печати";
            settings.Width = 400;
            settings.Height = 500;

            _windowManager.ShowWindow(PrintPreviewControl, null, settings);
        }

        #endregion

        #region Properties

        public DownloadViewModel DownloadViewModel
        {
            get;
            private set;
        }

        public PrintSelectionViewModel PrintSelectionControl
        {
            get;
            private set;
        }

        public PrintPreviewViewModel PrintPreviewControl
        {
            get;
            private set;
        }

        public string MenuTitleText
        {
            get { return _menuTitleText; }
            set
            {
                _menuTitleText = value;
                NotifyOfPropertyChange("MenuTitleText");
            }
        }

        public string BlockVersionText
        {
            get { return _blockVersionText; }
            set
            {
                _blockVersionText = value;
                NotifyOfPropertyChange("BlockVersionText");
            }
        }
        
        public string TypeBlockText
        {
            get { return _typeBlock; }
            set
            {
                _typeBlock = value;
                NotifyOfPropertyChange("TypeBlockText");
            }
        }

        public string TypeDriveText
        {
            get { return _typeDrive; }
            set
            {
                _typeDrive = value;
                NotifyOfPropertyChange("TypeDriveText");
            }
        }

        public string FactoryNumberText
        {
            get { return _factoryNumber; }
            set
            {
                _factoryNumber = value;
                NotifyOfPropertyChange("FactoryNumberText");
            }
        }
        
        public bool SaveButtonEnable
        {
            get { return _saveButtonEnable; }
            set
            {
                _saveButtonEnable = value;
                NotifyOfPropertyChange("SaveButtonEnable");
            }
        }


        public bool PrintButtonEnable
        {
            get { return _printButtonEnable; }
            set
            {
                _printButtonEnable = value;
                NotifyOfPropertyChange("PrintButtonEnable");
            }
        }
        
        #endregion

    }
}
