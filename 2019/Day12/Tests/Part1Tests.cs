using System.Collections.Generic;
using System.Linq;
using Day12;
using NUnit.Framework;
using Shouldly;

namespace Day12Tests
{
    public class Tests
    {
        [TestCaseSource(nameof(VelocityCases))]
        public void VelocityIsAppliedCorrectly(Moon moon, Point3D velocity, Point3D expectedPosition)
        {
            // Arrange
            moon.Velocity.X = velocity.X;
            moon.Velocity.Y = velocity.Y;
            moon.Velocity.Z = velocity.Z;

            // Act
            moon.ApplyVelocity();

            // Assert
            moon.Position.ShouldBe(expectedPosition);
        }

        private static IEnumerable<object[]> VelocityCases()
        {
            yield return new object[] { new Moon("<x=1, y=2, z=3>"), new Point3D(-2, 0, 3), new Point3D(-1, 2, 6) };
        }

        [TestCaseSource(nameof(GravityCases))]
        public void GravityIsAppliedCorrectly(Moon one, Moon two, Point3D expectedVelocityOne, Point3D expectedVelocityTwo)
        {
            // Arrange

            // Act
            Program.ApplyGravity(one, two);

            // Assert
            one.Velocity.ShouldBe(expectedVelocityOne);
            two.Velocity.ShouldBe(expectedVelocityTwo);
        }

        private static IEnumerable<object[]> GravityCases()
        {
            yield return new object[] { new Moon("<x=1, y=2, z=3>"), new Moon("<x=5,y=-2,z=3>"), new Point3D(1, -1, 0), new Point3D(-1, 1, 0) };
        }

        [TestCaseSource(nameof(MotionCases))]
        public void SimulatesMotionCorrectly(string input, int steps, string[] expectedStates)
        {
            // Arrange

            // Act
            var result = Program.Part1(input, steps);

            // Assert
            result.Select(m => m.ToString()).ShouldBe(expectedStates);
        }

