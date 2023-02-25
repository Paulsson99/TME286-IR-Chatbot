using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class DialogueCorpus
    {
        private List<DialogueCorpusItem> itemList;
        private Vocabulary vocabulary;

        public DialogueCorpus()
        {
            itemList = new List<DialogueCorpusItem>();
        }

        // To do: Write the Process() method below.

        // The method will be quite complex, so you may (should, probably) implement it
        // in parts, adding more methods as needed, or perhaps more classes.
        // Note, however, that some methods (which you should use) have been added already (see below).
       
        // You may, for example, want to add a class for preprocessing (cleaning) the text, and
        // another class for identifying the text that should be kept (following the
        // three conditions above). For any class that you add, make sure to put it
        // in a *separate* file, named accordingly. Do *not* place multiple classes in
        // the same file.

        // (i)   Preprocess the data, i.e. remove (some) special characters, turn the text to lower-case, and so on.
        // (ii)  Tokenize and generate the vocabulary with the (distinct) tokens from the raw data set(s).
        // (iii) Build sentence pairs (S_1,S_2), making sure that S_2 really follows S_1 in the dialogue.
        //       You can do that by, for example, considering exchanges such as
        //       SpeakerA: ... 
        //       SpeakerB: ...
        //       SpeakerA: ...
        //       In that case, it is very likely that the first two sentences form a pair. If, instead, a third
        //       speaker (SpeakerC) were to give the third utterance (intead of SpeakerA), it is far from certain
        //       that the first two sentences form a valid pair, and so on. In other words: To identify valid
        //       pairs, you must consider exchanges involving *two* speakers, and omitting the final sentence.
        //       Note that you should also discard exchanges involving *long* sentences - see the assignment PDF.
        //
        //       NOTE: An easier way is to use the movie_conversations.txt file that is released along with
        //       the list of sentences (movie_lines.txt) for the Cornell Movie Dialog Corpus.
        //       This file does precisely what is described above! (You just have to figure out how ...)
        // 
        // (iv)  Compute and store (e.g. in the vocabulary) the IDF for each word in the vocabulary, treating 
        //       sentences as documents.
        // (v)   Compute and store normalized TF-IDF vectors for each sentence S_1 of every sentence pair. 

        // If you like, you may also add code for showing the dialogue corpus on-screen, in
        // the dialogueCorpusListBox. However, since the dialogue corpus will be very large,
        // you should only display (say) the first 1000 sentence pairs or so.
        // Here, you can use the AsString() method in the DialogueCorpusItem.

        //  The end result of calling the Process() method should be a list of DialogueCorpusItems added
        //  to the itemList. 
        //
        public void Process(string rawData)
        {
            // Add (a lot of ...) code here, possibly calling other methods (e.g. the ones below).
        }

        // Should be called from Process(...)
        public void GenerateVocabulary()
        {

        }


        // Should also be called from Process(...)
        //
        // This method should compute the IDF for each word in the vocabulary
        // Note that, here, each sentence (S_1) of a pair counts as a *document*.
        // Once you have preprocessed all the data files, you will have a set of
        // sentence pairs (each stored as a DialogueCorpusItem in the dialogue corpus)
        // where the sentence S_1 (query) forms a document, for the purpose of the IDF
        // calculation here.
        //
        // Note: This method assumes that GenerateVocabulary() has been executed first.
        public void ComputeIDFs()
        {
            // Add code here ...
        }


        // Note: The IDFs must be computed before calling this method, of course.
        public void ComputeTFIDFVectors()
        {
            foreach (DialogueCorpusItem item in itemList)
            {
                item.ComputeTFIDFVector(vocabulary);
            }
        }

        public List<DialogueCorpusItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public Vocabulary Vocabulary
        {
            get { return vocabulary; }
            set { vocabulary = value; }
        }
    }
}
