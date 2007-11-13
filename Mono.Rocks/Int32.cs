//
// Int32.cs
//
// Author:
//   Jb Evain (jbevain@novell.com)
//
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
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

	public static class Int32Rocks {

		public static void Times (this int self, Action<int> action)
		{
			for (int i = 0; i < self; i++) {
				action (i);
			}
		}

		public static void UpTo (this int self, int limit, Action<int> action)
		{
			for (int i = self; i <= limit; i++) {
				action (i);
			}
		}

		public static void DownTo (this int self, int limit, Action<int> action)
		{
			for (int i = self; i >= limit; i--) {
				action (i);
			}
		}

		public static void Step (this int self, int limit, int step, Action<int> action)
		{
			for (int i = self; i <= limit; i += step) {
				action (i);
			}
		}
	}
}
