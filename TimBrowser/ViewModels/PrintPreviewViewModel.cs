using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using TimBrowser.Services.Print;

namespace TimBrowser.ViewModels
{
    public class PrintPreviewViewModel : Screen
    {

        public PrintPreviewViewModel()
        {
        }

        private FixedDocument _fixedDocument;
        private IDocumentPaginatorSource _printDocument;

        // этот код отображается при печати в левом нижнем углу страницы
        private string _code;

        public void PrintRequest()
        {
            var printDialog = new PrintDialog();

            // разрешаем выбирать страницы у документа для печати
            printDialog.UserPageRangeEnabled = true;

            if (printDialog.ShowDialog() == true)
            {
                // для разметки на страницы используем хитрый DocumentPaginator
                PageRangeDocumentPaginator paginator = new PageRangeDocumentPaginator(
                _fixedDocument.DocumentPaginator, printDialog.PageRange,
                printDialog.PageRangeSelection, _code);

                printDialog.PrintDocument(paginator, "Печать таблиц информационного модуля");
            }
        }



        public FixedDocument FixedDocument
        {
            get { return _fixedDocument; }
            set 
            { 
                _fixedDocument = value;

                PrintDocument = _fixedDocument.DocumentPaginator.Source;
                _code = PrintCode.GetPrintCode(_fixedDocument.Pages.Count);
            }
        }

        public IDocumentPaginatorSource PrintDocument
        {
            get { return _printDocument; }
            set
            {
                _printDocument = value;
                NotifyOfPropertyChange("PrintDocument");
            }
        }


    }
}
