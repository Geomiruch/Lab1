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

                    //richTextBox1.AppendText("\r\n");

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
            int length_s = msg.Length;


            int columns = length_s / key;
            if (length_s % key != 0)
            {
                columns += 1;
            }
            char[,] matrix = new char[key,columns];
            int count = 0;

            for (int j = 0; j < columns; j++)
            {

                for (int i = key - 1; i >= 0; i--)
                {
                    if (count < length_s)
                    {
                        matrix[i,j] = msg[count];
                        count += 1;
                    }
                    else
                    {
                        matrix[i,j] = '$';
                    }

                }
            }
            


            int counter = 0;
            string result = "";
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrix[i,j] != '$')
                    {
                        result += matrix[i,j];
                        counter++;
                    }

                }
            }


            return result;
        }

        public string decrypt(string msg, int key)
        {
            int length_s = msg.Length;

            string result = "";
            int columns = length_s / key;
            bool odd = true;
            if (length_s % key != 0)
            {
                columns += 1;
                odd = false;
            }
            char[,] matrix = new char[key,columns];
            int count = 0;
            
            if (odd == true)
            {
                for (int i = 0; i < key; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i,j] = msg[count];
                        count += 1;
                    }
                }
                
            }
            else
            {
                for (int i = 0; i < key; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j == columns - 1)
                        {
                            matrix[i,j] = '$';
                        }
                    }
                }
                int remain = length_s % key;
                int counter = length_s - remain;
                for (int i = key - 1; i > key - 1 - remain; i--)
                {
                    matrix[i,columns - 1] = ' ';
                }


                for (int i = 0; i < key; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (matrix[i,j]!='$')
                        {
                            matrix[i, j] = msg[count];
                            count += 1;
                        }

                    }
                }


            }
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    richTextBox4.Text += matrix[i, j];
                }
                richTextBox4.Text += "\n";

            }


            for (int j = 0; j < columns; j++)
            {

                for (int i = key - 1; i >= 0; i--)
                {
                    if (matrix[i,j] != '$')
                    {
                        result += matrix[i,j];
                    }
                }
            }
            return result;
        }
    }
}
