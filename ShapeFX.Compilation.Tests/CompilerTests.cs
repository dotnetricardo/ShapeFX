using System;
using ShapeFX.Compilation;

namespace ShapeFX.CompilationHub.Tests
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    using ShapeFX.Compilation.Engines;
    using ShapeFX.CompilationHub;

    [TestFixture]
    public class CompilerTests
    {
        
        [Test]
        public void CompileShoudReturnTypeWhenEngineIsRoslynAndDependenciesArePassed()
        {
            // arrange 
            var code = File.ReadAllText("ToCompileClasses/MyTestClass.cs");
            var references = AppDomain.CurrentDomain.GetAssemblies().ToArray();

            // act
            var result = new Compiler<RoslynEngine>().Compile(code, references, false);

            // assert
            Assert.True(result.Name == "MyTestClass");
        }

        [Test]
        public void CompileShoudReturnTypeWhenEngineIsRoslynAndNoDependenciesArePassed()
        {
            // arrange 
            var code = File.ReadAllText("ToCompileClasses/MyTestClass.cs");

            // act
            var result = new Compiler<RoslynEngine>().Compile(code);

            // assert
            Assert.True(result.Name == "MyTestClass");
        }

        [Test]
        public void CompileShoudReturnTypeWhenEngineIsCodeDom()
        {
            // arrange 
            var code = File.ReadAllText("ToCompileClasses/MyTestClass.cs");
            var references = AppDomain.CurrentDomain.GetAssemblies().ToArray();

            // act
            var result = new Compiler<CodeDomEngine>().Compile(code, references, false);

            // assert
            Assert.True(result.Name == "MyTestClass");
        }

        [Test]
        public void CompileShoudReturnTypeWhenEngineIsMono()
        {
            // arrange 
            var code = File.ReadAllText("ToCompileClasses/MyTestClass.cs");
            var references = AppDomain.CurrentDomain.GetAssemblies().ToArray();

            // act
            var result = new Compiler<MonoEngine>().Compile(code, references, false);

            // assert
            Assert.True(result.Name == "MyTestClass");
        }

        [Test]
        public void CompileShoudReturnGenericTypeWhenEngineIsRoslynAndTypeToCompileIsGeneric()
        {
            // arrange 
            var code = File.ReadAllText("ToCompileClasses/MyGenericClass.cs");
            var references = AppDomain.CurrentDomain.GetAssemblies().ToArray();

            // act
            var result = new Compiler<RoslynEngine>().Compile(code, references, false);

            // assert
            Assert.True(result.IsGenericType);
        }

        [Test]
        public void CompileShoudReturnGenericTypeWhenEngineIsCodeDomAndTypeToCompileIsGeneric()
        {
            // arrange 
            var code = File.ReadAllText("ToCompileClasses/MyGenericClass.cs");
            var references = AppDomain.CurrentDomain.GetAssemblies().ToArray();

            // act
            var result = new Compiler<CodeDomEngine>().Compile(code, references, false);

            // assert
            Assert.True(result.IsGenericType);
        }

        [Test]
        public void CompileShoudReturnGenericTypeWhenEngineIsMonoAndTypeToCompileIsGeneric()
        {
            // arrange 
            var code = File.ReadAllText("ToCompileClasses/MyGenericClass.cs");
            var references = AppDomain.CurrentDomain.GetAssemblies().ToArray();

            // act
            var result = new Compiler<MonoEngine>().Compile(code, references, false);

            // assert
            Assert.True(result.IsGenericType);
        }
    }
}
