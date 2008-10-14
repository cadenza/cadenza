//
// EnumerableValueReader.cs
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
using System.Linq;

namespace Mono.Rocks {

	public sealed class EnumerableValueReader<T> : IValueReader<EnumerableValueReader<T>>, IDisposable
	{
		IEnumerator<T> items;

		public EnumerableValueReader (IEnumerable<T> values)
		{
			if (values == null)
				throw new ArgumentNullException ("values");
			this.items = values.GetEnumerator ();
		}

		internal R Item<R> ()
		{
			if (!items.MoveNext ())
				throw new InvalidOperationException ("no more elements");

			return Either.TryParse<T, R> (items.Current)
				.Fold<R> (v => v, v => {throw v;});
		}

		public void Dispose ()
		{
			items.Dispose ();
		}

		public EnumerableValueReader<T> Read (out bool value)
		{
			value = Item<bool> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out byte value)
		{
			value = Item<byte> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out char value)
		{
			value = Item<char> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out DateTime value)
		{
			value = Item<DateTime> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out decimal value)
		{
			value = Item<decimal> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out double value)
		{
			value = Item<double> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out short value)
		{
			value = Item<short> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out int value)
		{
			value = Item<int> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out long value)
		{
			value = Item<long> ();
			return this;
		}

		[CLSCompliant (false)]
		public EnumerableValueReader<T> Read (out sbyte value)
		{
			value = Item<sbyte> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out float value)
		{
			value = Item<float> ();
			return this;
		}

		public EnumerableValueReader<T> Read (out string value)
		{
			value = Item<string> ();
			return this;
		}

		[CLSCompliant (false)]
		public EnumerableValueReader<T> Read (out ushort value)
		{
			value = Item<ushort> ();
			return this;
		}

		[CLSCompliant (false)]
		public EnumerableValueReader<T> Read (out uint value)
		{
			value = Item<uint> ();
			return this;
		}

		[CLSCompliant (false)]
		public EnumerableValueReader<T> Read (out ulong value)
		{
			value = Item<ulong> ();
			return this;
		}
	}

	public static class EnumerableValueReaderRocks {

		public static EnumerableValueReader<TSource> Read<TSource, TValue> (this EnumerableValueReader<TSource> self, out TValue value)
		{
			value = self.Item<TValue> ();
			return self;
		}
	}
}

