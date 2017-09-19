using System;
using System.IO;

namespace CodingGame.CorrectionAutomatique
{
    using System.Collections.Generic;

    public class Program
    {
        static void Main(string[] args)
        {
            Execute(Console.In, Console.Out);
        }

        public static void Execute(TextReader input, TextWriter output)
        {
            string line = input.ReadLine();
            int tailleDico = int.Parse(line);
            var dico = new List<string>();
            for (int i = 0; i < tailleDico; i++)
            {
                line = input.ReadLine();
                dico.Add(line);
            }
            line = input.ReadLine();
            int nbMots = int.Parse(line);
            for (int i = 0; i < nbMots; i++)
            {
                var mot = input.ReadLine();
                int best = int.MaxValue - 10;
                string result = null;
                foreach (var dic in dico)
                {
                    var newValue = CalculerProximite(new Splice(mot), new Splice(dic), best + 1);
                    if (newValue < best || (newValue == best && string.CompareOrdinal(dic, result) < 0))
                    {
                        best = newValue;
                        result = dic;
                    }
                }

                output.WriteLine(result);
            }
        }

        public struct Splice
        {
            public string word;

            public int offset;

            public Splice(string w, int o = 0)
            {
                word = w;
                offset = o;
                Length = w.Length - offset;
            }

            public int Length;

            public Splice Substring(int off)
            {
                return new Splice(word, offset + off);
            }

            public char this[int off]
            {
                get
                {
                    return word[offset + off];
                }
            }
        }

        private static int CalculerProximite(Splice mot, Splice dico, int max = Int32.MaxValue)
        {
            if (max < 0) return max;
            if (dico.Length == 0) return Math.Min(max, mot.Length * 2);
            if (mot.Length == 0) return Math.Min(max, dico.Length * 2);
            if (mot[0] == dico[0]) return CalculerProximite(mot.Substring(1), dico.Substring(1));
            int best = max;
            var remplacement = 3 + CalculerProximite(mot.Substring(1), dico.Substring(1), best - 3);
            if (remplacement < best) best = remplacement;
            var suppression1 = 2 + CalculerProximite(mot.Substring(1), dico, best - 2);
            if (suppression1 < best) best = suppression1;
            var suppression2 = 2 + CalculerProximite(mot, dico.Substring(1), best - 2);
            if (suppression2 < best) best = suppression2;
            if (dico.Length >= 2 && mot.Length >= 2)
            {
                if (dico[0] == mot[1] && dico[1] == mot[0])
                {
                    var inversion = 3 + CalculerProximite(mot.Substring(2), dico.Substring(2), best - 3);
                    if (inversion < best) best = inversion;
                }
            }
            return best;
        }
    }
}