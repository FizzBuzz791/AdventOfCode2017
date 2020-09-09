using System.Drawing;

namespace Solutions.Day15
{
	public class Node
	{
		public Point Location { get; }
		public int Distance { get; set; } = int.MaxValue;
		public bool Visited { get; set; }

		public Node(Point location) => Location = location;
	}
}