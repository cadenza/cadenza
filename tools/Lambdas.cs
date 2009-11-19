//
// Lambdas.cs
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
using System.Linq;
using System.Text;

namespace Mono.Rocks.Tools {

	class Lambdas : FileGenerator
	{
		public static int Main (string[] args)
		{
			return new Lambdas ().Run (args);
		}

		protected override string Header {
			get {return "Lambdas.cs: C# Lambda Expression Helpers.";}
		}

		protected override IEnumerable<string> GetUsings ()
		{
			yield return "System";
			yield return "System.Linq.Expressions";
		}

		protected override IEnumerable<CodeTypeDeclaration> GetRocksNamespaceTypes ()
		{
			yield return CreateLambda (TypeParameterCount);
		}

		static CodeTypeDeclaration CreateLambda (int n)
		{
			var lambda = new CodeTypeDeclaration ("Lambda");
			lambda.IsPartial = true;
			lambda.Members.Add (A (0));
			lambda.Members.Add (F (0));
			for (int i = 1; i <= n; ++i) {
				lambda.Members.Add (A (i));
				lambda.Members.Add (F (i));
				lambda.Members.Add (XA (i));
				lambda.Members.Add (XF (i));
				lambda.Members.Add (RecFunc (i));
			}
			lambda.Comments.AddDocs (
					"<summary>",
					"  Provides static utility methods to generate anonymous delegates ",
					"  or expression trees of pre-determined types.",
					"</summary>",
					"<remarks>",
					"  <para>",
					"   C# lambda methods and anonymous delegates are a curious ",
					"   1.5-class citizen: They are implicitly convertable to any",
					"   delegate type, but have no type by themselves.  Thus,",
					"   the following code fails to compile:",
					"  </para>",
					"  <code lang=\"C#\">",
					"  ((int x) => Console.WriteLine (x))(5);",
					"  </code>",
					"  <para>It would instead need:</para>",
					"  <code lang=\"C#\">",
					"  // either:",
					"  Action&lt;int&gt; a = x => Console.WriteLine (x);",
					"  a (5);",
					"  //",
					"  // or",
					"  //",
					"  ((Action&lt;int&gt;) (x => Console.WriteLine (x)))(5);",
					"  </code>",
					"  <para>",
					"   So you'd either need to assign the lambda to an actual",
					"   delegate type, or insert a cast.",
					"  </para>",
					"  <para>",
					"   <see cref=\"M:Mono.Rocks.Lambda.A\" /> allows you to",
					"   provide a lambda body for the <see cref=\"T:System.Action\"/> ",
					"   builtin delegate type, and <see cref=\"M:Mono.Rocks.Lambda.F\" />",
					"   allows you to provide a lambda body for the ",
					"   <see cref=\"T:System.Func{TResult}\"/> delegate type, ",
					"   thus removing the need for a cast or an extra variable:",
					"  </para>",
					"  <code lang=\"C#\">",
					"  Lambda.F ((int x) => Console.WriteLine (x)) (5);</code>",
					"  <para>",
					"   <see cref=\"T:Mono.Rocks.Lambda\"/> provides the following sets of",
					"   functionality:",
					"  </para>",
					"  <list type=\"bullet\">",
					"   <item><term>Delegate creation methods, which return ",
					"    <see cref=\"T:System.Action\"/>-like delegates:",
					"    <see cref=\"M:Mono.Rocks.Lambda.A(System.Action)\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.A``1(System.Action{``0})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.A``2(System.Action{``0,``1})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.A``3(System.Action{``0,``1,``2})\"/>, and",
					"    <see cref=\"M:Mono.Rocks.Lambda.A``4(System.Action{``0,``1,``2,``3})\"/>.",
					"   </term></item>",
					"   <item><term>Delegate creation methods which return ",
					"    return <see cref=\"T:System.Func{TResult}\"/>-like delegates",
					"    <see cref=\"M:Mono.Rocks.Lambda.F``1(System.Func{``0})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.F``2(System.Func{``0,``1})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.F``3(System.Func{``0,``1,``2})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.F``4(System.Func{``0,``1,``2,``3})\"/>, and",
					"    <see cref=\"M:Mono.Rocks.Lambda.F``5(System.Func{``0,``1,``2,``3,``4})\"/>.",
					"   </term></item>",
					"   <item><term><see cref=\"T:System.Linq.Expressions.Expression\"/>-creating methods:",
					"    <see cref=\"M:Mono.Rocks.Lambda.XA(System.Linq.Expressions.Expression{System.Action})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XA``1(System.Linq.Expressions.Expression{System.Action{``0}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XA``2(System.Linq.Expressions.Expression{System.Action{``0,``1}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XA``3(System.Linq.Expressions.Expression{System.Action{``0,``1,``2}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XA``4(System.Linq.Expressions.Expression{System.Action{``0,``1,``2,``3}})\"/>, and",
					"    <see cref=\"M:Mono.Rocks.Lambda.XF``1(System.Linq.Expressions.Expression{System.Func{``0}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XF``2(System.Linq.Expressions.Expression{System.Func{``0,``1}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XF``3(System.Linq.Expressions.Expression{System.Func{``0,``1,``2}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XF``4(System.Linq.Expressions.Expression{System.Func{``0,``1,``2,``3}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.XF``5(System.Linq.Expressions.Expression{System.Func{``0,``1,``2,``3,``4}})\"/>.",
					"   </term></item>",
					"   <item><term>Y-Combinators, which permit writing recursive lambdas:",
					"    <see cref=\"M:Mono.Rocks.Lambda.RecFunc``2(System.Func{System.Func{``0,``1},System.Func{``0,``1}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.RecFunc``3(System.Func{System.Func{``0,``1,``2},System.Func{``0,``1,``2}})\"/>,",
					"    <see cref=\"M:Mono.Rocks.Lambda.RecFunc``4(System.Func{System.Func{``0,``1,``2,``3},System.Func{``0,``1,``2,``3}})\"/>, and",
					"    <see cref=\"M:Mono.Rocks.Lambda.RecFunc``5(System.Func{System.Func{``0,``1,``2,``3,``4},System.Func{``0,``1,``2,``3,``4}})\"/>.",
					"   </term></item>",
					"  </list>",
					"</remarks>"
			);
			return lambda;
		}

