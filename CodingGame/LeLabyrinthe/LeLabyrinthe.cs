using System;
using System.IO;
using System.Collections.Generic;
using CodingGame;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    struct Pos : IEquatable<Pos>
    {
        public int x;
        public int y;

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }

        public bool Equals(Pos other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Pos && Equals((Pos) obj);
        }
    }

    static void Main(string[] args)
    {
        Execute(Console.In, Console.Out);
    }

    sealed class Grid : Astar<Pos>
    {
        public char[,] rows;
        public bool findVide;

        protected override bool IsInRange(Pos pos)
        {
            return !(pos.x < 0 || pos.x >= rows.GetLength(1) || pos.y < 0 || pos.y >= rows.GetLength(0));
        }

        protected override bool IsPassable(Pos pos)
        {
            return rows[pos.y, pos.x] != '#';
        }

        protected override bool IsAlternateTarget(Pos pos)
        {
            return findVide && rows[pos.y, pos.x] == '?';
        }

        protected override int Heuristique(Pos pos, Pos target)
        {
            return Math.Abs(pos.x - target.x) + Math.Abs(pos.y - target.y);
        }

        protected override int Distance(Pos pos, Pos target)
        {
            return Math.Abs(pos.x - target.x) + Math.Abs(pos.y - target.y);
        }

        protected override IEnumerable<Pos> Neighbors(Pos depart)
        {
            yield return new Pos { x = depart.x + 1, y = depart.y };
            yield return new Pos { x = depart.x - 1, y = depart.y };
            yield return new Pos { x = depart.x, y = depart.y - 1 };
            yield return new Pos { x = depart.x, y = depart.y + 1 };
        }
    }

    public static void Execute(TextReader input, TextWriter output)
    {
        string[] inputs;
        var readLine = input.ReadLine();
        Console.Error.WriteLine(readLine);
        inputs = readLine.Split(' ');
        int R = int.Parse(inputs[0]); // number of rows.
        int C = int.Parse(inputs[1]); // number of columns.
        int A = inputs.Length > 2 ? int.Parse(inputs[2]) : 0;
            // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.

        bool exit = false;
        bool commandFound = false;
        Pos commandPos = new Pos();
        Pos exitPos = new Pos();
        Pos unknown = new Pos();
        Pos my = new Pos();
        var grid = new Grid();
        grid.rows = new char[R, C];

        // game loop
        while (true)
        {
            readLine = input.ReadLine();
            if (readLine == null) break;
            Console.Error.WriteLine(readLine);
            inputs = readLine.Split(' ');
            my.y = int.Parse(inputs[0]); // row where Kirk is located.
            my.x = int.Parse(inputs[1]); // column where Kirk is located.

            if (commandFound && my.x == commandPos.x && my.y == commandPos.y)
            {
                exit = true;
            }

            int du = 100000;
            
            for (int i = 0; i < R; i++)
            {
                string ROW = input.ReadLine(); // C of the characters in '#.TC?' (i.e. one line of the ASCII maze).
                Console.Error.WriteLine(ROW);
                for (int j = 0; j < C; j++)
                {
                    var c = ROW[j];
                    if (exit && c == '?') c = '#';
                    grid.rows[i, j] = c;
                    if (!commandFound && c == 'C')
                    {
                        commandFound = true;
                        commandPos.x = j;
                        commandPos.y = i;
                    }
                    else if (exit && c == 'T')
                    {
                        exitPos.x = j;
                        exitPos.y = i;
                    }
                    else if (!exit && c == '?')
                    {
                        var d = Math.Abs(i - my.y) + Math.Abs(j - my.x);
                        if (d < du)
                        {
                            du = d;
                            unknown.x = j;
                            unknown.y = i;
                        }
                    }
                }
            }

            string ret;

            if (exit)
            {
                ret = Rechercher(grid, exitPos, my, false);
            }
            else if (commandFound)
            {
                ret = Rechercher(grid, commandPos, my, false);
            }
            else
            {
                ret = Rechercher(grid, unknown, my, true);
            }

            output.WriteLine(ret);
        }
    }

    static string Rechercher(Grid grid, Pos objectif, Pos depart, bool findVide)
    {
        grid.findVide = findVide;
        var result = grid.GetNext(objectif, depart);
        return ReconstructPath(result, depart);
    }

    private static string ReconstructPath(Pos pos, Pos depart)
    {
        if (pos.x < depart.x)
        {
            return ("LEFT");
        }
        if (pos.x > depart.x)
        {
            return ("RIGHT");
        }
        if (pos.y < depart.y)
        {
            return ("UP");
        }
        if (pos.y > depart.y)
        {
            return ("DOWN");
        }
        return "ERROR";
    }
}