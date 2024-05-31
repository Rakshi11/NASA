// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace MarsRoverApp
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public class Rover
    {
        private const int GridSize = 100;
        public int Position { get; private set; }
        public Direction Facing { get; private set; }

        public Rover()
        {
            Position = 1;
            Facing = Direction.South;
        }

        public void Move(int meters)
        {
            int row = (Position - 1) / GridSize;
            int col = (Position - 1) % GridSize;

            switch (Facing)
            {
                case Direction.North:
                    row -= meters;
                    break;
                case Direction.South:
                    row += meters;
                    break;
                case Direction.East:
                    col += meters;
                    break;
                case Direction.West:
                    col -= meters;
                    break;
            }

            if (row < 0 || row >= GridSize || col < 0 || col >= GridSize)
            {
                Console.WriteLine("Move halted: outside grid boundaries.");
                return;
            }

            Position = row * GridSize + col + 1;
        }

        public void TurnLeft()
        {
            Facing = (Direction)(((int)Facing + 3) % 4);
        }

        public void TurnRight()
        {
            Facing = (Direction)(((int)Facing + 1) % 4);
        }

        public void Report()
        {
            Console.WriteLine($"Position: {Position}, Direction: {Facing}");
        }

        public void ExecuteCommands(List<string> commands)
        {
            foreach (var command in commands)
            {
                if (command.EndsWith("m"))
                {
                    int meters = int.Parse(command.TrimEnd('m'));
                    Move(meters);
                }
                else if (command.Equals("Left", StringComparison.OrdinalIgnoreCase))
                {
                    TurnLeft();
                }
                else if (command.Equals("Right", StringComparison.OrdinalIgnoreCase))
                {
                    TurnRight();
                }
                else
                {
                    Console.WriteLine($"Invalid command: {command}");
                }
                Report();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rover rover = new Rover();

            List<string> commands = new List<string>
            {
                "50m",
                "Left",
                "23m",
                "Left",
                "4m"
            };

            rover.ExecuteCommands(commands);
        }
    }
}
