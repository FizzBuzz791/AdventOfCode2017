using System;

namespace AdventOfCode.Year2019.Day12
{
    public class Moon
    {
        public Point3D Position { get; }
        public Point3D Velocity { get; } = new Point3D(0, 0, 0);
        public int PotentialEnergy => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
        public int KineticEnergy => Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);

        public Moon(string position)
        {
            string[] parts = position.Trim('<', '>').Split(',');

            int x = int.Parse(parts[0].Substring(parts[0].IndexOf('=') + 1));
            int y = int.Parse(parts[1].Substring(parts[1].IndexOf('=') + 1));
            int z = int.Parse(parts[2].Substring(parts[2].IndexOf('=') + 1));

            Position = new Point3D(x, y, z);
        }
		
        public Moon(Moon moon)
        {
            Position = new Point3D(moon.Position.X, moon.Position.Y, moon.Position.Z);
            Velocity = new Point3D(moon.Velocity.X, moon.Velocity.Y, moon.Velocity.Z);
        }

        public void ApplyVelocity()
        {
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
        }

        public override string ToString() => $"pos={Position}, vel={Velocity}";
        public override bool Equals(object? obj) => Equals(obj as Moon);

        private bool Equals(Moon? other)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(other, null))
                return false;

            // Optimization for a common success case.
            if (ReferenceEquals(this, other))
                return true;

            // If run-time types are not exactly the same, return false.
            if (GetType() != other.GetType())
                return false;

            return Position == other.Position && Velocity == other.Velocity;
        }

        public override int GetHashCode() => Position.GetHashCode() + Velocity.GetHashCode();
        public static bool operator ==(Moon? lhs, Moon? rhs) => lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        public static bool operator !=(Moon lhs, Moon rhs) => !(lhs == rhs);
    }
}