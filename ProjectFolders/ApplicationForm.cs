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

namespace ProjectFolders
{
    public partial class ApplicationForm : Form
    {
        State nextAction;
        private OpenFileDialog oFilePicker;
        private FolderBrowserDialog fDestination;

        public ApplicationForm()
        {
            InitializeComponent();
        }

        private void bOperate_Click(object sender, EventArgs e)
        {
            switch (nextAction)
            {
                case State.loadFolderFile:
                    GetFolderFile();
                    break;
                case State.selectingDestination:
                    GetDestination();
                    break;
                case State.writingFolders:
                    Generate(oFilePicker.FileName, fDestination.SelectedPath);
                    break;
            }


        }

        private void GetDestination()
        {
            fDestination = new FolderBrowserDialog();
            Text = "Project Folder Generator " + "-> Select location to create folder structure";

            if (fDestination.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.AppendText("\nWriting to folder:" + fDestination.SelectedPath);
                ShowNewEntries();
                nextAction = State.writingFolders;
                bOperate.Text = "Write Folder Structure";
                bCancel.Show();
            }
            
        }

        private void ShowNewEntries()
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }

        private void GetFolderFile()
        {
            richTextBox1.Text = "";
            oFilePicker = new OpenFileDialog();
            Text = "Project Folder Generator " + "-> Select file containing folder names";
            if (oFilePicker.ShowDialog() == DialogResult.OK)
            {
                OutputFile(oFilePicker.FileName);
                nextAction = State.selectingDestination;
                bOperate.Text = "Select Destination Folder";
                bCancel.Show();
            }
        }
        private void OutputFile(string fileName)
        {
            richTextBox1.AppendText("\nReading from:" + fileName);
            richTextBox1.AppendText(" contains....\n");
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    richTextBox1.AppendText(sr.ReadLine() + "\n");
                }
            }
            richTextBox1.AppendText("...finished reading file (scroll up to check)\n");

            ShowNewEntries();

        }

        private void Generate(string fileName, string selectedPath)
        {
            bCancel.Hide();
            richTextBox1.AppendText("\nWriting folders from:" + fileName);

            using (StreamReader sr = new StreamReader(fileName))
            {
                string foldername;
                while (!sr.EndOfStream)
                {
                    foldername = Path.Combine(selectedPath, sr.ReadLine());
                    //foldername = selectedPath + "\\" + sr.ReadLine();
                    //Directory.CreateDirectory(foldername);
                    richTextBox1.AppendText("\n" + foldername + " <-- created");
                }
                richTextBox1.AppendText("\n...Writing finished");

            }
            nextAction = State.loadFolderFile;
            bOperate.Text = "Select Folder File";
            ShowNewEntries();

        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            nextAction = State.loadFolderFile;
            bCancel.Hide();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            nextAction = State.loadFolderFile;
            bOperate.Text = "Select Folder File";
            richTextBox1.AppendText("\n...process aborted");

            bCancel.Hide();
        }
    }
}
