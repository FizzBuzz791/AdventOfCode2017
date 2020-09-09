using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Solutions.Day15
{
	public class RepairDroidControlSystem
	{
		private IntCodeMachine.IntCodeMachine Software { get; }
		public static Point Origin => new Point(Midpoint, Midpoint);
		public Point Destination { get; private set; }
		private Point DroidLocation { get; set; } = new Point(Midpoint, Midpoint);
		private char[,] Grid { get; } = new char[Midpoint * 2, Midpoint * 2];
		private const int Midpoint = 25; // Artificial middle
		private const char Wall = '#';
		private const char Oxygen = 'O';
		private const char Visit = '.';
		private const char OriginMarker = 'o';
		public RepairDroidControlSystem(BigInteger[] memory) => Software = new IntCodeMachine.IntCodeMachine(memory);

		public void Explore()
		{
			// Not 100% sure this is useful, but let's do it anyway.
			Grid[DroidLocation.X, DroidLocation.Y] = OriginMarker;

			var lastOutput = Response.Moved;
			var lastInstruction = Instruction.North;
			do
			{
				var instruction = CalculateMoveDirection(lastInstruction, lastOutput);
				lastInstruction = instruction;

				Software.InputValues.Enqueue((int) instruction);
				Software.Execute(false);

				if (Enum.TryParse(Software.Outputs.Last(), out lastOutput))
				{
					switch (lastOutput)
					{
						case Response.Wall:
							AddWall(instruction);
							break;
						case Response.Moved:
							AddVisit(instruction);
							UpdateLocation(instruction);
							break;
						case Response.Oxygen:
							AddOxygen(instruction);
							UpdateLocation(instruction);
							Destination = new Point(DroidLocation.X, DroidLocation.Y);
							break;
						default:
							throw new ArgumentException($"Unexpected output: {lastOutput}", nameof(lastOutput));
					}
				}
				else
				{
					throw new ApplicationException($"Unexpected output occurred: {Software.Outputs.Last()}");
				}

				// Exit condition
				if (lastOutput == Response.Moved && DroidLocation.X == Midpoint && DroidLocation.Y == Midpoint)
					break;
			} while (true);
		}

		// Dijkstra's Algo
		public IEnumerable<Node> FindPath(Point source)
		{
			var nodes = new List<Node>();
			for (var row = 0; row < Grid.GetLength(0); row++)
			{
				for (var col = 0; col < Grid.GetLength(1); col++)
				{
					if (Grid[row, col] == OriginMarker || Grid[row, col] == Visit || Grid[row, col] == Oxygen)
						nodes.Add(new Node(new Point(row, col)));
				}
			}

			var currentNode = nodes.SingleOrDefault(n => n.Location == source);
			if (currentNode != null)
			{
				currentNode.Distance = 0;
				var priorityQueue = new List<Node> { currentNode };

				do
				{
					currentNode = priorityQueue.First();
					List<Node> neighbours = nodes
					                        .Where(n => AreNeighbours(currentNode.Location, n.Location) && !n.Visited)
					                        .ToList();

					foreach (var node in neighbours)
					{
						if (node.Distance > currentNode.Distance + 1)
							node.Distance = currentNode.Distance + 1;

						if (priorityQueue.All(x => x.Location != node.Location))
							priorityQueue.Add(node);
					}

					currentNode.Visited = true;
					priorityQueue.Remove(currentNode);
				} while (priorityQueue.Any());
			}

			return nodes;
		}

		private static bool AreNeighbours(Point p1, Point p2) =>
			(p1.X == p2.X || p1.Y == p2.Y) && (Math.Abs(p1.X - p2.X) == 1 || Math.Abs(p1.Y - p2.Y) == 1);

		private void AddFeature(Instruction instruction, char feature)
		{
			var featureLocation = CalculateLocation(instruction);
			if (Grid[featureLocation.X, featureLocation.Y] == 0)
				Grid[featureLocation.X, featureLocation.Y] = feature;
		}

		private void AddWall(Instruction instruction)
		{
			AddFeature(instruction, Wall);
		}

		private void AddOxygen(Instruction instruction)
		{
			AddFeature(instruction, Oxygen);
		}

		private void AddVisit(Instruction instruction)
		{
			AddFeature(instruction, Visit);
		}

		private void UpdateLocation(Instruction instruction)
		{
			var newDroidLocation = CalculateLocation(instruction);
			DroidLocation = new Point(newDroidLocation.X, newDroidLocation.Y);
		}

		private Point CalculateLocation(Instruction instruction)
		{
			var newLocation = new Point(DroidLocation.X, DroidLocation.Y);

			switch (instruction)
			{
				case Instruction.North:
					newLocation.Y--;
					break;
				case Instruction.South:
					newLocation.Y++;
					break;
				case Instruction.West:
					newLocation.X--;
					break;
				case Instruction.East:
					newLocation.X++;
					break;
				default:
					throw new ArgumentException($"Unexpected argument: {instruction}", nameof(instruction));
			}

			return newLocation;
		}

		// Dumb wall follower
		private static Instruction CalculateMoveDirection(Instruction lastDirection, Response lastOutput)
		{
			if (lastOutput == Response.Wall)
			{
				// Turn counter-clockwise
				return lastDirection switch
				{
					Instruction.North => Instruction.West,
					Instruction.West => Instruction.South,
					Instruction.South => Instruction.East,
					Instruction.East => Instruction.North,
					_ => throw new ArgumentOutOfRangeException($"Unexpected direction: {lastDirection}",
					                                           nameof(lastDirection))
				};
			}

			// Turn clockwise
			return lastDirection switch
			{
				Instruction.North => Instruction.East,
				Instruction.East => Instruction.South,
				Instruction.South => Instruction.West,
				Instruction.West => Instruction.North,
				_ => throw new ArgumentOutOfRangeException($"Unexpected direction: {lastDirection}",
				                                           nameof(lastDirection))
			};
		}
	}
}