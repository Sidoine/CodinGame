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
class Indy
{
    static void Main(string[] args)
    {
        Exécute(Console.In, Console.Out);
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    class Room
    {
        public Dictionary<Direction, Direction> Liens { get; set; }

        public Room()
        {
            Liens = new Dictionary<Direction, Direction>();
        }

        public Room AddLien(Direction from, Direction to)
        {
            Liens.Add(from, to);
            return this;
        }
    }

    public static void Exécute(TextReader input, TextWriter output)
    {
        var rooms = new List<Room>();
        rooms.Add(new Room());
        rooms.Add(
            new Room().AddLien(Direction.Up, Direction.Down)
                .AddLien(Direction.Left, Direction.Down)
                .AddLien(Direction.Right, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Left, Direction.Right).AddLien(Direction.Right, Direction.Left));
        rooms.Add(new Room().AddLien(Direction.Up, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Up, Direction.Left).AddLien(Direction.Right, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Up, Direction.Right).AddLien(Direction.Left, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Left, Direction.Right).AddLien(Direction.Right, Direction.Left));
        rooms.Add(new Room().AddLien(Direction.Up, Direction.Down).AddLien(Direction.Right, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Left, Direction.Down).AddLien(Direction.Right, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Up, Direction.Down).AddLien(Direction.Left, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Up, Direction.Left));
        rooms.Add(new Room().AddLien(Direction.Up, Direction.Right));
        rooms.Add(new Room().AddLien(Direction.Right, Direction.Down));
        rooms.Add(new Room().AddLien(Direction.Left, Direction.Down));

        string[] inputs;
        inputs = input.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // number of columns.
        int H = int.Parse(inputs[1]); // number of rows.
        Room[][] map = new Room[H][];
        for (int i = 0; i < H; i++)
        {
            string LINE = input.ReadLine(); // represents a line in the grid and contains W integers. Each integer represents one room of a given type.
            var line = LINE.Split(' ').Select(x => rooms[int.Parse(x)]).ToArray();
            map[i] = line;
        }

        int EX = int.Parse(input.ReadLine()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

        // game loop
        while (true)
        {
            inputs = input.ReadLine().Split(' ');
            if (inputs.Length == 0) break;
            int XI = int.Parse(inputs[0]);
            int YI = int.Parse(inputs[1]);
            string POS = inputs[2];
            var room = map[YI][XI];
            Direction direction = Direction.Down;
            switch (POS)
            {
                case "TOP":
                    direction = Direction.Up;
                    break;
                case "LEFT":
                    direction = Direction.Left;
                    break;
                case "RIGHT":
                    direction = Direction.Right;
                    break;
            }
            var sortie = room.Liens[direction];

            switch (sortie)
            {
                case Direction.Down:
                    YI++;
                    break;
                case Direction.Left:
                    XI--;
                    break;
                case Direction.Right:
                    XI++;
                    break;

            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            output.WriteLine(XI + " " + YI); // One line containing the X Y coordinates of the room in which you believe Indy will be on the next turn.
        }
    }
}