		static CodeMemberMethod CreateMethod (string name, CodeTypeReference type, string arg, int argc, bool tret)
		{
			var m = new CodeMemberMethod () {
				Attributes  = MemberAttributes.Static | MemberAttributes.Public,
				Name        = name,
				ReturnType  = type,
			};
			m.TypeParameters.AddRange (Types.GetTypeParameters (argc, tret));
			m.Parameters.Add (new CodeParameterDeclarationExpression (type, arg));
			m.Statements.Add (new CodeMethodReturnStatement (new CodeVariableReferenceExpression (arg)));
			return m;
		}

		static CodeMemberMethod A (int args)
		{
			var t = Types.Action (args);
			var m = CreateMethod ("A", t, "lambda", args, false);
			m.Comments.AddDocs (
					XmlDocs.TypeParams (m.TypeParameters, t),
					XmlDocs.Param ("lambda", "The " + XmlDocs.See (t) + " to return."),
					XmlDocs.Summary ("Creates a " + XmlDocs.See (t) + " delegate."),
					XmlDocs.Returns ("Returns <paramref name=\"lambda\" />.")
			);
			return m;
		}

		static CodeMemberMethod XA (int args)
		{
			var t = new CodeTypeReference ("System.Linq.Expressions.Expression", Types.Action (args));
			var m = CreateMethod ("XA", t, "expr", args, false);
			m.Comments.AddDocs (
					XmlDocs.TypeParams (m.TypeParameters, t),
					XmlDocs.Param ("expr", "The " + XmlDocs.See (t) + " to return."),
					XmlDocs.Summary ("Creates a " + XmlDocs.See (t) + " expression tree."),
					XmlDocs.Returns ("Returns <paramref name=\"expr\" />.")
			);
			return m;
		}

		static CodeMemberMethod F (int args)
		{
			var t = Types.Func (args);
			var m = CreateMethod ("F", t, "lambda", args, true);
			m.Comments.AddDocs (
					XmlDocs.TypeParams (m.TypeParameters, t),
					XmlDocs.Param ("lambda", "The " + XmlDocs.See (t) + " to return."),
					XmlDocs.Summary ("Creates a " + XmlDocs.See (t) + " delegate."),
					XmlDocs.Returns ("Returns <paramref name=\"lambda\" />.")
			);
			return m;
		}

		static CodeMemberMethod XF (int args)
		{
			var t = new CodeTypeReference ("System.Linq.Expressions.Expression", Types.Func (args));
			var m = CreateMethod ("XF", t, "expr", args, true);
			m.Comments.AddDocs (
					XmlDocs.TypeParams (m.TypeParameters, t),
					XmlDocs.Param ("expr", "The " + XmlDocs.See (t) + " to return."),
					XmlDocs.Summary ("Creates a " + XmlDocs.See (t) + " expression tree."),
					XmlDocs.Returns ("Returns <paramref name=\"expr\" />.")
			);
			return m;
		}

		static CodeMemberMethod RecFunc (int args)
		{
			var t = Types.Func (args);
			var m = new CodeMemberMethod () {
				Attributes  = MemberAttributes.Static | MemberAttributes.Public,
				Name        = "RecFunc",
				ReturnType  = t,
			};
			m.TypeParameters.AddRange (Types.GetTypeParameters (args, true).ToArray ());
			var a = "lambda";
			m.Parameters.Add (new CodeParameterDeclarationExpression (new CodeTypeReference ("Func", t, t), a));
			m.Statements.ThrowWhenArgumentIsNull (a);
			var expr = AppendArgs (new StringBuilder (), args);
			expr.Append (" => lambda (RecFunc (lambda))");
			AppendArgs (expr, args);
			m.Statements.Add (new CodeMethodReturnStatement (new CodeSnippetExpression (expr.ToString ())));
			m.Comments.AddRange ("From: http://blogs.msdn.com/madst/archive/2007/05/11/recursive-lambda-expressions.aspx");
			m.Comments.AddDocs (
					XmlDocs.TypeParams (m.TypeParameters, t),
					XmlDocs.Param ("lambda", "The " + XmlDocs.See (t) + " to use."),
					XmlDocs.Summary ("Creates a " + XmlDocs.See (t) + " delegate, which may be recursive."),
					XmlDocs.Returns ("Returns a " + XmlDocs.See (t) + " which (eventually) invokes <paramref name=\"lambda\"/>."),
					XmlDocs.ArgumentNullException ("lambda"),
					XmlDocs.Remarks (
						"<para>",
						"  The following example makes use of a recursive lambda:",
						"</para>",
						"<code lang=\"C#\">",
						"  Func<int, int> factorial = Lambda.RecFunc<int, int> (".Replace ("<", "&lt;"),
						"      fac => x => x == 0 ? 1 : x * fac (x-1));",
						"  Console.WriteLine (factorial (5));  // prints \"120\"",
						"</code>"
					)
			);
			return m;
		}

		static StringBuilder AppendArgs (StringBuilder expr, int args)
		{
			expr.Append ("(value1");
			for (int i = 1; i < args; ++i)
				expr.Append (", value" + (i+1));
			return expr.Append (")");
		}
	}
}
