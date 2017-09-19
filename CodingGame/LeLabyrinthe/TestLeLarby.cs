using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingGame.Maya;
using Xunit;

namespace CodingGame.LeLabyrinthe
{
    public class TestLeLarby
    {
        [Theory]
        [MemberData(nameof(GetData), MemberType = typeof(TestLeLarby))]
        public void Exemple1(StringReader input, string o)
        {
            var output = new StringWriter();
            Player.Execute(input, output);
            Assert.Equal(o, output.ToString());
        }

        public static IEnumerable<object[]> GetData()
        {
            return TestHelpers.GetTestData("LeLabyrinthe");
        }
    }
}
