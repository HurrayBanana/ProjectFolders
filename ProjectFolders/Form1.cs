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
    public partial class Form1 : Form
    {
        private string filename;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            Text = "Project Folder Generator " + "-> Select file containing folder names";
            if (o.ShowDialog() == DialogResult.OK)
            {
                OutputFile(o.FileName);

                FolderBrowserDialog f = new FolderBrowserDialog();
                Text = "Project Folder Generator " + "-> Select location to create folder structure";

                if (f.ShowDialog() == DialogResult.OK)
                {
                    Generate(o.FileName, f.SelectedPath);
                }
            }
        }

        private void OutputFile(string fileName)
        {
            richTextBox1.AppendText("\nReading from:" + fileName);
            richTextBox1.AppendText("contains....\n");
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    richTextBox1.AppendText(sr.ReadLine() + "\n");
                }
            }

        }

        private void Generate(string fileName, string selectedPath)
        {
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
