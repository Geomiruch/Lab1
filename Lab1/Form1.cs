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

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                richTextBox2.Clear();
                richTextBox3.Clear();
                StreamReader sr = File.OpenText(openFileDialog1.FileName);

                string line = null;
                line = sr.ReadLine();

                while (line != null)
                {
                    richTextBox1.AppendText(line);

                    richTextBox1.AppendText("\r\n");

                    line = sr.ReadLine();
                }

                sr.Close();

                int key = Convert.ToInt32(textBox1.Text);
                string message = richTextBox1.Text;
                string encrypted = encrypt(message, key);
                string decrypted = decrypt(encrypted, key);

                richTextBox2.Text = encrypted;
                richTextBox3.Text = decrypted;
            }
        }
        public string encrypt(string msg, int key)
        {
            string result = "";
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; i + j < msg.Length; j += key)
                {
                    result += msg[i + j];
                }
            }
            return result;
        }

        public string decrypt(string msg, int key)
        {
            StringBuilder result = new StringBuilder(msg);
            int k = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; i + j < msg.Length; j += key)
                {
                    result[i + j] = msg[k++];
                }
            }
            return result.ToString();
        }
    }
}
