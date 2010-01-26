using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cdh.Toolkit.Json
{
    internal static class Rocks
    {
        internal static void ReadChar(this TextReader reader, char @char)
        {
            if (reader.ReadChar() != @char)
                throw new InvalidDataException("Expected '" + @char + "'");
        }

        internal static char ReadChar(this TextReader reader)
        {
            int read = reader.Read();
            if (read < 0)
                throw new EndOfStreamException();

            return (char)read;
        }

        internal static char PeekChar(this TextReader reader)
        {
            int read = reader.Peek();
            if (read < 0)
                throw new EndOfStreamException();

            return (char)read;
        }

        internal static void SkipWhitespace(this TextReader reader)
        {
            for (; ; )
            {
                int read = reader.Peek();
                if (read < 0)
                    return;

                if (!char.IsWhiteSpace((char)read))
                    return;

                reader.Read();
            }
        }
    }
}
