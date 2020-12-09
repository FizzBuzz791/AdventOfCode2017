using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutions.Day4
{
    public class ActivityRecord
    {
        public int GuardId { get; }
        private DateTime StartsShift { get; }
        public IList<DateTime> FallsAsleep { get; } = new List<DateTime>();
        public IList<DateTime> WakesUp { get; } = new List<DateTime>();

        public int TimeAsleep => FallsAsleep.Select((t, i) => WakesUp[i].Minute - t.Minute).Sum();

        public ActivityRecord(IEnumerable<string> activityRecords)
        {
            foreach (string activityRecord in activityRecords)
            {
                string[] activityParts = activityRecord.Split(']');
                string timestamp = activityParts[0];
                string information = activityParts[1];

                if (information.Contains("Guard"))
                {
                    GuardId = int.Parse(information.Trim(' ').Split(" ")[1].Trim('#'));
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

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Guard ID: {GuardId}");
            builder.AppendLine($"Starts Shift: {StartsShift}");
            builder.AppendLine($"Time Asleep: {TimeAsleep}");

            return builder.ToString();
        }
    }
}