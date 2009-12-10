//
// Delegates.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cadenza.Tools {

	class Delegates : FileGenerator
	{
		public static int Main (string[] args)
		{
			return new Delegates ().Run (args);
		}

		protected override string Header {
			get {return "Delegates.cs: Extension methods for various delegate types.";}
		}

		protected override IEnumerable<string> GetUsings ()
		{
			yield return "System";
			yield return "System.Collections.Generic";
			yield return "System.Diagnostics";
			yield return "System.Linq.Expressions";
		}

		protected override IEnumerable<CodeTypeDeclaration> GetRocksNamespaceTypes ()
		{
			yield return CreateDelegateRocks (TypeParameterCount);
		}

		CodeTypeDeclaration CreateDelegateRocks (int n)
		{
			var dr = new CodeTypeDeclaration ("DelegateCoda") {
				IsPartial      = true,
				TypeAttributes = TypeAttributes.Public,
			};
			for (int i = 0; i <= n; ++i) {
				dr.Members.AddRange (CreateCurryActions (i));
				dr.Members.AddRange (CreateCurryTupleActions (i));
				dr.Members.AddRange (CreateTraditionalCurryActions (i));
				dr.Members.AddRange (CreateCurryFuncs (i));
				dr.Members.AddRange (CreateCurryTupleFuncs (i));
				dr.Members.AddRange (CreateTraditionalCurryFuncs (i));
				dr.Members.AddRange (CreateCompose (i));
				dr.Members.AddRange (CreateTimings (i));
			}
			dr.Comments.AddDocs (
					XmlDocs.Summary (
						"Provides extension methods on <see cref=\"T:System.Action{T}\"/>,",
						"<see cref=\"T:System.Func{T,TResult}\"/>, and related delegates."),
					XmlDocs.Remarks (
						"<para>",
						" " + XmlDocs.See (DefaultNamespace, dr) + "provides methods methods for:",
						"</para>",
						"<list type=\"bullet\">",
						" <item><term>",
						"  Delegate currying and partial application (<see cref=\"M:Cadenza.DelegateCoda.Curry\" />)",
						" </term></item>",
						" <item><term>",
						"  Delegate composition (<see cref=\"M:Cadenza.DelegateCoda.Compose\" />)",
						" </term></item>",
						" <item><term>",
						"  Timing generation (<see cref=\"M:Cadenza.DelegateCoda.Timings\" />)",
						" </term></item>",
						"</list>",
						"<para>",
						" Currying via partial application is a way to easily transform ",
						" functions which accept N arguments into functions which accept ",
						" N-1 arguments, by \"fixing\" arguments with a value.",
						"</para>",
						"<code lang=\"C#\">",
						"// partial application:",
						"Func&lt;int,int,int,int&gt; function = (int a, int b, int c) => a + b + c;",
						"Func&lt;int,int,int&gt;     f_3      = function.Curry (3);",
						"Func&lt;int&gt;             f_321    = function.Curry (3, 2, 1);",
						"Console.WriteLine (f_3 (2, 1));  // prints (3 + 2 + 1) == \"6\"",
						"Console.WriteLine (f_321 ());    // prints (3 + 2 + 1) == \"6\"</code>",
						"<para>",
						" \"Traditional\" currying converts a delegate that accepts N arguments",
						" into a delegate which accepts only one argument, but when invoked may ",
						" return a further delegate (etc.) until the final value is returned.",
						"</para>",
						"<code lang=\"C#\">",
						"// traditional currying:",
						"Func&lt;int, Func&lt;int, Func&lt;int, int&gt;&gt;&gt; curry = function.Curry ();",
						"Func&lt;int, Func&lt;int, int&gt;&gt;            fc_1  = curry (1);",
						"Func&lt;int, int&gt;                       fc_12 = fc_1 (2);",
						"Console.WriteLine (fc_12 (3));        // prints (3 + 2 + 1) == \"6\"",
						"Console.WriteLine (curry (3)(2)(1));  // prints (3 + 2 + 1) == \"6\"</code>",
						"<para>",
						" Composition is a way to easy chain (or pipe) together multiple delegates",
						" so that the return value of a \"composer\" delegate is used as the input ",
						" parameter for the chained delegate:",
						"</para>",
						"<code lang=\"C#\">",
						"Func&lt;int,string> tostring = Lambda.F ((int n) => n.ToString ());",
						"Func&lt;int, int>    doubler = Lambda.F ((int n) => n * 2);",
						"Func&lt;int, string>",
						"     double_then_tostring = tostring.Compose (doubler);",
						"Console.WriteLine (double_then_tostring (5));",
						"    // Prints \"10\";</code>",
						"<para>",
						" All possible argument and return delegate permutations are provided",
						" for the <see cref=\"T:System.Action{T}\"/>, ",
						" <see cref=\"T:System.Func{T,TResult}\"/>, and related types.",
						"</para>")
			);
			return dr;
		}

		static CodeMemberMethod CreateCurryMethod (Func<int, int, CodeTypeReference> getSelfType, Func<int, int, CodeTypeReference> getRetType, int n, int a, bool tret)
		{
			var selfType = getSelfType (n, 0);
			var retType = getRetType (n, a);
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Static,
				Name = "Curry",
				ReturnType = retType,
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n, i));
			if (tret)
				m.TypeParameters.Add ("TResult");
			m.Parameters.Add (new CodeParameterDeclarationExpression (selfType, "self"));
			for (int i = 0; i < a; ++i) {
				m.Parameters.Add (new CodeParameterDeclarationExpression (Types.GetTypeParameter (n, i), Value (n, i)));
			}
			m.Statements.AddCheck ("Self", "self");
			var expr = new StringBuilder ().Append ("(");
			Values (expr, n, a, n);
			expr.Append (") => self (");
			Values (expr, n, 0, n);
			expr.Append (")");
			m.Statements.Add (new CodeMethodReturnStatement (new CodeSnippetExpression (expr.ToString ())));
			return m;
		}

		static string Value (int n, int i)
		{
			if (n == 0 || n == 1)
				return "value";
			return "value" + (i+1);
		}

		static void Values (StringBuilder buf, int n, int start, int end)
		{
			bool comma = false;
			for (int i = start; i < end; ++i) {
				if (comma)
					buf.Append (", ");
				buf.Append (Value (n, i));
				comma = true;
			}
		}

		static IEnumerable<string> Values (int n, int start, int end)
		{
			for (int i = start; i < end; ++i)
				yield return Value (n, i);
		}

		IEnumerable<CodeTypeMember> CreateCurryActions (int n)
		{
			for (int i = 1; i <= n; ++i)
				yield return CreateCurryMethod (Types.ThisAction, Types.Action, n, i, false);
		}

		IEnumerable<CodeTypeMember> CreateCurryFuncs (int n)
		{
			for (int i = 1; i <= n; ++i)
				yield return CreateCurryMethod (Types.ThisFunc, Types.Func, n, i, true);
		}

		static CodeMemberMethod CreateCurryTupleMethod (Func<int, int, CodeTypeReference> getSelfType, Func<int, int, CodeTypeReference> getRetType, int n, int a, bool tret)
		{
			var selfType = getSelfType (n, 0);
			var retType  = getRetType (n, a);
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Static,
				Name = "Curry",
				ReturnType = retType,
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n, i));
			if (tret)
				m.TypeParameters.Add ("TResult");
			m.Parameters.Add (new CodeParameterDeclarationExpression (selfType, "self"));
			m.Parameters.Add (new CodeParameterDeclarationExpression (
					new CodeTypeReference ("Cadenza.Tuple", Types.GetTypeParameterReferences (n, 0, a, false).ToArray ()),
					"values"));
			m.Statements.AddCheck ("Self", "self");
			var expr = new StringBuilder ().Append ("(");
			Values (expr, n, a, n);
			expr.Append (") => self (values._1");
			for (int i = 1; i < a; ++i)
				expr.Append (", ").Append ("values._" + (i+1));
			if (a < n) {
				expr.Append (", ");
				Values (expr, n, a, n);
			}
			expr.Append (")");
			m.Statements.Add (new CodeMethodReturnStatement (new CodeSnippetExpression (expr.ToString ())));
			return m;
		}

		IEnumerable<CodeTypeMember> CreateCurryTupleActions (int n)
		{
			for (int i = 1; i <= n; ++i)
				yield return CreateCurryTupleMethod (Types.ThisAction, Types.Action, n, i, false);
		}

		IEnumerable<CodeTypeMember> CreateCurryTupleFuncs (int n)
		{
			for (int i = 1; i <= n; ++i)
				yield return CreateCurryTupleMethod (Types.ThisFunc, Types.Func, n, i, true);
		}

		static CodeMemberMethod CreateTraditionalCurryMethod (Func<int, int, CodeTypeReference> getSelfType, Func<int, int, CodeTypeReference> getRetType, int n, bool tret)
		{
			var selfType = getSelfType (n, 0);
			var retType  = getRetType (n, n - 1);
			for (int i = n - 2; i >= 0; --i)
				retType = new CodeTypeReference ("System.Func", new CodeTypeReference (Types.GetTypeParameter (n, i)), retType);
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Static,
				Name = "Curry",
				ReturnType = retType,
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n, i));
			if (tret)
				m.TypeParameters.Add ("TResult");
			m.Parameters.Add (new CodeParameterDeclarationExpression (selfType, "self"));
			m.Statements.AddCheck ("Self", "self");
			var expr = new StringBuilder ();
			for (int i = 0; i < n; ++i)
				expr.Append (Value (n, i)).Append (" => ");
			expr.Append ("self (");
			Values (expr, n, 0, n);
			expr.Append (")");
			m.Statements.Add (new CodeMethodReturnStatement (new CodeSnippetExpression (expr.ToString ())));
			return m;
		}

		IEnumerable<CodeTypeMember> CreateTraditionalCurryActions (int n)
		{
			if (n <= 1)
				yield break;
			yield return CreateTraditionalCurryMethod (Types.ThisAction, Types.Action, n, false);
		}

		IEnumerable<CodeTypeMember> CreateTraditionalCurryFuncs (int n)
		{
			if (n <= 1)
				yield break;
			yield return CreateTraditionalCurryMethod (Types.ThisFunc, Types.Func, n, true);
		}

		static CodeMemberMethod CreateComposeMethod (Func<int, int, int, CodeTypeReference> getSelfType, Func<int, int, int, CodeTypeReference> getRetType, int n, bool tret)
		{
			var selfType = getSelfType (n + 1, n, 1);
			var retType  = getRetType (n + 1, 0, n);
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Static,
				Name       = "Compose",
				ReturnType = retType,
			};
			for (int i = 0; i < n + 1; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n+1, i));
			if (tret)
				m.TypeParameters.Add ("TResult");
			m.Parameters.Add (new CodeParameterDeclarationExpression (selfType, "self"));
			var composerType = new CodeTypeReference (
					Types.FuncType (n),
					Types.GetTypeParameterReferences (n + 1, 0, n + 1, false).ToArray ());
			m.Parameters.Add (new CodeParameterDeclarationExpression (composerType, "composer"));
			m.Statements.AddCheck ("Self", "self");
			m.Statements.AddCheck ("Composer", "composer");
			var expr = new StringBuilder ().Append ("(");
			Values (expr, n, 0, n);
			expr.Append (") => self (composer (");
			Values (expr, n, 0, n);
			expr.Append ("))");
			m.Statements.Add (new CodeMethodReturnStatement (new CodeSnippetExpression (expr.ToString ())));
			return m;
		}

		IEnumerable<CodeTypeMember> CreateCompose (int n)
		{
			yield return CreateComposeMethod (Types.ThisAction, Types.Action, n, false);
			yield return CreateComposeMethod (Types.ThisFunc, Types.Func, n, true);
		}

		static CodeMemberMethod CreateTimingsHeader (int n, string name, MemberAttributes protection)
		{
			var m = new CodeMemberMethod () {
				Attributes = protection | MemberAttributes.Static,
				Name       = name,
				ReturnType = new CodeTypeReference (typeof (IEnumerable<TimeSpan>)),
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (n, i));
			m.Parameters.Add (new CodeParameterDeclarationExpression (
						protection == MemberAttributes.Public ? Types.ThisAction (n, 0) : Types.Action (n, 0),
						"self"));
			for (int i = 0; i < n; ++i)
				m.Parameters.Add (new CodeParameterDeclarationExpression (Types.GetTypeParameter (n, i), Value (n, i)));
			m.Parameters.Add (new CodeParameterDeclarationExpression (typeof (int), "runs"));

			return m;
		}

		static CodeMemberMethod CreateTimingsRunsMethod (int n)
		{
			var m = CreateTimingsHeader (n, "Timings", MemberAttributes.Public);

			var e = new CodeMethodInvokeExpression (
					new CodeMethodReferenceExpression (null, "Timings"),
					new CodeVariableReferenceExpression ("self"));
			for (int i = 0; i < n; ++i)
				e.Parameters.Add (new CodeVariableReferenceExpression (Value (n, i)));
			e.Parameters.Add (new CodeVariableReferenceExpression ("runs"));
			e.Parameters.Add (new CodePrimitiveExpression (1));
			m.Statements.Add (new CodeMethodReturnStatement (e));

			return m;
		}

		static CodeMemberMethod CreateTimingsRunsLoopsMethod (int n)
		{
			var m = CreateTimingsHeader (n, "Timings", MemberAttributes.Public);

			m.Parameters.Add (new CodeParameterDeclarationExpression (typeof (int), "loopsPerRun"));

			m.Statements.AddCheck ("Self", "self");
			m.Statements.ThrowWhenArgumentIsLessThanZero ("runs");
			m.Statements.ThrowWhenArgumentIsLessThanZero ("loopsPerRun");
			var e = new CodeMethodInvokeExpression (
					new CodeMethodReferenceExpression (null, "CreateTimingsIterator"),
					new CodeVariableReferenceExpression ("self"));
			for (int i = 0; i < n; ++i)
				e.Parameters.Add (new CodeVariableReferenceExpression (Value (n, i)));
			e.Parameters.Add (new CodeVariableReferenceExpression ("runs"));
			e.Parameters.Add (new CodeVariableReferenceExpression ("loopsPerRun"));
			m.Statements.Add (new CodeMethodReturnStatement (e));

			return m;
		}

		static CodeMemberMethod CreateTimingsIteratorMethod (int n)
		{
			var m = CreateTimingsHeader (n, "CreateTimingsIterator", MemberAttributes.Private);

			m.Parameters.Add (new CodeParameterDeclarationExpression (typeof (int), "loopsPerRun"));

			m.Statements.Add (new CodeCommentStatement ("Ensure that required methods are already JITed"));
			m.Statements.Add (
					new CodeVariableDeclarationStatement (typeof (Stopwatch), "watch",
					new CodeMethodInvokeExpression (
						new CodeTypeReferenceExpression (typeof (Stopwatch)),
							"StartNew")));
			m.Statements.Add (
					new CodeDelegateInvokeExpression (
						new CodeVariableReferenceExpression ("self"),
						Values (n, 0, n).Select (v => new CodeVariableReferenceExpression (v)).ToArray ()));
			m.Statements.Add (
					new CodeMethodInvokeExpression (
						new CodeVariableReferenceExpression ("watch"),
						"Stop"));
			m.Statements.Add (
					new CodeMethodInvokeExpression (
						new CodeVariableReferenceExpression ("watch"),
						"Reset"));

			m.Statements.Add (
					new CodeIterationStatement (
						new CodeVariableDeclarationStatement (typeof (int), "i", new CodePrimitiveExpression (0)),
						new CodeBinaryOperatorExpression (
							new CodeVariableReferenceExpression ("i"),
							CodeBinaryOperatorType.LessThan,
							new CodeVariableReferenceExpression ("runs")),
						new CodeAssignStatement (
							new CodeVariableReferenceExpression ("i"),
							new CodeBinaryOperatorExpression (
								new CodeVariableReferenceExpression ("i"),
								CodeBinaryOperatorType.Add,
								new CodePrimitiveExpression (1))),
						new CodeExpressionStatement (new CodeMethodInvokeExpression (new CodeVariableReferenceExpression ("watch"), "Start")),
						new CodeIterationStatement (
							new CodeVariableDeclarationStatement (typeof (int), "j", new CodePrimitiveExpression (0)),
							new CodeBinaryOperatorExpression (
								new CodeVariableReferenceExpression ("j"),
								CodeBinaryOperatorType.LessThan,
								new CodeVariableReferenceExpression ("loopsPerRun")),
							new CodeAssignStatement (
								new CodeVariableReferenceExpression ("j"),
								new CodeBinaryOperatorExpression (
									new CodeVariableReferenceExpression ("j"),
									CodeBinaryOperatorType.Add,
									new CodePrimitiveExpression (1))),
							new CodeExpressionStatement (
								new CodeDelegateInvokeExpression (
									new CodeVariableReferenceExpression ("self"),
									Values (n, 0, n).Select (v => new CodeVariableReferenceExpression (v)).ToArray ()))),
						new CodeExpressionStatement (new CodeMethodInvokeExpression (new CodeVariableReferenceExpression ("watch"), "Stop")),
						new CodeSnippetStatement ("                yield return watch.Elapsed;"),
						new CodeExpressionStatement (new CodeMethodInvokeExpression (new CodeVariableReferenceExpression ("watch"), "Reset"))));

			return m;
		}

		IEnumerable<CodeTypeMember> CreateTimings (int n)
		{
			yield return CreateTimingsRunsMethod (n);
			yield return CreateTimingsRunsLoopsMethod (n);
			yield return CreateTimingsIteratorMethod (n);
		}
	}
}
