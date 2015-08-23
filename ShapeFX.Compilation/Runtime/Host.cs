namespace ShapeFX.Compilation.Runtime
{
    using System.Diagnostics;

    internal class Host
    {
        internal static bool IsWebApp()
        {
            var process = Process.GetCurrentProcess().ProcessName.ToLower();
            return process.Contains("iis") || process.Contains("w3wp");
        }
    }
}
