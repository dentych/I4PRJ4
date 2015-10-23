using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedLib.Protocol
{
    /*
     * An expandable buffer of XML documents.
     * The buffer takes a stream of XML-documents and seperates
     * documents. Recognizes end of documents by "</command>".
     */
    public class XmlBuffer
    {
        private static string DocumentEnd = "</Command>";
        private StringBuilder _buffer = new StringBuilder();

        public void AddData(string data)
        {
            _buffer.Append(data);
        }

        public IEnumerable<string> GetDocuments()
        {
            var data = _buffer.ToString();
            int pos;
            
            while((pos = NextCmdLength(data)) != -1)
            {
                var endsAt = pos;
                var doc = data.Substring(0, endsAt);
                data = data.Substring(endsAt);
                yield return doc;
            }

            _buffer.Clear();
            _buffer.Append(data);
        }

        private int NextCmdLength(string buffer)
        {
            var pos = buffer.IndexOf(DocumentEnd);
            if (pos != -1)
                return pos;

            Regex rx = new Regex(@"<Command Name="".*"" />",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(buffer);

            if (matches.Count > 0)
            {
                var match = matches[0].ToString();
                var length = match.Length;
                var p = buffer.IndexOf(match);
                return p + length;
            }

            return -1;
        }
    }
}
