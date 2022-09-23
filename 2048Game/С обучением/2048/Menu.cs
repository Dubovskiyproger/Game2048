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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameForm form1 = new GameForm(false);
            this.Hide();
            form1.ShowDialog();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Notifications form3 = new Notifications(1);
            this.Hide();
            form3.ShowDialog();
            if (form3.DialogResult == DialogResult.Cancel)
            {
                this.Show();
            }
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игра с графическим интерфейсом '2048' была создана в рамках курсового проекта по дисциплине " +
                "'Разработка программных модулей' студентом Института среднего профессионального образования," +
                "группы 32919/1 Дубовским Андреем Александровичем", "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
