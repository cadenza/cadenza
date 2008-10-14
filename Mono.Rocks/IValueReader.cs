//
// IValuReader.cs
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

namespace Mono.Rocks {

	[CLSCompliant (false)]
	public interface IValueReader<TSelf>
		where TSelf : IValueReader<TSelf>
	{
		TSelf Read (out bool value);
		TSelf Read (out byte value);
		TSelf Read (out char value);
		TSelf Read (out DateTime value);
		TSelf Read (out decimal value);
		TSelf Read (out double value);
		TSelf Read (out short value);
		TSelf Read (out int value);
		TSelf Read (out long value);
		TSelf Read (out sbyte value);
		TSelf Read (out float value);
		TSelf Read (out string value);
		TSelf Read (out ushort value);
		TSelf Read (out uint value);
		TSelf Read (out ulong value);
	}
}

