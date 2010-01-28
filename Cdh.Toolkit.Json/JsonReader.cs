// 
// JsonReader.cs
//  
// Author:
//       Chris Howie <cdhowie@gmail.com>
// 
// Copyright (c) 2010 Chris Howie
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cdh.Toolkit.Json
{
    public class JsonReader
    {
        private TextReader reader;

        public JsonReader(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            this.reader = reader;
        }

        public static object Parse(string json)
        {
            return new JsonReader(new StringReader(json)).Read();
        }

        public object Read()
        {
            try
            {
                return ReadValue();
            }
            catch (EndOfStreamException ex)
            {
                throw new InvalidDataException("Malformed JSON: Unexpected EOF", ex);
            }
        }

        private object ReadValue()
        {
            reader.SkipWhitespace();

            char next = reader.PeekChar();

            if (next == '"')
                return ReadString();

            if (next == '-' || char.IsDigit(next))
                return ReadNumber();

            if (next == '[')
                return ReadArray();

            if (next == '{')
                return ReadObject();

            return ReadKeyword();
        }

        private object ReadKeyword()
        {
            StringBuilder sb = new StringBuilder();

            int read;
            while ((read = reader.Peek()) >= 0 && char.IsLetter((char)read))
                sb.Append(reader.ReadChar());

            switch (sb.ToString())
            {
                case "true":
                    return true;

                case "false":
                    return false;

                case "null":
                    return null;
            }

            throw new InvalidDataException("Invalid JSON: Invalid keyword: " + sb.ToString());
        }

        private string ReadString()
        {
            reader.ReadChar('"');

            StringBuilder sb = new StringBuilder();

            char i;
            while ((i = reader.ReadChar()) != '"')
            {
                if (i != '\\')
                {
                    sb.Append(i);
                }
                else
                {
                    char escaped = reader.ReadChar();

                    switch (escaped)
                    {
                        case '"':
                        case '\\':
                        case '/':
                            sb.Append(escaped);
                            break;

                        case 'b':
                            sb.Append('\b');
                            break;

                        case 'f':
                            sb.Append('\f');
                            break;

                        case 'n':
                            sb.Append('\n');
                            break;

                        case 'r':
                            sb.Append('\r');
                            break;

                        case 't':
                            sb.Append('\t');
                            break;

                        case 'u':
                            string num = new string(new char[] {
                                reader.ReadChar(), reader.ReadChar(),
                                reader.ReadChar(), reader.ReadChar() });

                            sb.Append((char)int.Parse(num, System.Globalization.NumberStyles.HexNumber));
                            break;

                        default:
                            throw new InvalidDataException("Unexpected escape sequence: \\" + escaped);
                    }
                }
            }

            return sb.ToString();
        }

        private bool ReadDigitSequence(StringBuilder sb)
        {
            for (; ; )
            {
                // Careful, EOF here is valid.
                int read = reader.Peek();
                if (read < 0)
                    return false;

                char i = (char)read;
                if (!char.IsDigit(i))
                    return true;

                sb.Append(i);
                reader.Read();
            }
        }

        private double ReadNumber()
        {
            // double.Parse should correctly parse any JSON number.  But we
            // should still verify that it's a valid number according to the
            // JSON spec.
            StringBuilder sb = new StringBuilder();

            // Optional -
            if (reader.PeekChar() == '-')
            {
                reader.Read();
                sb.Append('-');
            }

            // Sequence of digits
            char i = reader.ReadChar();
            if (!char.IsDigit(i))
                throw new InvalidDataException("Malformed JSON: Expected digit");

            sb.Append(i);

            // There are only supposed to be more digits if this digit was not
            // a 0.
            if (i != '0')
            {
                if (!ReadDigitSequence(sb))
                    goto FINISH;
            }

            // An optional '.'.
            if ((char)reader.Peek() == '.')
            {
                sb.Append('.');

                // More digits
                i = reader.ReadChar();
                if (!char.IsDigit(i))
                    throw new InvalidDataException("Malformed JSON: Expected digit");

                sb.Append(i);

                if (!ReadDigitSequence(sb))
                    goto FINISH;
            }

            // An optional 'e'.
            int read = reader.Peek();
            if (read >= 0 && char.ToUpper((char)read) == 'E')
            {
                sb.Append('E');
                reader.Read();

                // An optional + or -.
                i = reader.ReadChar();
                if (i == '+' || i == '-')
                {
                    sb.Append(i);
                    i = reader.ReadChar();
                }

                // More digits
                if (!char.IsDigit(i))
                    throw new InvalidDataException("Malformed JSON: Expected digit");

                sb.Append(i);

                ReadDigitSequence(sb);
            }

        FINISH:
            return double.Parse(sb.ToString());
        }

        private List<object> ReadArray()
        {
            reader.ReadChar('[');

            List<object> list = new List<object>();

            reader.SkipWhitespace();
            if (reader.PeekChar() == ']')
            {
                reader.ReadChar();
                return list;
            }

            for (; ; )
            {
                list.Add(ReadValue());
                reader.SkipWhitespace();

                switch (reader.ReadChar())
                {
                    case ',': break;
                    case ']': return list;
                    default: throw new InvalidDataException("Malformed JSON: Expected , or ]");
                }
            }
        }

        private Dictionary<string, object> ReadObject()
        {
            reader.ReadChar('{');

            Dictionary<string, object> obj = new Dictionary<string, object>();

            reader.SkipWhitespace();
            if (reader.PeekChar() == '}')
            {
                reader.ReadChar();
                return obj;
            }

            for (; ; )
            {
                reader.SkipWhitespace();
                string key = ReadString();

                reader.SkipWhitespace();
                reader.ReadChar(':');

                obj[key] = ReadValue();

                reader.SkipWhitespace();
                switch (reader.ReadChar())
                {
                    case ',': break;
                    case '}': return obj;
                    default: throw new InvalidDataException("Malformed JSON: Expected , or }");
                }
            }
        }
    }
}
