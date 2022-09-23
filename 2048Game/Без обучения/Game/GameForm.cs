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
        PictureBox[,] emptyCells = new PictureBox[4,4];
        public GameForm()
        {
            InitializeComponent();
            map[0, 0] = 1;
            map[0, 1] = 1;
            CreateMap();
            CreatePics();
            GenerateNewPic();
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
                    emptyCells[i,j].Location = new Point(12 + 56 * j, 10+ 56 * i);
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
        private void CreatePics()
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
                    Console.WriteLine("GameOver");
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

    }
}
