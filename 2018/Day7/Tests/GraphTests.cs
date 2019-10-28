using System;
using System.Linq;
using Day7;
using NUnit.Framework;

namespace Day7Tests
{
    public class Tests
    {
        private DirectedGraph Graph;

        [SetUp]
        public void Setup()
        {
            Graph = new DirectedGraph();
        }

        [Test]
        public void TestTransientReduction()
        {
            var input = new string[] {
                "Step A must be finished before step I can begin",
                "Step A must be finished before step O can begin",
                "Step A must be finished before step F can begin",
                "Step B must be finished before step A can begin",
                "Step B must be finished before step I can begin",
                "Step B must be finished before step F can begin",
                "Step B must be finished before step U can begin",
                "Step B must be finished before step L can begin",
                "Step B must be finished before step G can begin",
                "Step F must be finished before step L can begin",
                "Step F must be finished before step K can begin",
                "Step F must be finished before step H can begin",
                "Step H must be finished before step U can begin",
                "Step H must be finished before step K can begin",
                "Step H must be finished before step M can begin",
                "Step H must be finished before step G can begin",
                "Step H must be finished before step I can begin",
                "Step H must be finished before step O can begin",
                "Step G must be finished before step M can begin",
                "Step G must be finished before step I can begin",
                "Step I must be finished before step O can begin"
            };

            Graph.Build(input);
            Graph.ApplyTransientReduction(Graph.Roots.Select(r => r.Name).ToList());

            Assert.That(Graph.GetStepOrder(), Is.EqualTo("BAFHGIKLMOU"));
        }
    }
}