using NUnit.Framework;

namespace Day9
{
	[TestFixture]
	public class UnitTests
	{
		[TestCase("a", 0, TestName = "NoIncrement")]
		[TestCase("{", 1, TestName = "Increment_OneLevel")]
		[TestCase("{{", 2, TestName = "Increment_TwoLevels")]
		[TestCase("!{", 0, TestName = "NoIncrement_Ignore")]
		[TestCase("{!{", 1, TestName = "Increment_OneLevel_Ignore")]
		public void Read_IncrementsLevel(string stream, int expectedResult)
		{
			// Arrange
			GarbageStreamReader sut = new GarbageStreamReader(stream);

			// Act
			while (sut.Peek() != -1)
			{
				sut.Read();
			}

			// Assert
			Assert.That(sut.Level, Is.EqualTo(expectedResult));
		}

		[TestCase("a", 0, TestName = "NoDecrement")]
		[TestCase("}", 0, TestName = "NoDecrement_Negative")]
		[TestCase("{}", 0, TestName = "Decrement_OneLevel")]
		[TestCase("{{}}", 0, TestName = "Decrement_TwoLevels")]
		[TestCase("!{}", 0, TestName = "NoDecrement_IgnoreStart")]
		[TestCase("{!}", 1, TestName = "NoDecrement_IgnoreEnd")]
		public void Read_DecrementsLevel(string stream, int expectedResult)
		{
			// Arrange
			GarbageStreamReader sut = new GarbageStreamReader(stream);

			// Act
			while (sut.Peek() != -1)
			{
				sut.Read();
			}

			// Assert
			Assert.That(sut.Level, Is.EqualTo(expectedResult));
		}

		[TestCase("", 0, TestName = "NoGroups")]
		[TestCase("{}", 1, TestName = "OneGroup")]
		[TestCase("{{}}", 3, TestName = "TwoGroups_Nested")]
		[TestCase("{<a>}", 1, TestName = "OneGroup_Garbage")]
		[TestCase("{<a>{<b>}<c>}", 3, TestName = "TwoGroups_Nested_Garbage")]
		[TestCase("{{},{}}", 5, TestName = "ThreeGroups_InternalsNotNested")]
		[TestCase("{{<},{>}}", 3, TestName = "ThreeGroups_InternalsNotNested")]
		[TestCase("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9, TestName = "FiveGroups_InternalsNotNested_DoubleIgnores")]
		[TestCase("{{{},{},{{}}}}", 16, TestName = "ComplexGroupings")]
		[TestCase("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3, TestName = "GarbageIgnore")]
		public void Read_CalculatesGroupScore(string stream, int expectedResult)
		{
			// Arrange
			GarbageStreamReader sut = new GarbageStreamReader(stream);

			// Act
			while (sut.Peek() != -1)
			{
				sut.Read();
			}

			// Assert
			Assert.That(sut.GroupScore, Is.EqualTo(expectedResult));
		}

		[TestCase("<>", 0, TestName = "NoGarbage")]
		[TestCase("<a>", 1, TestName = "OneGarbage")]
		[TestCase("<<>", 1, TestName = "GarbageCharInGarbage")]
		[TestCase("<!!>", 0, TestName = "DoubleIgnoreInGarbage")]
		[TestCase("<{o\"i!a,<{i<a>", 10, TestName = "SampleGarbage")]
		public void Read_CalculatesGarbageCount(string stream, int expectedResult)
		{
			// Arrange
			GarbageStreamReader sut = new GarbageStreamReader(stream);

			// Act
			while (sut.Peek() != -1)
			{
				sut.Read();
			}

			// Assert
			Assert.That(sut.GarbageCount, Is.EqualTo(expectedResult));
		}
	}
}