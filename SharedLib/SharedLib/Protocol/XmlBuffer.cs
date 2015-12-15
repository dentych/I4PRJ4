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


    /// <summary>
    /// XmlBuffer specific buffer which implements the IProtocolBuffer interface.
    /// </summary>
    public class XmlBuffer : IProtocolBuffer
    {
        private StringBuilder _buffer = new StringBuilder();
        private static Regex _cmdEndPattern = new Regex(
                @"(</Command>|<Command\s+Name=""([^""]*)""\s/>)+",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Add the incoming data from a transmission into a buffer collection
        /// </summary>
        /// <param name="data">recieved data</param>
        public void AddData(string data)
        {
            _buffer.Append(data);
        }

        /// <summary>
        /// Get all the fully transferred commands from the buffer collection 
        /// </summary>
        /// <returns>Enumerable with commands</returns>
        public IEnumerable<string> GetDocuments()
        {
            var data = _buffer.ToString();

            while (true)
            {
                var match = _cmdEndPattern.Match(data);

                if (match.Value == String.Empty)
                    break;

                var docLength = match.Index + match.Length;
                var doc = data.Substring(0, docLength);
                data = data.Substring(docLength);

                yield return doc;
            }

            _buffer.Clear();
            _buffer.Append(data);
        }
    }
}
