//
// MathContract.cs
//
// Author:
//   Jonathan Pryor
//
// Copyright (c) 2010 Novell, Inc. (http://www.novell.com)
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
using System.Linq;

using NUnit.Framework;

using Cadenza;
using Cadenza.Tests;

namespace Cadenza.Numerics.Tests {

	public class MathContract<T> : BaseRocksFixture {

		[Test]
		public void LessThan ()
		{
			var m = Math<T>.Default;
			Assert.IsTrue (m.LessThan (m.FromInt32 (3), m.FromInt32 (4)));
			Assert.IsFalse (m.LessThan (m.FromInt32 (3), m.FromInt32 (3)));
			Assert.IsFalse (m.LessThan (m.FromInt32 (4), m.FromInt32 (3)));
		}

		[Test]
		public void LessThanOrEqual ()
		{
			var m = Math<T>.Default;
			Assert.IsTrue (m.LessThanOrEqual (m.FromInt32 (3), m.FromInt32 (4)));
			Assert.IsTrue (m.LessThanOrEqual (m.FromInt32 (3), m.FromInt32 (3)));
			Assert.IsFalse (m.LessThanOrEqual (m.FromInt32 (4), m.FromInt32 (3)));
		}

		[Test]
		public void GreaterThan ()
		{
			var m = Math<T>.Default;
			Assert.IsFalse (m.GreaterThan (m.FromInt32 (3), m.FromInt32 (4)));
			Assert.IsFalse (m.GreaterThan (m.FromInt32 (3), m.FromInt32 (3)));
			Assert.IsTrue (m.GreaterThan (m.FromInt32 (4), m.FromInt32 (3)));
		}

		[Test]
		public void GreaterThanOrEqual ()
		{
			var m = Math<T>.Default;
			Assert.IsFalse (m.GreaterThanOrEqual (m.FromInt32 (3), m.FromInt32 (4)));
			Assert.IsTrue (m.GreaterThanOrEqual (m.FromInt32 (3), m.FromInt32 (3)));
			Assert.IsTrue (m.GreaterThanOrEqual (m.FromInt32 (4), m.FromInt32 (3)));
		}

		[Test]
		public void Max ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (m.FromInt32 (3), m.Max (m.FromInt32 (2), m.FromInt32 (3)));
		}

		[Test]
		public void Min ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (m.FromInt32 (2), m.Min (m.FromInt32 (2), m.FromInt32 (3)));
		}

		[Test]
		public void Successor ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (m.FromInt32 (1), m.Successor (m.FromInt32 (0)));
			try {
				var max = m.MaxValue;
				Assert.Throws<OverflowException>(() => m.Successor (max));
			}
			catch (NotSupportedException) {
				// thrown if m.MaxValue doesn't exist
			}
		}

		[Test]
		public void Predecessor ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (m.FromInt32 (0), m.Predecessor (m.FromInt32 (1)));
			try {
				var min = m.MinValue;
				Assert.Throws<OverflowException>(() => m.Predecessor (min));
			}
			catch (NotSupportedException) {
				// thrown if m.MinValue doesn't exist
			}
		}

		[Test]
		public void ToInt32 ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (5, m.ToInt32 (m.FromInt32 (5)));
		}
	}
}
