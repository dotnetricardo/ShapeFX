using System.Collections.Generic;

namespace ShapeFX.Compilation.Engines
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using System.Reflection;

    using ShapeFX.Compilation.Interfaces;

    using CodeParser = ShapeFX.Compilation.Runtime.CodeParser;

    public class CodeDomEngine : IEngine
    {
        public Type CreateType(string code, IEnumerable<Assembly> references, bool debug)
        {
            code = CodeParser.RandomNamespaceGenerate(code);

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var compilerParams = new CompilerParameters
                                 {
                                     GenerateInMemory = true,
                                     IncludeDebugInformation = debug,
                                     GenerateExecutable = false,
                                     TempFiles = new TempFileCollection(Environment.GetEnvironmentVariable("TEMP"), true)
                                 };

            if (references == null)
            {
                references = AppDomain.CurrentDomain.GetAssemblies();
            }

            references.ToList().ForEach(x => compilerParams.ReferencedAssemblies.Add(x.Location));


            var compiler = provider.CreateCompiler();

            var results = compiler.CompileAssemblyFromSource(compilerParams, code);

            return results.CompiledAssembly.GetTypes().FirstOrDefault();
        }



        public void Eval(string code, string usings, IEnumerable<Assembly> references)
        {
            throw new NotImplementedException();
        }
    }
}
