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

namespace Cadenza.Tools {

	public static class Types {

		public const int MaxFuncArgs = 4;

		public static CodeTypeReference Action (int total)
		{
			return Action (total, 0);
		}

		public static CodeTypeReference Action (int total, int start)
		{
			return Action (total, start, total - start);
		}

		public static CodeTypeReference Action (int total, int start, int count)
		{
			return new CodeTypeReference (
					(total - (start + count)) <= MaxFuncArgs ? "System.Action" : "Cadenza.CadenzaAction",
					GetTypeParameterReferences (total, start, count, false).ToArray ());
		}

		public static CodeTypeReference ThisAction (int total, int start)
		{
			return ThisAction (total, start, total - start);
		}

		public static CodeTypeReference ThisAction (int total, int start, int count)
		{
			return new CodeTypeReference (
					(total - count) <= MaxFuncArgs ? "this System.Action" : "this Cadenza.CadenzaAction",
					GetTypeParameterReferences (total, start, count, false).ToArray ());
		}

		public static IEnumerable<CodeTypeReference> GetTypeParameterReferences (int total, bool ret)
		{
			return GetTypeParameterReferences (total, 0, ret);
		}

		public static IEnumerable<CodeTypeReference> GetTypeParameterReferences (int total, int start, bool ret)
		{
			return GetTypeParameterReferences (total, start, total - start, ret);
		}

		public static IEnumerable<CodeTypeReference> GetTypeParameterReferences (int total, int start, int end, bool ret)
		{
			return GetTypeParameters (total, start, end, ret)
				.Select (p => new CodeTypeReference (p));
		}

		public static IEnumerable<CodeTypeParameter> GetTypeParameters (int total, bool ret)
		{
			return GetTypeParameters (total, 0, ret);
		}

		public static IEnumerable<CodeTypeParameter> GetTypeParameters (int total, int start, bool ret)
		{
			return GetTypeParameters (total, start, total - start, ret);
		}

		public static IEnumerable<CodeTypeParameter> GetTypeParameters (int total, int start, int count, bool ret)
		{
			return Enumerable.Range (start, count)
				.Select (
					v => new CodeTypeParameter (GetTypeParameter (total, v)))
				.Concat (ret ? new[]{new CodeTypeParameter ("TResult")} : new CodeTypeParameter [0]);
		}

		public static CodeTypeReference Func (int total)
		{
			return Func (total, 0);
		}

		public static CodeTypeReference Func (int total, int start)
		{
			return Func (total, start, total - start);
		}

		public static string FuncType (int total)
		{
			return total <= MaxFuncArgs
				? "System.Func"
				: "Cadenza.CadenzaFunc";
		}

		public static CodeTypeReference Func (int total, int start, int count)
		{
			return new CodeTypeReference (
					FuncType (total - count),
					GetTypeParameterReferences (total, start, count, true).ToArray ());
		}

		public static CodeTypeReference ThisFunc (int total, int start)
		{
			return ThisFunc (total, start, total - start);
		}

		public static CodeTypeReference ThisFunc (int total, int start, int count)
		{
			return new CodeTypeReference (
					"this " + FuncType (total - count),
					GetTypeParameterReferences (total, start, true).ToArray ());
		}

		public static string GetTypeParameter (int total, int index)
		{
			return total == 1 ? "T" : "T" + (index+1);
		}

		public static string GetTypeParameterList (int n)
		{
			var targs = new StringBuilder ();
			targs.Append (GetTypeParameter (n, 0));
			for (int i = 1; i < n; ++i) {
				targs.Append (",").Append (GetTypeParameter (n, i));
			}
			return targs.ToString ();
		}
	}
}
