namespace AdventOfCode.Year2018.Day7
{
    public class Worker
    {
        public Node? WorkingOn { get; private set; }
        private int TimeRemaining { get; set; }
        public bool IsFree => WorkingOn == null;

        public void AssignStep(Node step)
        {
            WorkingOn = step;
            TimeRemaining = step.Time;
        }

        public Node? DoWork()
        {
            Node? completedStep = null;

            TimeRemaining--;

            if (TimeRemaining == 0)
            {
                completedStep = WorkingOn;
                WorkingOn = null;
            }

            return completedStep;
        }
    }
}