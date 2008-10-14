//
// StreamConverter.cs
//
// Authors:
//   Jonathan Pryor  <jpryor@novell.com>
//   Bojan Rajkovic  <bojanr@brandeis.edu>
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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Rocks {

	public abstract class StreamConverter : IValueReader<StreamConverter>, IValueWriter<StreamConverter>
	{
		protected StreamConverter ()
		{
		}

		public abstract StreamConverter Read (out bool value);
		public abstract StreamConverter Read (out byte value);
		public abstract StreamConverter Read (byte[] value, int offset, int count);
		public abstract StreamConverter Read (out char value);
		public abstract StreamConverter Read (out DateTime value);
		public abstract StreamConverter Read (out decimal value);
		public abstract StreamConverter Read (out double value);
		public abstract StreamConverter Read (out short value);
		public abstract StreamConverter Read (out int value);
		public abstract StreamConverter Read (out long value);
		public abstract StreamConverter Read (out float value);
		public abstract StreamConverter Read (out string value);
		public abstract StreamConverter Read (int size, Encoding encoding, out string value);

		[CLSCompliant (false)]
		public virtual StreamConverter Read (out sbyte value)
		{
			byte v;
			Read (out v);
			value = (sbyte) v;
			return this;
		}

		[CLSCompliant (false)]
		public virtual StreamConverter Read (out ushort value)
		{
			short v;
			Read (out v);
			value = (ushort) v;
			return this;
		}

		[CLSCompliant (false)]
		public virtual StreamConverter Read (out uint value)
		{
			int v;
			Read (out v);
			value = (uint) v;
			return this;
		}

		[CLSCompliant (false)]
		public virtual StreamConverter Read (out ulong value)
		{
			long v;
			Read (out v);
			value = (ulong) v;
			return this;
		}

		public StreamConverter Read (byte[] value)
		{
			Check.Value (value);

			return Read (value, 0, value.Length);
		}

		public abstract StreamConverter Write (bool value);
		public abstract StreamConverter Write (byte value);
		public abstract StreamConverter Write (char value);
		public abstract StreamConverter Write (DateTime value);
		public abstract StreamConverter Write (decimal value);
		public abstract StreamConverter Write (double value);
		public abstract StreamConverter Write (short value);
		public abstract StreamConverter Write (int value);
		public abstract StreamConverter Write (long value);
		public abstract StreamConverter Write (float value);
		public abstract StreamConverter Write (string value);
		public abstract StreamConverter Write (byte[] value, int offset, int count);

		[CLSCompliant (false)]
		public virtual StreamConverter Write (sbyte value)
		{
			return Write ((byte) value);
		}

		[CLSCompliant (false)]
		public virtual StreamConverter Write (ushort value)
		{
			return Write ((short) value);
		}

		[CLSCompliant (false)]
		public virtual StreamConverter Write (uint value)
		{
			return Write ((int) value);
		}

		[CLSCompliant (false)]
		public virtual StreamConverter Write (ulong value)
		{
			return Write ((long) value);
		}

		public StreamConverter Write (byte[] value)
		{
			Check.Value (value);

			return Write (value, 0, value.Length);
		}
	}

	public static class StreamConverterRocks
	{
		public static StreamConverter Read<TValue> (this StreamConverter self, out TValue value)
		{
			Check.Self (self);

			byte[] data = new byte [Marshal.SizeOf (typeof (TValue))];
			self.Read (data);
			GCHandle handle = GCHandle.Alloc (data, GCHandleType.Pinned);

			try { 
				value = (TValue) Marshal.PtrToStructure (handle.AddrOfPinnedObject (), typeof (TValue)); 
			} finally {
				handle.Free();
			}

			return self;
		}

		public static StreamConverter Write<TValue> (this StreamConverter self, TValue value)
		{
			Check.Self (self);

			byte[] data = new byte [Marshal.SizeOf (typeof (TValue))];

			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			try {
				Marshal.StructureToPtr (value, handle.AddrOfPinnedObject(), false);
			} finally {
				handle.Free();
			}

			return self.Write (data);
		}
	}
}

