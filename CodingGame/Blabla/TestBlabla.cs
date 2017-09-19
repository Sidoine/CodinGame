using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingGame.Blabla
{
    public class TestBlabla
    {
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(TestBlabla))]
        public void Exemple1(StringReader input, string o)
        {
            var output = new StringWriter();
            ProgramBlabla.Execute(input, output);
            Assert.Equal(o, output.ToString());
        }

        public static IEnumerable<object[]> GetData()
        {
            return TestHelpers.GetTestData("Blabla");
        }
    }
}
