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
using System.Reflection;
using System.Text;

namespace Cadenza.Tools {

	class Eithers : FileGenerator
	{
		public static int Main (string[] args)
		{
			return new Eithers ().Run (args);
		}

		protected override string Header {
			get {return "Eithers.cs: Either Types.";}
		}

		protected override IEnumerable<string> GetUsings ()
		{
			yield return "System";
			yield return "System.Collections.Generic";
		}

		protected override IEnumerable<CodeTypeDeclaration> GetRocksNamespaceTypes ()
		{
			for (int i = 2; i <= TypeParameterCount; ++i)
				yield return CreateEither (i);
		}

		CodeTypeDeclaration CreateEither (int n)
		{
			var either = new CodeTypeDeclaration ("Either") {
				TypeAttributes = TypeAttributes.Public | TypeAttributes.Abstract,
			};
			either.BaseTypes.Add (new CodeTypeReference ("IEquatable", GetEitherType (n)));
			either.TypeParameters.AddRange (Types.GetTypeParameters (n, false));
			either.Members.Add (new CodeConstructor () {
					Attributes = MemberAttributes.Private,
			});
			for (int i = 0; i < n; ++i)
				either.Members.Add (CreateCreator (either, i, n));
			either.Members.Add (CreateFold (either, n));
			either.Members.Add (CreateCheckFolders (n));
			either.Members.Add (CreateEqualsObject ());
			either.Members.Add (CreateEquals (n));
			either.Members.Add (CreateGetHashCode ());
			for (int i = 0; i < n; ++i)
				either.Members.Add (CreateHandler (i, n));
			either.Comments.AddDocs (
					XmlDocs.TypeParams (either.TypeParameters),
					XmlDocs.Summary ("A union of " + n + " values."),
					"<remarks>",
					"  <para>",
					"   An <c>Either</c> is an immutable, strongly typed union of variously ",
					"   typed values with each value lacking an otherwise meaningful name aside ",
					"   from its position, which is not exposed.  It stores only one (non-null) ",
					"   value from a set of types (as determined by the type parameter list).",
					"  </para>",
					"  <para>",
					"   The value held by a " + XmlDocs.See (DefaultNamespace, either) + " instance",
					"   can be converted into a value by using the ",
					"   <see cref=\"" + XmlDocs.Cref (DefaultNamespace, either, either.GetMethods ("Fold").First ()) + "\" /> method.",
					"   <c>Fold</c> takes a list of delegates to perform the conversion; the",
					"   delegate used to perform the conversion is based upon the internal ",
					"   position of the value stored.",
					"  </para>",
					"  <para>",
					"   <c>Either</c> instances are created through one of the following",
					"   creation methods:",
					"  </para>",
					"  <list type=\"bullet\">",
					GetCreators (either, n),
					"  </list>",
					"  <code lang=\"C#\">",
					"  var a = Either&lt;double, string&gt;.A (Math.PI);   // value stored in 1st position",
					"  ",
					"  int r = a.Fold (",
					"          v => (int) v,                                 // 1st position converter",
					"          v => v.Length);                               // 2nd position converter",
					"  ",
					"  Console.WriteLine (r);                        // prints 3</code>",
					"</remarks>"
			);
			for (int i = 0; i < n; ++i)
				AddCreatorDocs (either, either.GetMethods (A (i)).First (), i, n);
			AddFoldDocs (either, either.GetMethods ("Fold").First (), n);
			AddEqualsObjectDocs (either,
					either.GetMethods ("Equals").First (m => m.Parameters [0].Type.BaseType == "System.Object"),  n);
			AddEqualsDocs (either,
					either.GetMethods ("Equals").First (m => m.Parameters [0].Type.BaseType != "System.Object"),  n);
			AddGetHashCodeDocs (either, either.GetMethods ("GetHashCode").First (), n);
			return either;
		}

		static CodeTypeReference GetEitherType (int n)
		{
			return new CodeTypeReference ("Either", Types.GetTypeParameterReferences (n, false).ToArray ());
		}

		IEnumerable<string> GetCreators (CodeTypeDeclaration type, int n)
		{
			for (int i = 0; i < n; ++i) {
				yield return "    <item><term><see cref=\"" +
					XmlDocs.Cref (DefaultNamespace, type, type.GetMethods (A (i)).First ()) +
					"\" /></term></item>";
			}
		}

