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

	static class Types {

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
					(total - (start + count)) <= MaxFuncArgs ? "System.Action" : "Mono.Rocks.RocksAction",
					GetTypeParameterReferences (total, start, count, false).ToArray ());
		}

		public static CodeTypeReference ThisAction (int total, int start)
		{
			return ThisAction (total, start, total - start);
		}

		public static CodeTypeReference ThisAction (int total, int start, int count)
		{
			return new CodeTypeReference (
					(total - count) <= MaxFuncArgs ? "this System.Action" : "this Mono.Rocks.RocksAction",
					GetTypeParameterReferences (total, start, count, false).ToArray ());
		}

		public static IEnumerable<CodeTypeReference> GetTypeParameterReferences (int args, bool ret)
		{
			return GetTypeParameterReferences (args, 0, ret);
		}

		public static IEnumerable<CodeTypeReference> GetTypeParameterReferences (int args, int start, bool ret)
		{
			return GetTypeParameterReferences (args, start, args - start, ret);
		}

		public static IEnumerable<CodeTypeReference> GetTypeParameterReferences (int args, int start, int end, bool ret)
		{
			return GetTypeParameters (args, start, end, ret)
				.Select (p => new CodeTypeReference (p));
		}

		public static IEnumerable<CodeTypeParameter> GetTypeParameters (int args, bool ret)
		{
			return GetTypeParameters (args, 0, ret);
		}

		public static IEnumerable<CodeTypeParameter> GetTypeParameters (int args, int start, bool ret)
		{
			return GetTypeParameters (args, start, args - start, ret);
		}

		public static IEnumerable<CodeTypeParameter> GetTypeParameters (int args, int start, int end, bool ret)
		{
			return Enumerable.Range (start, end)
				.Select (
					v => new CodeTypeParameter (GetTypeParameter (v, args)))
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

		public static string GetTypeParameter (int index, int total)
		{
			return total == 1 ? "T" : "T" + (index+1);
		}
	}
}
