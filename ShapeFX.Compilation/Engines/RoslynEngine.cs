using System.Configuration;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Mono.CSharp;

namespace ShapeFX.Compilation.Engines
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Interfaces;
    using Runtime;

    public class RoslynEngine : IEngine
    {
        public Type CreateType(string code, IEnumerable<Assembly> references, bool emitDebugInfo)
        {
            code = CodeParser.RandomNamespaceGenerate(code);
            var refs = new List<MetadataReference>();
            var assemblyName = string.Concat("dyn_", Guid.NewGuid());

            if (references == null)
            {
                references = AppDomain.CurrentDomain.GetAssemblies();
            }

            references.ToList().ForEach(x => refs.Add(MetadataReference.CreateFromFile(x.Location)));
            refs.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));

            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: emitDebugInfo ? OptimizationLevel.Debug : OptimizationLevel.Release);

            var tree = SyntaxFactory.ParseSyntaxTree(code);
            var compilation =
                CSharpCompilation.Create(assemblyName, options: options, references: refs)
                    .AddSyntaxTrees(tree);

            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);

                result.Diagnostics.ToList().ForEach(
                    (x) =>
                    {
                        if (!result.Success)
                        {
                            throw new Exception(x.GetMessage());
                        }
                    });

                assemblyName = $"{assemblyName}.{"dll"}";

                File.WriteAllBytes(Path.Combine(Path.GetTempPath(), assemblyName), stream.GetBuffer());

                var assembly = Assembly.LoadFile(Path.Combine(Path.GetTempPath(), assemblyName));

                return assembly.GetTypes().FirstOrDefault();
            }
        }

        public void Eval(string code, string usings, IEnumerable<Assembly> references)
        {
            throw new NotImplementedException();
        }
    }
}
