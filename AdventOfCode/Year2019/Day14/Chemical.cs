namespace AdventOfCode.Year2019.Day14
{
    public record Chemical
    {
        public string Name { get; }
        public double Amount { get; }

        public Chemical(string chemical)
        {
            string[] parts = chemical.Trim().Split(" ");

            Name = parts[1];
            Amount = double.Parse(parts[0]);
        }

        public override string ToString() => $"{Amount} {Name}";
    }
}