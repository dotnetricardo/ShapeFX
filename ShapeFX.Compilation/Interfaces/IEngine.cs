// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IShapeCompiler.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IShapeCompiler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace ShapeFX.Compilation.Interfaces
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    using ShapeFX.Compilation.Contracts;

    [ContractClass(typeof(EngineContract))]
    public interface IEngine
    {
        Type CreateType(string code, IEnumerable<Assembly> references, bool emitDebugInfo);

        void Eval(string code, string usings, IEnumerable<Assembly> references);

    }
}
