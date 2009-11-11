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

		public static CodeTypeReference Action (int args)
		{
			return new CodeTypeReference (
					args <= MaxFuncArgs ? "System.Action" : "Mono.Rocks.RocksAction",
					GetTypeParameterReferences (args, false).ToArray ());
		}

		public static IEnumerable<CodeTypeReference> GetTypeParameterReferences (int args, bool ret)
		{
			return GetTypeParameters (args, ret)
				.Select (p => new CodeTypeReference (p));
		}

		public static IEnumerable<CodeTypeParameter> GetTypeParameters (int args, bool ret)
		{
			return Enumerable.Range (0, args)
				.Select (
					v => new CodeTypeParameter (GetTypeParameter (v, args)))
				.Concat (ret ? new[]{new CodeTypeParameter ("TResult")} : new CodeTypeParameter [0]);
		}

		public static CodeTypeReference Func (int args)
		{
			return new CodeTypeReference (
					args <= MaxFuncArgs ? "System.Func" : "Mono.Rocks.RocksFunc",
					GetTypeParameterReferences (args, true).ToArray ());
		}

		public static string GetTypeParameter (int index, int total)
		{
			return total == 1 ? "T" : "T" + (index+1);
		}
	}
}