        private static IEnumerable<object[]> MotionCases()
        {
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                0,
                new string[] {
                    "pos=<x=-1, y= 0, z= 2>, vel=<x= 0, y= 0, z= 0>",
                    "pos=<x= 2, y=-10, z=-7>, vel=<x= 0, y= 0, z= 0>",
                    "pos=<x= 4, y=-8, z= 8>, vel=<x= 0, y= 0, z= 0>",
                    "pos=<x= 3, y= 5, z=-1>, vel=<x= 0, y= 0, z= 0>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                1,
                new string[] {
                    "pos=<x= 2, y=-1, z= 1>, vel=<x= 3, y=-1, z=-1>",
                    "pos=<x= 3, y=-7, z=-4>, vel=<x= 1, y= 3, z= 3>",
                    "pos=<x= 1, y=-7, z= 5>, vel=<x=-3, y= 1, z=-3>",
                    "pos=<x= 2, y= 2, z= 0>, vel=<x=-1, y=-3, z= 1>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                2,
                new string[] {
                    "pos=<x= 5, y=-3, z=-1>, vel=<x= 3, y=-2, z=-2>",
                    "pos=<x= 1, y=-2, z= 2>, vel=<x=-2, y= 5, z= 6>",
                    "pos=<x= 1, y=-4, z=-1>, vel=<x= 0, y= 3, z=-6>",
                    "pos=<x= 1, y=-4, z= 2>, vel=<x=-1, y=-6, z= 2>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                3,
                new string[] {
                    "pos=<x= 5, y=-6, z=-1>, vel=<x= 0, y=-3, z= 0>",
                    "pos=<x= 0, y= 0, z= 6>, vel=<x=-1, y= 2, z= 4>",
                    "pos=<x= 2, y= 1, z=-5>, vel=<x= 1, y= 5, z=-4>",
                    "pos=<x= 1, y=-8, z= 2>, vel=<x= 0, y=-4, z= 0>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                4,
                new string[] {
                    "pos=<x= 2, y=-8, z= 0>, vel=<x=-3, y=-2, z= 1>",
                    "pos=<x= 2, y= 1, z= 7>, vel=<x= 2, y= 1, z= 1>",
                    "pos=<x= 2, y= 3, z=-6>, vel=<x= 0, y= 2, z=-1>",
                    "pos=<x= 2, y=-9, z= 1>, vel=<x= 1, y=-1, z=-1>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                5,
                new string[] {
                    "pos=<x=-1, y=-9, z= 2>, vel=<x=-3, y=-1, z= 2>",
                    "pos=<x= 4, y= 1, z= 5>, vel=<x= 2, y= 0, z=-2>",
                    "pos=<x= 2, y= 2, z=-4>, vel=<x= 0, y=-1, z= 2>",
                    "pos=<x= 3, y=-7, z=-1>, vel=<x= 1, y= 2, z=-2>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                6,
                new string[] {
                    "pos=<x=-1, y=-7, z= 3>, vel=<x= 0, y= 2, z= 1>",
                    "pos=<x= 3, y= 0, z= 0>, vel=<x=-1, y=-1, z=-5>",
                    "pos=<x= 3, y=-2, z= 1>, vel=<x= 1, y=-4, z= 5>",
                    "pos=<x= 3, y=-4, z=-2>, vel=<x= 0, y= 3, z=-1>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                7,
                new string[] {
                    "pos=<x= 2, y=-2, z= 1>, vel=<x= 3, y= 5, z=-2>",
                    "pos=<x= 1, y=-4, z=-4>, vel=<x=-2, y=-4, z=-4>",
                    "pos=<x= 3, y=-7, z= 5>, vel=<x= 0, y=-5, z= 4>",
                    "pos=<x= 2, y= 0, z= 0>, vel=<x=-1, y= 4, z= 2>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                8,
                new string[] {
                    "pos=<x= 5, y= 2, z=-2>, vel=<x= 3, y= 4, z=-3>",
                    "pos=<x= 2, y=-7, z=-5>, vel=<x= 1, y=-3, z=-1>",
                    "pos=<x= 0, y=-9, z= 6>, vel=<x=-3, y=-2, z= 1>",
                    "pos=<x= 1, y= 1, z= 3>, vel=<x=-1, y= 1, z= 3>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                9,
                new string[] {
                    "pos=<x= 5, y= 3, z=-4>, vel=<x= 0, y= 1, z=-2>",
                    "pos=<x= 2, y=-9, z=-3>, vel=<x= 0, y=-2, z= 2>",
                    "pos=<x= 0, y=-8, z= 4>, vel=<x= 0, y= 1, z=-2>",
                    "pos=<x= 1, y= 1, z= 5>, vel=<x= 0, y= 0, z= 2>"
                }
            };
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                10,
                new string[] {
                    "pos=<x= 2, y= 1, z=-3>, vel=<x=-3, y=-2, z= 1>",
                    "pos=<x= 1, y=-8, z= 0>, vel=<x=-1, y= 1, z= 3>",
                    "pos=<x= 3, y=-6, z= 1>, vel=<x= 3, y= 2, z=-3>",
                    "pos=<x= 2, y= 0, z= 4>, vel=<x= 1, y=-1, z=-1>"
                }
            };
        }

        [TestCaseSource(nameof(EnergyCases))]
        public void CalculatesEnergyCorrectly(string input, int steps, int[] expectedPotentialEnergy, int[] expectedKineticEnergy)
        {
            // Arrange

            // Act
            var result = Program.Part1(input, steps);

            // Assert
            result.Select(m => m.PotentialEnergy).ShouldBe(expectedPotentialEnergy);
            result.Select(m => m.KineticEnergy).ShouldBe(expectedKineticEnergy);
        }

        private static IEnumerable<object[]> EnergyCases()
        {
            yield return new object[] { "<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>",
                10,
                new int[] { 6, 9, 10, 6 },
                new int[] { 6, 5, 8, 3 }
            };
        }
    }
}