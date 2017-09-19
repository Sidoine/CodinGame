using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingGame.CorrectionAutomatique
{
    public class TestCorrectionAutomatique
    {
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(TestCorrectionAutomatique))]
        public void Exemple1(StringReader input, string o)
        {
            var output = new StringWriter();
            Program.Execute(input, output);
            Assert.Equal(o, output.ToString());
        }

        public static IEnumerable<object[]> GetData()
        {
            return TestHelpers.GetTestData("CorrectionAutomatique");
        }
    }
}
