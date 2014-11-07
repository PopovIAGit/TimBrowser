using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using TimBrowser.Model;
using TimBrowser.Services;
using TimBrowser.Services.Print;

namespace TimBrowser.ViewModels
{
    public class PrintSelectionViewModel : Screen
    {
        public PrintSelectionViewModel(TimDataService timDataService)
        {
            _timDataService = timDataService;
            _printTableService = new PrintTableService();
            _printEngine = new PrintEngine();            
        }

        private TimDataService _timDataService;
        private PrintTableService _printTableService;
        private PrintEngine _printEngine;
        private List<PrintSelectionItem> _printSelections;

        public System.Action<FixedDocument> PrintDocumentReadyAction;

        // Перед печатью активируется окно предпросмотра
        public void Print()
        {
            PrintPreview();
        }

        protected override void OnActivate()
        {
            // забираем список для выбора таблиц на печать
            PrintSelections = _printTableService.GetPrintListSelections();
        }

        private void PrintPreview()
        {
            List<PrintTableItem> tableItems = _printTableService.PrintTablesRequest(_printSelections,
                _timDataService.CurrentInformationModule);

            // основной заголовок на первой странице документа
            string mainTitle = "Информационный модуль " +
                _timDataService.CurrentInformationModule.DeviceInfo.DeviceName + " " +
                _timDataService.CurrentInformationModule.DeviceInfo.DeviceNumberString;

            // строчка ниже основного заголовка на первой странице документа
            string subTitle = "Дата и время считывания " + _timDataService.CurrentFuncDownloadData.DateTimeDownload
                .ToString(Helper.Constants.DATE_TIME_FORMAT_STRING);

            // создаем документ для печати
            FixedDocument fixDoc = _printEngine.GetFixedDocument(tableItems,
                mainTitle, subTitle);

            if (PrintDocumentReadyAction != null)
                PrintDocumentReadyAction(fixDoc);
        }

        public List<PrintSelectionItem> PrintSelections
        {
            get { return _printSelections; }
            set
            {
                _printSelections = value;
                NotifyOfPropertyChange("PrintSelections");
            }
        }
        


    }
}
