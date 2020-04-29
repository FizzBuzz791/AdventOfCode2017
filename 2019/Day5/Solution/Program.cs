using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NAoCHelper;

namespace Day5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(GetCookie());
            var puzzle = new Puzzle(user, 2019, 5);
            var input = puzzle.GetInputAsync().Result;

        }

        public static string GetCookie()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", false, true)
                            .AddUserSecrets("9f311380-1bb8-4ef6-8431-101bfeed90df");
            var config = builder.Build();

            var secretValues = config.GetSection("Secrets").GetChildren();
            return secretValues.FirstOrDefault(s => s.Key == "Cookie")?.Value ?? string.Empty;
        }
    }
}
