using System;
using System.IO;
using System.Web.Mvc;

namespace HtmlPdfReport
{
    public class FakeView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
