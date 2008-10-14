//
// TextValueReader.cs
//
// Author:
//   Jonathan Pryor  <jpryor@novell.com>
//
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Mono.Rocks {

	public sealed class TextValueReader : IValueReader<TextValueReader>, IDisposable
	{
		IEnumerator<string> items;

		public TextValueReader (IEnumerable<string> values)
		{
			if (values == null)
				throw new ArgumentNullException ("values");
			this.items = values.GetEnumerator ();
		}

		private string Word ()
		{
			if (!items.MoveNext ())
				throw new InvalidOperationException ("no more elements");
			return items.Current;
		}

		public void Dispose ()
		{
			items.Dispose ();
		}

		public TextValueReader Read (out bool value)
		{
			value = bool.Parse (Word ());
			return this;
		}

		public TextValueReader Read (out byte value)
		{
			value = byte.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out byte value)
		{
			value = byte.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out char value)
		{
			value = char.Parse (Word ());
			return this;
		}

		public TextValueReader Read (out DateTime value)
		{
			value = DateTime.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out DateTime value)
		{
			value = DateTime.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out decimal value)
		{
			value = decimal.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out decimal value)
		{
			value = decimal.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out double value)
		{
			value = double.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out double value)
		{
			value = double.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out short value)
		{
			value = short.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out short value)
		{
			value = short.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out int value)
		{
			value = int.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out int value)
		{
			value = int.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out long value)
		{
			value = long.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out long value)
		{
			value = long.Parse (Word (), provider);
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (out sbyte value)
		{
			value = sbyte.Parse (Word ());
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (IFormatProvider provider, out sbyte value)
		{
			value = sbyte.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out float value)
		{
			value = float.Parse (Word ());
			return this;
		}

		public TextValueReader Read (IFormatProvider provider, out float value)
		{
			value = float.Parse (Word (), provider);
			return this;
		}

		public TextValueReader Read (out string value)
		{
			value = Word ();
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (out ushort value)
		{
			value = ushort.Parse (Word ());
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (IFormatProvider provider, out ushort value)
		{
			value = ushort.Parse (Word (), provider);
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (out uint value)
		{
			value = uint.Parse (Word ());
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (IFormatProvider provider, out uint value)
		{
			value = uint.Parse (Word (), provider);
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (out ulong value)
		{
			value = ulong.Parse (Word ());
			return this;
		}

		[CLSCompliant (false)]
		public TextValueReader Read (IFormatProvider provider, out ulong value)
		{
			value = ulong.Parse (Word (), provider);
			return this;
		}
	}

	public static class TextValueReaderRocks
	{
		public static TextValueReader Read<TValue> (this TextValueReader self, out TValue value)
		{
			Check.Self (self);

			string s;
			self.Read (out s);

			value = Either.TryParse<TValue> (s)
				.Fold<TValue> (v => v, v => {throw v;});

			return self;
		}
	}
}

