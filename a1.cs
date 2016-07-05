using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

using System.Net;
//using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        class dg
        {
            public DataGridView name;
            public enum values { integer, floating };
            public int val_column = 0, val_row = 0;
            public void init(int max_column, int max_row, int begin_num = 0, int off_auto_zagolovok = 0)
            {
                name.Rows.Clear();
                name.Columns.Clear();
                int stro = 0;
                if (off_auto_zagolovok == 0) for (int i = begin_num; i <= max_column; i++) name.Columns.Add("Column", i.ToString());
                if (off_auto_zagolovok == 1) for (int i = begin_num; i <= max_column; i++) name.Columns.Add("Column", "");
                for (int i = begin_num; i < max_row; i++) name.Rows.Add();
                for (int i = begin_num; i <= max_row; i++) name.Rows[stro++].HeaderCell.Value = i.ToString();
                name.AutoResizeColumns();
            }
            public void rando(values valu = values.integer, int min = 0, int max = 65535)
            {
                Random a = new Random();
                for (int i = 0; i < name.ColumnCount; i++)
                    for (int j = 0; j < name.RowCount; j++)
                        if (valu == values.integer) name.Rows[j].Cells[i].Value = a.Next(min, max);
                        else name.Rows[j].Cells[i].Value = a.NextDouble() * (max - min) + min;
                name.AutoResizeColumns();
            }
            public int search(string stroka, int begin_row = 0, int begin_column = 0)
            {
                val_column = 0; val_row = 0;
                var res = Tuple.Create(0, 0);
                for (int j = begin_row; j < name.RowCount; j++)
                    for (int i = begin_column; i < name.ColumnCount; i++)
                        if (name.Rows[j].Cells[i].Value.ToString() == stroka)
                        {
                            val_column = i + 1; val_row = j + 1;
                            name.Rows[j].Cells[i].Style.BackColor = Color.Pink;

                        }
                return 1;
            }
        }
            public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string html0=textBox1.Text ;
                //выбираем значение            
                ////  string match = Regex.Match(html0, @"<td class=""fontMD"">.*?</td></tr><tr class=""even"">").ToString();
                ////      string match = Regex.Match(html0, @"<td class=.*?").ToString();
                //вынимаем чсло
                //match = Regex.Match(match, @"[0-9]").ToString();
                ///textBox1.Text= textBox1.Text+match;
                ///


                //Regex Reg = new Regex("class=\"fontMD smallTdPadding\".*?\">");
                string beg1 = "<td class=\"fontMD\"><a href=\"",end11= "even";
                char end1 = '"';

                string beg2 = "</a></td><td class=\"fontMD\">";
                char end2 = '<';

                
                //Regex Reg = new Regex(beg1+".*?"+end1);
                // Regex Reg = new Regex(beg1 + ".*?" + end1);


                //Regex Reg = new Regex(@"<title>(.*)</title>");
                // Regex Reg = new Regex(@"(?<=<title>)(.*)(?=</title>)");
                Regex Reg = new Regex(@"(?<="+beg1+")(.*)?"+end1);
                //Regex Reg4 = new Regex(@"<tr class=\"odd\">"  + ".*\"");
                Regex Reg2 = new Regex(@"(?<=" + beg2 + ").*" + end2);
                string beg3 = "\">";
                Regex Reg3 = new Regex(@"(?<="+beg3+ ").*" + "(?<=</a></td>)");
                string beg4 = "smallTdPadding\"><img src";
                Regex Reg4 = new Regex(@"" + beg4 + ".*" + "(?=</td)");
                MatchCollection matches4 = Reg4.Matches(html0);
                int chet1 = 0, chet = 0; ;
                foreach (Match match in matches4)
                {
                    textBox2.Text += (++chet1).ToString() + ". " + (match.ToString() + Environment.NewLine);
                    MatchCollection matches = Reg.Matches(match.ToString());
                    ++chet;
                    foreach (Match match1 in matches)
                    {
                        string res1 = match1.ToString();

                        string match3 = Regex.Match(res1, @"(?<=" + beg3 + ").*" + "(?<=</a></td>)").ToString();
                        match3 = match3.Remove(match3.IndexOf('<'), match3.Length - match3.IndexOf('<'));
                        //res1 = res1.Substring(0, res1.LastIndexOf('/'));
                        res1 = res1.Remove(res1.IndexOf(end1), res1.Length - res1.IndexOf(end1));
                        dataGridView1.Rows[chet].Cells[0].Value = res1;
                        dataGridView1.Rows[chet].Cells[2].Value = match3;
                        string res6 = match.ToString();
                        //res1 = res1.Substring(0, res1.LastIndexOf('/'));
                    //   res6 = res6.Remove(-10,10);
                    
                    //    dataGridView1.Rows[chet].Cells[3].Value = match.ToString();
                        dataGridView1.Rows[chet].Cells[3].Value = res6.Substring(res6.Length - 10, 10);
                        MatchCollection matches2 = Reg2.Matches(match.ToString());
                        foreach (Match match5 in matches2)
                        {
                            string res5 = match5.ToString();
                            //res1 = res1.Substring(0, res1.LastIndexOf('/'));
                            res5 = res5.Remove(res5.IndexOf(end2), res5.Length - res5.IndexOf(end2));
                            dataGridView1.Rows[chet].Cells[1].Value = res5;
                           // ++chet;
                            //   textBox2.Text += (++chet).ToString() + ". " + (res1 + Environment.NewLine);

                        }
                        //   textBox2.Text += (++chet).ToString()+". "+(match.ToString() + Environment.NewLine);

                    }
                    // 2
                    /*
                    MatchCollection matches2 = Reg2.Matches(html0);
                    chet = 0;
                    foreach (Match match in matches2)
                    {
                        string res1 = match.ToString();
                        //res1 = res1.Substring(0, res1.LastIndexOf('/'));
                        res1 = res1.Remove(res1.IndexOf(end2), res1.Length - res1.IndexOf(end2));
                        dataGridView1.Rows[chet].Cells[1].Value = res1;
                        ++chet;
                        //   textBox2.Text += (++chet).ToString() + ". " + (res1 + Environment.NewLine);

                    }
                    */

                }

                /*
                    MatchCollection matches = Reg.Matches(html0);


                int chet = 0;
                foreach (Match match in matches)
                {
                    string res1 = match.ToString();

                    string match3 = Regex.Match(res1, @"(?<=" + beg3 + ").*" + "(?<=</a></td>)").ToString();
                    match3 = match3.Remove(match3.IndexOf('<'), match3.Length - match3.IndexOf('<'));
                    //res1 = res1.Substring(0, res1.LastIndexOf('/'));
                    res1 = res1.Remove(res1.IndexOf(end1), res1.Length - res1.IndexOf(end1));
                    dataGridView1.Rows[chet].Cells[0].Value = res1;
                    dataGridView1.Rows[chet].Cells[2].Value = match3;
                 //   textBox2.Text += (++chet).ToString()+". "+(match.ToString() + Environment.NewLine);
                    
                }
                // 2
                MatchCollection matches2 = Reg2.Matches(html0);
                 chet = 0;
                foreach (Match match in matches2)
                {
                    string res1 = match.ToString();
                    //res1 = res1.Substring(0, res1.LastIndexOf('/'));
                    res1 = res1.Remove(res1.IndexOf(end2), res1.Length - res1.IndexOf(end2));
                    dataGridView1.Rows[chet].Cells[1].Value = res1;
                    ++chet;
                 //   textBox2.Text += (++chet).ToString() + ". " + (res1 + Environment.NewLine);

                }
                */
            }
              
            catch { }
        }

        public string reggo()
        {
            return "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string html0 = "-";
                WebRequest request = WebRequest.Create(new Uri(@"https://www.symantec.com/security_response/landing/threats.jsp"));
                //WebRequest request = WebRequest.Create(new Uri(@"http://edis.pp.ua/project+/button/index.php?date_req=" + DateTime.Now.ToString("dd.MM.yyyy")));

                // Получить ответ с сервера
                WebResponse response = request.GetResponse();

                // Получаем поток данных из ответа
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    // Выводим исходный код страницы
                    string line;
                    while ((line = stream.ReadLine()) != null)
                        html0 += line + "\n";
                }
                textBox1.Text = html0;


            }

            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
dg tab1 = new dg();
            tab1.name = dataGridView1;
            tab1.init(4, 150, 1,0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
        }
    }
}
