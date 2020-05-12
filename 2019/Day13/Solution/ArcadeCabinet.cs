using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using MoreLinq;

namespace Day13
{
    public class ArcadeCabinet
    {
        public IntCodeMachine Software { get; }
        public int BlockCount
        {
            get
            {
                var blockCount = 0;
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    for (int x = 0; x < Grid.GetLength(0); x++)
                    {
                        if (Grid[x, y] == Block)
                            blockCount++;
                    }
                }
                return blockCount;
            }
        }
        public int[,] Grid { get; private set; }
        public int Score { get; private set; }
        public (int? x, int? y) BallLocation => GetLocation(Ball);
        public (int? x, int? y) PaddleLocation => GetLocation(Paddle);

        private IEnumerable<Instruction> Instructions { get; set; }
        private const int Empty = 0;
        private const int Wall = 1;
        private const int Block = 2;
        private const int Paddle = 3;
        private const int Ball = 4;

        public ArcadeCabinet(BigInteger[] memory)
        {
            Software = new IntCodeMachine(memory);
            Software.Execute(false);

            Instructions = Software.Outputs.Where(o => o != "Halt").ToList().SplitList(3).Select(i => new Instruction(i));

            int maxX = Instructions.MaxBy(instruction => instruction.X).First().X + 1;
            int maxY = Instructions.MaxBy(instruction => instruction.Y).First().Y + 1;

            Grid = new int[maxX, maxY];
        }

        public void InitialiseGrid()
        {
            foreach (var instruction in Instructions)
            {
                if (instruction.X == -1 && instruction.Y == 0)
                    Score = instruction.TileId;
                else
                    Grid[instruction.X, instruction.Y] = instruction.TileId;
            }

            // If you didn't put in a quarter, the program will now halt.
            // If you did put in a quarter, the program will now be expecting input.
        }

        public void PrintScreen()
        {
            var gridOutput = new StringBuilder();
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                for (int x = 0; x < Grid.GetLength(0); x++)
                {
                    switch (Grid[x, y])
                    {
                        case Empty:
                            gridOutput.Append(" ");
                            break;
                        case Wall:
                            gridOutput.Append("|");
                            break;
                        case Block:
                            gridOutput.Append("#");
                            break;
                        case Paddle:
                            gridOutput.Append("-");
                            break;
                        case Ball:
                            gridOutput.Append("*");
                            break;
                    }
                }
                gridOutput.Append(Environment.NewLine);
            }

            Console.WriteLine(gridOutput.ToString());
        }

        public void MoveJoyStick(Direction direction)
        {
            var joystickInput = direction switch
            {
                Direction.Neutral => 0,
                Direction.Left => -1,
                Direction.Right => 1,
                _ => throw new ArgumentException($"Unsupported Direction: {direction}", nameof(direction))
            };

            Software.InputValues.Enqueue(joystickInput);
            Software.Outputs.Clear();
            Software.Execute(false);

            var newInstructions = Software.Outputs.Where(o => o != "Halt").ToList().SplitList(3).Select(i => new Instruction(i));
            foreach (var instruction in newInstructions)
            {
                if (instruction.X == -1 && instruction.Y == 0)
                    Score = instruction.TileId;
                else
                    Grid[instruction.X, instruction.Y] = instruction.TileId;
            }
        }

        private (int? x, int? y) GetLocation(int tileId)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                for (int x = 0; x < Grid.GetLength(0); x++)
                {
                    if (Grid[x, y] == tileId)
                        return (x, y);
                }
            }

            return (null, null);
        }
    }

    public enum Direction
    {
        Neutral,
        Left,
        Right
    }
}