using System.Web.Mvc.Razor;
using System.Web.Razor;

namespace MarkdownRazor
{
    public class MarkdownRazorHost : MvcWebPageRazorHost
    {
        public MarkdownRazorHost(string virtualPath, string physicalPath)
            : base(virtualPath, physicalPath)
        {
        }

        protected override RazorCodeLanguage GetCodeLanguage()
        {
            var language = MarkdownRazor.GetCodeLanguage(VirtualPath);

            if ((language ?? string.Empty).ToLower() == "vb")
                return new VBRazorCodeLanguage();

            return new CSharpRazorCodeLanguage();
        }
    }
}