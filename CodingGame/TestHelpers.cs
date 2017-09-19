using System.Collections.Generic;
using System.IO;

namespace CodingGame
{
    public static class TestHelpers
    {
        public static IEnumerable<object[]> GetTestData(string directory)
        {
            foreach (var file in Directory.EnumerateFiles(directory, "*.in.txt"))
            {
                var outputName = file.Replace(".in.txt", ".out.txt");
                var input = File.ReadAllText(file);
                var output = File.ReadAllText(outputName);
                yield return new object[] {new StringReader(input), output};
            }
        }
    }
}
