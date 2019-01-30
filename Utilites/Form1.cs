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

        private void button1_Click(object sender, EventArgs e)
        {
            string t = cbTo.Text;
            cbTo.Text = cbFrom.Text;
            cbFrom.Text = t;
        }

        private void cbMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetric.Text)
            {
                case "Длина":
                    metrica.Clear();
                    metrica.Add("mm", 1);
                    metrica.Add("cm", 10);
                    metrica.Add("dm", 100);
                    metrica.Add("m", 1000);
                    metrica.Add("km", 1000000);
                    metrica.Add("mile", 1609344);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("mm");
                    cbFrom.Items.Add("cm");
                    cbFrom.Items.Add("dm");
                    cbFrom.Items.Add("m");
                    cbFrom.Items.Add("km");
                    cbFrom.Items.Add("mile");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("mm");
                    cbTo.Items.Add("cm");
                    cbTo.Items.Add("dm");
                    cbTo.Items.Add("m");
                    cbTo.Items.Add("km");
                    cbTo.Items.Add("mile");
                    cbFrom.Text = "mm";
                    cbTo.Text = "mm";
                    break;
                case "Вес":
                    metrica.Clear();
                    metrica.Add("g", 1);
                    metrica.Add("kg", 1000);
                    metrica.Add("t", 1000000);
                    metrica.Add("lb", 453.6);
                    metrica.Add("oz", 283);
                    
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("h");
                    cbFrom.Items.Add("kh");
                    cbFrom.Items.Add("t");
                    cbFrom.Items.Add("lb");
                    cbFrom.Items.Add("oz");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("g");
                    cbTo.Items.Add("kg");
                    cbTo.Items.Add("t");
                    cbTo.Items.Add("lb");
                    cbTo.Items.Add("oz");
                    cbFrom.Text = "g";
                    cbTo.Text = "g";
                    break;
                default:
                    break;
            }
        }
    }
}