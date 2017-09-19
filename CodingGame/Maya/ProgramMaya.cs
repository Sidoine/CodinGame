using System;
using System.Collections.Generic;
using System.IO;

namespace CodingGame.Maya
{
    public class ProgramMaya
    {
        static void Main(string[] args)
        {
            Execute(Console.In, Console.Out);
        }

        public static void Execute(TextReader input, TextWriter output)
        {
            string[] inputs = input.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int H = int.Parse(inputs[1]);
            var chiffres = new string[20];

            for (int i = 0; i < H; i++)
            {
                string numeral = input.ReadLine();
                for (int n = 0; n < 20; n++)
                {
                    var chiffre = numeral.Substring(n*L, L);
                    chiffres[n] += chiffre;
                }
            }
            var c = new Dictionary<string, int>();
            for (var i = 0; i < chiffres.Length; i++)
            {
                c[chiffres[i]] = i;
            }

            long v1 = ReadNumber(input, H, c);
            long v2 = ReadNumber(input, H, c);
            string operation = input.ReadLine();
            long result = 0;
            switch (operation)
            {
                case "+":
                    result = v1 + v2;
                    break;
                case "-":
                    result = v1 - v2;
                    break;
                case "*":
                    result = v1 * v2;
                    break;
                case "/":
                    result = v1/v2;
                    break;
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            WriteNumber(output, H, chiffres, result);
        }

        private static void WriteNumber(TextWriter output, int h, string[] chiffres, long result)
        {
            var sub = result/20;
            if (sub != 0)
            {
                WriteNumber(output, h, chiffres, sub);
            }
            var chiffre = chiffres[result%20];
            int l = chiffre.Length / h;
            for (var i = 0; i < h; i++)
            {
                output.WriteLine(chiffre.Substring(i*l, l));
            }
        }

        private static long ReadNumber(TextReader input, int H, Dictionary<string, int> c)
        {
            int S1 = int.Parse(input.ReadLine());
            long v1 = 0;
            for (int i = 0; i < S1/H; i++)
            {
                string s1 = "";
                for (int j = 0; j < H; j++)
                {
                    string num1Line = input.ReadLine();
                    s1 += num1Line;
                }
                var a = c[s1];
                v1 = v1*20 + a;
            }
            return v1;
        }
    }
}