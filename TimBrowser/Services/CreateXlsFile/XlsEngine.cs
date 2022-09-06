using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using TimBrowser.Model;

namespace TimBrowser.Services.CreateXlsFile
{
    class XlsEngine
    {
        public XlsEngine()
        {
            //_fontFamily = new FontFamily("Arial");
        }
        /*
        #region Fields

        Microsoft.Office.Interop.Excel.Application _app;
        Workbook _workbook;
        
        public const double DOCUMENT_COLUMN_WIDTH = 999;
        public const double DOCUMENT_PAGE_HEIGHT = 1122.5;
        public const double DOCUMENT_PAGE_WIDTH = 793.7;
        public const int DOCUMENT_MAIN_TITILE_FONTSIZE = 14;
        public const int DOCUMENT_SUB_TITILE_FONTSIZE = 12;
        public const int DOCUMENT_PAGE_TITILE_FONTSIZE = 12;

        private FontFamily _fontFamily;
        private string _documentMainTitle;
        private string _documentSubTitle;

        #endregion
        
        public void CreateXlsDocument(List<PrintTableItem> tableItems,
            string documentMainTitle, string documentSubTitle)
        {
            _documentMainTitle = documentMainTitle;
            _documentSubTitle = documentSubTitle;

            int countSheet = 0;

            _app = new Microsoft.Office.Interop.Excel.Application();        //создаем COM-объект Excel
           
            _workbook = _app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);  //добавляем книгу

            List<Table> tableDocuments = new List<Table>();
            List<string> tableTitles = new List<string>();

            foreach (var t in tableItems)
            {
                //ссылка на книгу (она у нас одна) а вот листов много
                Worksheet Worksheet;
                Worksheet = (Worksheet)this._app.Worksheets.Add();
                countSheet++;

                (_app.Worksheets[1] as Worksheet).Name = t.Title;
                
                if (t.Title.Contains("Параметры"))
                {
                    Worksheet.Range["A1:C1"].Merge(XlPivotTableMissingItems.xlMissingItemsMax);
                    Worksheet.Range["A3:C3"].Merge(XlPivotTableMissingItems.xlMissingItemsMax);
                    Worksheet.Range["A5:C5"].Merge(XlPivotTableMissingItems.xlMissingItemsMax);
                    Worksheet.Range["A7"].EntireColumn.ColumnWidth = Worksheet.Range["A7"].EntireColumn.ColumnWidth * 2;
                    Worksheet.Range["B7"].EntireColumn.ColumnWidth = Worksheet.Range["B7"].EntireColumn.ColumnWidth * 6;
                    Worksheet.Range["C7"].EntireColumn.ColumnWidth = Worksheet.Range["C7"].EntireColumn.ColumnWidth * 2;
                }
                else
                {
                    Worksheet.Range["A1:D1"].Merge(XlPivotTableMissingItems.xlMissingItemsMax);
                    Worksheet.Range["A3:D3"].Merge(XlPivotTableMissingItems.xlMissingItemsMax);
                    Worksheet.Range["A5:D5"].Merge(XlPivotTableMissingItems.xlMissingItemsMax);
                    Worksheet.Range["B7"].EntireColumn.ColumnWidth = Worksheet.Range["B7"].EntireColumn.ColumnWidth * 4;
                    Worksheet.Range["C7"].EntireColumn.ColumnWidth = Worksheet.Range["C7"].EntireColumn.ColumnWidth * 3;
                    Worksheet.Range["D7"].EntireColumn.ColumnWidth = Worksheet.Range["D7"].EntireColumn.ColumnWidth * 2;
                }

                Worksheet.Range["A1"].Font.Bold = true;
                Worksheet.Range["A1:A3"].Font.Size = 12;
                Worksheet.Range["A5"].Font.Size = 10;
                Worksheet.Range["A3"].Font.Bold = false;
                Worksheet.Range["A5"].Font.Bold = false;

                Worksheet.Range["A1"].Value = documentMainTitle;
                Worksheet.Range["A3"].Value = documentSubTitle;
                Worksheet.Range["A5"].Value = t.Title;

                Worksheet.Range["A1:A6"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Worksheet.Range["A1:A6"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Worksheet.Range["A1:A6"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                // Добавим один новый лист после текущего
                Microsoft.Office.Interop.Excel.Worksheet oSheet = (Microsoft.Office.Interop.Excel.Worksheet)_app.ActiveSheet;

                CreateTableXls(t, Worksheet);
            }
           
            //_app.Visible = true;//???                                       //делаем объект видимым
            //_app.WindowState = XlWindowState.xlMaximized;//???              //разворачиваем на весь экран
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Exel-файл|*.xlsx";
            saveFileDialog1.Title = "Сохранить опросный лист в формате Exel";
            saveFileDialog1.FileName = _documentMainTitle+"_" + _documentSubTitle+".xlsx";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                _workbook.SaveAs(saveFileDialog1.FileName);
            }
            _workbook.Close(0);
            _app.Quit();
        }



        private void CreateTableXls(PrintTableItem tableItem, Worksheet Worksheet)
        {
            // число колонок
            int columnsCount = tableItem.Columns.First().Length;
            // число рядов (строк)
            int rowsCount = tableItem.Columns.Count;
            //формируем таблицу

            string BaseString = "ABCDEFGHIJKLMNO";
            string cellPointDownRight = BaseString.Substring(columnsCount - 1, 1) + (7 + rowsCount).ToString();

            var cells = Worksheet.get_Range("A7", cellPointDownRight); // выделяем

            cells.Borders[XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;        // внутренние вертикальные
            cells.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;      // внутренние горизонтальные            
            cells.Borders[XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;            // верхняя внешняя
            cells.Borders[XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;          // правая внешняя
            cells.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;           // левая внешняя
            cells.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;         // нижняя внешняя

            cells.Borders[XlBordersIndex.xlEdgeTop].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;            // верхняя внешняя
            cells.Borders[XlBordersIndex.xlEdgeRight].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;          // правая внешняя
            cells.Borders[XlBordersIndex.xlEdgeLeft].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;           // левая внешняя
            cells.Borders[XlBordersIndex.xlEdgeBottom].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;         // нижняя внешняя
            

            cells.HorizontalAlignment =  Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            for (int iColumn = 0; iColumn < columnsCount; iColumn++)
            {
                Worksheet.Cells[7, iColumn + 1] = tableItem.Columns[0][iColumn].ToString();

                string headcellPointDownRight = BaseString.Substring(columnsCount - 1, 1) + "7";

                var headTabl = Worksheet.get_Range("A7", headcellPointDownRight); // выделяем

                headTabl.Borders[XlBordersIndex.xlEdgeBottom].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;         // нижняя внешняя
                headTabl.Borders[XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;        // внутренние вертикальные
            }

            
            // заполняем таблицу
            // Начинаем с 1, т.к. нулевой ряд - шапка
            for (int iRow = 1; iRow < rowsCount; iRow++)
            {
                //Worksheet.Cells.RowRows.Add(new TableRow());

                for (int iColumn = 0; iColumn < columnsCount; iColumn++)
                {
                    Worksheet.Cells[iRow+7, iColumn+1] = tableItem.Columns[iRow][iColumn].ToString();
                }
            }
        }
        */
    }
}
