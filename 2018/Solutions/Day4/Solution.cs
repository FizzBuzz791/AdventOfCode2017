using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using NAoCHelper;

namespace Solutions.Day4
{
    public class Solution : BaseSolution<List<string>>, ISolvable
    {
        private List<IGrouping<int, ActivityRecord>> GuardSleepRecords { get; }
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split("\n").ToList())
        {
            Input.Sort();

            var activityRecords = new List<ActivityRecord>();
            while(Input.Count > 0)
            {
                int nextGuard = Input.FindIndex(1, a => a.Contains("Guard"));
                if (nextGuard == -1)
                    nextGuard = Input.Count;
                activityRecords.Add(new ActivityRecord(Input.Take(nextGuard)));
                Input.RemoveRange(0, nextGuard);
            }

            GuardSleepRecords = activityRecords.GroupBy(a => a.GuardId).ToList();
        }

        public string SolvePart1()
        {
            IGrouping<int, ActivityRecord>? sleepiestGuard =
                GuardSleepRecords.MaxBy(g => g.ToList().Sum(s => s.TimeAsleep)).FirstOrDefault();
            KeyValuePair<int, int> maxMinutes = CalculateMaximumSleepMinute(sleepiestGuard);

            return $"Part 1: {sleepiestGuard.Key * maxMinutes.Key}";
        }

        public string SolvePart2()
        {
            var mostAsleep = new Dictionary<int,KeyValuePair<int, int>>();
            foreach (var sleepRecord in GuardSleepRecords)
            {
                KeyValuePair<int, int> maxSleepMinute = CalculateMaximumSleepMinute(sleepRecord);
                mostAsleep.Add(sleepRecord.Key, maxSleepMinute);
            }

            (int guardId, KeyValuePair<int, int> asleepTime) = mostAsleep.MaxBy(v => v.Value.Value).SingleOrDefault();
            
            return $"Part 2: {guardId * asleepTime.Key}";
        }

        private static KeyValuePair<int, int> CalculateMaximumSleepMinute(IEnumerable<ActivityRecord> guard)
        {
            var sleepingMinutes = new Dictionary<int, int>();
            foreach (ActivityRecord sleepRecord in guard)
            {
                for (var i = 0; i < 60; i++)
                {
                    for (var j = 0; j < sleepRecord.FallsAsleep.Count; j++)
                    {
                        if (i >= sleepRecord.FallsAsleep[j].Minute && i < sleepRecord.WakesUp[j].Minute)
                        {
                            if (sleepingMinutes.ContainsKey(i))
                                sleepingMinutes[i]++;
                            else
                                sleepingMinutes.Add(i, 1);
                        }
                    }
                }
            }

            return sleepingMinutes.MaxBy(m => m.Value).FirstOrDefault();
        }
    }
}