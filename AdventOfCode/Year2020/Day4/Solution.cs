using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2020.Day4
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
            
        }

        public string SolvePart1()
        {
            List<Document> documents = new();
            string currentDocument = string.Empty;
            foreach (string line in Input)
            {
                if (line.Length > 0)
                {
                    currentDocument += $" {line}";
                }
                else
                {
                    documents.Add(new Document(currentDocument, false));
                    currentDocument = string.Empty;
                }
            }
            
            // Edge-case: last line is a document
            if (currentDocument.Length > 0)
                documents.Add(new Document(currentDocument, false));
            
            return $"Part 1: {documents.Count(d => d.IsPassport)}";
        }

        public string SolvePart2()
        {
            List<Document> documents = new();
            string currentDocument = string.Empty;
            foreach (string line in Input)
            {
                if (line.Length > 0)
                {
                    currentDocument += $" {line}";
                }
                else
                {
                    documents.Add(new Document(currentDocument, true));
                    currentDocument = string.Empty;
                }
            }
            
            // Edge-case: last line is a document
            if (currentDocument.Length > 0)
                documents.Add(new Document(currentDocument, true));
            
            return $"Part 2: {documents.Count(d => d.IsPassport)}";
        }
    }
}