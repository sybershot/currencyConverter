using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9')) //Позволяет вводить в текстбокс только цифры от 0 до 9.
                return;
            if (e.KeyChar == ',') e.KeyChar = '.'; //Заменяет запятую на точку в случае необходимости.
            //Вот эта нихреновина запрещает вводить больше одной точки (ну, дробного разделителя, тобишь) в текстбокс.
            if (e.KeyChar == '.')
            {
                if ((textBox1.Text.IndexOf('.') != -1) || (textBox1.Text.Length == 0))
                {
                    e.Handled = true;
                }
                return;
            }
            //Аж до сюда ^ , охудеть.
            if (Char.IsControl(e.KeyChar))  //Эм, вроде перекидывает на текстбокс2 по нажатию на enter. Иначе на кнопку конвертации.
            {                               //Вот какой я умный (IQ 94).
                if(e.KeyChar == (char)Keys.Enter)
                {
                    if (sender.Equals(textBox1))
                        textBox2.Focus();
                    else
                        button1.Focus();
                }
                return;
            }
            e.Handled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //Защита от идиотов. Деактивирует кнопку конвертации, если
        {                                                             //Один из текстбоксов пустой. СТРАДАЙТЕ СУКИ
            label3.Text = "";
            if ((textBox1.Text.Length != 0) && (textBox2.Text.Length != 0))
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double usd, k, rub;
            usd = Double.Parse(textBox1.Text);
            k = Double.Parse(textBox2.Text); //Охудеть, оказывается разделителем дробной части является точка.
            rub = usd * k;
            label3.Text = rub.ToString("C");
        }
    }
}
