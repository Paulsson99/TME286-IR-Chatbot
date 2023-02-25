using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    [DataContract]
    public class DialogueCorpusItem
    {
        //
        // This class stores a sentence pair as well as the (normalized) TF-IDF embedding
        // for the query sentence (S_1) (you must compute it first, though; see below).
        //
        private string query; // = S_1 in the assignment (used for computing cosine similarity) (need not be a question, though!)
        private string response; // = S_2 in the assignment
        private SparseVector tfIdfVector; // Must be generated - see below.

        private string[] tokens;
        
        // You can add fields here, if you also wish to store the tokenized version of
        // each sentence, or (better) the indices of the words (tokens) from the vocabulary



        // This method returns the sentence pair in the format required for saving the
        // dialogue corpus to file (as specified in the assignment).
        //
        // NOTE! Very important! The query and response sentences may *not* contain any tab (\t) characters,
        // since that character is used for separating the two sentences in the AsString() method.
        public string AsString()
        {
            string itemAsString = query + " \t " + response;
            return itemAsString;
        }

        public DialogueCorpusItem(string query, string response)
        {
            this.query = query;
            this.response = response;
        }

        public void Clean()
        {
            query = CleanString(query);
            response = CleanString(response);
        }

        private string CleanString(string str)
        {
            if (str == null)
            {
                return null;
            }
            string cleanString = str.ToLower();
            cleanString = cleanString.Replace('\t', ' ');
            cleanString = Regex.Replace(cleanString, @"[^\w.!?%,´' ]+", "");
            return cleanString;
        }

        public void Tokenize()
        {
            tokens = query.Split(new char[] { ' ', '.', '!', '?', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void ComputeTFIDFVector(Vocabulary dictionary)
        {
            // Write this method
            // It should generate the (note!) normalized (to unit length)
            // TF-IDF vector for the query sentence (S_1)
            tfIdfVector = new SparseVector();

            foreach (string token in tokens)
            {
                int index = dictionary.Words.BinarySearch(token);
                if (index >= 0)
                {
                    tfIdfVector[index] += dictionary.IDFs[index];
                }
            }
            tfIdfVector.Normalize();
        }
        public SparseVector TFIDFVector
        {
            get { return tfIdfVector; }
            set { tfIdfVector = value; }
        }

        [DataMember]
        public string Query
        {
            get { return query; }
            set { query = value; }
        }

        [DataMember]
        public string Response
        {
            get { return response; }
            set { response = value; }
        }

        public string[] Tokens { get { return tokens; } }
    }
}
