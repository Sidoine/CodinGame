using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Scaffolding
{
    class Program
    {
        static void Main(string[] args)
        {
            const int NumberOfTests = 10;
            var projectName = Console.ReadLine().Trim();
            Directory.CreateDirectory(projectName);

            var programText = $@"using System;
using System.IO;

namespace CodingGame.{projectName}
{{
    public class Program{projectName}
    {{
        static void Main(string[] args)
        {{
            Execute(Console.In, Console.Out);
        }}

        public static void Execute(TextReader input, TextWriter output)
        {{
        }}
    }}
}}";
            File.WriteAllText(Path.Combine(projectName, $"Program{projectName}.cs"), programText);

            var testText =
                $@"using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingGame.{projectName}
{{
    public class Test{projectName}
    {{
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(Test{projectName}))]
        public void Exemple1(StringReader input, string o)
        {{
            var output = new StringWriter();
            Program{projectName}.Execute(input, output);
            Assert.Equal(o, output.ToString());
        }}

        public static IEnumerable<object[]> GetData()
        {{
            return TestHelpers.GetTestData(""{projectName}"");
        }}
    }}
}}
";
            File.WriteAllText(Path.Combine(projectName, $"Test{projectName}.cs"), testText);

            for (var i = 0; i < NumberOfTests; i++)
            {
                File.WriteAllText(Path.Combine(projectName, $"{i}.in.txt"), "");
                File.WriteAllText(Path.Combine(projectName, $"{i}.out.txt"), "");
            }

            var ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var xml = XDocument.Load("CodingGame.csproj");
            var compile = xml.Descendants(XName.Get("Compile", ns)).First();
            var compileProgram = new XElement(XName.Get("Compile", ns));
            compileProgram.Add(new XAttribute(XName.Get("Include"), $"{projectName}\\Program{projectName}.cs"));
            compile.AddAfterSelf(compileProgram);
            var compileTest = new XElement(XName.Get("Compile", ns));
            compileTest.Add(new XAttribute(XName.Get("Include"), $"{projectName}\\Test{projectName}.cs"));
            compile.AddAfterSelf(compileTest);
            var content = xml.Descendants(XName.Get("Content", ns)).First();
            for (var i = 0; i < NumberOfTests; i++)
            {
                var inOuts = new string[] {"in", "out"};
                foreach (var inOut in inOuts)
                {
                    var contentTest = new XElement(XName.Get("Content", ns));
                    contentTest.Add(new XAttribute(XName.Get("Include"), $"{projectName}\\{i}.{inOut}.txt"));
                    var copyToOutput = new XElement(XName.Get("CopyToOutputDirectory", ns));
                    copyToOutput.Add(new XText("PreserveNewest"));
                    contentTest.Add(copyToOutput);
                    content.AddAfterSelf(contentTest);
                }
            }
            xml.Save("CodingGame.csproj");
        }
    }
}
