using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/*******
 * Read input from Console
 * Use Console.WriteLine to output your result.
 * Use:
 *       Utils.LocalPrint( variable); 
 * to display simple variables in a dedicated area.
 * 
 * Use:
 *      
		Utils.LocalPrintArray( collection)
 * to display collections in a dedicated area.
 * ***/

namespace CSharpContestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(Console.In, Console.Out);
        }

        public static void Test(TextReader input, TextWriter output)
        { 
            string line;
            line = input.ReadLine();
 
            int rendre = int.Parse(line);
            line = input.ReadLine();
            int nbPieces = int.Parse(line);

            var pieces = new Dictionary<int, int>();
            for (var i = 0; i < nbPieces; i++)
            {
                line = input.ReadLine();
                var data = line.Split(' ');
                int nombre = int.Parse(data[0]);
                int valeur = int.Parse(data[1]);
                pieces[valeur] = nombre;

            }

            int restant = rendre;
            var types = pieces.OrderByDescending(x => x.Key).Select(x => x.Key).ToList();
            int c = Combin(pieces, restant, 0, types);
            if (c == int.MaxValue)
            {
                output.WriteLine("IMPOSSIBLE");
                return;
            }

            output.WriteLine(c);
        }

        public static int Combin(Dictionary<int, int> pieces, int restant, int piece, List<int> types)
        {
            if (restant == 0) return 0;
            if (piece == types.Count) return int.MaxValue;
            var key = types[piece];
            int nMax = restant/key;
            var n = Math.Min(nMax, pieces[key]);
            int min = int.MaxValue;
            for (var i = 0; i <= n; i++)
            {
                var test = Combin(pieces, restant - key*i, piece + 1, types);
                if (test == int.MaxValue) continue;
                test += i;
                if (test < min) min = test;
            }
            return min;
        }
    }
}