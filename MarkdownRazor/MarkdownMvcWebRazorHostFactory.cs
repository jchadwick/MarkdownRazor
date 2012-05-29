using System.Web.Mvc;

namespace MarkdownRazor
{
    public class MarkdownMvcWebRazorHostFactory : MvcWebRazorHostFactory
    {
        public override System.Web.WebPages.Razor.WebPageRazorHost CreateHost(string virtualPath, string physicalPath)
        {
            if(MarkdownRazor.IsMarkdownRazorFilename(virtualPath))
                return new MarkdownRazorHost(virtualPath, physicalPath);
            
            return base.CreateHost(virtualPath, physicalPath);
        }
    }
}