//
// WeakReferenceTest.cs
//
// Author:
//   Andrés G. Aragoneses
//
// Copyright (c) 2010 Andrés G. Aragoneses
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

using NUnit.Framework;

using Cadenza;

namespace Cadenza.Tests {

	[TestFixture]
	public class WeakReferenceTest : BaseRocksFixture {

		[Test]
		public void TargetBaseCtor ()
		{
			var val = new Uri ("file:///someuri/somepath");
			var wr = new Cadenza.WeakReference<Uri> (val);
			Assert.AreSame (val, wr.Target);

			val = new Uri ("file:///someuri/somepath");
			Assert.AreNotSame (val, wr.Target);
			wr.Target = val;
			Assert.AreSame (val, wr.Target);
		}

		[Test]
		public void TargetAdvancedCtor ()
		{
			var track = true;
			var val = new Uri ("file:///someuri/somepath");
			var wr = new Cadenza.WeakReference<Uri> (val, track);
			Assert.AreSame (val, wr.Target);
			Assert.AreEqual (track, wr.TrackResurrection);

			track = false;
			wr = new Cadenza.WeakReference<Uri> (val, track);
			Assert.AreEqual (track, wr.TrackResurrection);
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void AssignIncorrectTarget ()
		{
			WeakReference<string> r = new WeakReference<string> ("foo");
			WeakReference b = r;
			b.Target = new List<string> ();
		}
	}
}
