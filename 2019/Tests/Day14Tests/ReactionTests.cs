using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Solutions.Day14;

namespace Tests.Day14Tests
{
	public class ReactionTests
	{
		[TestCaseSource(nameof(CreationCases))]
		public void CanCreateReactionCorrectly(string input, List<Chemical> expectedInputs, Chemical expectedOutput)
		{
			// Arrange

			// Act
			var reaction = new Reaction(input);

			// Assert
			reaction.Output.ShouldBe(expectedOutput);
			reaction.Inputs.ShouldBe(expectedInputs);

		}

		private static IEnumerable<object[]> CreationCases()
		{
			yield return new object[] { "1 ORE => 1 B", new List<Chemical> { new Chemical("1 ORE") }, new Chemical("1 B") };
			yield return new object[] { "10 ORE => 10 A", new List<Chemical> { new Chemical("10 ORE") }, new Chemical("10 A") };
			yield return new object[] { "7 A, 1 B => 1 C", new List<Chemical> { new Chemical("7 A"), new Chemical("1 B") }, new Chemical("1 C") };
		}
	}
}