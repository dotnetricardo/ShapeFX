using System;
using System.Collections.Generic;
using System.Reflection;
using ShapeFX.Compilation.Configuration;
using ShapeFX.Compilation.Interfaces;
using ShapeFX.Compilation.Runtime;

namespace ShapeFX.Compilation
{

    /// <summary>
    /// The Compiler class
    /// </summary>
    public class Compiler<T> : ICompiler where T : IEngine, new()
    {
        private readonly T _engine;

        public Compiler()
        {
            this._engine = new T();
        }

        public Type Compile(string code, IEnumerable<Assembly> references, bool emitDebugInfo)
        {
            return this._engine.CreateType(code, references, emitDebugInfo);
        }

        public Type Compile(string code)
        {
            return this.Compile(code, null, true);
        }


        public void Evaluate(string code, string usings, IEnumerable<Assembly> references)
        {
            this._engine.Eval(code, usings, references);
        }

        public void Evaluate(string code)
        {
            this._engine.Eval(code, null, null);
        }
    }
}
