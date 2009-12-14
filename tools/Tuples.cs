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
			yield return CreateTupleCreatorType (TypeParameterCount);
			for (int i = 1; i <= TypeParameterCount; ++i)
				yield return CreateTupleType (i);
		}

		CodeTypeDeclaration CreateTupleCreatorType (int n)
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
							new CodeTypeReference (Types.GetTypeParameter (n, i)), Tuple.item (n, i)));
			m.Statements.Add (
					new CodeMethodReturnStatement (
						new CodeObjectCreateExpression ("Cadenza.Tuple",
							Enumerable.Range (0, n).Select (p => new CodeVariableReferenceExpression (Tuple.item (n, p))).ToArray ())));
			return m;
		}

		CodeTypeDeclaration CreateTupleType (int n)
		{
			var tuple = new CodeTypeDeclaration ("Tuple") {
				TypeAttributes = TypeAttributes.Public,
				IsPartial      = true,
			};
			for (int i = 0; i < n; ++i) {
				tuple.TypeParameters.Add (Types.GetTypeParameter (n, i));
				tuple.Members.Add (new CodeMemberField (Types.GetTypeParameter (n, i), Tuple.item (n, i)));
				var p = new CodeMemberProperty () {
					Attributes  = MemberAttributes.Public | MemberAttributes.Final,
					Name        = Tuple.Item (n, i),
					HasGet      = true,
					HasSet      = false,
					Type        = new CodeTypeReference (Types.GetTypeParameter (n, i)),
				};
				p.GetStatements.Add (
						new CodeMethodReturnStatement (new CodeFieldReferenceExpression (new CodeThisReferenceExpression (), Tuple.item (n, i))));
				tuple.Members.Add (p);
			}
			var c = new CodeConstructor () {
				Attributes  = MemberAttributes.Public,
			};
			for (int i = 0; i < n; ++i) {
				c.Parameters.Add (new CodeParameterDeclarationExpression (Types.GetTypeParameter (n, i), Tuple.item (n, i)));
				c.Statements.Add (new CodeAssignStatement (
							new CodeFieldReferenceExpression (new CodeThisReferenceExpression (), Tuple.item (n, i)),
							new CodeVariableReferenceExpression (Tuple.item (n, i))));
			}
			tuple.Members.Add (c);
			tuple.Members.Add (CreateTupleEqualsMethod (n));
			tuple.Members.Add (CreateTupleGetHashCodeMethod (n));
			return tuple;
		}

		CodeMemberMethod CreateTupleEqualsMethod (int n)
		{
			var m = new CodeMemberMethod () {
				Attributes  = MemberAttributes.Override | MemberAttributes.Public,
				Name        = "Equals",
				ReturnType  = new CodeTypeReference (typeof (bool)),
			};
			var t = "Tuple<" + Types.GetTypeParameterList (n) + ">";
			m.Parameters.Add (new CodeParameterDeclarationExpression (typeof (object), "obj"));
			m.Statements.Add (new CodeSnippetStatement (string.Format ("            {0} o = obj as {0};", t)));
			m.Statements.Add (new CodeConditionStatement (
					new CodeBinaryOperatorExpression (
						new CodeVariableReferenceExpression ("o"),
						CodeBinaryOperatorType.ValueEquality,
						new CodePrimitiveExpression (null)),
					new CodeMethodReturnStatement (new CodePrimitiveExpression (false))));
			Func<int, CodeMethodInvokeExpression> c = w =>
					new CodeMethodInvokeExpression (
						new CodePropertyReferenceExpression (
							new CodeTypeReferenceExpression (
								new CodeTypeReference ("System.Collections.Generic.EqualityComparer",
									new CodeTypeReference (new CodeTypeParameter (Types.GetTypeParameter (n, w))))),
							"Default"),
						"Equals",
						new CodeFieldReferenceExpression (
							new CodeThisReferenceExpression (), Tuple.item (n, w)),
						new CodeFieldReferenceExpression (
							new CodeVariableReferenceExpression ("o"), Tuple.item (n, w)));
			CodeExpression pred = c (0);
			for (int i = 1; i < n; ++i) {
				pred = new CodeBinaryOperatorExpression (
						pred,
						CodeBinaryOperatorType.BooleanAnd,
						c (i));
			}
			m.Statements.Add (
					new CodeMethodReturnStatement (pred));
			return m;
		}

		CodeMemberMethod CreateTupleGetHashCodeMethod (int n)
		{
			var m = new CodeMemberMethod () {
				Attributes  = MemberAttributes.Override | MemberAttributes.Public,
				Name        = "GetHashCode",
				ReturnType  = new CodeTypeReference (typeof (int)),
			};
			m.Statements.Add (new CodeVariableDeclarationStatement (typeof (int), "hc", new CodePrimitiveExpression (0)));
			for (int i = 0; i < n; ++i) {
				m.Statements.Add (new CodeSnippetStatement (
							string.Format ("            hc ^= {0}.GetHashCode();", Tuple.Item (n, i))));
			}
			m.Statements.Add (
					new CodeMethodReturnStatement (new CodeVariableReferenceExpression ("hc")));
			return m;
		}
	}

	static class Tuple {

		public static string item (int n, int i)
		{
			if (n == 0 || n == 1)
				return "item1";
			return "item" + (i+1);
		}

		public static string Item (int n, int i)
		{
			if (n == 0 || n == 1)
				return "Item1";
			return "Item" + (i+1);
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
		}

		protected override IEnumerable<CodeTypeDeclaration> GetRocksNamespaceTypes ()
		{
			yield return CreateTupleCodaType (TypeParameterCount);
		}

		CodeTypeDeclaration CreateTupleCodaType (int n)
		{
			var t = new CodeTypeDeclaration ("TupleCoda") {
				IsClass   = true,
				IsPartial = true,
			};
			for (int i = 1; i <= n; ++i) {
				t.Members.Add (CreateAggregateMethod (i));
				t.Members.Add (CreateMatchMethod (i));
			}
			return t;
		}

		CodeMemberMethod CreateAggregateMethod (int n)
		{
			var retType   = new CodeTypeReference ("TResult");
			var m = new CodeMemberMethod () {
				Attributes  = MemberAttributes.Public | MemberAttributes.Static,
				Name        = "Aggregate",
				ReturnType  = retType,
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n, i));
			m.TypeParameters.Add ("TResult");

			var selfType = new CodeTypeReference ("this Tuple", Types.GetTypeParameterReferences (n, false).ToArray ());
			m.Parameters.Add (new CodeParameterDeclarationExpression (selfType, "self"));

			var funcType = Types.Func (n);
			m.Parameters.Add (new CodeParameterDeclarationExpression (funcType, "func"));

			m.Statements.AddCheck ("Self", "self");
			m.Statements.AddCheck ("Func", "func");
			m.Statements.Add (
					new CodeMethodReturnStatement (
						new CodeDelegateInvokeExpression (
							new CodeVariableReferenceExpression ("func"),
							Enumerable.Range (0, n).Select (p =>
								new CodePropertyReferenceExpression (new CodeVariableReferenceExpression ("self"), Tuple.Item (n, p))).ToArray ())));

			return m;
		}

		CodeMemberMethod CreateMatchMethod (int n)
		{
			var retType   = new CodeTypeReference ("TResult");
			var m = new CodeMemberMethod () {
				Attributes  = MemberAttributes.Public | MemberAttributes.Static,
				Name        = "Match",
				ReturnType  = retType,
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n, i));
			m.TypeParameters.Add ("TResult");

			var selfType = new CodeTypeReference ("this Tuple", Types.GetTypeParameterReferences (n, false).ToArray ());
			m.Parameters.Add (new CodeParameterDeclarationExpression (selfType, "self"));

			var matchersType = new CodeTypeReference (
					new CodeTypeReference (Types.FuncType (n),
						Types.GetTypeParameterReferences (n, false)
						.Concat (new[]{new CodeTypeReference ("Cadenza.Maybe", new[]{new CodeTypeReference ("TResult")})})
						.ToArray ()),
					1);
			var matchersParam = new CodeParameterDeclarationExpression (matchersType, "matchers");
			matchersParam.CustomAttributes.Add (new CodeAttributeDeclaration ("System.ParamArrayAttribute"));
			m.Parameters.Add (matchersParam);

			m.Statements.AddCheck ("Self", "self");
			m.Statements.ThrowWhenArgumentIsNull ("matchers");

			var loop = new StringBuilder ();
			loop.Append ("            foreach (var m in matchers) {\n");
			loop.Append ("              var r = m (self.Item1");
			for (int i = 1; i < n; ++i)
				loop.Append (", self.").Append (Tuple.Item (n, i));
			loop.Append (");\n");
			loop.Append ("              if (r.HasValue)\n");
			loop.Append ("                return r.Value;\n");
			loop.Append ("            }");
			m.Statements.Add (new CodeSnippetStatement (loop.ToString ()));
			m.Statements.Add (
					new CodeThrowExceptionStatement (
						new CodeObjectCreateExpression (
							new CodeTypeReference ("System.InvalidOperationException"),
							new CodePrimitiveExpression ("no match"))));
			return m;
		}
	}
}
