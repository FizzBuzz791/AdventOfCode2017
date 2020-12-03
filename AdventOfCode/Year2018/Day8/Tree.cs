using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2018.Day8
{
    public class Tree
    {
        public Node? RootNode { get; private set; }

        public void Build(string licenseFile)
        {
            List<string> licenseFileParts = licenseFile.Split(' ').ToList();
            RootNode = GetNextNode(licenseFileParts);
        }

        private static Node GetNextNode(List<string> licenseFile)
        {
            int childNodeCount = int.Parse(licenseFile[0]);
            int metadataCount = int.Parse(licenseFile[1]);
            licenseFile.RemoveRange(0, 2);

            var node = new Node();

            for (var c = 0; c < childNodeCount; c++)
            {
                node.ChildNodes.Add(GetNextNode(licenseFile));
            }

            for (var m = 0; m < metadataCount; m++)
            {
                int metadata = int.Parse(licenseFile[0]);
                licenseFile.RemoveAt(0);
                node.Metadata.Add(metadata);
            }

            return node;
        }

        public static int GetMetadataTotal(Node node)
        {
            int metadataTotal = node.Metadata.Sum(m => m);

            foreach (var childNode in node.ChildNodes)
            {
                metadataTotal += GetMetadataTotal(childNode);
            }

            return metadataTotal;
        }

        public static int GetNodeValue(Node node)
        {
            var nodeValue = 0;

            if (node.ChildNodes.Count == 0)
            {
                nodeValue = node.Metadata.Sum(m => m);
            }
            else
            {
                foreach (int index in node.Metadata)
                {
                    if (index - 1 < node.ChildNodes.Count)
                        nodeValue += GetNodeValue(node.ChildNodes[index - 1]);
                }
            }

            return nodeValue;
        }
    }
}