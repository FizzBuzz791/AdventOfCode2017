using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day13
{
    public class ArcadeCabinet
    {
        public IntCodeMachine Software { get; }
        public int BlockTileCount => Instructions?.Count(i => i[2] == "2") ?? 0;

        private IEnumerable<List<string>>? Instructions { get; set; }

        public ArcadeCabinet(BigInteger[] memory)
        {
            Software = new IntCodeMachine(memory);
        }

        public void GenerateGrid()
        {
            Software.Execute(false);

            Instructions = Software.Outputs.Where(o => o != "Halt").ToList().SplitList(3);
        }
    }
}