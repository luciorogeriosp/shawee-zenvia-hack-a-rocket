using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FrontEnd.Util
{
    public class ViewTools
    {
        public static string RenderViewToString(string viewName, object viewData, Controller controller)
        {
            var renderedView = new StringBuilder();

            using (var responseWriter = new StringWriter(renderedView))
            {
                var fakeResponse = new HttpResponse(responseWriter);

                var fakeContext = new HttpContext(System.Web.HttpContext.Current.Request, fakeResponse);

                var fakeControllerContext = new ControllerContext(new HttpContextWrapper(fakeContext), controller.ControllerContext.RouteData,
                    controller.ControllerContext.Controller);

                var oldContext = System.Web.HttpContext.Current;
                System.Web.HttpContext.Current = fakeContext;

                using (var viewPage = new ViewPage())
                {
                    var viewContext = new ViewContext(fakeControllerContext, new FakeView(), new ViewDataDictionary(), new TempDataDictionary(), responseWriter);

                    var html = new HtmlHelper(viewContext, viewPage);
                    html.RenderPartial(viewName, viewData);
                    System.Web.HttpContext.Current = oldContext;
                }
            }

            return renderedView.ToString();
        }

        public static byte[] RenderPDF(string htmlText, PdfPageEventHelper PageEvent, bool Portrait = true)
        {
            byte[] renderedBuffer;

            const int HorizontalMargin = 40;
            const int VerticalMargin = 40;

            using (var outputMemoryStream = new MemoryStream())
            {
                using (var pdfDocument = new Document(PageSize.A4 , HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
                {

                    if (!Portrait)
                    {
                        pdfDocument.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    }

                    iTextSharp.text.pdf.PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);       

                    pdfWriter.CloseStream = false;
                    pdfWriter.PageEvent = PageEvent;

                    pdfDocument.Open();
                    using (var htmlViewReader = new StringReader(htmlText))
                    {
                        pdfDocument.AddCreator("SAL - Sistema Administrativo de Loja");
                        //pdfDocument.AddTitle("Seção 211");
                        
                        XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDocument, htmlViewReader);
                    }

                }

                renderedBuffer = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
            }

            return renderedBuffer;
        }

        public class HtmlPageEventHelper : PdfPageEventHelper
        {
            private string html { get; set; }
            private int headersize { get; set; }

            public HtmlPageEventHelper(string html, int headersize)
            {
                this.html = html;
                this.headersize = headersize;
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                html = html.Replace("[PAGINA]", document.PageNumber.ToString());
                html = html.Replace("[PAGINAS]", totcountPage.ToString());

                ColumnText ct = new ColumnText(writer.DirectContent);
                XMLWorkerHelper.GetInstance().ParseXHtml(new ColumnTextElementHandler(ct), new StringReader(html));
                ct.SetSimpleColumn(document.Left, document.Top, document.Right, document.Top + 40, 10, Element.ALIGN_MIDDLE);
                ct.Go();
            }

            int totcountPage = 0;
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {                
                totcountPage = writer.PageNumber;                
            }
        }
    }

    public class ColumnTextElementHandler : IElementHandler
    {
        public ColumnTextElementHandler(ColumnText ct)
        {
            this.ct = ct;
        }

        ColumnText ct = null;

        public void Add(IWritable w)
        {
            if (w is WritableElement)
            {
                foreach (IElement e in ((WritableElement)w).Elements())
                {
                    ct.AddElement(e);
                }
            }
        }
    }

    public class FakeView : IView
    {
        #region IView Members

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}