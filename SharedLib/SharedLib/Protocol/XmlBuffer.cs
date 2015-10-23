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
        private StringBuilder _buffer = new StringBuilder();
        private static Regex _cmdEndPattern = new Regex(
                @"(</Command>|<Command\s+Name="".*""\s*/>)+",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);


        public void AddData(string data)
        {
            _buffer.Append(data);
        }

        public IEnumerable<string> GetDocuments()
        {
            var data = _buffer.ToString();
            int length;
            
            while((length = NextCmdLength(data)) != -1)
            {
                var doc = data.Substring(0, length);
                data = data.Substring(length);
                yield return doc;
            }

            _buffer.Clear();
            _buffer.Append(data);
        }

        private int NextCmdLength(string buffer)
        {
            var match = _cmdEndPattern.Match(buffer);

            if(match.Value != String.Empty)
                return match.Index + match.Length;

            return -1;
        }
    }
}
