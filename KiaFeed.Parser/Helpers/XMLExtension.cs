namespace KiaFeed.Parser.Helpers
{
    using System;
    using System.Linq;

    public static class XMLExtension
    {

        public static string ExtractFeedString(this string str)
        {
            var startWith = "<![CDATA[";
            var endWith = "]]>";

            return str.IndexOf(startWith, StringComparison.OrdinalIgnoreCase) != -1 && str.LastIndexOf(endWith, StringComparison.OrdinalIgnoreCase) != -1
                ? str.Replace(startWith, String.Empty).Replace(endWith, string.Empty)
                : str;
        }

        public static string UnescapeXMLValue(this string xmlString)
        {
            if (xmlString == null)
                throw new ArgumentNullException("xmlString");

            return xmlString.Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
        }

        public static string EscapeXMLValue(this string xmlString)
        {

            if (xmlString == null)
                throw new ArgumentNullException("xmlString");

            return xmlString.Replace("'", "&apos;").Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;");
        }

        public static string SplittingWhiteSpaceElementAt(this string str, int index)
        {
            var items = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            return (items.Length > index) ? items.ElementAt(index) : String.Empty;
        }

        public static string SplittingWhiteSpaceFirstElement(this string str)
        {
            var items = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            return items.FirstOrDefault();
        }

        public static string SplittingWhiteSpaceLastElement(this string str)
        {
            var items = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            return items.LastOrDefault();
        }

    }
}