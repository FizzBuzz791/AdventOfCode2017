using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Day14
{
    public record Reaction
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
    }
}