		CodeMemberMethod CreateCreator (CodeTypeDeclaration type, int w, int n)
		{
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Static,
				Name       = ((char) ('A' + w)).ToString (),
				ReturnType = GetEitherType (n),
			};
			m.Parameters.Add (new CodeParameterDeclarationExpression (Types.GetTypeParameter (n, w), "value"));
			m.Statements.ThrowWhenArgumentIsNull ("value");
			m.Statements.Add (
					new CodeMethodReturnStatement (
						new CodeObjectCreateExpression (A (w) + "Handler",
							new CodeVariableReferenceExpression ("value"))));
			return m;
		}

		void AddCreatorDocs (CodeTypeDeclaration type, CodeMemberMethod method, int w, int n)
		{
			var tp    = Types.GetTypeParameter (n, w);
			var idx   = XmlDocs.GetIndex (tp);
			var fold  = type.GetMethods ("Fold").First ();
			method.Comments.AddDocs (
					XmlDocs.Param ("value",
					"  A <typeparamref name=\"" + tp + "\" /> containing the value",
					"  to provide to the " + idx,
					"  " + XmlDocs.See (DefaultNamespace, type, fold),
					"  delegate."
					),
					XmlDocs.Summary (
					"  Creates a " + XmlDocs.See (DefaultNamespace, type) + " instance which",
					"  holds a <typeparamref name=\"" + tp + "\" /> value."
					),
					XmlDocs.Returns (
					"  A " + XmlDocs.See (DefaultNamespace, type) + " instance which holds a ",
					"  holds a <typeparamref name=\"" + tp + "\" /> value."
					),
					XmlDocs.Remarks (
					"  <para>",
					"   When",
					"   " + XmlDocs.See (DefaultNamespace, type, fold),
					"   is invoked,",
					"   the returned " + XmlDocs.See (DefaultNamespace, type) + " instance",
					"   will invoke the " + idx + " delegate",
					"   for conversions.",
					"  </para>"
					),
					XmlDocs.ArgumentNullException ("value")
			);
		}

