using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdh.Toolkit.Json
{
    public interface IJsonSerializable
    {
        object JsonSerialize();
    }
}
