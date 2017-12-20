using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Day7
{
    [TestFixture]
    public class UnitTests
    {
        [TestCase("bxlur (38)", "bxlur", 38, 0, TestName = "NoChildren")]
        [TestCase("ehsqyyb (174) -> xtcdt", "ehsqyyb", 174, 1, TestName = "OneChild")]
        [TestCase("ehsqyyb (174) -> xtcdt, tujcuy, wiqohmb, cxdwmu", "ehsqyyb", 174, 4, TestName = "MultipleChildren")]
        public void Parse_CorrectlyInterprets_Description(string nodeDescription, string name, int weight, int childNodeCount)
        {
            // Arrange
            // Nothing to do.

            // Act
            Node result = Node.Parse(nodeDescription);

            // Assert
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Weight, Is.EqualTo(weight));
            Assert.That(result.ChildNodes.Count, Is.EqualTo(childNodeCount));
        }

        [TestCase]
        public void FindBaseNode_ReturnsExpectedResult()
        {
            // Arrange
            List<Node> flatNodeList = new List<Node>
            {
                new Node("2", 2, new List<Node> {new Node("3", 0, new List<Node>())}),
				new Node("3", 3, new List<Node>())
            };

            // Act
            Node tower = Node.FindBaseNode(flatNodeList);

            // Assert
            Assert.That(tower.Name, Is.EqualTo("2"));
        }

		[TestCase]
	    public void DeepClone_FunctionsCorrectly()
	    {
			// Arrange
			Node node = new Node("1", 1, new List<Node>());

			// Act
		    Node clone = Node.DeepClone(node);

		    // Assert
			Assert.That(node, Is.Not.EqualTo(clone));
	    }

		[TestCase]
	    public void BuildTower_ConstructsFullTower_Correctly()
	    {
			// Arrange
			List<Node> flatNodeList = new List<Node>
			{
				new Node("pbga", 66, new List<Node>()),
				new Node("xhth", 57, new List<Node>()),
				new Node("ebii", 61, new List<Node>()),
				new Node("havc", 66, new List<Node>()),
				new Node("ktlj", 57, new List<Node>()),
				new Node("fwft", 72, new List<Node>
				{
					new Node("ktlj", 0, new List<Node>()),
					new Node("cntj", 0, new List<Node>()),
					new Node("xhth", 0, new List<Node>())
				}),
				new Node("qoyq", 66, new List<Node>()),
				new Node("padx", 45, new List<Node>
				{
					new Node("pbga", 0, new List<Node>()),
					new Node("havc", 0, new List<Node>()),
					new Node("qoyq", 0, new List<Node>())
				}),
				new Node("tknk", 41, new List<Node>
				{
					new Node("ugml", 0, new List<Node>()),
					new Node("padx", 0, new List<Node>()),
					new Node("fwft", 0, new List<Node>())
				}),
				new Node("jptl", 61, new List<Node>()),
				new Node("ugml", 68, new List<Node>
				{
					new Node("gyxo", 0, new List<Node>()),
					new Node("ebii", 0, new List<Node>()),
					new Node("jptl", 0, new List<Node>())
				}),
				new Node("gyxo", 61, new List<Node>()),
				new Node("cntj", 57, new List<Node>())
			};

			// Act
		    Node baseNode = Node.FindBaseNode(flatNodeList);
		    baseNode = Node.BuildTower(baseNode, flatNodeList);

		    // Assert
			Assert.That(baseNode.Name, Is.EqualTo("tknk"));
			Assert.That(baseNode.ChildNodes.Count, Is.EqualTo(3));
		    foreach (Node childNode in baseNode.ChildNodes)
		    {
				Assert.That(childNode.ChildNodes.Count, Is.EqualTo(3));
		    }
	    }

		[TestCaseSource(nameof(StackBalancingData))]
		public void StackBalanced_DeterminesBalanceCorrectly(Node sourceNode, bool expectedResult)
	    {
			// Arrange

			// Act

			// Assert
			Assert.That(sourceNode.StackBalanced, Is.EqualTo(expectedResult));
	    }

		[TestCaseSource(nameof(TargetWeightData))]
	    public void FindUnbalancedWeight_CorrectlyIdentifiesUnbalancedWeight(Node tower, int expectedResult)
	    {
			Assert.That(Node.FindTargetWeight(tower), Is.EqualTo(expectedResult));
	    }

	    private static IEnumerable<TestCaseData> StackBalancingData
	    {
		    [UsedImplicitly]
		    get
		    {
			    yield return new TestCaseData(new Node("1", 1, new List<Node>()), true) {TestName = "Empty_Stack"};
			    yield return new TestCaseData(new Node("1", 1, new List<Node>
			    {
				    new Node("2", 2, new List<Node>()),
				    new Node("3", 2, new List<Node>())
			    }), true) {TestName = "Simple_Stack_Balanced"};
			    yield return new TestCaseData(new Node("1", 1, new List<Node>
			    {
				    new Node("2", 2, new List<Node>()),
				    new Node("3", 3, new List<Node>())
			    }), false) {TestName = "Simple_Stack_Unbalanced"};
			    yield return new TestCaseData(new Node("tknk", 41, new List<Node>
			    {
				    new Node("ugml", 68, new List<Node>
				    {
					    new Node("gyxo", 61, new List<Node>()),
					    new Node("ebii", 61, new List<Node>()),
					    new Node("jptl", 61, new List<Node>())
				    }),
					new Node("padx", 45, new List<Node>
					{
						new Node("pbga", 66, new List<Node>()),
						new Node("havc", 66, new List<Node>()),
						new Node("qoyq", 66, new List<Node>())
					}),
					new Node("fwft", 72, new List<Node>
					{
						new Node("ktlj", 57, new List<Node>()),
						new Node("cntj", 57, new List<Node>()),
						new Node("xhth", 57, new List<Node>())
					})
			    }), false) {TestName = "Sample_Unbalanced"};
		    }
	    }

	    private static IEnumerable<TestCaseData> TargetWeightData
	    {
		    [UsedImplicitly]
		    get
		    {
			    yield return new TestCaseData(new Node("1", 1, new List<Node>()), -1) { TestName = "Empty_Stack" };
			    yield return new TestCaseData(new Node("tknk", 41, new List<Node>
				    {
					    new Node("ugml", 68, new List<Node>
					    {
						    new Node("gyxo", 61, new List<Node>()),
						    new Node("ebii", 61, new List<Node>()),
						    new Node("jptl", 61, new List<Node>())
					    }),
					    new Node("padx", 45, new List<Node>
					    {
						    new Node("pbga", 66, new List<Node>()),
						    new Node("havc", 66, new List<Node>()),
						    new Node("qoyq", 66, new List<Node>())
					    }),
					    new Node("fwft", 72, new List<Node>
					    {
						    new Node("ktlj", 57, new List<Node>()),
						    new Node("cntj", 57, new List<Node>()),
						    new Node("xhth", 57, new List<Node>())
					    })
				    }), 60)
				    { TestName = "Sample_Unbalanced" };
		    }
	    }
	}
}