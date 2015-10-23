using System.Collections.Generic;
using System.Text;

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

            while ((pos = data.IndexOf(DocumentEnd)) != -1)
            {
                var endsAt = pos + DocumentEnd.Length;
                var doc = data.Substring(0, endsAt);
                data = data.Substring(endsAt);
                yield return doc;
            }

            _buffer.Clear();
            _buffer.Append(data);
        }
    }
}
