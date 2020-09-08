using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using MoreLinq.Extensions;

namespace Solutions.Day13
{
	public class ArcadeCabinet
	{
		private IntCodeMachine.IntCodeMachine Software { get; }

		public int BlockCount
		{
			get
			{
				var blockCount = 0;
				for (var y = 0; y < Grid.GetLength(1); y++)
				{
					for (var x = 0; x < Grid.GetLength(0); x++)
					{
						if (Grid[x, y] == Block)
							blockCount++;
					}
				}

				return blockCount;
			}
		}

		private int[,] Grid { get; }
		public int Score { get; private set; }
		public Point BallLocation => GetLocation(Ball);
		public Point PaddleLocation => GetLocation(Paddle);
		private IEnumerable<Instruction> Instructions { get; }
		private const int Block = 2;
		private const int Paddle = 3;
		private const int Ball = 4;

		public ArcadeCabinet(BigInteger[] memory)
		{
			Software = new IntCodeMachine.IntCodeMachine(memory);
			Software.Execute(false);

			Instructions = Software.Outputs.Where(o => o != "Halt")
			                       .ToList()
			                       .SplitList(3)
			                       .Select(i => new Instruction(i))
			                       .ToList();

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

		public void MoveJoyStick(Direction direction)
		{
			int joystickInput = direction switch
			{
				Direction.Neutral => 0,
				Direction.Left => -1,
				Direction.Right => 1,
				_ => throw new ArgumentException($"Unsupported Direction: {direction}", nameof(direction))
			};

			Software.InputValues.Enqueue(joystickInput);
			Software.Outputs.Clear();
			Software.Execute(false);

			var newInstructions = Software.Outputs.Where(o => o != "Halt")
			                              .ToList()
			                              .SplitList(3)
			                              .Select(i => new Instruction(i));
			foreach (var instruction in newInstructions)
			{
				if (instruction.X == -1 && instruction.Y == 0)
					Score = instruction.TileId;
				else
					Grid[instruction.X, instruction.Y] = instruction.TileId;
			}
		}

		private Point GetLocation(int tileId)
		{
			for (var y = 0; y < Grid.GetLength(1); y++)
			{
				for (var x = 0; x < Grid.GetLength(0); x++)
				{
					if (Grid[x, y] == tileId)
						return new Point(x, y);
				}
			}

			return Point.Empty;
		}
	}
}