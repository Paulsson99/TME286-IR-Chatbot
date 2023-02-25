using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    [DataContract]
    public class Vocabulary
    {
        // Write this class - it should hold the vocabulary, i.e. the list of all distinct words (tokens) sorted
        // in alphabetical order, along with the IDFs, computed from the raw data set.

        // You may use a list of WordData instances if you wish (where each WordData instance would hold information
        // about one word).
        private List<string> words;
        private List<double> idfs;

        private List<string[]> allSentences;

        public Vocabulary()
        {
            words = new List<string>();
            idfs = new List<double>();

            allSentences = new List<string[]>();
        }

        public void Add(string[] tokenizedSentence)
        {
            allSentences.Add(tokenizedSentence);
            words.AddRange(tokenizedSentence);
        }

        public void Process()
        {
            // Get a list of distinct tokens sorted in alphabetical order
            words = words.Distinct().ToList();
            words.Sort();

            int totalNumberOfSentences = allSentences.Count;
            int numberOfWords = words.Count;

            int[] inNumberOfSentences = new int[words.Count];

            // Count how many sentences each word appers in
            foreach (string[] sentence in allSentences)
            {
                string[] distinctSentence = sentence.Distinct().ToArray();
                foreach (string word in distinctSentence)
                {
                    int index = words.BinarySearch(word);
                    inNumberOfSentences[index]++;
                }
            }

            // Calculate the IDF
            for (int i = 0; i < numberOfWords; i++)
            {
                int n = inNumberOfSentences[i];
                double idf = -Math.Log10((double)n / (double)totalNumberOfSentences);
                idfs.Add(idf);
            }
        }

        public List<string> Words 
        {
            get { return words; }
        }

        public List<double> IDFs
        {
            get { return idfs; }
        }

        public int Size
        {
            get { return words.Count; }
        }
    }
}
