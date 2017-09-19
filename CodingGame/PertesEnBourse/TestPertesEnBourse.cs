using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingGame.PertesEnBourse
{
    public class TestPertesEnBourse
    {
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(TestPertesEnBourse))]
        public void Exemple1(StringReader input, string o)
        {
            var output = new StringWriter();
            global::CodingGame.PertesEnBourse.ProgramPertesEnBourse.Exécute(input, output);
            Assert.Equal(o, output.ToString());
        }

        public static IEnumerable<object[]> GetData()
        {
            return TestHelpers.GetTestData("PertesEnBourse");
        }
    }
}
