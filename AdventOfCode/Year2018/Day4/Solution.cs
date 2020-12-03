using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day4
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        private readonly List<IGrouping<int, ActivityRecord>> _guardSleepRecords;
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
            List<string> activities = Input.ToList();
            activities.Sort();

            var activityRecords = new List<ActivityRecord>();
            while(activities.Count > 0)
            {
                int nextGuard = activities.FindIndex(1, a => a.Contains("Guard"));
                if (nextGuard == -1)
                    nextGuard = activities.Count;
                activityRecords.Add(new ActivityRecord(activities.Take(nextGuard)));
                activities.RemoveRange(0, nextGuard);
            }

            _guardSleepRecords = activityRecords.GroupBy(a => a.GuardId).ToList();
        }

        public string SolvePart1()
        {
            IGrouping<int, ActivityRecord>? sleepiestGuard =
                _guardSleepRecords.MaxBy(g => g.ToList().Sum(s => s.TimeAsleep)).FirstOrDefault();
            KeyValuePair<int, int> maxMinutes = CalculateMaximumSleepMinute(sleepiestGuard);

            return $"Guard #{sleepiestGuard.Key}: Most asleep at 00:{maxMinutes.Key}.";
        }

        public string SolvePart2()
        {
            var mostAsleep = new Dictionary<int,KeyValuePair<int, int>>();
            foreach (var sleepRecord in _guardSleepRecords)
            {
                KeyValuePair<int, int> maxSleepMinute = CalculateMaximumSleepMinute(sleepRecord);
                mostAsleep.Add(sleepRecord.Key, maxSleepMinute);
            }

            (int guardId, KeyValuePair<int, int> asleepTime) = mostAsleep.MaxBy(v => v.Value.Value).SingleOrDefault();
            
            return $"Guard #{guardId}: Most asleep at 00:{asleepTime.Key}.";
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

            var maxBy = sleepingMinutes.MaxBy(m => m.Value);
            return maxBy.FirstOrDefault();
        }
    }
}