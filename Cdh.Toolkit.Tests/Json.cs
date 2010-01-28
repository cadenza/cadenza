// 
// Json.cs
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
using NUnit.Framework;
using System.IO;
using Cdh.Toolkit.Json;

namespace Cdh.Toolkit.Tests
{
    [TestFixture]
    public class Json
    {
        [Test]
        public void JsonWriter()
        {
            var dict = new Dictionary<string, object>();

            dict["byte"] = (byte)42;
            dict["sbyte"] = (sbyte)-5;

            dict["char"] = 'b';

            dict["ushort"] = 49152;
            dict["short"] = -12345;

            dict["int"] = int.MinValue;
            dict["uint"] = uint.MaxValue << 3;

            dict["long"] = long.MinValue;
            dict["ulong"] = ulong.MaxValue << 3;

            dict["single"] = 0.12345f;
            dict["double"] = 0.000000000012345;

            dict["string"] = "a test \n\r string";

            dict["array"] = new List<string>() { "a", "test", "array" };

            dict["emptyarray"] = new object[0];
            dict["emptydict"] = new Dictionary<string, object>();

            using (StreamWriter writer = File.CreateText("output.txt"))
            {
                JsonWriter json = new JsonWriter(writer);
                json.Formatting = Formatting.Spaces;

                json.Write(dict);
            }
        }
    }
}
