using System.Collections.Generic;

namespace ShapeFX.Compilation.Interfaces
{
    using System;
    using System.Reflection;

    public interface ICompiler
    {
        Type Compile(string code, IEnumerable<Assembly> references, bool emitDebugInfo);
        Type Compile(string code);
        void Evaluate (string code, string usings, IEnumerable<Assembly> references);
        void Evaluate(string code);
    }
}
