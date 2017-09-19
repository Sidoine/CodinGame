using System;
using System.Collections.Generic;
using System.IO;

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

namespace CSharpContestProjectS
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
            int i = 0;
            var balises = new List<char>();
            var profondeurs = new Dictionary<char, List<int>>();
            while (i < line.Length)
            {
                if (line[i] == '-')
                {
                    i++;
                    i++;
                    //if (!profondeurs.ContainsKey(fermant))
                    //    profondeurs.Add(fermant, new List<int>());
                    //profondeurs[fermant].Add(balises.Count);
                    balises.RemoveAt(balises.Count - 1);
                }
                else
                {
                    var type = line[i++]
                        ;
                    balises.Add(type);
                    if (!profondeurs.ContainsKey(type))
                        profondeurs.Add(type, new List<int>());
                    profondeurs[type].Add(balises.Count);
                }
            }
            char minc = '0';
            double value = 0;
            foreach (var p in profondeurs)
            {
                double result =0;
                foreach (var i1 in p.Value)
                {
                    result += (double) 1/i1;
                }
                if (result >= value || p.Key < minc)
                {
                    value = result;
                    minc = p.Key;
                }
            }
            // Vous pouvez aussi effectuer votre traitement ici après avoir lu toutes les données 
            output.WriteLine(minc);
        }
    }
}