//
// IValueWriter.cs
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
	public interface IValueWriter<TSelf>
		where TSelf : IValueWriter<TSelf>
	{
		TSelf Write (bool value);
		TSelf Write (byte value);
		TSelf Write (char value);
		TSelf Write (DateTime value);
		TSelf Write (decimal value);
		TSelf Write (double value);
		TSelf Write (short value);
		TSelf Write (int value);
		TSelf Write (long value);
		TSelf Write (sbyte value);
		TSelf Write (float value);
		TSelf Write (string value);
		TSelf Write (ushort value);
		TSelf Write (uint value);
		TSelf Write (ulong value);
	}
}

