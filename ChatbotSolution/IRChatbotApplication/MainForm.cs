using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatbotLibrary;

namespace IRChatbotApplication
{
    public partial class MainForm : Form
    {
        private DialogueCorpus corpus = null; // The dialogue corpus, consisting of sentence pairs.
        private Chatbot chatbot;

        // Add fields here as needed, e.g. for the raw data.

        public MainForm()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                inputTextBox.InputReceived += new EventHandler<StringEventArgs>(HandleInputReceived);
            }
        }

        private void loadRawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Write code here for loading and parsing raw data.

                generateDialogueCorpusButton.Enabled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //
        // This method you get for free ... :)
        private void generateChatBotButton_Click(object sender, EventArgs e)
        {
            generateChatBotButton.Enabled = false;
            chatbot = new Chatbot();
            chatbot.SetDialogueCorpus(corpus);
            inputTextBox.Enabled = true;
            mainTabControl.SelectedTab = chatTabPage;
        }

        //
        // To do: Write the method below.
        //
        // This method is called whenever the user enters text (= hits return) in
        // the input text box.
        // 
        // The input sentence should be passed to the chatbot, which then generates
        // an output. Both the (user) input and the (chatbot) output should be displayed in the
        // listbox (listbox.Items.Insert(0, ...), so that one can follow the
        // entire dialogue
        // 
        private void HandleInputReceived(object sender, StringEventArgs e)
        {
            string inputSentence = e.Information;

            // Add code here ...
        }

        // To do: Write this method. Hint: Use the StreamWriter class
        // as well as the AsString() method from the DialogueCorpusItem class.
        private void saveDialogueCorpusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This method should save the dialogue corpus, with one exchange per
            // row, i.e. S_1, tab-character ("\t"), S_2. As mentioned in the assignment
            // you must hand in this file *in addition* to the raw data.
        }

        private void generateDialogueCorpusButton_Click(object sender, EventArgs e)
        {
            corpus = new DialogueCorpus();
            // Add methods (in the DialogueCorpus class) for processing the raw
            // data, e.g. corpus.Process(rawData) ...


            generateChatBotButton.Enabled = true;
            saveDialogueCorpusToolStripMenuItem.Enabled = true;
        }
    }
}
