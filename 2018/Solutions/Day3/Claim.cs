using System.Drawing;

namespace Solutions.Day3
{
    public class Claim
    {
        public int Id { get; }
        public Rectangle ClaimArea { get; }

        public Claim(string claimRepresentation)
        {
            string[] claimParts = claimRepresentation.Split(' ');
            string[] claimCoordinates = claimParts[2].Split(',');
            string[] claimSize = claimParts[3].Split('x');

            Id = int.Parse(claimParts[0].Trim('#'));
            ClaimArea = new Rectangle(
                int.Parse(claimCoordinates[0]),
                int.Parse(claimCoordinates[1].Trim(':')),
                int.Parse(claimSize[0]),
                int.Parse(claimSize[1])
            );
        }
    }
}