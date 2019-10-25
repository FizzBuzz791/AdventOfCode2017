using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    public class Program
    {
        private static readonly string[] Input = {
            "Step C must be finished before step P can begin.",
            "Step V must be finished before step Q can begin.",
            "Step T must be finished before step X can begin.",
            "Step B must be finished before step U can begin.",
            "Step Z must be finished before step O can begin.",
            "Step P must be finished before step I can begin.",
            "Step D must be finished before step G can begin.",
            "Step A must be finished before step Y can begin.",
            "Step R must be finished before step O can begin.",
            "Step J must be finished before step E can begin.",
            "Step N must be finished before step S can begin.",
            "Step X must be finished before step H can begin.",
            "Step F must be finished before step L can begin.",
            "Step S must be finished before step I can begin.",
            "Step W must be finished before step Q can begin.",
            "Step H must be finished before step K can begin.",
            "Step K must be finished before step Q can begin.",
            "Step E must be finished before step L can begin.",
            "Step Q must be finished before step O can begin.",
            "Step U must be finished before step G can begin.",
            "Step L must be finished before step O can begin.",
            "Step Y must be finished before step G can begin.",
            "Step G must be finished before step I can begin.",
            "Step M must be finished before step I can begin.",
            "Step I must be finished before step O can begin.",
            "Step A must be finished before step N can begin.",
            "Step H must be finished before step O can begin.",
            "Step T must be finished before step O can begin.",
            "Step H must be finished before step U can begin.",
            "Step A must be finished before step I can begin.",
            "Step B must be finished before step R can begin.",
            "Step V must be finished before step T can begin.",
            "Step H must be finished before step M can begin.",
            "Step C must be finished before step A can begin.",
            "Step B must be finished before step G can begin.",
            "Step L must be finished before step Y can begin.",
            "Step T must be finished before step J can begin.",
            "Step A must be finished before step R can begin.",
            "Step X must be finished before step L can begin.",
            "Step B must be finished before step L can begin.",
            "Step A must be finished before step F can begin.",
            "Step K must be finished before step O can begin.",
            "Step W must be finished before step M can begin.",
            "Step Z must be finished before step N can begin.",
            "Step Z must be finished before step S can begin.",
            "Step R must be finished before step K can begin.",
            "Step Q must be finished before step L can begin.",
            "Step G must be finished before step O can begin.",
            "Step F must be finished before step Y can begin.",
            "Step V must be finished before step H can begin.",
            "Step E must be finished before step I can begin.",
            "Step W must be finished before step Y can begin.",
            "Step U must be finished before step I can begin.",
            "Step F must be finished before step K can begin.",
            "Step M must be finished before step O can begin.",
            "Step Z must be finished before step H can begin.",
            "Step X must be finished before step S can begin.",
            "Step J must be finished before step O can begin.",
            "Step B must be finished before step I can begin.",
            "Step F must be finished before step H can begin.",
            "Step D must be finished before step U can begin.",
            "Step E must be finished before step M can begin.",
            "Step Z must be finished before step X can begin.",
            "Step P must be finished before step L can begin.",
            "Step W must be finished before step H can begin.",
            "Step C must be finished before step D can begin.",
            "Step A must be finished before step X can begin.",
            "Step Q must be finished before step I can begin.",
            "Step R must be finished before step Y can begin.",
            "Step B must be finished before step A can begin.",
            "Step N must be finished before step L can begin.",
            "Step H must be finished before step G can begin.",
            "Step Y must be finished before step M can begin.",
            "Step L must be finished before step G can begin.",
            "Step G must be finished before step M can begin.",
            "Step Z must be finished before step R can begin.",
            "Step S must be finished before step Q can begin.",
            "Step P must be finished before step J can begin.",
            "Step V must be finished before step J can begin.",
            "Step J must be finished before step I can begin.",
            "Step J must be finished before step X can begin.",
            "Step W must be finished before step O can begin.",
            "Step B must be finished before step F can begin.",
            "Step R must be finished before step M can begin.",
            "Step V must be finished before step S can begin.",
            "Step H must be finished before step E can begin.",
            "Step E must be finished before step U can begin.",
            "Step R must be finished before step W can begin.",
            "Step X must be finished before step Q can begin.",
            "Step N must be finished before step G can begin.",
            "Step T must be finished before step I can begin.",
            "Step L must be finished before step M can begin.",
            "Step H must be finished before step I can begin.",
            "Step U must be finished before step M can begin.",
            "Step C must be finished before step H can begin.",
            "Step P must be finished before step H can begin.",
            "Step J must be finished before step F can begin.",
            "Step A must be finished before step O can begin.",
            "Step X must be finished before step M can begin.",
            "Step H must be finished before step L can begin.",
            "Step W must be finished before step K can begin."
        };

        public static void Main()
        {
            var stepGraph = new DirectedGraph();
            stepGraph.Build(Input);
            stepGraph.ApplyTransientReduction(stepGraph.Roots.Select(r => r.Name).ToList());

            Console.WriteLine($"Step Order: {stepGraph.GetStepOrder()}");
        }
    }

    public class DirectedGraph
    {
        public List<Node> Nodes { get; } = new List<Node>();
        public List<Link> Links { get; } = new List<Link>();

        public List<Node> Roots
        {
            get
            {
                var rootNodes = new List<Node>();
                foreach (Node node in Nodes)
                {
                    if (Links.All(l => l.Target != node.Name))
                    {
                        rootNodes.Add(node);
                    }
                }
                return rootNodes;
            }
        }

        public void Build(string[] input)
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

        public void TryAddNode(char name)
        {
            if (Nodes.All(n => n.Name != name))
            {
                Nodes.Add(new Node(name));
            }
        }

        public void TryAddLink(char source, char target)
        {
            if (!Links.Any(l => l.Source == source && l.Target == target))
            {
                Links.Add(new Link(source, target));
            }
        }

        public void ApplyTransientReduction(IList<char> nodes)
        {
            foreach (char node in nodes)
            {
                var childNodes = GetChildNodes(node);
                foreach (char childNode in childNodes)
                {
                    var matchingLinks = DepthFirstSearch(childNode).SelectMany(dfs => childNodes.Where(child => child != childNode && child == dfs)).ToList();
                    if (matchingLinks.Any())
                    {
                        foreach (char link in matchingLinks)
                        {
                            int indexToRemove = Links.FindIndex(l => l.Source == node && l.Target == link);
                            if (indexToRemove >= 0)
                            {
                                Links.RemoveAt(indexToRemove);
                                Console.WriteLine($"Removed {node} -> {link}");
                            }
                        }
                    }
                }

                ApplyTransientReduction(childNodes);
            }
        }

        public List<char> GetChildNodes(char node)
        {
            return Links.Where(l => l.Source == node).Select(l => l.Target).ToList();
        }

        public HashSet<char> DepthFirstSearch(char start)
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
                        {
                            discoveredNodes.Push(neighbor.Target);
                        }
                    }
                }
            }

            return visited;
        }

        public string GetStepOrder()
        {
            var stepsReadyToComplete = Roots.Select(r => r.Name).ToList();
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
            {
                throw new Exception("Algorithm is borked.");
            }

            return completedSteps;
        }

        public List<char> GetStepsReadyToComplete(char source, string alreadyCompleted)
        {
            var matchingLinks = Links.Where(l => l.Source == source).Select(l => l.Target).ToList();
            var filteredMatchingLinks = new List<char>();
            foreach (char matchingLink in matchingLinks)
            {
                if (!alreadyCompleted.Contains(matchingLink))
                {
                    var otherParents = Links.Where(l => l.Target == matchingLink && l.Source != source).Select(l => l.Source).ToList();
                    var uncompletedParents = new List<char>();
                    foreach (char parent in otherParents)
                    {
                        if (!alreadyCompleted.Contains(parent))
                        {
                            uncompletedParents.Add(parent);
                        }
                    }

                    if (!uncompletedParents.Any())
                    {
                        filteredMatchingLinks.Add(matchingLink);
                    }
                }
            }

            return filteredMatchingLinks;
        }
    }

    public class Node
    {
        public char Name { get; }

        public Node(char name)
        {
            Name = name;
        }
    }

    public class Link
    {
        public char Source { get; }
        public char Target { get; }

        public Link(char source, char target)
        {
            Source = source;
            Target = target;
        }
    }
}
