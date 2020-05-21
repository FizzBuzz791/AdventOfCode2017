using System;
using System.Diagnostics.CodeAnalysis;

namespace Day14
{
    public class Chemical : IEquatable<Chemical>
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public Chemical(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }

        public Chemical(string chemical)
        {
            var parts = chemical.Trim().Split(" ");

            Name = parts[1];
            Amount = Int32.Parse(parts[0]);
        }

        public Chemical(Chemical chemical)
        {
            Name = chemical.Name;
            Amount = chemical.Amount;
        }

        public override string ToString() => $"{Amount} {Name}";

        public override bool Equals(object? obj) => Equals(obj as Chemical);

        public bool Equals([AllowNull] Chemical other)
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

            return Name == other.Name && Amount == other.Amount;
        }

        public override int GetHashCode() => Name.GetHashCode() + Amount * 0x00100000;

        public static bool operator ==(Chemical lhs, Chemical rhs) => lhs is null ? rhs is null : lhs.Equals(rhs);

        public static bool operator !=(Chemical lhs, Chemical rhs) => !(lhs == rhs);
    }
}