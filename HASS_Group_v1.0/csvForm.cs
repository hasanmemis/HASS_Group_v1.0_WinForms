﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HASS_Group_v1._0
{
    public partial class csvForm : Form
    {
        public csvForm()
        {
            InitializeComponent();
        }
        void MoveIndicator(Control control)
        {
            indicator.Top = control.Top;
            indicator.Height = control.Height;
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private void csvForm_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
        }
        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            jsonForm json=new jsonForm();
            json.Show();
            this.Hide();
        }
        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
        }
        private void bunifuTileButton5_Click(object sender, EventArgs e)
        {
            MoveIndicator((Control)sender);
            xmlForm xml=new xmlForm();
            xml.Show();
            this.Hide();
        }
        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
        }
        private void bunifuTileButton24_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pathBox.Text = openFileDialog1.FileName;
            try
            {
                katilimciDataTablo(pathBox.Text);
                int katilimci = katilimciTablo.RowCount - 1;
                label2.Text = katilimci.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Yanlış format türünde bir dosya seçtiniz. Lütfen tekrar deneyiniz!", "HATA!", MessageBoxButtons.OK);
            }
        }
        private void katilimciDataTablo(string filePath)
        {
            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                katilimciTablo.DataSource = dt;
            }
        }
        private void kazananDataTablo(List<string> kazananlar)
        {
            DataTable dt = new DataTable();
            pathBox.Text = openFileDialog1.FileName;
            string[] lines = File.ReadAllLines(pathBox.Text);
            if (lines.Length > 0)
            {
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                for (int i = 0; i < kazananlar.Count; i++)
                {
                    string[] dataWords = kazananlar[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                kazananTablo.DataSource = dt;
            }
        }
        private void yedekDataTablo(List<string> yedekKazananlar)
        {
            DataTable dt = new DataTable();
            pathBox.Text = openFileDialog1.FileName;
            string[] lines = File.ReadAllLines(pathBox.Text);
            if (lines.Length > 0)
            {
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string headerWord in headerLabels)
                {
                    dt.Columns.Add(new DataColumn(headerWord));
                }
                for (int i = 0; i < yedekKazananlar.Count; i++)
                {
                    string[] dataWords = yedekKazananlar[i].Split(',');
                    DataRow dr = dt.NewRow();
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (dt.Rows.Count > 0)
            {
                yedekTablo.DataSource = dt;
            }
        }
        private void bunifuTileButton10_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(pathBox.Text);
            List<string> list = lines.ToList();
            Random rnd = new Random();
            List<string> kazananLines = new List<string>();
            List<string> yedekLines = new List<string>();
            for (int i = 1; i < kazananSayisi.Value+1; i++)
            {
                int kura = rnd.Next(1, list.Count);
                kazananLines.Add(list[kura]);
                list.RemoveAt(kura);
            }
            for (int i = 1; i < yedekSayisi.Value+1; i++)
            {
                int yedekKura = rnd.Next(1, list.Count);
                yedekLines.Add(list[yedekKura]);
                list.RemoveAt(yedekKura);
            }
            kazananDataTablo(kazananLines);
            yedekDataTablo(yedekLines);
        }
        private void kazananSayisi_ValueChanged(object sender, EventArgs e)
        {
            yedekSayisi.Value = kazananSayisi.Value;
        }
        private void bunifuTileButton6_Click(object sender, EventArgs e)
        {
            var src = DateTime.Now;
            var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = path + @"\hass_giveaway_cikti_csv.txt";
            File.Delete(fileName);
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();
            File.AppendAllText(fileName, " ----- AS TALİHLİLER -----\n");
            for (int j = 0; j < kazananTablo.Rows.Count - 1; j++)
            {
                List<object> ciktiList = new List<object>();
                for (int k = 0; k < kazananTablo.Columns.Count; k++)
                {
                    ciktiList.Add(kazananTablo.Rows[j].Cells[k].Value.ToString());
                }
                string dizi = "";
                foreach (var item in ciktiList)
                {
                    dizi = $"{dizi} {item}";
                }
                File.AppendAllText(fileName, dizi + Environment.NewLine);
            }
            File.AppendAllText(fileName, "\n ----- YEDEK TALİHLİLER -----\n");
            for (int j = 0; j < yedekTablo.Rows.Count - 1; j++)
            {
                List<object> ciktiList = new List<object>();
                for (int k = 0; k < kazananTablo.Columns.Count; k++)
                {
                    ciktiList.Add(yedekTablo.Rows[j].Cells[k].Value.ToString());
                }
                string dizi = "";
                foreach (var item in ciktiList)
                {
                    dizi = $"{dizi} {item}";
                }
                File.AppendAllText(fileName, dizi + Environment.NewLine);
            }
            File.AppendAllText(fileName, "\n*** Çekiliş "+ hm.ToString("dd'/'MM'/'yyyy HH:mm", CultureInfo.InvariantCulture) +" tarihinde, adil bir şekilde HASS Giveaway tarafından yapılmıştır. © ***\n");
        }
        private void bunifuTileButton9_Click(object sender, EventArgs e)
        {
            pathBox.Text = null;
            katilimciTablo.DataSource = null;
            kazananTablo.DataSource = null;
            yedekTablo.DataSource = null;
            kazananSayisi.Value = 0;
            yedekSayisi.Value = 0;
            label2.Text = "0";
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            switch (MessageBox.Show(this, "Programı kapatmak istediğinize emin misiniz?", "Kapatılıyor..", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }
    }
}
