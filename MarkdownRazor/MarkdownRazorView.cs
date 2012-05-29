using System.IO;
using System.Web.Mvc;

namespace MarkdownRazor
{
    public class MarkdownRazorView : IView
    {
        private static readonly MarkdownRazor Markdown = new MarkdownRazor();

        private readonly RazorView _view;

        public MarkdownRazorView(RazorView view)
        {
            _view = view;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            using (var buffer = new StringWriter())
            {
                _view.Render(viewContext, buffer);
                var markdown = buffer.GetStringBuilder().ToString();
                var transformed = Markdown.Transform(markdown);
                writer.Write(transformed);
            }
        }
    }
}