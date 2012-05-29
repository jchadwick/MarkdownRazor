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
            FileExtensions = ReplaceWithMarkdownFileExtensions(FileExtensions);

            AreaViewLocationFormats = ReplaceWithMarkdownFileExtensions(AreaViewLocationFormats);
            AreaMasterLocationFormats = ReplaceWithMarkdownFileExtensions(AreaMasterLocationFormats);
            AreaPartialViewLocationFormats = ReplaceWithMarkdownFileExtensions(AreaPartialViewLocationFormats);
            ViewLocationFormats = ReplaceWithMarkdownFileExtensions(ViewLocationFormats);
            MasterLocationFormats = ReplaceWithMarkdownFileExtensions(MasterLocationFormats);
            PartialViewLocationFormats = ReplaceWithMarkdownFileExtensions(PartialViewLocationFormats);
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
            // NOTE: Only support partial views right now...
            return CreatePartialView(controllerContext, viewPath);
        }

        private static string[] ReplaceWithMarkdownFileExtensions(IEnumerable<string> filenames)
        {
            return ReplaceWithMarkdownFileExtensionsCore(filenames).Distinct().ToArray();
        }

        private static IEnumerable<string> ReplaceWithMarkdownFileExtensionsCore(IEnumerable<string> filenames)
        {
            foreach (var filename in filenames)
            {
                foreach (var extension in MarkdownRazor.MarkdownFileExtensions)
                {
                    yield return Regex.Replace(filename, "([^.]*)html$", extension);
                    yield return Regex.Replace(filename, "([^.]*)html$", "$1" + extension);
                }
            }
        }
    }
}