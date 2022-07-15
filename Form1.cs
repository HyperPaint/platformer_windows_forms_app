using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static PictureBox Picture;
        public static Bitmap Background;
        Buff buff;
        public static Entity MyEntity;
        public const int Persons = 1;
        Person[] p = new Person[Persons];
        public static Vector2f SpawnPoint;

        public Form1()
        {
            InitializeComponent();
            Picture = pictureBox1;
            Background = new Bitmap("Texture/background.png");
            buff = new Buff();
        }

        /*public static string[] Map = {
            "          00000000000000000000000000000000000000000000000000000000000000000000000000          ",
            "          000                                                                    000          ",
            "          000                                                                    000          ",
            "          000                                                                    000          ",
            "          000                                                        P            000          ",
            "          000                                                                    000          ",
            "G         000                                                     0000000000W   W000          ",
            "DG        000                  WWWWWWWW                 WWWWWWWW               WWW00          ",
            "DDG       000                 WBBBBBBBBW               WBBBBBBBBW             WWWWW0          ",
            "DDDG      000                 BBBBBBBBBB               BBBBBBBBBB            WWWWWWW          ",
            "DDDDG     000                 BBBBBBBBBB      GGG      BBBBBBBBBB           WWWWWWWWW         ",
            "DDDDDGGGGGGGGGGGGGGGGGGGGGGGGGWWWWWWWWWWGGGGGGDDDGGGGGGWWWWWWWWWWGGGGGGGBGGGGGGGGGGGGGGGGGGGGG",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDWBBWWWWWWWWWWWWWWWWWDDD",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDWBBBBBBBBBBBBBBBBBBBWWW",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDWBBBBBBBBBBBBBBBBBBBBB",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDWWWWWWWWWWWWWWWWBBBBB",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDWWWWW",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD",
            "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD"};*/

        public static string[] Map =
        {
            "      ",
            "      ",
            "    P  ",
            "      ",
            "WW    ",
            "WWWWWW"
        };

        void Main()
        {
            for (int Height = 0; Height < Map.Length; Height++)
            {
                for (int Width = 0; Width < Map[Height].Length; Width++)
                {
                    if (Map[Height][Width] == 'P')
                    {
                        SpawnPoint = new Vector2f(Width * Resource.BlockSize, Height * Resource.BlockSize);
                        Map[Height] = Map[Height].Remove(Width, 1);
                    }
                }
            }

            MyEntity = new Entity(SpawnPoint);
            for (int i = 0; i < Persons; i++)
            {
                p[i] = new Person(SpawnPoint, new Random().Next(0, 5));
            }

            timer = new Timer();
            timer.Tick += (s, e) => Process();
            timer.Interval = 100;
            timer.Start();
        }

        Timer timer;

        void Process()
        {
            buff.Clear();
            Sprite block;
            for (int Height = 0; Height < Map.Length; Height++)
            {
                for (int Width = 0; Width < Map[Height].Length; Width++)
                {
                    switch (Map[Height][Width])
                    {
                        case 'W':
                            {
                                block = Resource.GetBlock(0);
                                block.Position = new Vector2f(Width * Resource.BlockSize - MyEntity.GetPosition.X + Entity.OffsetX, Height * Resource.BlockSize - MyEntity.GetPosition.Y + Entity.OffsetY);
                                buff.Draw(block);
                                break;
                            }
                        case 'B':
                            {
                                block = Resource.GetBlock(1);
                                block.Position = new Vector2f(Width * Resource.BlockSize - MyEntity.GetPosition.X + Entity.OffsetX, Height * Resource.BlockSize - MyEntity.GetPosition.Y + Entity.OffsetY);
                                buff.Draw(block);
                                break;
                            }
                        case 'D':
                            {
                                block = Resource.GetBlock(2);
                                block.Position = new Vector2f(Width * Resource.BlockSize - MyEntity.GetPosition.X + Entity.OffsetX, Height * Resource.BlockSize - MyEntity.GetPosition.Y + Entity.OffsetY);
                                buff.Draw(block);
                                break;
                            }
                        case 'G':
                            {
                                block = Resource.GetBlock(3);
                                block.Position = new Vector2f(Width * Resource.BlockSize - MyEntity.GetPosition.X + Entity.OffsetX, Height * Resource.BlockSize - MyEntity.GetPosition.Y + Entity.OffsetY);
                                buff.Draw(block);
                                break;
                            }
                    }
                }
            }
            for (int i = 0; i < Persons; i++)
            {
                buff.Draw(p[i].Draw());
            }
            buff.Draw(MyEntity.Draw());
            
            buff.Display();
        }

        private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        MyEntity.Jump();
                        break;
                    }
                case Keys.A:
                    {
                        MyEntity.isMoving = true;
                        MyEntity.FacingRight = false;
                        break;
                    }
                case Keys.D:
                    {
                        MyEntity.isMoving = true;
                        MyEntity.FacingRight = true;
                        break;
                    }
                case Keys.Escape:
                    {
                        Application.Exit();
                        break;
                    }
            }
        }

        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    {
                        MyEntity.isMoving = false;
                        break;
                    }
                case Keys.D:
                    {
                        MyEntity.isMoving = false;
                        break;
                    }
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Main();
            ((Form)sender).Activated -= Form1_Activated;
        }
    }
}
