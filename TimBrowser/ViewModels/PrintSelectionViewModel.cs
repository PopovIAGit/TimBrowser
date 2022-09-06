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
using TimBrowser.Services.CreateXlsFile;
using System.Windows.Controls;
//using Microsoft.Office.Interop.Excel;
using iTextSharp.text.pdf;

namespace TimBrowser.ViewModels
{
    public class PrintSelectionViewModel : Screen
    {
        public PrintSelectionViewModel(TimDataService timDataService)
        {
            _timDataService = timDataService;
            _printTableService = new PrintTableService();
            _printEngine = new PrintEngine();
            //_xlsEngine = new XlsEngine();
        }

        private TimDataService _timDataService;
        private PrintTableService _printTableService;
        private PrintEngine _printEngine;
        //private XlsEngine _xlsEngine;
        private List<PrintSelectionItem> _printSelections;

        public System.Action<FixedDocument> PrintDocumentReadyAction;

        // Перед печатью активируется окно предпросмотра
        public void Print()
        {
            PrintPreview();
        }

        public void SavePdf()
        {
            CreatePdfFile();
        }

        /*public void SaveXls()
        {
            CreateXlsFile();
        }*/

        protected override void OnActivate()
        {
            // забираем список для выбора таблиц на печать
            PrintSelections = _printTableService.GetPrintListSelections();
        }

        //запускаем создание файла PDF
        private void CreatePdfFile()
        {
            List<PrintTableItem> tableItems;
            string mainTitle;
            string subTitle;
            CreateListForFiles(out tableItems, out mainTitle, out subTitle);

            /*PdfWriter writer = PdfWriter.GetInstance(document, output);
            writer.PageEvent = new PdfPageEvents();*/
            /*var document = new Document();
            float docHeight = document.PageSize.Height - heightOffset;
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetMargins(50, 50, 10, 40);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            document.Add(contents[0].Table);

            pdfDoc.Open();
            pdfDoc.Add(new Paragraph("Some data"));
            PdfContentByte cb = writer.DirectContent;
            cb.MoveTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height / 2);
            cb.LineTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height);
            cb.Stroke();

            pdfDoc.Close();*/ 
        }

        //запускаем создание файла XLS
        /*private void CreateXlsFile()
        {
            List<PrintTableItem> tableItems;
            string mainTitle;
            string subTitle;
            CreateListForFiles(out tableItems, out mainTitle, out subTitle);

            _xlsEngine.CreateXlsDocument(tableItems,mainTitle, subTitle);
            // создаем документ и сохраняем в формат Xls
            FixedDocument fixDoc = _printEngine.GetFixedDocument(tableItems,
                mainTitle, subTitle);

        }*/

        private void PrintPreview()
        {

            List<PrintTableItem> tableItems;
            string mainTitle;
            string subTitle;
            CreateListForFiles(out tableItems, out mainTitle, out subTitle);

            // создаем документ для печати
            FixedDocument fixDoc = _printEngine.GetFixedDocument(tableItems,
                mainTitle, subTitle);

            if (PrintDocumentReadyAction != null)
                PrintDocumentReadyAction(fixDoc);
        }

        private void CreateListForFiles(out List<PrintTableItem> tableItems, out string mainTitle, out string subTitle)
        {
            // основной заголовок на первой странице документа
            mainTitle = "Информационный модуль " +
                _timDataService.CurrentInformationModule.DeviceInfo.DeviceName + " " +
                _timDataService.CurrentInformationModule.DeviceInfo.DeviceNumberString;

            // строчка ниже основного заголовка на первой странице документа
            subTitle = /*"Дата и время считывания " +*/ _timDataService.CurrentFuncDownloadData.DateTimeDownload
            .ToString(Helper.Constants.DATE_TIME_FORMAT_STRING);

            tableItems = _printTableService.PrintTablesRequest(_printSelections,
                _timDataService.CurrentInformationModule, mainTitle, subTitle);

            
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
