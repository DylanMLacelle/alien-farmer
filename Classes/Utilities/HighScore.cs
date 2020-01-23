using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Classes
{
    /// <summary>
    /// Stores the name of the player and score of a game
    /// </summary>
    public class HighScore : IComparable
    {
        const string DELIMETER = "-";
        int Score { get; set; }
        string Name { get; set; }
        public HighScore(string name, int score)
        {
            Score = score;
            Name = name;
        }

        /// <summary>
        /// Takes a string and converts it into a highscore object
        /// </summary>
        public static HighScore ToHighScore(string str)
        {
            string[] parts = str.Split(new string[] { DELIMETER }, StringSplitOptions.None);
            string name = parts[0];
            int score = int.Parse(parts[1]);
            return new HighScore(name,score);
        }
        /// <summary>
        /// Parses a highscore object to string
        /// </summary>
        public override string ToString()
        {
            return $"{Name.Trim()} - {Score.ToString().Trim()}";
        }
        /// <summary>
        /// Checks score of highscores and sorts
        /// </summary>
        public int CompareTo(object obj)
        {
            int score = (obj as HighScore).Score;
            return Score.CompareTo(score);
        }
    }
}
