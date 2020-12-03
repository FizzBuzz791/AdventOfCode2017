using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day7
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        private readonly DirectedGraph _graph;
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
            _graph = new DirectedGraph();
            _graph.Build(Input);
            _graph.ApplyTransientReduction(_graph.Roots.Select(r => r.Name).ToList());
        }

        public string SolvePart1()
        {
            return $"Part 1: {_graph.GetStepOrder()}";
        }

        public string SolvePart2()
        {
            var workGroup = new WorkGroup(5);

            var completedSteps = new List<Node?>();
            var availableSteps = new List<Node>(_graph.Roots);
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
            while (completedSteps.Count < _graph.Nodes.Count)
            {
                List<Node?> workResult = workGroup.DoWork();
                if (workResult.Any())
                {
                    completedSteps.AddRange(workResult);

                    // Get newly available steps
                    foreach (var stepsReadyToComplete in completedSteps.Where(c => c != null).Select(completedStep =>
                        _graph.GetStepsReadyToComplete(completedStep!.Name,
                            string.Join(string.Empty, completedSteps.Select(c => c!.Name)))))
                    {
                        availableSteps.AddRange(stepsReadyToComplete.Select(s => new Node { Name = s }));
                    }

                    availableSteps = availableSteps.DistinctBy(s => s.Name).ToList();
                    availableSteps.Sort();

                    // Need to account for "in progress" steps.
                    foreach (int index in workGroup.BusyWorkers
                        .Select(worker => availableSteps.FindIndex(s => s.Name == worker.WorkingOn?.Name))
                        .Where(index => index >= 0))
                    {
                        availableSteps.RemoveAt(index);
                    }
                }

                foreach (Worker worker in workGroup.AvailableWorkers.Where(_ => availableSteps.Any()))
                {
                    worker.AssignStep(availableSteps.First());
                    availableSteps.RemoveAt(0);
                }

                totalTime++;
            }

            return $"Part 2: {totalTime}";
        }
    }
}