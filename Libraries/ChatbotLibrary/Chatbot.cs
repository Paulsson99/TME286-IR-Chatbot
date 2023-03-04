using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class Chatbot
    {
        protected const int DEFAULT_NUMBER_OF_MATCHES = 5;

        protected DialogueCorpus dialogueCorpus;
        protected Random randomNumberGenerator;
        protected int numberOfMatches = DEFAULT_NUMBER_OF_MATCHES;
        


        public virtual void Initialize()
        {
            randomNumberGenerator = new Random();
        }

        public void SetDialogueCorpus(DialogueCorpus dialogueCorpus)
        {
            this.dialogueCorpus = dialogueCorpus;
        }

        //
        // ToDo: Write the method below
        //
        // Given an input sentence, the method should 
        // compute the cosine similarity between the normalized
        // TF-IDF vector of the test sentence and the normalized
        // TF-IDF vectors of every sentence S_1 (i.e. the Query sentence
        // in each DialogueCorpusItem). The results should then be
        // sorted (you may add a class for this, perhaps with two fields,
        // one field for the index of the sentence pair (in the dialogue corpus), and
        // one field for the cosine similarity. Finally, a sentence pair should
        // be selected (as described below) and the corresponding Response should be returned.
        //
        // Then 
        // (i) sort the results in falling order of cosine similarity,
        // (ii) pick a random sentence pair (index) from among the top five (numberOfMatches, see above).
        // (iii) return sentence S_2 (Response) from the selected pair (DialogueCorpusItem).
        //
        //
        public virtual string GenerateResponse(string inputSentence)
        {
            DialogueCorpusItem userInput = new DialogueCorpusItem(inputSentence, null);
            userInput.Clean();
            userInput.Tokenize();
            userInput.ComputeTFIDFVector(dialogueCorpus.Vocabulary);

            // Calculate the cosine similarity between all input sentences
            List<(int index, double gamma)> cosineSimilarity = new List<(int index, double gamma)>();
            for (int i = 0; i < dialogueCorpus.Size; i++)
            {
                SparseVector TFIDFVector = dialogueCorpus.ItemList[i].TFIDFVector;
                double gamma = userInput.TFIDFVector.Dot(TFIDFVector);
                cosineSimilarity.Add((i, gamma));
            }

            // Sort in descending based on the cosine similarity
            cosineSimilarity.Sort((x, y) => x.gamma.CompareTo(y.gamma));
            cosineSimilarity.Reverse();

            // Select one of the top items randomly
            int[] topIndex = new int[numberOfMatches];
            for (int i = 0; i < numberOfMatches; i++)
            {
                topIndex[i] = cosineSimilarity[i].index;
            }
            int randomIndex = randomNumberGenerator.Next(numberOfMatches);
            int selectedIndex = topIndex[randomIndex];
            DialogueCorpusItem selectedItem = dialogueCorpus.ItemList[selectedIndex];
            return selectedItem.Response;
        }

    }
}
