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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mono.Rocks.Tools {

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
			var dr = new CodeTypeDeclaration ("DelegateRocks") {
				IsPartial      = true,
				TypeAttributes = TypeAttributes.Public,
			};
			for (int i = 1; i <= n; ++i) {
				dr.Members.AddRange (CreateCurryActions (i));
				dr.Members.AddRange (CreateCurryTupleActions (i));
				dr.Members.AddRange (CreateTraditionalCurryActions (i));
				dr.Members.AddRange (CreateCurryFuncs (i));
				dr.Members.AddRange (CreateCurryTupleFuncs (i));
				dr.Members.AddRange (CreateTraditionalCurryFuncs (i));
				dr.Members.AddRange (CreateCompose (i));
			}
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
				m.TypeParameters.Add (Types.GetTypeParameter (i, n));
			if (tret)
				m.TypeParameters.Add ("TResult");
			m.Parameters.Add (new CodeParameterDeclarationExpression (selfType, "self"));
			for (int i = 0; i < a; ++i) {
				m.Parameters.Add (new CodeParameterDeclarationExpression (Types.GetTypeParameter (i, n), Value (n, i)));
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
				m.TypeParameters.Add (Types.GetTypeParameter (i, n));
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
				retType = new CodeTypeReference ("System.Func", new CodeTypeReference (Types.GetTypeParameter (i, n)), retType);
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Static,
				Name = "Curry",
				ReturnType = retType,
			};
			for (int i = 0; i < n; ++i)
				m.TypeParameters.Add (Types.GetTypeParameter (i, n));
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
				m.TypeParameters.Add (Types.GetTypeParameter (i, n+1));
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
	}
}
