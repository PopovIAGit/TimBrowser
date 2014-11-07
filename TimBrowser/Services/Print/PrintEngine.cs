using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TimBrowser.Model;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.IO.Packaging;
using System.IO;
using System.Windows.Markup;
using System.ComponentModel;



namespace TimBrowser.Services.Print
{
    public class PrintEngine
    {
        public PrintEngine()
        {
            _fontFamily = new FontFamily("Arial");
        }

        #region Fields

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

        public FixedDocument GetFixedDocument(List<PrintTableItem> tableItems,
            string documentMainTitle, string documentSubTitle)
        {
            _documentMainTitle = documentMainTitle;
            _documentSubTitle = documentSubTitle;

            List<Table> tableDocuments = new List<Table>();
            List<string> tableTitles = new List<string>();

            foreach (var t in tableItems)
            {
                Table table = CreateTableDocument(t);
                string title = t.Title;

                tableDocuments.Add(table);
                tableTitles.Add(title);
            }

            List<FlowDocument> flowDocList = CreateFlowDocuments(tableDocuments, tableTitles);

            List<XpsDocument> xpsDocumentList = new List<XpsDocument>();
            List<FixedDocumentSequence> fixedDocSequenceList = new List<FixedDocumentSequence>();

            int pageNum = 0;

            foreach (var document in flowDocList)
            {
                XpsDocument xps = CreateXpsDocument(document, pageNum);

                xpsDocumentList.Add(xps);

                FixedDocumentSequence xpsSeq = xps.GetFixedDocumentSequence();

                fixedDocSequenceList.Add(xpsSeq);

                pageNum += xps.FixedDocumentSequenceReader.FixedDocuments[0].FixedPages.Count;
            }

            List<PageContent> fixedPages = GetAllFixedPages(fixedDocSequenceList);

            FixedDocumentSequence fixedDocumentSequence = CreateNewDocumentFromFixedPages(fixedPages);

            FixedDocument fixedDocument = fixedDocumentSequence.References[0].GetDocument(false);

            return fixedDocument;
        }



        private Table CreateTableDocument(PrintTableItem tableItem)
        {
            // число колонок
            int columnsCount = tableItem.Columns.First().Length;
            // число рядов (строк)
            int rowsCount = tableItem.Columns.Count;

            // значения для ячеек таблицы
            double cellPaddingValue = (double)new LengthConverter().ConvertFrom("0,1cm");
            double cellBorderThicknessValue = (double)new LengthConverter().ConvertFrom("0,01cm");
            double cellFontSize = 10;

            // значения для шапки таблицы
            double headBorderThicknessValue = (double)new LengthConverter().ConvertFrom("0,04cm");
            double tableCellSpacing = 0;
            FontWeight headFontWeight = FontWeights.Bold;
            double headFontSize = 12;

            // Стиль ячейки таблицы
            Style cellStyle = new Style(typeof(TableCell));
            cellStyle.Setters.Add(new Setter(TableCell.PaddingProperty, new Thickness(cellPaddingValue)));
            cellStyle.Setters.Add(new Setter(TableCell.BorderBrushProperty, new SolidColorBrush(Colors.Black)));
            cellStyle.Setters.Add(new Setter(TableCell.BorderThicknessProperty, new Thickness(cellBorderThicknessValue)));
            cellStyle.Setters.Add(new Setter(TableCell.TextAlignmentProperty, TextAlignment.Center));
            cellStyle.Setters.Add(new Setter(TableCell.FontSizeProperty, cellFontSize));
            cellStyle.Setters.Add(new Setter(TableCell.FontFamilyProperty, _fontFamily));

            Table table = new Table();

            table.CellSpacing = tableCellSpacing;
            table.Resources.Add(typeof(TableCell), cellStyle);

            // Создаем колонки
            for (int iColumn = 0; iColumn < columnsCount; iColumn++)
            {
                table.Columns.Add(new TableColumn());                                       // Создаем новую колонку

                table.Columns[iColumn].Width = tableItem.ColumnsWidths[iColumn];            // Устанавливаем ширину колонки  
            }

            // добавляем группу рядов. все ряды текущий таблицы будут в ней
            table.RowGroups.Add(new TableRowGroup());

            // Шапка таблицы
            table.RowGroups[0].Rows.Add(new TableRow());

            for (int iColumn = 0; iColumn < columnsCount; iColumn++)
            {
                table.RowGroups[0].Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(
                    tableItem.Columns[0][iColumn]))));

                table.RowGroups[0].Rows[0].Cells[iColumn].FontWeight = headFontWeight;
                table.RowGroups[0].Rows[0].Cells[iColumn].FontSize = headFontSize;

