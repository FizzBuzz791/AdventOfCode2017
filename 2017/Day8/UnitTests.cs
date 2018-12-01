using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Day8
{
	[TestFixture]
	public class UnitTests
	{
		[TestCaseSource(nameof(ConstructorData))]
		public void ConstructorCorrectlyAssignsPartsOfInstruction(string[] instruction, string register, bool increment, int step,
			string[] condition)
		{
			// Arrange
			
			// Act
			Instruction sut = new Instruction(instruction);

			// Assert
			Assert.That(sut.Register, Is.EqualTo(register));
			Assert.That(sut.Increment, Is.EqualTo(increment));
			Assert.That(sut.Step, Is.EqualTo(step));
			Assert.That(sut.Condition, Is.EqualTo(condition));
		}

		[TestCaseSource(nameof(RegisterControllerData))]
		public void InitialiseRegisters_CorrectlyInitialises(List<Instruction> instructions, int expectedCount)
		{
			// Arrange
			RegisterController sut = new RegisterController();

			// Act
			sut.InitialiseRegisters(instructions);

			// Assert
			Assert.That(sut.Registers.Count, Is.EqualTo(expectedCount));
		}

		[TestCaseSource(nameof(ProcessInstructionData))]
		public void ProcessInstruction_ModifiesCorrectRegister(List<Instruction> instructions, string targetRegister, int expectedValue)
		{
			// Arrange
			RegisterController sut = new RegisterController();
			sut.InitialiseRegisters(instructions);

			// Act
			sut.ProcessInstruction(instructions[0]);

			// Assert
			Assert.That(sut.Registers[targetRegister], Is.EqualTo(expectedValue));
		}

		private static IEnumerable<TestCaseData> ConstructorData
		{
			[UsedImplicitly]
			get
			{
				yield return new TestCaseData(new[] {"a", "inc", "5", "if", "a", ">", "1"}, "a", true, 5,
					new[] {"a", ">", "1"}) {TestName = "Inc_Pos"};
				yield return new TestCaseData(new[] {"a", "dec", "-5", "if", "a", ">", "1"}, "a", false, -5,
					new[] {"a", ">", "1"}) {TestName = "Dec_Neg"};
			}
		}

		private static IEnumerable<TestCaseData> RegisterControllerData
		{
			[UsedImplicitly]
			get
			{
				yield return new TestCaseData(new List<Instruction>(), 0) {TestName = "Empty_Instructions"};
				yield return new TestCaseData(
					new List<Instruction> {new Instruction(new List<string> {"a", "inc", "5", "a", ">", "1"})}, 1)
				{
					TestName = "One_Instruction"
				};
			}
		}

		private static IEnumerable<TestCaseData> ProcessInstructionData
		{
			[UsedImplicitly]
			get
			{
				yield return new TestCaseData(
					new List<Instruction> {new Instruction(new List<string> {"a", "inc", "5", "if", "a", ">", "1"})}, "a", 0)
				{
					TestName = "False_Conditional_GreaterThan"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", ">", "-1" }) }, "a", 5)
				{
					TestName = "True_Conditional_GreaterThan"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "<", "0" }) }, "a", 0)
				{
					TestName = "False_Conditional_LessThan"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "<", "1" }) }, "a", 5)
				{
					TestName = "True_Conditional_LessThan"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", ">=", "1" }) }, "a", 0)
				{
					TestName = "False_Conditional_GreaterThanOrEqual"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", ">=", "0" }) }, "a", 5)
				{
					TestName = "True_Conditional_GreaterThanOrEqual"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "<=", "-1" }) }, "a", 0)
				{
					TestName = "False_Conditional_LessThanOrEqual"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "<=", "0" }) }, "a", 5)
				{
					TestName = "True_Conditional_LessThanOrEqual"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "!=", "0" }) }, "a", 0)
				{
					TestName = "False_Conditional_DoesNotEqual"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "!=", "1" }) }, "a", 5)
				{
					TestName = "True_Conditional_DoesNotEqual"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "==", "1" }) }, "a", 0)
				{
					TestName = "False_Conditional_Equal"
				};
				yield return new TestCaseData(
					new List<Instruction> { new Instruction(new List<string> { "a", "inc", "5", "if", "a", "==", "0" }) }, "a", 5)
				{
					TestName = "True_Conditional_Equal"
				};
			}
		}
	}
}