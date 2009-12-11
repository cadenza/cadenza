//
// Tuples.cs
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
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cadenza.Tools {

	class TuplesGenerator {
		public static int Main (string[] args)
		{
			if (args.Any(a => new[]{"-h", "--help", "/help", "/?"}.Contains (a)))
				return new Tuples ().Run (args);
			foreach (var p in new FileGenerator[]{new Tuples (), new TuplesCoda ()}) {
				var r = p.Run (args);
				if (r != 0)
					return r;
			}
			return 0;
		}
	}

	class Tuples : FileGenerator
	{
		protected override string Header {
			get {return "Tuples.cs: Tuple types.";}
		}

		protected override IEnumerable<string> GetUsings ()
		{
			yield return "System";
			yield return "System.Collections";
			yield return "System.Collections.Generic";
			yield return "System.Reflection";
			yield return "System.Text";
		}

		protected override IEnumerable<CodeTypeDeclaration> GetRocksNamespaceTypes ()
		{
			yield return CreateTupleType (TypeParameterCount);
		}

		CodeTypeDeclaration CreateTupleType (int n)
		{
			var tuple = new CodeTypeDeclaration ("Tuple") {
				TypeAttributes = TypeAttributes.Public,
				IsPartial      = true,
			};
			var maxValues = new CodeMemberProperty () {
				Attributes      = MemberAttributes.Public | MemberAttributes.Static,
				Name            = "MaxValues",
				Type            = new CodeTypeReference (typeof (int)),
				HasGet          = true,
				HasSet          = false,
			};
			maxValues.GetStatements.Add (
					new CodeMethodReturnStatement (new CodePrimitiveExpression (n)));
			tuple.Members.Add (maxValues);
			for (int i = 0; i < n; ++i) {
				tuple.Members.Add (CreateCreateMethod (i+1));
			}
			return tuple;
		}

		CodeMemberMethod CreateCreateMethod (int n)
		{
			var retType = new CodeTypeReference ("Cadenza.Tuple", Types.GetTypeParameterReferences (n, false).ToArray ());
			var m = new CodeMemberMethod () {
				Attributes  = MemberAttributes.Public | MemberAttributes.Static,
				Name        = "Create",
				ReturnType  = retType,
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n, i));
			for (int i = 0; i < n; ++i)
				m.Parameters.Add (
						new CodeParameterDeclarationExpression (
							new CodeTypeReference (Types.GetTypeParameter (n, i)), Item (n, i)));
			m.Statements.Add (
					new CodeMethodReturnStatement (
						new CodeObjectCreateExpression ("Cadenza.Tuple",
							Enumerable.Range (0, n).Select (p => new CodeVariableReferenceExpression (Item (n, p))).ToArray ())));
			return m;
		}

		static string Item (int n, int i)
		{
			if (n == 0 || n == 1)
				return "item";
			return "item" + (i+1);
		}
	}

	class TuplesCoda : FileGenerator
	{
		protected override TextWriter GetOutputFile (string outputFile)
		{
			if (outputFile != null)
				outputFile = outputFile.Replace (".cs", "Coda.cs");
			return base.GetOutputFile (outputFile);
		}

		protected override string Header {
			get {return "TuplesCoda.cs: Tuple extension methods.";}
		}

		protected override IEnumerable<string> GetUsings ()
		{
			yield return "System";
			yield return "System.Collections";
			yield return "System.Collections.Generic";
			yield return "System.Reflection";
			yield return "System.Text";
		}

		protected override IEnumerable<CodeTypeDeclaration> GetRocksNamespaceTypes ()
		{
			yield break;
		}
	}
}
