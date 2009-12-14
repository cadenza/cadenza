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

	public static class XmlDocs {

		public static string Cref (CodeTypeReference type)
		{
			return new CrefBuilder ("T").AppendType (type).ToString ();
		}

		class CrefBuilder {
			public StringBuilder Cref = new StringBuilder ();

			public CrefBuilder (string crefType)
			{
				Cref.Append (crefType).Append (":");
			}

			public CrefBuilder AppendType (CodeTypeReference type)
			{
				AppendTypeName (type);
				if (type.TypeArguments.Count > 0) {
					Cref.Append ("{");
					AppendTypeParameter (type.TypeArguments [0]);
					for (int i = 1; i < type.TypeArguments.Count; ++i) {
						Cref.Append (",");
						AppendTypeParameter (type.TypeArguments [i]);
					}
					Cref.Append ("}");
				}
				return this;
			}

			protected virtual void AppendTypeName (CodeTypeReference type)
			{
				Cref.Append (XmlDocs.GetType (type));
			}

			protected virtual void AppendTypeParameter (CodeTypeReference type)
			{
				AppendType (type);
			}

			public override string ToString ()
			{
				return Cref.ToString ();
			}
		}

		public static string Cref (string ns, CodeTypeDeclaration type)
		{
			return Cref (new CrefBuilder ("T"), ns, type).ToString ();
		}

		static CrefBuilder Cref (CrefBuilder b, string ns, CodeTypeDeclaration type)
		{
			b.Cref.Append (ns).Append (".").Append (type.Name);
			if (type.TypeParameters.Count > 0) {
				b.Cref.Append ("{").Append (type.TypeParameters [0].Name);
				for (int i = 1; i < type.TypeParameters.Count; ++i)
					b.Cref.Append (",").Append (type.TypeParameters [i].Name);
				b.Cref.Append ("}");
			}
			return b;
		}

		public static string Cref (string ns, CodeTypeDeclaration declType, CodeMemberMethod method)
		{
			var b = new CrefMemberBuilder ("M", declType, method);
			Cref (b, ns, declType);
			b.Cref.Append (".").Append (method.Name);
			if (method.TypeParameters.Count > 0) {
				b.Cref.Append ("``").Append (method.TypeParameters.Count);
			}
			if (method.Parameters.Count > 0) {
				b.Cref.Append ("(");
				b.AppendType (method.Parameters [0].Type);
				for (int i = 1; i < method.Parameters.Count; ++i) {
					b.Cref.Append (",");
					b.AppendType (method.Parameters [i].Type);
				}
				b.Cref.Append (")");
			}
			return b.ToString ();
		}

		public static string Cref (string ns, CodeTypeDeclaration declType, CodeMemberProperty property)
		{
			var b = new CrefMemberBuilder ("P", declType, null);
			Cref (b, ns, declType);
			b.Cref.Append (".").Append (property.Name);
			return b.ToString ();
		}

		class CrefMemberBuilder : CrefBuilder {
			CodeTypeDeclaration declType;
			CodeMemberMethod method;

			public CrefMemberBuilder (string crefType, CodeTypeDeclaration declType, CodeMemberMethod method)
				: base (crefType)
			{
				this.declType = declType;
				this.method   = method;
			}

			protected override void AppendTypeName (CodeTypeReference type)
			{
				if (!AppendParameterIndex (type))
					base.AppendTypeName (type);
			}

			bool AppendParameterIndex (CodeTypeReference p)
			{
				var dt = declType.TypeParameters.Cast<CodeTypeParameter>().FirstOrDefault (e => e.Name == p.BaseType);
				if (dt != null) {
					Cref.Append ("`").Append (declType.TypeParameters.IndexOf (dt));
					return true;
				}
				if (method == null)
					return false;
				dt = method.TypeParameters.Cast<CodeTypeParameter>().FirstOrDefault (e => e.Name == p.BaseType);
				if (dt != null) {
					Cref.Append ("``").Append (method.TypeParameters.IndexOf (dt));
					return true;
				}
				return false;
			}

			protected override void AppendTypeParameter (CodeTypeReference type)
			{
				if (!AppendParameterIndex (type))
					base.AppendTypeParameter (type);
			}
		}

		static string GetType (CodeTypeReference type)
		{
			var b = type.BaseType.Replace ("this ", "");
			if (b.Contains ('`'))
				b = b.Substring (0, b.IndexOf ('`'));
			return b;
		}

		public static IEnumerable<string> Exception (Type ex, params object[] block)
		{
			return Block ("exception", "cref=\"T:" + ex.FullName + "\"", block);
		}

		static IEnumerable<string> Block (string element, string attrs, params object[] block)
		{
			yield return "<" + element + (!string.IsNullOrEmpty (attrs) ? " " + attrs : "") + ">";
			foreach (var o in Sequence.Expand (block))
				yield return "  " + o.ToString ();
			yield return "</" + element + ">";
		}

		public static IEnumerable<string> ArgumentNullException (string parameter)
		{
			return Exception (typeof (ArgumentNullException),
					"if <paramref name=\"" + parameter + "\"/> is <see langword=\"null\" />.");
		}

		public static IEnumerable<string> ArgumentNullException (IEnumerable<string> parameters)
		{
			yield return "<exception cref=\"T:" + typeof (ArgumentNullException).FullName + "\">";
			bool first = true;
			foreach (var p in parameters) {
				if (!first) {
					yield return "  <para>-or-</para>";
				}
				yield return "  <para>";
				yield return "    <paramref name=\"" + p + "\" /> is <see langword=\"null\" />.";
				yield return "  </para>";
				first = false;
			}
			yield return "</exception>";
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

		public static string See (string ns, CodeTypeDeclaration type)
		{
			return "<see cref=\"" + Cref (ns, type) + "\" />";
		}

		public static string See (string ns, CodeTypeDeclaration type, CodeMemberMethod method)
		{
			return "<see cref=\"" + Cref (ns, type, method) + "\" />";
		}

		public static string See (string ns, CodeTypeDeclaration type, CodeMemberProperty property)
		{
			return "<see cref=\"" + Cref (ns, type, property) + "\" />";
		}

		public static IEnumerable<string> Summary (params object[] block)
		{
			return Block ("summary", null, block);
		}

		public static IEnumerable<string> TypeParam (string name, params object[] block)
		{
			return Block ("typeparam", "name=\"" + name + "\"", block);
		}

		public static IEnumerable<string> TypeParams (CodeTypeParameterCollection typeParams)
		{
			return typeParams.Cast<CodeTypeParameter> ()
				.Select (p => Block ("typeparam", "name=\"" + p.Name + "\"",
					string.Format ("The {0} value type.", GetIndex (p.Name))))
				.SelectMany (x => x);
		}

		public static string GetIndex (string name)
		{
			if (name.Length == 1)
				name = "T1";
			name = name.Substring (1);
			switch (name) {
				case "1": return "first";
				case "2": return "second";
				case "3": return "third";
				case "4": return "fourth";
			}
			return "*unknown*";
		}

		public static string GetIndex (int index)
		{
			switch (index+1) {
				case 1: return "first";
				case 2: return "second";
				case 3: return "third";
				case 4: return "fourth";
			}
			return "*unknown*";
		}

		public static IEnumerable<string> TypeParams (CodeTypeParameterCollection typeParams, CodeTypeReference type)
		{
			return typeParams.Cast<CodeTypeParameter> ()
				.Select (p => Block ("typeparam", "name=\"" + p.Name + "\"",
					"A " + See (type) + (p.Name == "TResult" ? " return type." : " parameter type.")))
				.SelectMany (x => x);
		}

		public static IEnumerable<string> Value (params object[] block)
		{
			return Block ("value", null, block);
		}
	}
}
