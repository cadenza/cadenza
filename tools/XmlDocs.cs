//
// FileGenerator.cs
//
// Author:
//   Jonathan Pryor  <jpryor@novell.com>
//
// Copyright (c) 2009 Novell, Inc. (http://www.novell.com)
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
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CSharp;

using Mono.Options;

namespace Mono.Rocks.Tools {

	public static class XmlDocs {

		public static string Cref (CodeTypeReference type)
		{
			var cref = new StringBuilder ();
			cref.Append ("T:").Append (GetType (type));
			if (type.TypeArguments.Count > 0) {
				cref.Append ("{");
				cref.Append (GetType (type.TypeArguments [0]));
				for (int i = 1; i < type.TypeArguments.Count; ++i)
					cref.Append (",").Append (GetType (type.TypeArguments [i]));
				cref.Append ("}");
			}
			return cref.ToString ();
		}

		static string GetType (CodeTypeReference type)
		{
			var b = type.BaseType;
			if (b.Contains ('`'))
				b = b.Substring (0, b.IndexOf ('`'));
			return b;
		}

		public static IEnumerable<string> Exception (Type ex, params object[] block)
		{
			return Block ("exception", "cref=\"" + ex.FullName + "\"", block);
		}

		static IEnumerable<string> Block (string element, string attrs, params object[] block)
		{
			yield return "<" + element + (!string.IsNullOrEmpty (attrs) ? " " + attrs : "") + ">";
			foreach (var o in Seq.Expand (block))
				yield return "  " + o.ToString ();
			yield return "</" + element + ">";
		}

		public static IEnumerable<string> ArgumentNullException (string parameter)
		{
			return Exception (typeof (ArgumentNullException),
					"if <paramref name=\"" + parameter + "\"/> is <see langword=\"null\" />.");
		}

		public static IEnumerable<string> Param (string param, params object[] block)
		{
			return Block ("param", "name=\"" + param + "\"", block);
		}

		public static IEnumerable<string> Remarks (params object[] block)
		{
			return Block ("remarks", null, block);
		}

		public static IEnumerable<string> Returns (params object[] block)
		{
			return Block ("returns", null, block);
		}

		public static string See (CodeTypeReference type)
		{
			return "<see cref=\"" + Cref (type) + "\" />";
		}

		public static IEnumerable<string> Summary (params object[] block)
		{
			return Block ("summary", null, block);
		}

		public static IEnumerable<string> TypeParams (CodeTypeParameterCollection typeParams, CodeTypeReference type)
		{
			return typeParams.Cast<CodeTypeParameter> ()
				.Select (p => Block ("typeparam", "name=\"" + p.Name + "\"",
					"A " + See (type) + (p.Name == "TResult" ? " return type." : " parameter type.")))
				.SelectMany (x => x);
		}
	}
}
