using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MarkdownRazor
{
    public class MarkdownRazorViewEngine : RazorViewEngine
    {
        public MarkdownRazorViewEngine()
        {
            FileExtensions = InsertMarkdownFileExtensions(FileExtensions);

            AreaViewLocationFormats = InsertMarkdownFileExtensions(AreaViewLocationFormats);
            AreaMasterLocationFormats = InsertMarkdownFileExtensions(AreaMasterLocationFormats);
            AreaPartialViewLocationFormats = InsertMarkdownFileExtensions(AreaPartialViewLocationFormats);
            ViewLocationFormats = InsertMarkdownFileExtensions(ViewLocationFormats);
            MasterLocationFormats = InsertMarkdownFileExtensions(MasterLocationFormats);
            PartialViewLocationFormats = InsertMarkdownFileExtensions(PartialViewLocationFormats);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            var view = base.CreatePartialView(controllerContext, partialPath);

            if (MarkdownRazor.IsMarkdownRazorFilename(partialPath))
                return new MarkdownRazorView((RazorView)view);

            return view;
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var view = base.CreateView(controllerContext, viewPath, masterPath);

            if (MarkdownRazor.IsMarkdownRazorFilename(viewPath))
                return new MarkdownRazorView((RazorView)view);

            return view;
        }

        private static string[] InsertMarkdownFileExtensions(IEnumerable<string> filenames)
        {
            return InsertMarkdownFileExtensionsCore(filenames).Distinct().ToArray();
        }

        private static IEnumerable<string> InsertMarkdownFileExtensionsCore(IEnumerable<string> filenames)
        {
            foreach (var filename in filenames)
            {
                foreach (var extension in MarkdownRazor.MarkdownFileExtensions)
                {
                    yield return Regex.Replace(filename, "([^.]*)html$", extension);
                    yield return Regex.Replace(filename, "([^.]*)html$", "$1" + extension);
                }

                yield return filename;
            }
        }
    }
}