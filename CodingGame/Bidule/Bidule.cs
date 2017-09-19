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
class Bidule
{


    static void Main(string[] args)
    {
        string[] inputs;

        bool boostUsed = false;
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
            int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
            int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
            int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
            inputs = Console.ReadLine().Split(' ');
            int opponentX = int.Parse(inputs[0]);
            int opponentY = int.Parse(inputs[1]);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // You have to output the target position
            // followed by the power (0 <= thrust <= 100)
            // i.e.: "x y thrust"
            int thrust;

            int opod = opponentX * opponentX + opponentY * opponentY;
            /*if (nextCheckpointAngle > 90 || nextCheckpointAngle < -90)
            {
                Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " 0");
            }
            else*/
            if (nextCheckpointDist < 2000 && opod < 200000 && (nextCheckpointAngle < 20 && nextCheckpointAngle > -20))
            {
                Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " SHIELD");
            }
            else
            {
                if (!boostUsed && nextCheckpointDist > 2000 && nextCheckpointAngle < 5 && nextCheckpointAngle > -5)
                {
                    boostUsed = true;
                    thrust = 1000;
                }
                else
                    thrust = Math.Max(100, 100 - (int)Math.Abs(nextCheckpointAngle) * 6 + nextCheckpointDist / 100);
                Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " " + (thrust > 100 ? "BOOST" : thrust.ToString()));
            }

        }
    }
}
