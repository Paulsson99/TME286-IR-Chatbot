using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ChatbotLibrary;

namespace IRChatbotApplication
{
    public partial class MainForm : Form
    {
        private const string TSV_FILTER = "tab-separated values (*.tsv)|*.tsv";
        private const string TXT_FILTER = "Text files (*.txt)|*.txt";

        private DialogueCorpus corpus = null; // The dialogue corpus, consisting of sentence pairs.
        private Chatbot chatbot;

        // Add fields here as needed, e.g. for the raw data.
        MovieConversations movieConversations;
        MovieLines movieLines;

        // Threads for slow processes
        private Thread generateDialogueCorpusThread;

        public MainForm()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                inputTextBox.InputReceived += new EventHandler<StringEventArgs>(HandleInputReceived);
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
            chatbot.Initialize();
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

            string response = chatbot.GenerateResponse(inputSentence);

            dialogueListBox.Items.Insert(0, inputSentence);
            dialogueListBox.Items.Insert(0, response);
        }

        // To do: Write this method. Hint: Use the StreamWriter class
        // as well as the AsString() method from the DialogueCorpusItem class.
        private void saveDialogueCorpusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This method should save the dialogue corpus, with one exchange per
            // row, i.e. S_1, tab-character ("\t"), S_2. As mentioned in the assignment
            // you must hand in this file *in addition* to the raw data.
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = TXT_FILTER;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter dataWriter = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (DialogueCorpusItem sentencePair in corpus.ItemList)
                        {
                            dataWriter.WriteLine(sentencePair.AsString());
                        }
                        dataWriter.Close();
                    }
                }
            }
        }

        private void generateDialogueCorpusButton_Click(object sender, EventArgs e)
        {
            generateDialogueCorpusButton.Enabled = false;
            generateDialogueCorpusThread = new Thread(new ThreadStart(() => generateDialogueCorpus()));
            generateDialogueCorpusThread.IsBackground = true;
            generateDialogueCorpusThread.Start();
        }

        private void generateDialogueCorpus()
        {
            corpus = new DialogueCorpus();
            // Add methods (in the DialogueCorpus class) for processing the raw
            // data, e.g. corpus.Process(rawData) ...

            corpus.Process(movieLines, movieConversations);

            ThreadSafeToggleButtonEnabled(generateChatBotButton, true);
            ThreadSafeToggleMenuItemEnabled(saveDialogueCorpusToolStripMenuItem, true);
        }

        private void ThreadSafeToggleButtonEnabled(ToolStripButton button, Boolean enabled)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => button.Enabled = enabled)); }
            else { button.Enabled = enabled; }
        }

        private void ThreadSafeToggleMenuItemEnabled(ToolStripMenuItem menuItem, Boolean enabled)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => menuItem.Enabled = enabled)); }
            else { menuItem.Enabled = enabled; }
        }

        private void loadMoveLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TSV_FILTER;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportMoveLines(openFileDialog.FileName);
                }

                if (movieLines != null && movieConversations != null)
                {   
                    // Enable next step when all the raw data is read
                    generateDialogueCorpusButton.Enabled = true;
                }
            }
        }

        private void ImportMoveLines(string fileName)
        {
            movieLines = new MovieLines();
            using (StreamReader dataReader = new StreamReader(fileName))
            {
                while (!dataReader.EndOfStream)
                {
                    string rawLine = dataReader.ReadLine();
                    movieLines.Add(rawLine);
                }
                dataReader.Close();
            }
        }

        private void loadConversationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TSV_FILTER;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportMoveConversations(openFileDialog.FileName);
                }

                if (movieLines != null && movieConversations != null)
                {
                    // Enable next step when all the raw data is read
                    generateDialogueCorpusButton.Enabled = true;
                }
            }
        }

        private void ImportMoveConversations(string fileName)
        {
            movieConversations = new MovieConversations();
            using (StreamReader dataReader = new StreamReader(fileName))
            {
                while (!dataReader.EndOfStream)
                {
                    string rawLine = dataReader.ReadLine();
                    movieConversations.Add(rawLine);
                }
                dataReader.Close();
            }
        }
    }
}
