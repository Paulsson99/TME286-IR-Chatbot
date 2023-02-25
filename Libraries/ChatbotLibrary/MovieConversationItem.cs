using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;

namespace ChatbotLibrary
{
    public class MovieConversationItem
    {
        private List<int> utterancesID;
        private List<string> utterances;
        public MovieConversationItem(string rawConversationData) 
        { 
            utterancesID = new List<int>();
            utterances = new List<string>();

            string[] rawConversationDataList = rawConversationData.Split(new char[] { '\t' });

            // The fourth item in this list is an array with utterance ids. The rest of the data can be discarded
            string[] utterancesData = rawConversationDataList[3].Split(new char[] { ' ' });
            // Add every utterance id to the list of ids
            foreach (string rawUtteranceID in utterancesData)
            {
                // Strip away all non numeric characters from the id and convert to an integer
                string utteranceID = Regex.Match(rawUtteranceID, @"\d+").Value;
                int id = int.Parse(utteranceID);
                utterancesID.Add(id);
            }
        }

        public void MapIdToUtterance(MovieLines movieLines)
        {
            foreach (int id in utterancesID)
            {
                utterances.Add(movieLines.GetUtterance(id));
            }
        }

        public List<DialogueCorpusItem> GetSentencePairs()
        {
            // Return all sentence pairs in the move conversation

            List<DialogueCorpusItem> sentencePairs = new List<DialogueCorpusItem>();
            // One less pair then there are utterances
            int numberOfPairs = utterances.Count() - 1;
            for (int i = 0; i < numberOfPairs; i++)
            {
                string query = utterances[i];
                string response = utterances[i + 1];

                // Only add a sentence pair if both the response and query exists
                if (response != null && query != null)
                {
                    DialogueCorpusItem sentencePair = new DialogueCorpusItem(query, response);
                    sentencePairs.Add(sentencePair);
                }
            }
            return sentencePairs;
        }

        public List<string> AsStringArray()
        {
            List<DialogueCorpusItem> sentencePairs = GetSentencePairs();
            List<string> result = new List<string>();
            foreach (DialogueCorpusItem pair in sentencePairs)
            {
                result.Add(pair.AsString());
            }
            return result;
        }
    }
}
