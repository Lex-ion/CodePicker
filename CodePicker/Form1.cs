using System.Collections;
using System.Linq;

namespace CodePicker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int miliSekundy = 0;
        int sekundy = 0;
        int minuty = 0;

        int obtiznost;

        int pin = 1;

        int[] zamky =
        {
        0,
        0,
        0,
        0
        };

        int[] predloha =
        {
        5,
        5,
        5,
        5
        };

        int sipkaX = 13;

        bool enabled = false;

        int zobrazovanaObtiznost;


        private void button1_Click(object sender, EventArgs e)
        {
            miliSekundy = 0;
            sekundy = 0;
            minuty = 0;

            Random rnd = new Random();

            for (int i = 0; i < zamky.Length; i++)
            {
                predloha[i] = rnd.Next(0, 9);
            }

            label1.Text = predloha[0].ToString();
            label2.Text = predloha[1].ToString();
            label3.Text = predloha[2].ToString();
            label4.Text = predloha[3].ToString();

            zobrazovanaObtiznost = 5-trackBar1.Value;

            label6.Text = $"Obtížnost: {zobrazovanaObtiznost}";

            timer1.Enabled = true;
            timer1.Start();

            obtiznost = trackBar1.Value + 1;
            timer2.Interval = obtiznost * 250;
            timer2.Enabled = true;
            timer2.Start();

            button1.Enabled = false;
            trackBar1.Enabled = false;

            enabled = true;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (enabled)
            {
                //capture up arrow key
                if (keyData == Keys.Up)
                {
                    ZmenaCisla(keyData);
                    return true;
                }
                //capture down arrow key
                if (keyData == Keys.Down)
                {
                    ZmenaCisla(keyData);
                    return true;
                }
                //capture left arrow key
                if (keyData == Keys.Left)
                {
                    pin--;
                    if (pin < 1)
                    {
                        pin = 4;
                        sipkaX = 85;
                    }
                    else
                    {
                        sipkaX -= 24;
                    }

                    pictureBox1.Left = sipkaX;
                    return true;
                }
                //capture right arrow key
                if (keyData == Keys.Right)
                {
                    pin++;
                    if (pin > 4)
                    {
                        pin = 1;
                        sipkaX = 13;
                    }
                    else
                    {
                        sipkaX += 24;
                    }

                    pictureBox1.Left = sipkaX;
                    return true;
                }





            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        public void ZmenaCisla(Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                zamky[pin - 1]++;

                if (zamky[pin - 1] > 9)
                {
                    zamky[pin - 1] = 0;
                }

            }

            if (keyData == Keys.Down)
            {
                zamky[pin - 1]--;

                if (zamky[pin - 1] < 0)
                {
                    zamky[pin - 1] = 9;
                }
            }

            textBox1.Text = zamky[0].ToString();
            textBox2.Text = zamky[1].ToString();
            textBox3.Text = zamky[2].ToString();
            textBox4.Text = zamky[3].ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            if (zamky[0] == predloha[0] && zamky[1] == predloha[1] && zamky[2] == predloha[2] && zamky[3] == predloha[3])
            {
                timer1.Stop();
                timer2.Stop();

                button1.Enabled = true;
                trackBar1.Enabled = true;

                label9.Text = label8.Text;
                label8.Text = label7.Text;
                label7.Text = $"{minuty}:{sekundy}.{miliSekundy} / {zobrazovanaObtiznost}";

                enabled = false;
            }

            label5.Text = $"{minuty}:{sekundy}.{miliSekundy}";

            miliSekundy += 1;
            if (miliSekundy > 99)
            {
                miliSekundy = 0;
                sekundy++;
            }
            if (sekundy > 59)
            {
                sekundy = 0;
                minuty++;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();

            predloha[rnd.Next(0, 4)] = rnd.Next(0, 9);

            label1.Text = predloha[0].ToString();
            label2.Text = predloha[1].ToString();
            label3.Text = predloha[2].ToString();
            label4.Text = predloha[3].ToString();
        }
    }
}