		CodeMemberMethod CreateFold (CodeTypeDeclaration type, int n)
		{
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Abstract | MemberAttributes.Public,
				Name       = "Fold",
				ReturnType = new CodeTypeReference ("TResult"),
			};
			m.TypeParameters.Add (new CodeTypeParameter ("TResult"));
			for (int i = 0; i < n; ++i) {
				m.Parameters.Add (
						new CodeParameterDeclarationExpression (GetFoldParameterType (i, n), a (i)));
			}
			return m;
		}

		void AddFoldDocs (CodeTypeDeclaration type, CodeMemberMethod method, int n)
		{
			method.Comments.AddDocs (
					XmlDocs.TypeParam ("TResult",
					"  The type to convert the " + XmlDocs.See (DefaultNamespace, type) + " to."
					),
					GetFoldParametersDocs (type, n),
					XmlDocs.Summary (
					"  Converts a " + XmlDocs.See (DefaultNamespace, type) + " into a <typeparamref name=\"TResult\" /> value."
					),
					XmlDocs.Returns (
					"  A <typeparamref name=\"TResult\" /> as generated by one",
					"  of the conversion delegate parameters."
					),
					XmlDocs.Remarks (
					"  <para>",
					"   Converts a " + XmlDocs.See (DefaultNamespace, type) + " into a <typeparamref name=\"TResult\" />",
					"   by invoking one of the provided delegate parameters.",
					"  </para>",
					"  <para>",
					"   The parameter which is invoked is predicated upon the internal position of",
					"   the value held.  For example, if the internal value is in the first position ",
					"   (i.e. " + XmlDocs.See (DefaultNamespace, type, type.GetMethods (A (0)).First ()),
					"   was used to create the " + XmlDocs.See (DefaultNamespace, type) + " instance), then",
					"   <paramref name=\"a\" /> (the first delegate parameter) will be invoked to",
					"   convert the <typeparamref name=\"T1\" /> into a ",
					"   <typeparamref name=\"TResult\" />.",
					"  </para>"
					),
					XmlDocs.ArgumentNullException (Enumerable.Range (0, n).Select (v => a (v)))
			);
		}

		IEnumerable<string> GetFoldParametersDocs (CodeTypeDeclaration type, int n)
		{
			for (int i = 0; i < n; ++i) {
				var tp = Types.GetTypeParameter (n, i);
				yield return "<param name=\"" + a (i) + "\">";
				yield return "  A <see cref=\"T:System.Func{" + tp + ",TResult}\" /> ";
				yield return "  used if the " + XmlDocs.See (DefaultNamespace, type) + " stores a ";
				yield return "  <typeparamref name=\"" + tp + "\" /> value into a ";
				yield return "  <typeparamref name=\"TResult\" /> value.";
				yield return "</param>";
			}
		}

		static string A (int n)
		{
			return ((char) ('A' + n)).ToString ();
		}

		static string a (int n)
		{
			return ((char) ('a' + n)).ToString ();
		}

		static CodeTypeReference GetFoldParameterType (int i, int n)
		{
			return new CodeTypeReference ("System.Func",
					new CodeTypeReference (Types.GetTypeParameter (n, i)),
					new CodeTypeReference (new CodeTypeParameter ("TResult")));
		}

		static CodeMemberMethod CreateCheckFolders (int n)
		{
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Private | MemberAttributes.Static,
				Name       = "CheckFolders",
			};
			m.TypeParameters.Add (new CodeTypeParameter ("TResult"));
			for (int i = 0; i < n; ++i)
				m.Parameters.Add (
						new CodeParameterDeclarationExpression (GetFoldParameterType (i, n), a (i)));
			for (int i = 0; i < n; ++i)
				m.Statements.ThrowWhenArgumentIsNull (a (i));
			return m;
		}

		static CodeMemberMethod CreateEqualsObject ()
		{
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Abstract,
				Name       = "Equals",
				ReturnType = new CodeTypeReference ("override System.Boolean"),
			};
			m.Parameters.Add (new CodeParameterDeclarationExpression (typeof (object), "obj"));
			return m;
		}

		void AddEqualsObjectDocs (CodeTypeDeclaration type, CodeMemberMethod method, int n)
		{
			method.Comments.AddDocs (
					XmlDocs.Param ("obj",
							"  A <see cref=\"T:System.Object\"/> to compare this instance against."
					),
					XmlDocs.Summary (
							"  Determines whether the current instance and the specified object have the same value."
					),
					XmlDocs.Returns (
					"  <para>",
					"   <see langword=\"true\"/> if <paramref name=\"obj\"/> is a ",
					"   " + XmlDocs.See (DefaultNamespace, type) + " and each member of <paramref name=\"obj\"/>",
					"   and the current instance have the same value (according to",
					"   <see cref=\"M:System.Object.Equals(System.Object)\"/>); otherwise",
					"   <see langword=\"false\"/> is returned.",
					"  </para>"
					),
					XmlDocs.Remarks (
					"  <para>",
					"   This method checks for value equality ",
					"   (<see cref=\"M:System.Object.Equals(System.Object)\"/>), as defined by each",
					"   value type.",
					"  </para>",
					"  <para>",
					"   <block subset=\"none\" type=\"note\">",
					"    This method overrides <see cref=\"M:System.Object.Equals(System.Object)\"/>.",
					"   </block>",
					"  </para>"
					)
			);
		}

		static CodeMemberMethod CreateEquals (int n)
		{
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Abstract,
				Name       = "Equals",
				ReturnType = new CodeTypeReference (typeof (bool)),
			};
			m.Parameters.Add (new CodeParameterDeclarationExpression (GetEitherType (n), "obj"));
			return m;
		}

		void AddEqualsDocs (CodeTypeDeclaration type, CodeMemberMethod method, int n)
		{
			var et = XmlDocs.See (DefaultNamespace, type);
			method.Comments.AddDocs (
					XmlDocs.Param ("obj",
							"A " + et + "to compare this instance against."
					),
					XmlDocs.Summary (
							"  Determines whether the current instance and the specified " + et + " have the same value."
					),
					XmlDocs.Returns (
							"  <para>",
							"   <see langword=\"true\"/> if each member of <paramref name=\"obj\"/>",
							"   and the current instance have the same value (according to",
							"   <see cref=\"M:System.Object.Equals(System.Object)\"/>); otherwise",
							"   <see langword=\"false\"/> is returned.",
							"  </para>"
					),
					XmlDocs.Remarks (
							"  <para>",
							"   This method checks for value equality",
							"   (<see cref=\"M:System.Object.Equals(System.Object)\"/>), as defined by each",
							"   value type.",
							"  </para>"
					)
			);
		}

		static CodeMemberMethod CreateGetHashCode ()
		{
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Abstract,
				Name       = "GetHashCode",
				ReturnType = new CodeTypeReference ("override System.Int32"),
			};
			return m;
		}

		void AddGetHashCodeDocs (CodeTypeDeclaration type, CodeMemberMethod method, int n)
		{
			method.Comments.AddDocs (
					XmlDocs.Summary ("  Generates a hash code for the current instance."),
					XmlDocs.Returns ("  A <see cref=\"T:System.Int32\"/> containing the hash code for this instance."),
					XmlDocs.Remarks (
							"  <para>",
							"   <block subset=\"none\" type=\"note\">",
							"    This method overrides <see cref=\"M:System.Object.GetHashCode\"/>.",
							"   </block>",
							"  </para>"
					)
			);
		}

		static CodeTypeDeclaration CreateHandler (int w, int n)
		{
			var h = new CodeTypeDeclaration (A (w) + "Handler") {
				TypeAttributes = TypeAttributes.NestedPrivate,
			};
			h.BaseTypes.Add (GetEitherType (n));
			h.Members.Add (new CodeMemberField (Types.GetTypeParameter (n, w), "_value") {
					Attributes = MemberAttributes.Final | MemberAttributes.Private,
			});
			var c = new CodeConstructor () {
				Attributes = MemberAttributes.Public,
			};
			c.Parameters.Add (new CodeParameterDeclarationExpression (Types.GetTypeParameter (n, w), "value"));
			c.Statements.Add (
					new CodeAssignStatement (
						new CodeVariableReferenceExpression ("_value"),
						new CodeVariableReferenceExpression ("value")));
			h.Members.Add (c);

			// public override int GetHashCode ();
			var m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Override,
				Name       = "GetHashCode",
				ReturnType = new CodeTypeReference (typeof(int)),
			};
			m.Statements.Add (
					new CodeMethodReturnStatement (
						new CodeMethodInvokeExpression (
							new CodeVariableReferenceExpression ("_value"),
							"GetHashCode")));
			h.Members.Add (m);

			// public override bool Equals (object obj);
			m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Override,
				Name       = "Equals",
				ReturnType = new CodeTypeReference (typeof (bool)),
			};
			m.Parameters.Add (new CodeParameterDeclarationExpression (new CodeTypeReference (typeof (object)), "obj"));
			m.Statements.Add (new CodeSnippetStatement (string.Format ("                {0} o = obj as {0};", A (w) + "Handler")));
			m.Statements.Add (new CodeConditionStatement (
					new CodeBinaryOperatorExpression (
						new CodeVariableReferenceExpression ("o"),
						CodeBinaryOperatorType.ValueEquality,
						new CodePrimitiveExpression (null)),
					new CodeMethodReturnStatement (new CodePrimitiveExpression (false))));
			m.Statements.Add (new CodeMethodReturnStatement (
					new CodeMethodInvokeExpression (
						new CodeMethodReferenceExpression (
							new CodeThisReferenceExpression (),
							"Equals"),
						new CodeVariableReferenceExpression ("o"))));
			h.Members.Add (m);

			// public override bool Equals (Either<...> obj);
			m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Override,
				Name       = "Equals",
				ReturnType = new CodeTypeReference (typeof (bool)),
			};
			m.Parameters.Add (new CodeParameterDeclarationExpression (GetEitherType (n), "obj"));
			m.Statements.Add (new CodeSnippetStatement (string.Format ("                {0} o = obj as {0};", A (w) + "Handler")));
			m.Statements.Add (new CodeConditionStatement (
					new CodeBinaryOperatorExpression (
						new CodeVariableReferenceExpression ("o"),
						CodeBinaryOperatorType.ValueEquality,
						new CodePrimitiveExpression (null)),
					new CodeMethodReturnStatement (new CodePrimitiveExpression (false))));
			m.Statements.Add (
					new CodeMethodReturnStatement (
						new CodeMethodInvokeExpression (
							new CodePropertyReferenceExpression (
								new CodeTypeReferenceExpression (
									new CodeTypeReference ("System.Collections.Generic.EqualityComparer",
										new CodeTypeReference (new CodeTypeParameter (Types.GetTypeParameter (n, w))))),
								"Default"),
							"Equals",
							new CodeVariableReferenceExpression ("this._value"), new CodeVariableReferenceExpression ("o._value"))));
			h.Members.Add (m);

			// public override TResult Fold<TResult>(...);
			m = new CodeMemberMethod () {
				Attributes = MemberAttributes.Public | MemberAttributes.Override,
				Name       = "Fold",
				ReturnType = new CodeTypeReference (new CodeTypeParameter ("TResult")),
			};
			m.TypeParameters.Add (new CodeTypeParameter ("TResult"));
			for (int i = 0; i < n; ++i) {
				m.Parameters.Add (
						new CodeParameterDeclarationExpression (GetFoldParameterType (i, n), a (i)));
			}
			m.Statements.Add (
					new CodeMethodInvokeExpression (
						new CodeMethodReferenceExpression (null, "CheckFolders"),
						Enumerable.Range (0, n).Select (i => new CodeVariableReferenceExpression (a (i))).ToArray ()));
			m.Statements.Add (
					new CodeMethodReturnStatement (
						new CodeDelegateInvokeExpression (
							new CodeVariableReferenceExpression (a (w)),
							new CodeVariableReferenceExpression ("_value"))));
			h.Members.Add (m);

			return h;
		}
	}
}
