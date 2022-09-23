using System;

using System.Drawing;

using System.Windows.Forms;
using System.IO;


namespace Physics
{
    public partial class GameForm : Form
    {

        private int[,] map = new int[4, 4];
        private Label[,] labels = new Label[4, 4];
        private PictureBox[,] pics = new PictureBox[4, 4];
        private bool gameStarted = false;
        private int tutorialSteps = 0;
        private bool isTutorial;
        private int timeFromStart;
        PictureBox[,] emptyCells = new PictureBox[4,4];
        public GameForm(bool isTutorial)
        {
            this.isTutorial = isTutorial;
            gameStarted = false;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            map[0, 0] = 1;
            

            if (!isTutorial)
            {
                CreateMap();
                CreatePics(isTutorial);
                map[0, 1] = 1;
                GenerateNewPic();

                pictureBox1.Hide();
                label2.Hide();
                label3.Hide();
                button2.Hide();
                this.Size = new Size(260, 320);

                timer1.Start();

                gameStarted = true;

                debugButton.Visible = true;
            } 
            else
            {
                pictureBox1.ImageLocation = "./Resources/backArrow.jpg";
                pictureBox2.ImageLocation = "./Resources/pic2.png";
                pictureBox3.ImageLocation = "./Resources/pic1.png";
                pictureBox4.ImageLocation = "./Resources/arrow.png";
                pictureBox2.Show();
                map[3, 0] = 1;
            }
                

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //Перезаписываем функцию ProcessCmdKey чтобы работали клавиши-стрелочки
        {
            switch (keyData)
            {
                case Keys.Left:
                    OnKeyboardPressed(Keys.Left);
                    return true;

                case Keys.Right:
                    OnKeyboardPressed(Keys.Right);
                    return true;

                case Keys.Up:
                    OnKeyboardPressed(Keys.Up);
                    return true;

                case Keys.Down:
                    OnKeyboardPressed(Keys.Down);
                    return true;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void CreateMap()
        {
           
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    emptyCells[i, j] = new PictureBox();
                    emptyCells[i,j].Location = new Point(12 + 56 * j, 40+56 * i);
                    emptyCells[i, j].Size = new Size(50, 50);
                    emptyCells[i, j].BackColor = Color.Gray;
                    this.Controls.Add(emptyCells[i,j]);
                }
            }
        }

        private void GenerateNewPic()
        {
            Random rnd = new Random();
            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);
            while (pics[a, b] != null)
            {
                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }
            map[a, b] = 1;
            pics[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(emptyCells[0,0].Location.X +56*b,  emptyCells[0,0].Location.Y +56 * a);
            pics[a, b].Size = new Size(50, 50);
            pics[a, b].BackColor = Color.Yellow;
            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
            labels[a, b].ForeColor = Color.Red;
            
        }
        private bool NoSimiliarAround(int a,int b)
        {
            if (b == 0 && a == 0)
            {
                if (map[b + 1, a] == 1 && map[b, a + 1] == 1 && labels[b + 1, a].Text != labels[b, a].Text && labels[b, a + 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b == 3 && a == 3)
            {
                if (map[b - 1, a] == 1 && map[b, a - 1] == 1 && labels[b - 1, a].Text != labels[b, a].Text && labels[b, a - 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b == 3 && a == 0)
            {
                if (map[b - 1, a] == 1 && map[b, a + 1] == 1 && labels[b - 1, a].Text != labels[b, a].Text && labels[b, a + 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b == 0 && a == 3)
            {
                if (map[b + 1, a] == 1 && map[b, a - 1] == 1 && labels[b + 1, a].Text != labels[b, a].Text && labels[b, a - 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b > 0 && b != 3 && a == 0)
            {
                if (map[b + 1, a] == 1 && map[b - 1, a] == 1 && map[b, a + 1] == 1
                    && labels[b + 1, a].Text != labels[b, a].Text && labels[b - 1, a].Text != labels[b, a].Text && labels[b, a + 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b == 0 && a > 0 && a != 3)
            {
                if (map[b + 1, a] == 1 && map[b, a + 1] == 1 && map[b, a - 1] == 1
                    && labels[b + 1, a].Text != labels[b, a].Text && labels[b, a + 1].Text != labels[b, a].Text && labels[b, a - 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b == 3 && a > 0 && a != 3)
            {
                if (map[b - 1, a] == 1 && map[b, a + 1] == 1 && map[b, a - 1] == 1
                    && labels[b - 1, a].Text != labels[b, a].Text && labels[b, a + 1].Text != labels[b, a].Text && labels[b, a - 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b > 0 && b != 3 && a == 3)
            {
                if (map[b - 1, a] == 1 && map[b + 1, a] == 1 && map[b, a - 1] == 1
                    && labels[b - 1, a].Text != labels[b, a].Text && labels[b + 1, a].Text != labels[b, a].Text && labels[b, a - 1].Text != labels[b, a].Text)
                {
                    return true;
                }
            }
            else if (b < 3 && b > 0 && a < 3 && a > 0)
            {
                if (map[b + 1, a] == 1 && map[b, a + 1] == 1 && map[b - 1, a] == 1 && map[b, a - 1] == 1
                    && labels[b + 1, a].Text != labels[b, a].Text && labels[b, a + 1].Text != labels[b, a].Text && labels[b - 1, a].Text != labels[b, a].Text && labels[b, a - 1].Text != labels[b, a].Text)
                {
                    return true;

                }
            }
            return false;
        }
        private void CreatePics(bool isTutorial)
        {
            pics[0, 0] = new PictureBox();
            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(50, 50);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[0, 0].Controls.Add(labels[0, 0]);
            pics[0, 0].Location = emptyCells[0, 0].Location;
            pics[0, 0].Size = new Size(50, 50);
            pics[0, 0].BackColor = Color.Yellow;
            this.Controls.Add(pics[0, 0]);
            pics[0, 0].BringToFront();
            if (!isTutorial)
            {
                pics[0, 1] = new PictureBox();
                labels[0, 1] = new Label();
                labels[0, 1].Text = "2";
                labels[0, 1].Size = new Size(50, 50);
                labels[0, 1].TextAlign = ContentAlignment.MiddleCenter;
                labels[0, 1].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
                pics[0, 1].Controls.Add(labels[0, 1]);
                pics[0, 1].Location = emptyCells[0, 1].Location;
                pics[0, 1].Size = new Size(50, 50);
                pics[0, 1].BackColor = Color.Yellow;
                this.Controls.Add(pics[0, 1]);
                pics[0, 1].BringToFront();
            }
            else
            {
                pics[3, 0] = new PictureBox();
                labels[3, 0] = new Label();
                labels[3, 0].Text = "2";
                labels[3, 0].Size = new Size(50, 50);
                labels[3, 0].TextAlign = ContentAlignment.MiddleCenter;
                labels[3, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
                pics[3, 0].Controls.Add(labels[3, 0]);
                pics[3, 0].Location = emptyCells[3, 0].Location;
                pics[3, 0].Size = new Size(50, 50);
                pics[3, 0].BackColor = Color.Yellow;
                this.Controls.Add(pics[3, 0]);
                pics[3, 0].BringToFront();
            }

        }

        private void ChangeColor(int sum,int k,int j)
        {
            if (sum % 1024 == 0) pics[k, j].BackColor = Color.Pink;
            else if (sum % 512 == 0) pics[k, j].BackColor = Color.Red;
            else if (sum % 256 == 0) pics[k, j].BackColor = Color.DarkViolet;
            else if (sum % 128 == 0) pics[k, j].BackColor = Color.Blue;
            else if (sum % 64 == 0) pics[k, j].BackColor = Color.Brown;
            else if (sum % 32 == 0) pics[k, j].BackColor = Color.Coral;
            else if (sum % 16 == 0) pics[k, j].BackColor = Color.Cyan;
            else if (sum % 8 == 0) pics[k, j].BackColor = Color.Maroon;
            else pics[k, j].BackColor = Color.Green;
        }
       
        private void OnKeyboardPressed(Keys keys)
        {
            if (!gameStarted)
                return;
            bool ifPicWasMoved = false;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if(labels[i,j]!=null)
                    labels[i, j].ForeColor = Color.Black;
                }
            }
            switch (keys)
            {
                case Keys.Right:
                    for(int k = 0; k < 4; k++)
                    {
                        for(int l = 2; l >= 0; l--)
                        {
                            if(map[k,l] == 1)
                            {
                                for(int j = l + 1; j < 4; j++)
                                {
                                    if(map[k,j] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[k, j - 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j - 1];
                                        pics[k, j - 1] = null;
                                        labels[k, j] = labels[k, j - 1];
                                        labels[k, j - 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                                    }else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j-1].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            ChangeColor(a + b, k, j);
                                            map[k, j - 1] = 0;
                                            this.Controls.Remove(pics[k, j - 1]);
                                            this.Controls.Remove(labels[k, j - 1]);
                                            pics[k, j - 1] = null;
                                            labels[k, j - 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Left:
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 1; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l - 1; j >= 0; j--)
                                {
                                    if (map[k, j] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[k, j + 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j + 1];
                                        pics[k, j + 1] = null;
                                        labels[k, j] = labels[k, j + 1];
                                        labels[k, j + 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j + 1].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            ChangeColor(a + b, k, j);
                                            map[k, j + 1] = 0;
                                            this.Controls.Remove(pics[k, j + 1]);
                                            this.Controls.Remove(labels[k, j + 1]);
                                            pics[k, j + 1] = null;
                                            labels[k, j + 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    for (int k = 2; k >= 0; k--)
                    {
                        for (int l = 0; l <4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k + 1; j < 4; j++)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[j - 1,l] = 0;
                                        map[j,l] = 1;
                                        pics[j,l] = pics[j - 1,l];
                                        pics[j - 1,l] = null;
                                        labels[j,l] = labels[j - 1,l];
                                        labels[j - 1,l] = null;
                                        pics[j,l].Location = new Point(pics[j,l].Location.X, pics[j,l].Location.Y+56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j,l].Text);
                                        int b = int.Parse(labels[j - 1,l].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[j,l].Text = (a + b).ToString();
                                            ChangeColor(a + b, j,l);
                                            map[j - 1,l] = 0;
                                            this.Controls.Remove(pics[j - 1,l]);
                                            this.Controls.Remove(labels[j - 1,l]);
                                            pics[j - 1,l] = null;
                                            labels[j - 1,l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up:
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k - 1; j >=0; j--)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[j + 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j + 1, l];
                                        pics[j + 1, l] = null;
                                        labels[j, l] = labels[j + 1, l];
                                        labels[j + 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y - 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j + 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            ChangeColor(a + b, j, l);
                                            map[j + 1, l] = 0;
                                            this.Controls.Remove(pics[j + 1, l]);
                                            this.Controls.Remove(labels[j + 1, l]);
                                            pics[j + 1, l] = null;
                                            labels[j + 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            if (ifPicWasMoved)
            {
                GenerateNewPic();
            }
            else
            {
                if (GameOver())
                {
                    Notifications form3 = new Notifications(0);
                    form3.ShowDialog();
                    if (form3.DialogResult == DialogResult.OK)
                        this.Close();
                }
            }

            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] != 0)
                    {
                        if (labels[i, j].Text.Length > 3)
                        {
                            labels[i,j].Font = new Font(new FontFamily("Microsoft Sans Serif"), 12);
                        }
                        if (isTutorial)
                        {
                            if (labels[i, j].Text == "16")
                            {
                                label3.Text = "Вы научились играть в игру 2048!\n" +
                                    "Вы можете продолжить игру или начать новую, щелкнув по кнопке «Завершить»";
                                button2.Enabled = true;
                                button2.Text = "Завершить";
                                label4.Visible = true;
                            }
                        }
                        else
                        {
                           
                        }
                        if (labels[i, j].Text == "2048")
                        {
                            TimeSpan ts = TimeSpan.FromSeconds(timeFromStart);
                            using (StreamWriter sw = new StreamWriter("./statistics.txt", true))
                            {
                                sw.WriteLine("Затраченное время: " + ts.Minutes +" минут " +ts.Seconds+ " секунд");
                            }

                            Notifications form3 = new Notifications(2);
                            form3.ShowDialog();
                            if (form3.DialogResult == DialogResult.OK)
                                this.Close();

                        }
                    }
                       
                           
                }
            }
            
        }
        private bool GameOver()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] == 0)
                    {
                        return false;
                    }

                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!NoSimiliarAround(i,j))
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tutorialSteps++;
            pictureBox1.Hide();
            label2.Hide();
            if (tutorialSteps == 1)
                label3.Text = "Вы можете передвигать все цветные ячейки нажатием клавиш \n" +
                "«Стрелка вправо» для движения вправо\n" +
                "«Стрелка влево» для движения влево\n" +
                "«Стрелка вниз» для движения вниз\n" +
                "«Стрелка вверх» для движения вверх\n";
            else if (tutorialSteps == 2)
            {
                label3.Text = "Чтобы получить число побольше нужно совместить два одинаковых числа";
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
            }
            else if (tutorialSteps == 3)
            {
                label3.Text = "По ходу игры будут появляться новые закрашенные ячейки с числом 2";
            }
            else if (tutorialSteps == 4)
            {
                label3.Text = "А теперь попробуйте самостоятельно получить число 16\n" +
                    "Подсказка: используйте клавиши-стрелочки чтобы передвигать ячейки";
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                CreateMap();
                CreatePics(true);
                button2.Enabled = false;
                gameStarted = true;
            }
            else
                this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeFromStart++;
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i, j] != 0)
                    {
                        labels[i, j].Text = "2048";
                        return;
                    }
                        
        }
    }
}
