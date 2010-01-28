// 
// Rocks.cs
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
