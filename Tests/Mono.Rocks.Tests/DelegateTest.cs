//
// DelegateTest.cs
//
// Author:
//   Jonathan Pryor
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

using NUnit.Framework;

using Mono.Rocks;

// "The variable `r' is assigned but it's value is never used."
// It's value isn't supposed to be used; it's purpose is as a manual check the
// the generated .Curry() methods generate the correct return type.
#pragma warning disable 0219

namespace Mono.Rocks.Tests {

	[TestFixture]
	public class DelegateTest : BaseRocksFixture {

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A1_P1_SelfNull ()
		{
			Action<byte>  a = null;
			Action        r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F1_P1_SelfNull ()
		{
			Func<byte, char>  a = null;
			Func<char>        r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A2_P1_SelfNull ()
		{
			Action<byte, char>  a = null;
			Action<char>        r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A2_P2_SelfNull ()
		{
			Action<byte, char>  a = null;
			Action              r = a.Curry ((byte) 1, '2');
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F2_P1_SelfNull ()
		{
			Func<byte, char, short> a = null;
			Func<char, short>       r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F2_P2_SelfNull ()
		{
			Func<byte, char, short> a = null;
			Func<short>             r = a.Curry ((byte) 1, '2');
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A3_P1_SelfNull ()
		{
			Action<byte, char, short> a = null;
			Action<char, short>       r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A3_P2_SelfNull ()
		{
			Action<byte, char, short> a = null;
			Action<short>             r = a.Curry ((byte) 1, '2');
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A3_P3_SelfNull ()
		{
			Action<byte, char, short> a = null;
			Action                    r = a.Curry ((byte) 1, '2', (short) 3);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F3_P1_SelfNull ()
		{
			Func<byte, char, short, int>  a = null;
			Func<char, short, int>        r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F3_P2_SelfNull ()
		{
			Func<byte, char, short, int>  a = null;
			Func<short, int>              r = a.Curry ((byte) 1, '2');
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F3_P3_SelfNull ()
		{
			Func<byte, char, short, int>  a = null;
			Func<int>                     r = a.Curry ((byte) 1, '2', (short) 3);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A4_P1_SelfNull ()
		{
			Action<byte, char, short, int>  a = null;
			Action<char, short, int>        r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A4_P2_SelfNull ()
		{
			Action<byte, char, short, int>  a = null;
			Action<short, int>              r = a.Curry ((byte) 1, '2');
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A4_P3_SelfNull ()
		{
			Action<byte, char, short, int>  a = null;
			Action<int>                     r = a.Curry ((byte) 1, '2', (short) 3);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_A4_P4_SelfNull ()
		{
			Action<byte, char, short, int>  a = null;
			Action                          r = a.Curry ((byte) 1, '2', (short) 3, 4);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F4_P1_SelfNull ()
		{
			Func<byte, char, short, int, long>  a = null;
			Func<char, short, int, long>        r = a.Curry ((byte) 1);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F4_P2_SelfNull ()
		{
			Func<byte, char, short, int, long>  a = null;
			Func<short, int, long>              r = a.Curry ((byte) 1, '2');
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F4_P3_SelfNull ()
		{
			Func<byte, char, short, int, long>  a = null;
			Func<int, long>                     r = a.Curry ((byte) 1, '2', (short) 3);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Curry_F4_P4_SelfNull ()
		{
			Func<byte, char, short, int, long>  a = null;
			Func<long>                          r = a.Curry ((byte) 1, '2', (short) 3, 4);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A1_SelfNull ()
		{
			Action<char>      s = null;
			Func<byte, char>  x = a => (char) a;
			Action<byte>      r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A1_ComposerNull ()
		{
			Action<char>      s = a => {};
			Func<byte, char>  x = null;
			Action<byte>      r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F1_SelfNull ()
		{
			Func<char, short>   s = null;
			Func<byte, char>    x = a => (char) a;
			Func<byte, short>   r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F1_ComposerNull ()
		{
			Func<char, short>   s = a => (short) a;
			Func<byte, char>    x = null;
			Func<byte, short>   r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A2_SelfNull ()
		{
			Action<short>           s = null;
			Func<byte, char, short> x = (a, b) => a;
			Action<byte, char>      r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A2_ComposerNull ()
		{
			Action<short>           s = a => {};
			Func<byte, char, short> x = null;
			Action<byte, char>      r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F2_SelfNull ()
		{
			Func<short, int>        s = null;
			Func<byte, char, short> x = (a, b) => a;
			Func<byte, char, int>   r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F2_ComposerNull ()
		{
			Func<short, int>        s = a => a;
			Func<byte, char, short> x = null;
			Func<byte, char, int>   r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A3_SelfNull ()
		{
			Action<int>                   s = null;
			Func<byte, char, short, int>  x = (a, b, c) => c;
			Action<byte, char, short>     r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A3_ComposerNull ()
		{
			Action<int>                   s = a => {};
			Func<byte, char, short, int>  x = null;
			Action<byte, char, short>     r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F3_SelfNull ()
		{
			Func<int, long>               s = null;
			Func<byte, char, short, int>  x = (a, b, c) => c;
			Func<byte, char, short, long> r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F3_ComposerNull ()
		{
			Func<int, long>               s = a => a;
			Func<byte, char, short, int>  x = null;
			Func<byte, char, short, long> r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A4_SelfNull ()
		{
			Action<long>                        s = null;
			Func<byte, char, short, int, long>  x = (a, b, c, d) => d;
			Action<byte, char, short, int>      r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_A4_ComposerNull ()
		{
			Action<long>                        s = a => {};
			Func<byte, char, short, int, long>  x = null;
			Action<byte, char, short, int>      r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F4_SelfNull ()
		{
			Func<long, double>                    s = null;
			Func<byte, char, short, int, long>    x = (a, b, c, d) => d;
			Func<byte, char, short, int, double>  r = s.Compose(x);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Compose_F4_ComposerNull ()
		{
			Func<long, double>                    s = a => a;
			Func<byte, char, short, int, long>    x = null;
			Func<byte, char, short, int, double>  r = s.Compose(x);
		}

		[Test]
		public void Compose ()
		{
			var              tostring = Lambda.F ((int n) => n.ToString ());
			var               doubler = Lambda.F ((int n) => n * 2);
			var  double_then_tostring = tostring.Compose (doubler);
			Assert.AreEqual ("10", double_then_tostring (5));
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_A1_SelfNull ()
		{
			Action<byte> s = null;
			Action<byte> r = s.Curry ();
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_F1_SelfNull ()
		{
			Func<byte, char> s = null;
			Func<byte, char> r = s.Curry ();
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_A2_SelfNull ()
		{
			Action<byte, char>       s = null;
			Func<byte, Action<char>> r = s.Curry ();
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_F2_SelfNull ()
		{
			Func<byte, char, short>        s = null;
			Func<byte, Func<char, short>>  r = s.Curry ();
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_A3_SelfNull ()
		{
			Action<byte, char, short>              s = null;
			Func<byte, Func<char, Action<short>>>  r = s.Curry ();
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_F3_SelfNull ()
		{
			Func<byte, char, short, int>             s = null;
			Func<byte, Func<char, Func<short, int>>> r = s.Curry ();
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_A4_SelfNull ()
		{
			Action<byte, char, short, int>                   s = null;
			Func<byte, Func<char, Func<short, Action<int>>>> r = s.Curry ();
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void TraditionalCurry_F4_SelfNull ()
		{
			Func<byte, char, short, int, long>                   s = null;
			Func<byte, Func<char, Func<short, Func<int, long>>>> r = s.Curry ();
		}

		[Test]
		public void TraditionalCurry ()
		{
			var a = Lambda.F<int, int, int, int> ((x, y, z) => x + y + z);
			var b = a.Curry ();
			var c = b (1);
			var d = c (2);
			Assert.AreEqual (6, d (3));
		}
	}
}
