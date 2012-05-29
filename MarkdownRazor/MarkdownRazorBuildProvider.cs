using System.Web;
using System.Web.WebPages.Razor;
using MarkdownRazor;

[assembly: PreApplicationStartMethod(typeof(MarkdownRazorBuildProvider), "RegisterOnApplicationStart")]

namespace MarkdownRazor
{
    public class MarkdownRazorBuildProvider : RazorBuildProvider
    {        
        protected override WebPageRazorHost CreateHost()
        {
            if (MarkdownRazor.IsMarkdownRazorFilename(VirtualPath))
                return new MarkdownRazorHost(VirtualPath, null);

            return base.CreateHost();
        }

        public static void RegisterOnApplicationStart()
        {
            var provider = typeof(MarkdownRazorBuildProvider);

            foreach (var extension in MarkdownRazor.MarkdownFileExtensions)
            {
                RegisterBuildProvider("."+extension, provider);
            }
        }
    }
}