using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingGame.Maya
{
    public class TestMaya
    {
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(TestMaya))]
        public void Exemple1(StringReader input, string o)
        {
            var output = new StringWriter();
            ProgramMaya.Execute(input, output);
            Assert.Equal(o, output.ToString());
        }

        public static IEnumerable<object[]> GetData()
        {
            return TestHelpers.GetTestData("Maya");
        }
    }
}
