﻿namespace AdventOfCode.Year2019.Day12
{
    public class Point3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString() =>
            $"<x={X.ToString().PadLeft(2)}, y={Y.ToString().PadLeft(2)}, z={Z.ToString().PadLeft(2)}>";

        public override bool Equals(object? obj) => Equals(obj as Point3D);

        private bool Equals(Point3D? other)
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

            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode() => X * 0x00010000 + Y * 0x00100000 + Z * 0x01000000;
        public static bool operator ==(Point3D? lhs, Point3D? rhs) => lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        public static bool operator !=(Point3D lhs, Point3D rhs) => !(lhs == rhs);
    }
}