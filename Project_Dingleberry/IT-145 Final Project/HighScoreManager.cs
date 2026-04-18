// Project: John Stick
// Course: IT145 Foundations of Application Development
// Authors: Murdock MacAskill, Beth, and Landen
// File: HighScoreManager.cs
// Purpose: Saves, reads, and sorts high score data for the game.
// Date: 04/17/2026

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_Dingleberry
{
    public static class HighScoreManager
    {
        private const string FilePath = "highscores.txt";

        // Appends a new score to the text file
        public static void SaveScore(int level, string time)
        {
            // Format: Level,Time,Date
            string entry = $"{level},{time},{DateTime.Now:MM/dd/yyyy}";
            File.AppendAllLines(FilePath, new[] { entry });
        }

        // Retrieves, sorts, and formats the top 10 scores
        public static List<string> GetTopScores()
        {
            if (!File.Exists(FilePath))
            {
                return new List<string> { "No scores yet! Get in the Battle Zone!" };
            }

            var lines = File.ReadAllLines(FilePath);
            var sortedScores = lines
                .Select(line => line.Split(','))
                .Where(parts => parts.Length == 3)
                // Sort primarily by highest level, then by longest survival time
                .OrderByDescending(parts => int.Parse(parts[0]))
                .ThenByDescending(parts => parts[1])
                .Select((parts, index) => $"#{index + 1} - Level: {parts[0]} | Time: {parts[1]} | Date: {parts[2]}")
                .Take(10) // Only keep the top 10
                .ToList();

            return sortedScores.Count > 0 ? sortedScores : new List<string> { "No scores yet! Get in the Battle Zone!" };
        }
    }
}