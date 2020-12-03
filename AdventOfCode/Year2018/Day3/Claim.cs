using System;
using System.Drawing;

namespace AdventOfCode.Year2018.Day3
{
    public class Claim
    {
        public int Id {get; set;}
        public Rectangle ClaimArea {get; set;}

        public bool Overlaps {get; set;}

        public Claim(string claimRepresentation)
        {
            var claimParts = claimRepresentation.Split(' ');
            var claimCoordinates = claimParts[2].Split(',');
            var claimSize = claimParts[3].Split('x');
                
            Id = Int32.Parse(claimParts[0].Trim('#'));
            ClaimArea = new Rectangle(
                Int32.Parse(claimCoordinates[0]), 
                Int32.Parse(claimCoordinates[1].Trim(':')), 
                Int32.Parse(claimSize[0]), 
                Int32.Parse(claimSize[1])
            );

            Overlaps = false;
        }
    }
}