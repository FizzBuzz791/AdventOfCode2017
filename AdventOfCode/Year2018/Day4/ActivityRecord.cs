using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Year2018.Day4
{
    public class ActivityRecord
    {
        public int GuardId { get; set; }
        public DateTime StartsShift { get; }
        public IList<DateTime> FallsAsleep { get; } = new List<DateTime>();
        public IList<DateTime> WakesUp { get; } = new List<DateTime>();

        public int TimeAsleep
        {
            get
            {
                return FallsAsleep.Select((t, i) => WakesUp[i].Minute - t.Minute).Sum();
            }
        }

        public ActivityRecord(IEnumerable<string> activityRecords)
        {
            foreach (string activityRecord in activityRecords)
            {
                string[] activityParts = activityRecord.Split(']');
                string timestamp = activityParts[0];
                string information = activityParts[1];

                if (information.Contains("Guard"))
                {
                    GuardId = int.Parse(information.Substring(information.IndexOf('#') + 1, 4));
                    StartsShift = GetTimestamp(timestamp);
                }
                else if (information.Contains("wakes"))
                {
                    WakesUp.Add(GetTimestamp(timestamp));
                }
                else if (information.Contains("falls"))
                {
                    FallsAsleep.Add(GetTimestamp(timestamp));
                }
            }
        }

        private static DateTime GetTimestamp(string timestamp) =>
            DateTime.Parse(timestamp.Replace("[", string.Empty).Replace("]", string.Empty));

        public override string ToString() => new StringBuilder().AppendLine($"Guard ID: {GuardId}")
            .AppendLine($"Starts Shift: {StartsShift}").AppendLine($"Time Asleep: {TimeAsleep}").ToString();
    }
}