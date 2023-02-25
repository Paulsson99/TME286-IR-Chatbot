using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ChatbotLibrary
{
    public class MovieLines
    {
        private List<(int id, string utterance)> movieLines;

        private MovieUtteranceComparer movieUtteranceComparer;
        public MovieLines() 
        {
            movieLines = new List<(int id, string utterance)>();
            movieUtteranceComparer = new MovieUtteranceComparer();
        }

        public void Add(string rawLine)
        {
            // Extract the line id and utterance from the raw line
            string[] rawList = rawLine.Split(new char[] { '\t' });
            string rawID = rawList[0];
            string utterance = rawList[4];
            string cleanID = Regex.Match(rawID, @"\d+").Value;
            int id = int.Parse(cleanID);
            (int id, string utterance) moveLine = (id, utterance);
            movieLines.Add(moveLine);
        }

        public void Process()
        {
            // Remove long sentences
            List<(int id, string utterance)> tempMovieLines = new List<(int id, string utterance)>();
            foreach ((int id, string utterance) movieLine in movieLines)
            {
                // The average english word lenght is 4.7
                // So if I want to keep only sentences with 15-20 words
                // I should keep all sentences with ~100 characters
                if (movieLine.utterance.Length > 100)
                {
                    // Do not keep this sentence
                    continue;
                }
                tempMovieLines.Add(movieLine);
            }
            movieLines = tempMovieLines;

            // Sort the move lines in order of theire ids
            movieLines.Sort(movieUtteranceComparer);
        }

        public string GetUtterance(int id)
        {
            (int, string) tempItem = (id, null);
            int index = movieLines.BinarySearch(tempItem, movieUtteranceComparer);
            if (index < 0)
            {
                // Not found
                return null;
            }
            return movieLines[index].utterance;
        }
    }
}
