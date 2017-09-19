using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodingGame
{
    public class TestIndy
    {
        [InlineData(@"2 4
4 3
12 10
11 5
2 3
1
1 0 TOP
1 1 TOP
", @"1 1
0 1
")]
        public void Exemple1(string i, string o)
        {
            var input = new StringReader(i);
            var output = new StringWriter();
            Indy.Exécute(input, output);
            Assert.Equal(o, output.ToString());
        }
    }
}
