using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2018.Day7
{
    public class WorkGroup
    {
        public List<Worker> Workers { get; } = new();
        public IEnumerable<Worker> AvailableWorkers => Workers.Where(w => w.IsFree).ToList();
        public IEnumerable<Worker> BusyWorkers => Workers.Where(w => !w.IsFree).ToList();

        public WorkGroup(int amountOfWorkers)
        {
            for (var i = 0; i < amountOfWorkers; i++)
            {
                Workers.Add(new Worker());
            }
        }

        public List<Node?> DoWork() => BusyWorkers.Select(worker => worker.DoWork())
            .Where(workResult => workResult != null).ToList();
    }
}