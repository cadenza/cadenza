// 
// Extensions.cs
//  
// Author:
//       Chris Howie <cdhowie@gmail.com>
// 
// Copyright (c) 2010 Chris Howie
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.ComponentModel;

namespace Cdh.Toolkit.Extensions.ComponentModel
{
    public static class Extensions
    {
        public static void AutoInvoke(this ISynchronizeInvoke obj, Action method)
        {
            if (obj.InvokeRequired)
                obj.Invoke(method, null);
            else
                method();
        }

        public static object AutoInvoke(this ISynchronizeInvoke obj, Delegate method, params object[] args)
        {
            if (obj.InvokeRequired)
                return obj.Invoke(method, args);
            else
                return method.Method.Invoke(method.Target, args);
        }

        public static AsyncCallback Invoked(this AsyncCallback callback, ISynchronizeInvoke obj)
        {
            return result => obj.AutoInvoke(callback, result);
        }
    }
}
