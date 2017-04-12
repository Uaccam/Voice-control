using System;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    public partial class Form1 : Form
    {
           
        public Form1()
        {
            InitializeComponent();
            Text = "Shutdown";
        }

        static Label l;
        static bool Shutdown = false;
        

        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        { 
            if (Shutdown)
            {

                if (e.Result.Text == "enough" && e.Result.Confidence > 0.7)
                {
                    l.Text = "Shutting down";
                    System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            l = label1;

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-us");
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);


            Choices keyword = new Choices();
            keyword.Add(new string[] { "enough" });


            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(keyword);


            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shutdown = true;
            l.Text = "Listening...";
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This little program does only one thing: \nit turns off your computer. \nYou just need to say 'Enough'.");
        }
    }
}
