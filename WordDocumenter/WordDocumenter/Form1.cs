using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WordDocumenter
{
    public partial class Form1 : Form
    {
        private string filePath;

        public Form1()
        {
            InitializeComponent();
            init();
        }

        //Part of initialization

        private void init()
        {
            if (File.Exists((Path.Combine(Environment.CurrentDirectory, @"Data\Setup.txt"))) == true)
            {
                StreamReader newReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"Data\Setup.txt"));
                filePath = newReader.ReadLine();
                txtBoxFilePath.Text = filePath;
                newReader.Close();
                newReader.Dispose();
            }
        }

        //Button events

        private void btnEnter_Click(object sender, EventArgs e)
        {
            
            string romaji = "";
            string english = "";

            if (txtBoxEnglish.Text != "" && txtBoxRomaji.Text != "")
            {
                romaji = txtBoxRomaji.Text;
                english = txtBoxEnglish.Text;

                if (filePath != "")
                {
                    Write(romaji, english);
                }

                else
                {
                    MessageBox.Show("Please provide an output file in the settings menu!");
                }
            }

            else
            {
                MessageBox.Show("Please type something into the textboxes");
            }

            

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            fileDialog.ShowDialog();
            filePath = fileDialog.FileName;
            txtBoxFilePath.Text = filePath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            filePath = txtBoxFilePath.Text;

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, @"Data\Setup.txt")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, @"Data"));
                using (StreamWriter wr = File.CreateText(Path.Combine(Environment.CurrentDirectory, @"Data\Setup.txt")))
                {
                    wr.WriteLine(filePath);
                    wr.Close();
                    wr.Dispose();
                }
            }

            else
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, @"Data\Setup.txt"), filePath);      
            }
        }

        //Helper Methods

        private void Write(string romaji, string english)
        {
            StreamWriter wr;

             if(!File.Exists(filePath))
             {
                 File.Create(filePath);
             }

             using (wr = File.AppendText(filePath))
             {
                 wr.WriteLine(romaji + " = " + english);
             }
            
        }
        
    }
}
