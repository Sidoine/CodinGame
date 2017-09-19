using System;
using System.IO;

namespace CodingGame.Xml
{
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            Execute(Console.In, Console.Out);
        }

        public static void Execute(TextReader input, TextWriter output)
        {
            var line = input.ReadLine();
            int profondeur = 0;
            int position = 0;
            var res = new Dictionary<char, double>();
            while (position < line.Length)
            {
                char c = line[position];
                if (c == '-')
                {
                    position++;
                    c = line[position++];
                    profondeur--;
                }
                else
                {
                    profondeur++;
                    position++;
                    if (!res.ContainsKey(c))
                    {
                        res[c] = 0;
                    }
                    res[c] += 1.0 / profondeur;
                }
            }

            var max = res.Values.Max();
            var result = res.OrderBy(x => x.Key).First(x => x.Value == max).Key;
            output.WriteLine(result);
        }
    }
}