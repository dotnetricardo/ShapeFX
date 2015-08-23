using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFX.Compilation.Runtime
{
    using System.Text.RegularExpressions;

    internal class CodeParser
    {
        public static string RandomNamespaceGenerate(string code)
        {

            var @namespace = Regex.Match(code, Constants.Regex.Namespace).Value;
            var appName = Regex.Match(code, Constants.Regex.AppName).Value;

            code = code.Replace("namespace " + @namespace,
                "namespace " + RandomNamespace());

            var sb = new StringBuilder();

            var split = @namespace.Split('.');

            var concat = string.Empty;

            foreach (var dot in split)
            {
                if (concat != string.Empty)
                {
                    concat += "." + dot;
                }
                else
                {
                    concat = dot;
                }

                sb.Append("using ");
                sb.Append(concat);
                sb.Append("; ");
                sb.Append(Environment.NewLine);
            }


            sb.Append(code);

            return sb.ToString();
        }

        private static string RandomNamespace()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
