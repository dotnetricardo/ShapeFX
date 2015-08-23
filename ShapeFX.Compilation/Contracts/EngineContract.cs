// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EngineContract.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the RoslynCompilerContract type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace ShapeFX.Compilation.Contracts
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    using ShapeFX.Compilation.Interfaces;

    /// <summary>
    /// The Engine contract.
    /// </summary>
    [ContractClassFor(typeof(IEngine))]
    internal abstract class EngineContract : IEngine
    {
        public Type CreateType(string code, IEnumerable<Assembly> references, bool emitDebugInfo)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(code), "code");
            throw new NotImplementedException();
        }


        public void Eval(string code, string usings, IEnumerable<Assembly> references)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(code), "code");
        }
    }
}
