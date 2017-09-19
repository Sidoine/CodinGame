using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingGame.Peugeot
{
    public class TestPeugeot
    {
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(TestPeugeot))]
        public void Exemple1(StringReader input, string o)
        {
            var output = new StringWriter();
            //CSharpContestProjectS.Program.Test(input, output);
            // Assert.Equal(o, output.ToString());
        }

        public static IEnumerable<object[]> GetData()
        {
            return TestHelpers.GetTestData("Peugeot");
        }
    }
}
