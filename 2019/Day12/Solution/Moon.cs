using System;
using System.Diagnostics.CodeAnalysis;

namespace Day12
{
    public class Moon : IEquatable<Moon>
    {
        public Point3D Position { get; }
        public Point3D Velocity { get; } = new Point3D(0, 0, 0);
        public int PotentialEnergy { get { return Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z); } }
        public int KineticEnergy { get { return Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z); } }

        public Moon(string position)
        {
            var parts = position.Trim(new char[] { '<', '>' }).Split(',');

            var x = Int32.Parse(parts[0].Substring(parts[0].IndexOf('=') + 1));
            var y = Int32.Parse(parts[1].Substring(parts[1].IndexOf('=') + 1));
            var z = Int32.Parse(parts[2].Substring(parts[2].IndexOf('=') + 1));

            Position = new Point3D(x, y, z);
        }

        public void ApplyVelocity()
        {
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
        }

        public override string ToString() => $"pos={Position.ToString()}, vel={Velocity.ToString()}";

        public override bool Equals(object? obj) => this.Equals(obj as Moon);

        public bool Equals([AllowNull] Moon other)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(other, null))
                return false;

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
                return true;

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
                return false;

            return Position == other.Position && Velocity == other.Velocity;
        }

        public override int GetHashCode() => Position.GetHashCode() + Velocity.GetHashCode();

        public static bool operator ==(Moon lhs, Moon rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
                return Object.ReferenceEquals(rhs, null) ? true : false;
            else
                return lhs.Equals(rhs);
        }

        public static bool operator !=(Moon lhs, Moon rhs) => !(lhs == rhs);
    }
}