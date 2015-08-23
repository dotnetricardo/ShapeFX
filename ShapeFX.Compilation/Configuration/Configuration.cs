using System.IO;

namespace ShapeFX.Compilation.Configuration
{
    public sealed class Enums
    {
        public enum Compiler
        {
            Default = 0,
            Mono = 1,
            CodeDom = 2,
            Roslyn = 3
        }
    }
}
