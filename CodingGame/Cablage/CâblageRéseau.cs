using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class CâblageRéseau
{
    static void Main(string[] args)
    {
        Exécute(Console.In, Console.Out);
    }

    public static long Len(int[] m, int y)
    {
        return m.Sum(x => Math.Abs((long)x - y));
    }

    private struct Point
    {
        public int x;
        public int y;
    }

    public static void Exécute(TextReader input, TextWriter output)
    {
        int N = int.Parse(input.ReadLine());
        Point[] points = new Point[N];
        for (int i = 0; i < N; i++)
        {
            string[] inputs = input.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            points[i].y = Y;
            points[i].x = X;
        }

        if (N == 1)
        {
            output.WriteLine("0");
            return;
        }

        var y = points.Select(x => x.y).ToArray();
        var xs = points.Select(x => x.x).ToArray();

        var sorted = points.OrderBy(x => x.y).ToArray();
        var middle = sorted.Length / 2;

        int minY = sorted[middle - 1].y;
        int maxY = sorted[middle].y;

        points = points.OrderBy(x => x.x).ToArray();

        long upValue = Len(y, minY);
        long downValue = Len(y, maxY);
        while (minY < maxY - 1)
        {
            int mid = (minY + maxY) / 2;
            long midValue = Len(y, mid);
            if (upValue < downValue)
            {
                maxY = mid;
                downValue = midValue;
            }
            else
            {
                minY = mid;
                upValue = midValue;
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        output.WriteLine(Math.Min(upValue, downValue) + xs.Max() - xs.Min());
    }
}