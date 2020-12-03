using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2018.Day7
{
    public class DirectedGraph
    {
        public List<Node> Nodes { get; } = new();
        private List<Link> Links { get; } = new();

        public IEnumerable<Node> Roots => Nodes.Where(node => Links.All(l => l.Target != node.Name)).ToList();

        public void Build(IEnumerable<string> input)
        {
            foreach (string step in input)
            {
                char parent = step[5];
                char child = step[36];

                TryAddNode(parent);
                TryAddNode(child);

                TryAddLink(parent, child);
            }
        }

        private void TryAddNode(char name)
        {
            if (Nodes.All(n => n.Name != name))
                Nodes.Add(new Node { Name = name });
        }

        private void TryAddLink(char source, char target)
        {
            if (!Links.Any(l => l.Source == source && l.Target == target))
                Links.Add(new Link { Source = source, Target = target });
        }

        public void ApplyTransientReduction(IEnumerable<char> nodes)
        {
            foreach (char node in nodes)
            {
                List<char> childNodes = GetChildNodes(node);
                foreach (char childNode in childNodes)
                {
                    List<char> matchingLinks = DepthFirstSearch(childNode)
                        .SelectMany(dfs => childNodes.Where(child => child != childNode && child == dfs)).ToList();
                    if (matchingLinks.Any())
                    {
                        foreach (char link in matchingLinks)
                        {
                            int indexToRemove = Links.FindIndex(l => l.Source == node && l.Target == link);
                            if (indexToRemove >= 0)
                                Links.RemoveAt(indexToRemove);
                        }
                    }
                }

                ApplyTransientReduction(childNodes);
            }
        }

        private List<char> GetChildNodes(char node) => Links.Where(l => l.Source == node).Select(l => l.Target).ToList();

        private IEnumerable<char> DepthFirstSearch(char start)
        {
            var visited = new HashSet<char>();

            if (Links.All(l => l.Source != start))
                return visited;

            var discoveredNodes = new Stack<char>();
            discoveredNodes.Push(start);

            while (discoveredNodes.Count > 0)
            {
                char node = discoveredNodes.Pop();

                if (!visited.Contains(node))
                {
                    visited.Add(node);

                    foreach (Link neighbor in Links.Where(l => l.Source == node))
                    {
                        if (!visited.Contains(neighbor.Target))
                            discoveredNodes.Push(neighbor.Target);
                    }
                }
            }

            return visited;
        }

        public string GetStepOrder()
        {
            List<char> stepsReadyToComplete = Roots.Select(r => r.Name).ToList();
            string completedSteps = string.Empty;

            while (stepsReadyToComplete.Count > 0)
            {
                // Make sure we're not double counting anything.
                stepsReadyToComplete = stepsReadyToComplete.Distinct().ToList();
                // Make sure they're alphabetical, since that determines priority.
                stepsReadyToComplete.Sort();

                // Grab the next step that's ready and remove it from the possible steps.
                char completedStep = stepsReadyToComplete.First();
                stepsReadyToComplete.RemoveAt(0);

                // Add the next step to the order.
                completedSteps += completedStep;

                // Determine which new steps have been made available by completing the step.
                stepsReadyToComplete.AddRange(GetStepsReadyToComplete(completedStep, completedSteps));
            }

            if (completedSteps.Length != Nodes.Count)
                throw new Exception("Algorithm is borked.");

            return completedSteps;
        }

        public List<char> GetStepsReadyToComplete(char source, string alreadyCompleted)
        {
            List<char> matchingLinks = Links.Where(l => l.Source == source).Select(l => l.Target).ToList();
            var filteredMatchingLinks = new List<char>();
            foreach (char matchingLink in matchingLinks.Where(m => !alreadyCompleted.Contains(m)))
            {
                List<char> otherParents = Links.Where(l => l.Target == matchingLink && l.Source != source)
                    .Select(l => l.Source).ToList();
                List<char> uncompletedParents =
                    otherParents.Where(parent => !alreadyCompleted.Contains(parent)).ToList();

                if (!uncompletedParents.Any())
                    filteredMatchingLinks.Add(matchingLink);
            }

            return filteredMatchingLinks;
        }
    }
}