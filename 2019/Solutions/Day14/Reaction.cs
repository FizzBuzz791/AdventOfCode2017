using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Day14
{
	public class Reaction : IEquatable<Reaction>
	{
		public List<Chemical> Inputs { get; }
		public Chemical Output { get; }

		public Reaction(string reaction)
		{
			string[] interfaces = reaction.Split("=>");

			Inputs = interfaces[0].Split(',').Select(i => new Chemical(i)).ToList();
			Output = new Chemical(interfaces[1]);
		}

		public override string ToString() => $"{string.Join(", ", Inputs.Select(i => i.ToString()))} => {Output}";
		public override bool Equals(object? obj) => Equals(obj as Reaction);

		public bool Equals(Reaction? other)
		{
			// If parameter is null, return false.
			if (other is null)
				return false;

			// Optimization for a common success case.
			if (ReferenceEquals(this, other))
				return true;

			// If run-time types are not exactly the same, return false.
			if (GetType() != other.GetType())
				return false;

			return Inputs == other.Inputs && Output == other.Output;
		}

		public override int GetHashCode() => Inputs.GetHashCode() * Output.GetHashCode();
		public static bool operator ==(Reaction? lhs, Reaction? rhs) => lhs?.Equals(rhs) ?? rhs is null;
		public static bool operator !=(Reaction? lhs, Reaction? rhs) => !(lhs == rhs);
	}
}