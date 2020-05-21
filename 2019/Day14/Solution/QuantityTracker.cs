namespace Day14
{
    public partial class Program
    {
        public class QuantityTracker
        {
            public int Actual { get; set; }
            public int Extra { get; set; }

            public override string ToString() => $"Actual: {Actual}, Extra: {Extra}";
        }
    }
}
