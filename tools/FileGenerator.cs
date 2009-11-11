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

	public abstract class FileGenerator
	{
		protected FileGenerator ()
		{
		}

		protected int TypeParameterCount {get; set;}

		public virtual int Run (string[] args)
		{
			try {
				var p = new OptionSet () {
					{ "n|count=",
					  "The number of method overloads to generate.",
					  (int v) => TypeParameterCount = v },
				};
				bool help = false;
				AddOptions (p);
				p.Add ("h|help|?",
						"Show this message and exit.",
						v => help = v != null);
				p.Parse (args);
				if (help) {
					p.WriteOptionDescriptions (Console.Out);
					return 0;
				}
			}
			catch (Exception e) {
				Console.Error.WriteLine ("{0}: {1}", Program, e.Message);
				return 1;
			}

			var file = new CodeCompileUnit ();
			foreach (var ns in GetCodeNamespaces ())
				file.Namespaces.Add (ns);

			var o = new IndentedTextWriter (Console.Out, "\t");
			var provider = new CSharpCodeProvider ();
			provider.GenerateCodeFromCompileUnit (file, o, new CodeGeneratorOptions () {
					// BlankLinesBetweenMembers = true,
					BracingStyle = "C",
			});
			return 0;
		}

		protected virtual string Header {
			get {return "File.cs";}
		}

		protected virtual string Program {
			get {return Path.GetFileName (Environment.GetCommandLineArgs ()[0]);}
		}

		protected virtual string CommandLine {
			get {return string.Format ("{0} -n {1}", Program, TypeParameterCount);}
		}

		protected virtual IEnumerable<CodeNamespace> GetCodeNamespaces ()
		{
			var nsRocks = new CodeNamespace ("Mono.Rocks");
			nsRocks.Comments.AddRange (
					"",
					Header,
					"",
					"GENERATED CODE: DO NOT EDIT.",
					"",
					"To regenerate this code, execute: " + CommandLine,
					"",
					"Copyright (c) " + DateTime.Now.Year + " Novell, Inc. (http://www.novell.com)",
					"",
					"Permission is hereby granted, free of charge, to any person obtaining",
					"a copy of this software and associated documentation files (the",
					"\"Software\"), to deal in the Software without restriction, including",
					"without limitation the rights to use, copy, modify, merge, publish,",
					"distribute, sublicense, and/or sell copies of the Software, and to",
					"permit persons to whom the Software is furnished to do so, subject to",
					"the following conditions:",
					"",
					"The above copyright notice and this permission notice shall be",
					"included in all copies or substantial portions of the Software.",
					"",
					"THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND,",
					"EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF",
					"MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND",
					"NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE",
					"LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION",
					"OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION",
					"WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.",
					"");
			foreach (var ns in GetUsings ())
				nsRocks.Imports.Add (new CodeNamespaceImport (ns));
			foreach (var t in GetRocksNamespaceTypes ())
				nsRocks.Types.Add (t);

			yield return nsRocks;
		}

		protected virtual IEnumerable<string> GetUsings ()
		{
			yield break;
		}

		protected virtual IEnumerable<CodeTypeDeclaration> GetRocksNamespaceTypes ()
		{
			yield break;
		}

		protected virtual void AddOptions (OptionSet options)
		{
		}
	}
}
