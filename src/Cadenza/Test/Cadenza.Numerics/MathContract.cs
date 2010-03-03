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

		[Test]
		public void EnumerateFrom ()
		{
			var m = Math<T>.Default;
			AssertAreSame (
					new [] { 3, 4, 5 },
					m.EnumerateFrom (m.FromInt32 (3)).Take (3).Select (v => m.ToInt32 (v)));
		}

		[Test]
		public void EnumerateFromThen ()
		{
			var m = Math<T>.Default;
			AssertAreSame (
					new [] { 0, 3, 4, 5 },
					m.EnumerateFromThen (m.FromInt32 (0), m.FromInt32 (3)).Take (4).Select (v => m.ToInt32 (v)));
		}

		[Test]
		public void EnumerateFromTo ()
		{
			var m = Math<T>.Default;
			Assert.Throws<ArgumentException>(() => m.EnumerateFromTo (m.FromInt32 (5), m.FromInt32 (2)));
			AssertAreSame (
					new [] { 2, 3, 4, 5 },
					m.EnumerateFromTo (m.FromInt32 (2), m.FromInt32 (5)).Select (v => m.ToInt32 (v)));
		}

		[Test]
		public void EnumerateFromThenTo ()
		{
			var m = Math<T>.Default;
			Assert.Throws<ArgumentException>(() => m.EnumerateFromThenTo (m.FromInt32 (10), m.FromInt32 (5), m.FromInt32 (2)));
			AssertAreSame (
					new [] { 0, 2, 3, 4, 5 },
					m.EnumerateFromThenTo (m.FromInt32 (0), m.FromInt32 (2), m.FromInt32 (5)).Select (v => m.ToInt32 (v)));
		}

		[Test]
		public void Add ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (m.FromInt32 (3), m.Add (m.FromInt32 (1), m.FromInt32 (2)));
			Assert.AreEqual (m.FromInt32 (3), m.Add (m.FromInt32 (2), m.FromInt32 (1)));
			Assert.AreEqual (m.FromInt32 (2), m.Add (m.FromInt32 (2), m.FromInt32 (0)));
			Assert.AreEqual (m.FromInt32 (2), m.Add (m.FromInt32 (0), m.FromInt32 (2)));
			try {
				var max = m.MaxValue;
				var max1 = m.Add (max, m.FromInt32 (1));
				// If we're here, then T should be a floating point type
				Assert.IsTrue (m.IsIEEE (max1));
			}
			catch (OverflowException) {
				// thrown if m.MaxValue+1 can't be represented
			}
			catch (NotSupportedException) {
				// thrown if m.MaxValue doesn't exist
			}
		}

		[Test]
		public void Multiply ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (m.FromInt32 (1), m.Multiply (m.FromInt32 (1), m.FromInt32 (1)));
			Assert.AreEqual (m.FromInt32 (0), m.Multiply (m.FromInt32 (1), m.FromInt32 (0)));
			Assert.AreEqual (m.FromInt32 (0), m.Multiply (m.FromInt32 (0), m.FromInt32 (0)));
			Assert.AreEqual (m.FromInt32 (0), m.Multiply (m.FromInt32 (0), m.FromInt32 (1)));
			Assert.AreEqual (m.FromInt32 (6), m.Multiply (m.FromInt32 (2), m.FromInt32 (3)));

			try {
				var max = m.MaxValue;
				var max1 = m.Multiply (max, max);
				// If we're here, then T should be a floating point type
				Assert.IsTrue (m.IsIEEE (max1));
				Assert.IsTrue (m.IsInfinite (max1));
			}
			catch (OverflowException) {
				// thrown if m.MaxValue+1 can't be represented
			}
			catch (NotSupportedException) {
				// thrown if m.MaxValue doesn't exist
			}
		}

		[Test]
		public void Subtract ()
		{
			var m = Math<T>.Default;
			Assert.AreEqual (m.FromInt32 (1), m.Subtract (m.FromInt32 (3), m.FromInt32 (2)));
			try {
				var min = m.MinValue;
				var min1 = m.Subtract (min, m.FromInt32 (1));
				// If we're here, then T should be a floating point type
				Assert.IsTrue (m.IsIEEE (min1));
			}
			catch (OverflowException) {
				// thrown if m.MaxValue+1 can't be represented
			}
			catch (NotSupportedException) {
				// thrown if m.MaxValue doesn't exist
			}
		}

		[Test]
		public void Negate ()
		{
			var m = Math<T>.Default;
			try {
				Assert.AreEqual (m.FromInt32 (-1), m.Negate (m.FromInt32 (1)));
			}
			catch (NotSupportedException) {
				// likely unsigned values.
			}

			try {
				var min = m.MinValue;
				var nmin = m.Negate (min);
				Assert.IsTrue (m.IsIEEE (nmin));
			}
			catch (OverflowException) {
				// in 2's complement, `-int.MinValue` can't be held in an int.
			}
			catch (NotSupportedException) {
				// no MinValue
			}
		}

		[Test]
		public void Abs ()
		{
			var m = Math<T>.Default;

			Assert.AreEqual (m.FromInt32 (2), m.Abs (m.FromInt32 (2)));
			try {
				Assert.AreEqual (m.FromInt32 (2), m.Abs (m.FromInt32 (-2)));
			}
			catch (NotSupportedException) {
				// unsigned type
			}
		}

		[Test]
		public void Sign ()
		{
			var m = Math<T>.Default;

			Assert.AreEqual (m.FromInt32 (1), m.Sign (m.FromInt32 (3)));
			Assert.AreEqual (m.FromInt32 (1), m.Sign (m.FromInt32 (2)));
			Assert.AreEqual (m.FromInt32 (1), m.Sign (m.FromInt32 (1)));
			Assert.AreEqual (m.FromInt32 (0), m.Sign (m.FromInt32 (0)));

			var n = m.FromInt32 (2);
			Assert.AreEqual (n, m.Multiply (m.Abs (n), m.Sign (n)));

			n = m.FromInt32 (0);
			Assert.AreEqual (n, m.Multiply (m.Abs (n), m.Sign (n)));

			try {
				Assert.AreEqual (m.FromInt32 (-1), m.Sign (m.FromInt32 (-1)));
				Assert.AreEqual (m.FromInt32 (-1), m.Sign (m.FromInt32 (-2)));
				Assert.AreEqual (m.FromInt32 (-1), m.Sign (m.FromInt32 (-3)));

				n = m.FromInt32 (-2);
				Assert.AreEqual (n, m.Multiply (m.Abs (n), m.Sign (n)));
			}
			catch (NotSupportedException) {
				// unsigned
			}
		}
	}
}
