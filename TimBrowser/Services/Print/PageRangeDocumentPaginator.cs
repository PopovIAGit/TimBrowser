using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Reflection;

namespace TimBrowser.Services.Print
{
    public class PageRangeDocumentPaginator : DocumentPaginator
    {
        public PageRangeDocumentPaginator(
          DocumentPaginator paginator,
          PageRange pageRange, PageRangeSelection pageRangeSelection, string code)
        {
            _startIndex = pageRange.PageFrom - 1;
            _endIndex = pageRange.PageTo - 1;
            _paginator = paginator;

            _pageRangeSelection = pageRangeSelection;
            _code = code;

            // Adjust the _endIndex
            _endIndex = Math.Min(_endIndex, _paginator.PageCount - 1);
        }

        private int _startIndex;
        private int _endIndex;
        private DocumentPaginator _paginator;
        private string _code;
        PageRangeSelection _pageRangeSelection;

        public override DocumentPage GetPage(int pageNumber)
        {
            if (_pageRangeSelection == PageRangeSelection.UserPages)
                pageNumber += _startIndex;

            DocumentPage originalPage = _paginator.GetPage(pageNumber);

            Visual originalPageVisual = originalPage.Visual;

            FixedPage page = originalPageVisual as FixedPage;

            VisualHost host = new VisualHost();

            host.AddVisual(CreateVisual(30, originalPage.ContentBox.Height - 30, _code));

            // {0;0;793,7;1122,5} - на печать

            // {729,7;1058,5} - до XPS

            // 64, 64 - дельта

            // Расположение номера страницы
            // 729,7 + 40 ;1058,5 + 40 = 769,7; 1098,5

            page.Children.Add(host);

            return new DocumentPage(page);

            //return _paginator.GetPage(pageNumber + _startIndex);

        }

        public Visual CreateVisual(double width, double height, string message)
        {
            // create a drawing visual
            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext ctx = visual.RenderOpen())
            {
                FormattedText text = new FormattedText(message,
                    System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface("Arial"), 8, Brushes.Black);

                ctx.DrawText(text, new System.Windows.Point(width, height));

            }

            return visual;
        }


        public override bool IsPageCountValid
        {
            get { return _paginator.IsPageCountValid; }
        }

        public override int PageCount
        {
            get
            {
                if (_pageRangeSelection == PageRangeSelection.UserPages)
                {
                    if (_startIndex > _paginator.PageCount - 1)
                        return 0;
                    if (_startIndex > _endIndex)
                        return 0;

                    return _endIndex - _startIndex + 1;
                }
                else
                    return _paginator.PageCount;
            }
        }

        public override Size PageSize
        {
            get
            {
                return _paginator.PageSize;
            }
            set
            {
                _paginator.PageSize = value;
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return _paginator.Source; }
        }

    }

    public class VisualHost : UIElement
    {
        private List<Visual> fVisuals;

        public VisualHost()
        {
            fVisuals = new List<Visual>();
        }

        protected override Visual GetVisualChild(int index)
        {
            return fVisuals[index];
        }

        protected override int VisualChildrenCount
        {
            get { return fVisuals.Count; }
        }

        public void AddVisual(Visual visual)
        {
            fVisuals.Add(visual);
            base.AddVisualChild(visual);
        }

        public void RemoveVisual(Visual visual)
        {
            fVisuals.Remove(visual);
            base.RemoveVisualChild(visual);
        }
    }
}
