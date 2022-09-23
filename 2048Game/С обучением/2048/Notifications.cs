using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Physics
{
    public partial class Notifications : Form
    {
        int gameState;
        public Notifications(int gameState) //0 - игра проиграна, 1 - обучение, 2 - получено число 2048
        {
            InitializeComponent();
            this.gameState = gameState;
            if (gameState==0)
            {
                label1.Text = "Вы проиграли(";
                button1.Text = "В меню";
                this.Text = "Конец игры";
            }
            else if(gameState==1)
            {
                this.Text = "Обучение";
                label1.Text = "Добро пожаловать в игру 2048!\n" +
                    "Цель игры -получить число 2048\n" +
                    "Следуйте шагам этого обучения и вы быстро научитесь играть!";
                button1.Text = "Продолжить!";
            }
            else if (gameState == 2)
            {
                this.Text = "Победа";
                label2.Visible = true;
                label1.Text = "Вы победили в игре 2048";
                label1.Location = new Point(108, 58);
                button1.Text = "В меню";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gameState==1)
            {
                GameForm form1 = new GameForm(true);
                form1.Show();
                this.Close();
            }
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            DialogResult = DialogResult.OK;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