                table.RowGroups[0].Rows[0].Cells[iColumn].BorderThickness = new Thickness(headBorderThicknessValue);
            }

            // заполняем таблицу
            // Начинаем с 1, т.к. нулевой ряд - шапка
            for (int iRow = 1; iRow < rowsCount; iRow++)
            {
                table.RowGroups[0].Rows.Add(new TableRow());

                for (int iColumn = 0; iColumn < columnsCount; iColumn++)
                {
                    string rowText = tableItem.Columns[iRow][iColumn];

                    table.RowGroups[0].Rows[iRow].Cells.Add(new TableCell(new Paragraph(new Run(
                        tableItem.Columns[iRow][iColumn]))));
                }
            }

            return table;
        }

        private List<FlowDocument> CreateFlowDocuments(List<Table> tableDocuments, List<string> tableTitles)
        {
            List<FlowDocument> flowDocList = new List<FlowDocument>();

            int docCounter = 0;

            foreach (var tableDoc in tableDocuments)
            {
                FlowDocument document = new FlowDocument();
                document.ColumnWidth = DOCUMENT_COLUMN_WIDTH;
                document.PageHeight = DOCUMENT_PAGE_HEIGHT;      // A4
                document.PageWidth = DOCUMENT_PAGE_WIDTH;

                // Добавляем основные заголовки
                if (docCounter == 0)
                {
                    Block docTitleOne = new Paragraph(new Run(_documentMainTitle));
                    docTitleOne.TextAlignment = System.Windows.TextAlignment.Center;
                    docTitleOne.FontSize = DOCUMENT_MAIN_TITILE_FONTSIZE;
                    docTitleOne.FontFamily = _fontFamily;
                    document.Blocks.Add(docTitleOne);

                    if (!String.IsNullOrEmpty(_documentSubTitle))
                    {
                        Block docTitleTwo = new Paragraph(new Run(_documentSubTitle));
                        docTitleTwo.TextAlignment = System.Windows.TextAlignment.Center;
                        docTitleTwo.FontSize = DOCUMENT_SUB_TITILE_FONTSIZE;
                        docTitleTwo.FontFamily = _fontFamily;
                        document.Blocks.Add(docTitleTwo);
                    }
                }

                // Заголовок
                Block title = new Paragraph(new Run(tableTitles[docCounter]));
                title.TextAlignment = System.Windows.TextAlignment.Center;
                title.FontSize = DOCUMENT_PAGE_TITILE_FONTSIZE;
                title.FontFamily = _fontFamily;
                document.Blocks.Add(title);

                document.Blocks.Add(tableDocuments[docCounter]);

                flowDocList.Add(document);

                docCounter++;

            }

            return flowDocList;
        }

        private XpsDocument CreateXpsDocument(FlowDocument flowDocument, int startPageNum)
        {
            MemoryStream ms = new MemoryStream();
            Package pkg = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            string pack = "pack://report" + startPageNum.ToString() + ".xps";

            PackageStore.RemovePackage(new Uri(pack));
            PackageStore.AddPackage(new Uri(pack), pkg);
            XpsDocument doc = new XpsDocument(pkg, CompressionOption.NotCompressed, pack);
            XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(doc), false);

            DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
            paginator.PageSize = new System.Windows.Size(793.7, 1122.5);

            PimpedPaginator.Definition def = new PimpedPaginator.Definition(startPageNum);

            def.PageSize = paginator.PageSize;

            paginator = new PimpedPaginator(flowDocument, def);

            rsm.SaveAsXaml(paginator);

            rsm.Commit();

            return doc;
        }

        private List<PageContent> GetAllFixedPages(List<FixedDocumentSequence> documentSequence)
        {
            int lenght = documentSequence.Count;

            List<PageContent> pages = new List<PageContent>();

            for (int iDoc = 0; iDoc < lenght; iDoc++)
            {
                var docs = documentSequence[iDoc].References.Select(r => r.GetDocument(true));

                foreach (var doc in docs)
                {
                    pages.AddRange(doc.Pages);
                }
            }

            return pages;

        }

        private FixedDocumentSequence CreateNewDocumentFromFixedPages(IEnumerable<PageContent> pages)
        {
            FixedDocumentSequence newSequence = new FixedDocumentSequence();
            DocumentReference newDocReference = new DocumentReference();
            FixedDocument newDoc = new FixedDocument();
            newDocReference.SetDocument(newDoc);

            foreach (PageContent page in pages)
            {
                PageContent newPage = new PageContent();
                newPage.Source = page.Source;
                (newPage as IUriContext).BaseUri = ((IUriContext)page).BaseUri;
                newPage.GetPageRoot(true);
                newDoc.Pages.Add(newPage);
            }

            // The order in which you do this is REALLY important
            newSequence.References.Add(newDocReference);

            return newSequence;
        }

        private FixedDocument CreateFixedDocument(List<FlowDocument> flowDocList)
        {
            int pageNum = 0;

            List<XpsDocument> xpsDocumentList = new List<XpsDocument>();

            List<FixedDocumentSequence> fixedDocSequenceList = new List<FixedDocumentSequence>();

            foreach (var document in flowDocList)
            {
                XpsDocument xps = CreateXpsDocument(document, pageNum);

                xpsDocumentList.Add(xps);

                FixedDocumentSequence xpsSeq = xps.GetFixedDocumentSequence();

                fixedDocSequenceList.Add(xpsSeq);

                pageNum += xps.FixedDocumentSequenceReader.FixedDocuments[0].FixedPages.Count;
            }

            List<PageContent> fixedPages = GetAllFixedPages(fixedDocSequenceList);

            FixedDocumentSequence fixedDocumentSequence = CreateNewDocumentFromFixedPages(fixedPages);

            FixedDocument fixedDocument = fixedDocumentSequence.References[0].GetDocument(false);

            return fixedDocument;
        }

    }

}
