namespace ShapeFX.Compilation.Runtime
{
    internal static class Constants
    {
        internal static class Regex
        {
            internal const string SourceNameWithoutExtension = @"([^\\]+)(?=\.xaml)|([^\\]+)(?=\.cs)";
            internal const string ClassName = @"(?<=class\s)(.*?)(?=\s|:)";
            internal const string Namespace = @"(?<=namespace\s)(.*?)(?=\s)";
            internal const string AppName = @"(?<=namespace\s)(.*?)(?=\.)";
            internal const string GenericClassName = @"(?<=\<)(.*?)(?=>)";
            internal const string GenericParametersRegex = @"(?<=\[)(.*?)(?=])";
            internal const string DllRegex = @"([^\\]+)(?=\.dll)";
            internal const string PublicModifierRegex = @"(?<=public\s)(.*?)(?=\s)";
        }
    }
}
