using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MoreLinq;

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
            var graph = new DirectedGraph();
            graph.Build(Input);
            graph.ApplyTransientReduction(graph.Roots.Select(r => r.Name).ToList());

            Part1(graph);
            Part2(graph);
        }

        private static void Part1(DirectedGraph graph)
        {
            // Correct answer: BCADPVTJFZNRWXHEKSQLUYGMIO
            Console.WriteLine($"Step Order: {graph.GetStepOrder()}");
        }

        private static void Part2(DirectedGraph graph)
        {
            var workGroup = new WorkGroup(5);

            var completedSteps = new List<Node>();
            var availableSteps = new List<Node>(graph.Roots);
            availableSteps.Sort();

            // Initialise
            foreach (Worker worker in workGroup.Workers)
            {
                if (availableSteps.Any())
                {
                    worker.AssignStep(availableSteps.First());
                    availableSteps.RemoveAt(0);
                }
            }

            // Tick
            var totalTime = 0;
            while (completedSteps.Count < graph.Nodes.Count)
            {
                var workResult = workGroup.DoWork();
                if (workResult.Any())
                {
                    completedSteps.AddRange(workResult);

                    // Get newly available steps
                    foreach (var stepsReadyToComplete in completedSteps.Select(completedStep =>
                        graph.GetStepsReadyToComplete(completedStep.Name,
                            string.Join(string.Empty, completedSteps.Select(c => c.Name)))))
                    {
                        availableSteps.AddRange(stepsReadyToComplete.Select(s => new Node(s)));
                    }

                    availableSteps = availableSteps.DistinctBy(s => s.Name).ToList();
                    availableSteps.Sort();

                    // Need to account for "in progress" steps.
                    foreach (int index in workGroup.BusyWorkers
                        .Select(worker => availableSteps.FindIndex(s => s.Name == worker.WorkingOn.Name))
                        .Where(index => index >= 0))
                    {
                        availableSteps.RemoveAt(index);
                    }
                }

                foreach (Worker worker in workGroup.AvailableWorkers.Where(worker => availableSteps.Any()))
                {
                    worker.AssignStep(availableSteps.First());
                    availableSteps.RemoveAt(0);
                }

                totalTime++;
            }

            // Correct answer: 973
            Console.WriteLine($"Total time: {totalTime}s");
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

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Node : IComparable<Node>
    {
        public char Name { get; }
        public int Time => 60 + Name % 32;

        public Node(char name)
        {
            Name = name;
        }

        public int CompareTo(Node otherNode)
        {
            // Alphabetical sort
            return Name.CompareTo(otherNode.Name);
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

    public class Worker
    {
        public Node WorkingOn { get; set; }
        public int TimeRemaining { get; set; }
        public bool IsFree => WorkingOn == null;

        public void AssignStep(Node step)
        {
            WorkingOn = step;
            TimeRemaining = step.Time;
        }

        public Node DoWork()
        {
            Node completedStep = null;

            TimeRemaining--;

            if (TimeRemaining == 0)
            {
                completedStep = WorkingOn;
                Console.WriteLine($"Completed Node {WorkingOn.Name}");

                WorkingOn = null;
            }

            return completedStep;
        }
    }

    public class WorkGroup
    {
        public List<Worker> Workers { get; } = new List<Worker>();
        public List<Worker> AvailableWorkers => Workers.Where(w => w.IsFree).ToList();
        public List<Worker> BusyWorkers => Workers.Where(w => !w.IsFree).ToList();

        public WorkGroup(int amountOfWorkers)
        {
            for (var i = 0; i < amountOfWorkers; i++)
            {
                Workers.Add(new Worker());
            }
        }

        public List<Node> DoWork()
        {
            return BusyWorkers.Select(worker => worker.DoWork()).Where(workResult => workResult != null).ToList();
        }
    }
}
