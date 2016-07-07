using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
//using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace pharser2
{
    public partial class Form1 : Form
    {
        class dg
        {
            public DataGridView name;
            public enum values { integer, floating };
            public int val_column = 0, val_row = 0;
            public void zagolovok(int stolb, string value) { name.Columns[0].HeaderText = value; }
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
            public void arrto(int stolb, string[] mass)
            {for (int i = 0; i < mass.Length; i++)
                    name.Rows[i].Cells[stolb].Value = mass[i];
                name.AutoResizeColumns();
            }
        }

            public Form1()
        {
            InitializeComponent();
        }
        public string gethtml(string adress)
        {
            string result = "internal oper error";
            try
            {
               
                WebRequest request = WebRequest.Create(new Uri(@"" + adress));
                // Получить ответ с сервера
                WebResponse response = request.GetResponse();
                // Получаем поток данных из ответа
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    // Выводим исходный код страницы
                    string line;result = "";
                    while ((line = stream.ReadLine()) != null)
                        // result += line + "\n";
                        result += line;
                }                
            }
            catch
            { }
            return result;
        }
        public string[] textfromtag(string source,string begino,string finalo)
        {
            string[] res= { };
            int i = 0;

            finalo.Replace("/",@"\/");
            /////////
            //Regex Reg = new Regex(@"" + begino + "(.*?)" + finalo+"");
            Regex Reg = new Regex(@begino + @"(.*?|(\s?|\S?))" + @finalo + "");
            textBox28.Text = @begino + @"(.*?|(\s|\S))" + @finalo + "";
            MatchCollection reHref = Reg.Matches(source);

            foreach (Match match in reHref)
            { Array.Resize<string>(ref res, i+1);
                res[i] = match.ToString();
                res[i] = res[i].Remove(0,begino.Length);
                res[i] = res[i].Remove(res[i].Length-finalo.Length,finalo.Length);
                i++;
                textBox7.Text+= i.ToString()+"=";
            }
                

            /////////
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {  textBox2.Text=gethtml(textBox1.Text);
            tabControl1.SelectedTab = u;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int kol = 0;
            if (ch1.Checked == true) kol++;
            if (ch2.Checked == true) kol++;
            if (ch3.Checked == true) kol++;
            if (ch4.Checked == true) kol++;
            if (ch5.Checked == true) kol++;
            if (ch51.Checked == true) kol++;
            if (ch52.Checked == true) kol++;
            if (ch53.Checked == true) kol++;
            if (ch6.Checked == true) kol++;
            if (ch61.Checked == true) kol++;
            if (ch62.Checked == true) kol++;
            if (ch63.Checked == true) kol++;
            if (ch64.Checked == true) kol++;
            ///////////
            dg tab1 = new dg();
            tab1.name = dataGridView1;
            // tab1.init(kol, 150, 1, 0);

            string[] res;
            res=textfromtag(textBox2.Text, beg1.Text, end1.Text);
            //TextBox7.Text =;
            //шикарно будет если массив будет записываться в датагрид
            //  textBox7.Text = res.Length.ToString();
            tab1.init(kol, res.Length, 1, 1);
            
            tab1.arrto(0, res);
            tab1.zagolovok(0, beg1.Text+".."+end1.Text);


            //активируем вкладку с таблицей вывода
            tabControl1.SelectedTab=r;
        }
    }
}
