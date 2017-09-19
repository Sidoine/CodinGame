using System;
using System.IO;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
namespace CodingGame.PertesEnBourse
{
    public class ProgramPertesEnBourse
    {
        static void Main(string[] args)
        {
            Exécute(Console.In, Console.Out);
        }

        public static void Exécute(TextReader input, TextWriter output)
        {
            int n = int.Parse(input.ReadLine());
            string[] inputs = input.ReadLine().Split(' ');
            int max = int.MinValue;
            int best = 0;
            for (int i = 0; i < n; i++)
            {
                int v = int.Parse(inputs[i]);
                if (v > max) max = v;
                int perte = v - max;
                if (perte < best) best = perte;
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            output.WriteLine(best);
        }
    }
}