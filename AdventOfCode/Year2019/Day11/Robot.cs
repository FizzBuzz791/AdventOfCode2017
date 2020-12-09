using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using AdventOfCode.Year2019.IntCodeMachine;

namespace AdventOfCode.Year2019.Day11
{
    public class Robot
    {
        private Point CurrentLocation { get; set; } = new(0, 0);
		private Direction CurrentDirection { get; set; } = Direction.Up;
		public Dictionary<Point, Color> PaintedPanels { get; } = new();
		private IntCodeMachine.IntCodeMachine Brain { get; }

		public Robot(BigInteger[] memory) => Brain = new IntCodeMachine.IntCodeMachine(memory);

		public void Run(Color firstPanelColor = Color.Black)
		{
			var firstPanel = true;
			do
			{
				if (PaintedPanels.ContainsKey(CurrentLocation))
					Brain.InputValues.Enqueue((int) PaintedPanels[CurrentLocation]);
				else
					Brain.InputValues.Enqueue(firstPanel ? (int) firstPanelColor : (int) Color.Black);

				firstPanel = false;

				Brain.Execute(false);

				var offset = 2;
				if (Brain.Outputs.Contains("Halt"))
					offset = 3;
				var panelColor = Enum.Parse<Color>(Brain.Outputs.ToArray()[Brain.Outputs.Count - offset]);
				if (PaintedPanels.ContainsKey(CurrentLocation))
					PaintedPanels[CurrentLocation] = panelColor;
				else
					PaintedPanels.Add(CurrentLocation, panelColor);
				offset--;

				var turnDirection = Enum.Parse<Turn>(Brain.Outputs.ToArray()[Brain.Outputs.Count - offset]);
				Move(turnDirection);
			} while (Brain.State == MachineState.Paused && !Brain.Outputs.Contains("Halt"));
		}

		private void Move(Turn direction)
		{
			// Turn
			CurrentDirection = direction switch
			{
				Turn.Left => CurrentDirection switch
				{
					Direction.Up => Direction.Left,
					Direction.Left => Direction.Down,
					Direction.Down => Direction.Right,
					Direction.Right => Direction.Up,
					_ => CurrentDirection
				},
				Turn.Right => CurrentDirection switch
				{
					Direction.Up => Direction.Right,
					Direction.Right => Direction.Down,
					Direction.Down => Direction.Left,
					Direction.Left => Direction.Up,
					_ => CurrentDirection
				},
				_ => CurrentDirection
			};

			// Move forward
			CurrentLocation = CurrentDirection switch
			{
				Direction.Up => new Point(CurrentLocation.X, CurrentLocation.Y - 1),
				Direction.Left => new Point(CurrentLocation.X - 1, CurrentLocation.Y),
				Direction.Down => new Point(CurrentLocation.X, CurrentLocation.Y + 1),
				Direction.Right => new Point(CurrentLocation.X + 1, CurrentLocation.Y),
				_ => CurrentLocation
			};
		}
    }
}