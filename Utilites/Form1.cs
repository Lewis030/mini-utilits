using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilites
{
    public partial class Form1 : Form
    {
        int count = 0;
        Random rand = new Random();
        char[] special_symbols = { '%', '*', ')', '?', '#', '$', '^', '&', '~' };
        Dictionary<string, double> metrica;

        public Form1()
        {
            InitializeComponent();
            metrica = new Dictionary<string, double>();
            metrica.Add("mm", 1);
            metrica.Add("cm", 10);
            metrica.Add("dm", 100);
            metrica.Add("m", 1000);
            metrica.Add("km", 1000000);
            metrica.Add("mile", 1609344);
        }

        private void btnIncr_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnDecr_Click(object sender, EventArgs e)
        {
            count--;
            lblCount.Text = Convert.ToString(count);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString();
        }

        private void btnGenRandom_Click(object sender, EventArgs e)
        {
            int r = rand.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value)+1);
            lblRandomNumber.Text = r.ToString();
            tbRandom.AppendText(r + " ");
        }

        private void btnClearRandom_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
        }

        private void btnRandomCopyToBufer_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbRandom.Text);
        }

        private void tsmiInsertDate_Click(object sender, EventArgs e)
        {
           rtbNotepad.AppendText(DateTime.Now.ToShortDateString()+ " ");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortTimeString()+" ");
        }

        private void tsmiSaveNotepad_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNotepad.SaveFile("Notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении", " Error");
            }
        }

        void LoadNotepad()
        {
            try
            {
                rtbNotepad.LoadFile("Notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при открытии", " Колом братан");
            }
        }

        private void tsmiLoadNotepad_Click(object sender, EventArgs e)
        {
            LoadNotepad();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadNotepad();
            clbCheckSetPass.SetItemChecked(0,true);
            clbCheckSetPass.SetItemChecked(1, true);
        }

        private void btnCreatePass_Click(object sender, EventArgs e)
        {
            if (clbCheckSetPass.CheckedItems.Count == 0) return;
            string password = "";
            for (int i = 0; i < nudPassLenght.Value; i++)
            {
                int n = rand.Next(0, clbCheckSetPass.CheckedItems.Count);
                string s = clbCheckSetPass.CheckedItems[n].ToString();
                switch (s)
                {
                    case "Цифры": password += rand.Next(10).ToString();
                        break;
                    case "Прописные": password += Convert.ToChar(rand.Next(65,88));
                        break;
                    case "Строчные": password += Convert.ToChar(rand.Next(97, 122));
                        break;
                    default:
                        password += special_symbols[rand.Next(special_symbols.Length)];
                    break;
                }
                tbPass.Text = password;
                Clipboard.SetText(password);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double m1 = metrica[cbFrom.Text];
            double m2 = metrica[cbTo.Text];
            double n = Convert.ToDouble(tbFrom.Text);
            tbTo.Text = (n * m1 / m2).ToString();
        }
    }
}