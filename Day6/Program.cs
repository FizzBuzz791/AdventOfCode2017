using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
	internal class Program
	{
		private static void Main()
		{
			const string PUZZLEINPUT = "0\t5\t10\t0\t11\t14\t13\t4\t11\t8\t8\t7\t1\t4\t12\t11";

			Redistributer redistributer = new Redistributer();
			int cycles = redistributer.RedistributeUntilDuplicateState(PUZZLEINPUT.Split('\t'));
			Console.WriteLine($"Part 1: {cycles}");

			cycles = redistributer.FindCycles(PUZZLEINPUT.Split('\t'));
			Console.WriteLine($"Part 2: {cycles}");

			Console.ReadKey();
		}
	}

	internal class Redistributer
	{
		public int RedistributeUntilDuplicateState(string[] memoryBanks)
		{
			List<string> memoryBankHistory = new List<string>();
			List<int> memoryBanksConverted = memoryBanks.Select(int.Parse).ToList();
			int cycles = 0;

			while (!memoryBankHistory.Contains(string.Join(" ", memoryBanksConverted.ToArray())))
			{
				memoryBankHistory.Add(string.Join(" ", memoryBanksConverted.ToArray()));

				int largestBank = memoryBanksConverted.Max();
				int largestBankIndex = memoryBanksConverted.IndexOf(largestBank);

				memoryBanksConverted[largestBankIndex] = 0;

				while (largestBank > 0)
				{
					largestBankIndex = largestBankIndex == memoryBanksConverted.Count - 1 ? 0 : largestBankIndex + 1;
					memoryBanksConverted[largestBankIndex]++;
					largestBank--;
				}

				cycles++;
			}

			return cycles;
		}

		public int FindCycles(string[] memoryBanks)
		{
			List<string> memoryBankHistory = new List<string>();
			List<int> memoryBanksConverted = memoryBanks.Select(int.Parse).ToList();
			int cycles = 0;

			while (!memoryBankHistory.Contains(string.Join(" ", memoryBanksConverted.ToArray())))
			{
				memoryBankHistory.Add(string.Join(" ", memoryBanksConverted.ToArray()));

				int largestBank = memoryBanksConverted.Max();
				int largestBankIndex = memoryBanksConverted.IndexOf(largestBank);

				memoryBanksConverted[largestBankIndex] = 0;

				while (largestBank > 0)
				{
					largestBankIndex = largestBankIndex == memoryBanksConverted.Count - 1 ? 0 : largestBankIndex + 1;
					memoryBanksConverted[largestBankIndex]++;
					largestBank--;
				}

				cycles++;
			}

			return cycles - memoryBankHistory.IndexOf(string.Join(" ", memoryBanksConverted.ToArray()));
		}
	}
}