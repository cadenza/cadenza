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
