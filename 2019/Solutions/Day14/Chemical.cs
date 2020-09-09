using System;

namespace Solutions.Day14
{
	public class Chemical : IEquatable<Chemical>
	{
		public string Name { get; }
		public double Amount { get; }

		public Chemical(string chemical)
		{
			var parts = chemical.Trim().Split(" ");

			Name = parts[1];
			Amount = double.Parse(parts[0]);
		}

		public override string ToString() => $"{Amount} {Name}";

		public override bool Equals(object? obj) => Equals(obj as Chemical);

		public bool Equals(Chemical? other)
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

			return Name == other.Name && Math.Abs(Amount - other.Amount) < double.Epsilon;
		}

		public override int GetHashCode() => (int)Math.Round(Name.GetHashCode() + Amount * 0x00100000, 0);

		public static bool operator ==(Chemical? lhs, Chemical? rhs) => lhs?.Equals(rhs) ?? rhs is null;

		public static bool operator !=(Chemical? lhs, Chemical? rhs) => !(lhs == rhs);
	}
}