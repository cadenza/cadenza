using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Cdh.Toolkit.Json
{
    public class JsonWriter
    {
        private TextWriter writer;
        private int nesting = 0;

        public Formatting Formatting { get; set; }

        public JsonWriter(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            this.writer = writer;
        }

        public static void WriteTo(TextWriter writer, object obj)
        {
            new JsonWriter(writer).Write(obj);
        }

        private void WriteNewline()
        {
            if (Formatting == Formatting.None)
                return;

            writer.WriteLine();

            for (int i = 0; i < nesting; i++)
                writer.Write(Formatting == Formatting.Spaces ? "    " : "\t");
        }

        private void WriteSpace()
        {
            if (Formatting != Formatting.None)
                writer.Write(' ');
        }

        public void Write(object obj)
        {
            if (obj == null)
                writer.Write("null");

            else if (obj is IJsonSerializable)
                Write((IJsonSerializable)obj);

            else if (obj.GetType().IsPrimitive)
            {
                if (obj is bool)
                    Write((bool)obj);

                else if (obj is char)
                    Write((char)obj);

                else if (obj is long)
                    Write((long)obj);

                else if (obj is ulong)
                    Write((ulong)obj);

                else if (!(obj is IntPtr || obj is UIntPtr))
                    Write(Convert.ToDouble(obj));
            }

            else if (obj is string)
                Write((string)obj);

            else if (obj is IDictionary<string, object>)
                Write((IDictionary<string, object>)obj);

            else if (obj is IEnumerable)
                Write((IEnumerable)obj);

            else
                throw new ArgumentException("obj: Type " + obj.GetType().FullName + " cannot be serialized to JSON.");
        }

        public void Write(IJsonSerializable serializable)
        {
            Write(serializable.JsonSerialize());
        }

        public void Write(string str)
        {
            writer.Write('"');

            foreach (char i in str)
            {
                switch (i)
                {
                    case '"':
                    case '\\':
                    case '/':
                        writer.Write('\\');
                        writer.Write(i);
                        break;

                    case '\b':
                        writer.Write("\\b");
                        break;

                    case '\f':
                        writer.Write("\\f");
                        break;

                    case '\n':
                        writer.Write("\\n");
                        break;

                    case '\r':
                        writer.Write("\\r");
                        break;

                    case '\t':
                        writer.Write("\\t");
                        break;

                    default:
                        if (i < ' ' || i > '~')
                        {
                            writer.Write("\\u");
                            writer.Write(((int)i).ToString("x4"));
                        }
                        else
                        {
                            writer.Write(i);
                        }
                        break;
                }
            }

            writer.Write('"');
        }

        public void Write(IEnumerable array)
        {
            writer.Write('[');

            nesting++;

            bool comma = false;

            foreach (object i in array)
            {
                if (comma)
                    writer.Write(',');

                WriteNewline();

                comma = true;
                Write(i);
            }

            nesting--;
            if (comma)
                WriteNewline();

            writer.Write(']');
        }

        public void Write(IDictionary<string, object> obj)
        {
            writer.Write('{');

            nesting++;

            bool comma = false;

            foreach (var i in obj)
            {
                if (comma)
                    writer.Write(',');

                WriteNewline();

                comma = true;

                Write(i.Key);
                writer.Write(':');
                WriteSpace();
                Write(i.Value);
            }

            nesting--;
            if (comma)
                WriteNewline();

            writer.Write('}');
        }

        public void Write(bool boolean)
        {
            writer.Write(boolean ? "true" : "false");
        }

        public void Write(double number)
        {
            // Double.ToString returns JSON-compatible formatting.
            writer.Write(number.ToString());
        }

        #region Convenience overloads

        public void Write(float number)
        {
            writer.Write(number.ToString());
        }

        public void Write(int number)
        {
            writer.Write(number.ToString());
        }

        public void Write(uint number)
        {
            writer.Write(number.ToString());
        }

        public void Write(short number)
        {
            writer.Write(number.ToString());
        }

        public void Write(ushort number)
        {
            writer.Write(number.ToString());
        }

        public void Write(byte number)
        {
            writer.Write(number.ToString());
        }

        public void Write(sbyte number)
        {
            writer.Write(number.ToString());
        }

        public void Write(long number)
        {
            writer.Write(number.ToString());
        }

        public void Write(ulong number)
        {
            writer.Write(number.ToString());
        }

        public void Write(char ch)
        {
            Write(ch.ToString());
        }

        #endregion
    }
}
