using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Day7
{
	[Serializable]
	// ReSharper disable once UseNameofExpression
	[DebuggerDisplay("Name: {Name} | Weight: {Weight} | Child Nodes: {ChildNodes.Count} | Balanced: {StackBalanced}")]
    public class Node
    {
        public string Name { get; }
        public int Weight { get; }
        public List<Node> ChildNodes { get; }

		public bool HasChildren => ChildNodes.Count > 0;

	    public bool StackBalanced
	    {
		    get
		    {
			    bool balanced = true;

			    if (HasChildren)
			    {
						List<int> weights = new List<int>();
						foreach (Node childNode in ChildNodes)
						{
							weights.Add(childNode.Weight + childNode.ChildWeight);
					    }

					    balanced = !weights.Distinct().Skip(1).Any();
			    }

			    return balanced;
		    }
	    }

	    public int ChildWeight
	    {
		    get { return ChildNodes.Any() ? ChildNodes.Sum(c => c.Weight + c.ChildWeight) : 0; }
	    }

		public Node(string name, int weight, List<Node> childNodes)
        {
            Name = name;
            Weight = weight;
            ChildNodes = childNodes;
        }

        public static Node Parse(string nodeDescription)
        {
            string[] tokens = nodeDescription.Split(' ');

            string name = tokens[0].Trim(',');
            int weight = 0;

            if (tokens.Length > 1)
            {
                weight = int.Parse(tokens[1].Trim('(').Trim(')'));
            }

            List<Node> childNodes = new List<Node>();
            if (tokens.Length > 2)
            {
                int childTokenCount = tokens.Length - 3;
                string[] childNodeTokens = new string[childTokenCount];
                Array.Copy(tokens, 3, childNodeTokens, 0, childNodeTokens.Length);
                childNodes.AddRange(childNodeTokens.Select(Parse));
            }

            Node node = new Node(name, weight, childNodes);

            return node;
        }

        public static Node FindBaseNode(List<Node> nodes)
        {
			List<Node> seenNodes = new List<Node>();
	        foreach (Node node in nodes.Where(n => n.HasChildren))
	        {
		        seenNodes.AddRange(node.ChildNodes);
	        }

	        Node finalNode = null;
	        foreach (Node node in nodes)
	        {
		        if (seenNodes.All(n => n.Name != node.Name))
		        {
			        finalNode = node;
		        }
	        }

	        return finalNode;
        }

	    public static Node BuildTower(Node baseNode, List<Node> nodes)
	    {
			List<Node> fullChildNodes = new List<Node>();
		    foreach (Node childNode in baseNode.ChildNodes)
		    {
			    Node fullChildNode = nodes.SingleOrDefault(n => n.Name == childNode.Name);
			    if (fullChildNode != null)
			    {
				    if (fullChildNode.HasChildren)
				    {
					    fullChildNode = BuildTower(fullChildNode, nodes);
				    }

					fullChildNodes.Add(DeepClone(fullChildNode));
				}
		    }
			baseNode.ChildNodes.Clear();
		    baseNode.ChildNodes.AddRange(fullChildNodes);

		    return baseNode;
	    }

	    public static Node DeepClone(Node nodeToClone)
	    {
			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, nodeToClone);
				stream.Position = 0;
				return (Node)formatter.Deserialize(stream);
			}
		}

	    public static int FindTargetWeight(Node tower)
	    {
		    int unbalancedWeight = -1;

			if (!tower.StackBalanced)
			{
				if (tower.ChildNodes.All(c => c.StackBalanced))
				{
					List<int> weights = new List<int>();
					foreach (Node childNode in tower.ChildNodes)
					{
						weights.Add(childNode.Weight + childNode.ChildWeight);
					}

					List<IGrouping<int, int>> distinctWeights = weights.GroupBy(w => w).ToList();
					if (distinctWeights.Count > 1)
					{
						int targetWeight = 0;
						int unbalanced = 0;
						foreach (IGrouping<int, int> distinctWeight in distinctWeights)
						{
							if (distinctWeight.Count() > 1)
								targetWeight = distinctWeight.First();
							else
								unbalanced = distinctWeight.First();
						}

						Node targetNode = tower.ChildNodes.Single(n => n.Weight + n.ChildWeight == unbalanced);
						int weightDifference = targetWeight - targetNode.ChildWeight - targetNode.Weight;
						unbalancedWeight = targetNode.Weight + weightDifference;
					}
				}
				else
				{
					unbalancedWeight = FindTargetWeight(tower.ChildNodes.Single(c => !c.StackBalanced));
				}
			}

		    return unbalancedWeight;
	    }
    }
}