using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020.Day4
{
    public record Document
    {
        private string? BirthYear { get; }
        private string? IssueYear { get; }
        private string? ExpirationYear { get; }
        private string? Height { get; }
        private string? HairColor { get; }
        private string? EyeColor { get; }
        private string? PassportId { get; }

        public Document(string passport, bool validate)
        {
            string[] passportParts = passport.Split(" ");
            foreach (var part in passportParts)
            {
                string[] keyValuePair = part.Split(":");
                switch (keyValuePair[0])
                {
                    case "byr":
                    {
                        if (validate)
                        {
                            bool parseResult = int.TryParse(keyValuePair[1], out int birthYear);
                            if (parseResult && birthYear >= 1920 && birthYear <= 2002)
                                BirthYear = keyValuePair[1];
                        }
                        else
                        {
                            BirthYear = keyValuePair[1];
                        }

                        break;
                    }
                    case "iyr":
                    {
                        if (validate)
                        {
                            bool parseResult = int.TryParse(keyValuePair[1], out int issueYear);
                            if (parseResult && issueYear >= 2010 && issueYear <= 2020)
                                IssueYear = keyValuePair[1];
                        }
                        else
                        {
                            IssueYear = keyValuePair[1];
                        }

                        break;
                    }
                    case "eyr":
                    {
                        if (validate)
                        {
                            bool parseResult = int.TryParse(keyValuePair[1], out int expirationYear);
                            if (parseResult && expirationYear >= 2020 && expirationYear <= 2030)
                                ExpirationYear = keyValuePair[1];
                        }
                        else
                        {
                            ExpirationYear = keyValuePair[1];
                        }

                        break;
                    }
                    case "hgt":
                    {
                        if (validate)
                        {
                            bool parseResult = int.TryParse(keyValuePair[1].Substring(0, keyValuePair[1].Length - 2),
                                out int height);
                            if (parseResult)
                            {
                                if (keyValuePair[1].EndsWith("cm") && height >= 150 && height <= 193)
                                    Height = keyValuePair[1];
                                else if (keyValuePair[1].EndsWith("in") && height >= 59 && height <= 76)
                                    Height = keyValuePair[1];
                            }
                        }
                        else
                        {
                            Height = keyValuePair[1];
                        }

                        break;
                    }
                    case "hcl":
                    {
                        if (validate)
                        {
                            var regex = new Regex("^#[0-9a-f]{6}$");
                            if (regex.IsMatch(keyValuePair[1]))
                                HairColor = keyValuePair[1];
                        }
                        else
                        {
                            HairColor = keyValuePair[1];
                        }

                        break;
                    }
                    case "ecl":
                    {
                        if (validate)
                        {
                            if (new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(keyValuePair[1]))
                                EyeColor = keyValuePair[1];
                        }
                        else
                        {
                            EyeColor = keyValuePair[1];
                        }

                        break;
                    }
                    case "pid":
                    {
                        if (validate)
                        {
                            if (keyValuePair[1].Length == 9 && int.TryParse(keyValuePair[1], out int _))
                                PassportId = keyValuePair[1];
                        }
                        else
                        {
                            PassportId = keyValuePair[1];
                        }

                        break;
                    }
                    case "cid":
                    {
                        break;
                    }
                }
            }
        }

        public bool IsPassport => !string.IsNullOrWhiteSpace(BirthYear) && 
                                  !string.IsNullOrWhiteSpace(IssueYear) &&
                                  !string.IsNullOrWhiteSpace(ExpirationYear) && 
                                  !string.IsNullOrWhiteSpace(Height) &&
                                  !string.IsNullOrWhiteSpace(HairColor) && 
                                  !string.IsNullOrWhiteSpace(EyeColor) &&
                                  !string.IsNullOrWhiteSpace(PassportId);
    }
}