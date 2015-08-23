namespace ShapeFX.Compilation.Engines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    using global::Mono.CSharp;

    using ShapeFX.Compilation.Interfaces;
    using ShapeFX.Compilation.Runtime;

    public class MonoEngine : IEngine
    {
        private readonly ReportPrinter _printer;

        public MonoEngine() : this(new MonoReporter())
        { }
        public MonoEngine(ReportPrinter printer)
        {
            _printer = printer;
        }

        public Type CreateType(string code, IEnumerable<Assembly> references, bool emitDebugInfo)
        {
            code = CodeParser.RandomNamespaceGenerate(code);

            var evaluator = new Evaluator(
                new CompilerContext(
                    new CompilerSettings
                    {
                        GenerateDebugInfo = emitDebugInfo,
                        Optimize = emitDebugInfo,
                        LoadDefaultReferences = !Host.IsWebApp()
                    },
                    _printer));

            if (references == null)
            {
                references = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(
                        x => x.GetName()
                            .Name != "mscorlib");
            }


            references.ToList().ForEach(evaluator.ReferenceAssembly);

            evaluator.Compile(code);

            var assemblies = this.GetDynamicAssemblies();
            Type result = null;

            if (assemblies.Any())
            {
                result = assemblies.LastOrDefault().GetTypes().FirstOrDefault();
            }
            else
            {
                var evaluate = string.Format(
                    "typeof({0}.{1});",
                    Regex.Match(code, Constants.Regex.Namespace).Value.Trim(),
                    Regex.Match(code, Constants.Regex.ClassName).Value.Trim());

                result = (Type)evaluator.Evaluate(evaluate);
            }

            return result;
        }

        private List<Assembly> GetDynamicAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
              .Where(x => x.IsDynamic && x.GetName().Name.ToLower().Contains("eval")).ToList();
        }

        public void Eval(string code, string usings, IEnumerable<Assembly> references)
        {
            //code = CodeParser.RandomNamespaceGenerate(code);

            var sb = new StringBuilder();
            const string SystemUsing = "using System;";
            sb.Append(SystemUsing);
            sb.Append(usings);

            var evaluator = new Evaluator(
                new CompilerContext(
                    new CompilerSettings
                    {
                        GenerateDebugInfo = true,
                        Optimize = true,
                        LoadDefaultReferences = !Host.IsWebApp()
                    },
                    new MonoReporter()));

            if (references == null)
            {
                references = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(
                        x => x.GetName()
                            .Name != "mscorlib");
            }


            references.ToList().ForEach(evaluator.ReferenceAssembly);
            evaluator.Compile(sb.ToString());
            evaluator.Run(code);
        }
    }
}
