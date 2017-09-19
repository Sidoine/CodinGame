using System.Collections.Generic;
using System.IO;
using Xunit;

namespace CodingGame.Xml
{
    public class TestXml
    {
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(TestXml))]
        public void Exemple1(StringReader input, string o)
        {
            var output = new StringWriter();
            Program.Execute(input, output);
            Assert.Equal(o, output.ToString());
        }

        public static IEnumerable<object[]> GetData()
        {
            return TestHelpers.GetTestData("Xml");
        }
    }
}
