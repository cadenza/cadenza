//
// CodeStatementCollectionCoda.cs
//
// Author:
//   Jonathan Pryor (jpryor@novell.com)
//
// Copyright (c) 2009-2010 Novell, Inc. (http://www.novell.com)
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Cadenza;

namespace Cadenza.Tools
{
	public static class CodeStatementCollectionCoda
	{
		public static void AddCheck (this CodeStatementCollection self, string method, string argument)
		{
			self.Add (
					new CodeExpressionStatement (
						new CodeMethodInvokeExpression (
							new CodeTypeReferenceExpression ("Check"), method, new CodeVariableReferenceExpression (argument))));
		}

		public static void ThrowWhenArgumentIsNull (this CodeStatementCollection self, string argument)
		{
			ThrowWhenArgumentMatchesExpression (self, argument,
					new CodeBinaryOperatorExpression (
						new CodeVariableReferenceExpression (argument),
						CodeBinaryOperatorType.ValueEquality,
						new CodePrimitiveExpression (null)));
		}

		public static void ThrowWhenArgumentIsLessThanZero (this CodeStatementCollection self, string argument)
		{
			self.Add (
					new CodeConditionStatement (
						new CodeBinaryOperatorExpression (
							new CodeVariableReferenceExpression (argument),
							CodeBinaryOperatorType.LessThan,
							new CodePrimitiveExpression (0)),
						new CodeThrowExceptionStatement (
							new CodeObjectCreateExpression (
								new CodeTypeReference ("System.ArgumentException"),
								new CodePrimitiveExpression ("Negative values are not supported."),
								new CodePrimitiveExpression (argument)))));
		}

		public static void ThrowWhenArgumentMatchesExpression (this CodeStatementCollection self, string argument, CodeBinaryOperatorExpression expression)
		{
			self.Add (
					new CodeConditionStatement (
						expression,
						new CodeThrowExceptionStatement (
							new CodeObjectCreateExpression (
								new CodeTypeReference ("System.ArgumentNullException"),
								new CodePrimitiveExpression (argument)))));
		}
	}
}

