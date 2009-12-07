//
// SequenceTest.cs
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
using System.Linq;

using NUnit.Framework;

using Cadenza.Collections;
using Cadenza.Tests;

namespace Cadenza.Collections.Tests {

	[TestFixture]
	public class SequenceTest : BaseRocksFixture {

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Iterate_FuncNull ()
		{
			Func<int, int> f = null;
			Sequence.Iterate (0, f);
		}

		[Test]
		public void Iterate ()
		{
			// not entirely sure how you sanely test an infinite list...
			#region Iterate
			Assert.AreEqual ("16,8,4,2,1",
					Sequence.Iterate (16, v => v / 2).Take (5).Implode (","));
			Assert.AreEqual ("1,2,3,4,5",
					Sequence.Iterate (1, v => v+1).Take (5).Implode (","));
			#endregion
		}

		[Test]
		public void Repeat ()
		{
			// not entirely sure how you sanely test an infinite list...
			#region Repeat
			Assert.AreEqual ("1,1,1,1,1",
					Sequence.Repeat (1).Take (5).Implode (","));
			#endregion
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void GenerateReverse_SelectorNull ()
		{
			Func<int, Maybe<Tuple<int,int>>> f = null;
			Sequence.GenerateReverse (0, f);
		}

		[Test]
		public void GenerateReverse ()
		{
			#region GenerateReverse
			Assert.AreEqual ("10,9,8,7,6,5,4,3,2,1",
				Sequence.GenerateReverse (10, 
					b => Maybe.When (b > 0, Tuple.Create (b, b-1)))
				.Implode (","));
			#endregion
		}
	}
}
