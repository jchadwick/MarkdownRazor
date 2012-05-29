using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MarkdownDeep;

namespace MarkdownRazor
{
    public class MarkdownRazor : Markdown
    {
        internal static readonly IEnumerable<string> MarkdownFileExtensions =
            new[] { "md", "markdown" };

        public static readonly IEnumerable<string> MarkdownRazorFileExtensions =
            from language in new[] { "cs", "vb" }
            from extension in MarkdownFileExtensions
            select string.Format("{0}{1}", language, extension);

        private static readonly Regex CodeLanguageRegex =
            new Regex(@"\.(?<Language>.*)(" + string.Join("|", MarkdownFileExtensions) + ")$", RegexOptions.IgnoreCase);


        public MarkdownRazor()
        {
            ExtraMode = true;
            SafeMode = false;
        }


        public static string GetCodeLanguage(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return null;

            var match = CodeLanguageRegex.Match(filename);

            if (match.Success && match.Groups.Count > 0)
                return match.Groups[0].Value;

            return null;
        }

        public static bool IsMarkdownRazorFilename(string filename)
        {
            var language = GetCodeLanguage(filename);
            return !string.IsNullOrWhiteSpace(language);
        }
    }
}