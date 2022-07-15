using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class Vector2f
    {
        public Vector2f()
        {
            
        }

        public Vector2f(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public float X;
        public float Y;
    }

    public class Vector2i
    {
        public Vector2i()
        {

        }

        public Vector2i(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int X;
        public int Y;
    }

    public class Sprite
    {
        public Sprite()
        {

        }

        public Sprite(Bitmap Texture)
        {
            this.Texture = Texture;
            Result = Texture;
        }

        public Bitmap Result;
        public Bitmap Texture;
        public Rectangle TextureRect_;
        public void TextureRect(Rectangle value)
        {
            TextureRect_ = value;
            Result = new Bitmap(Math.Abs(value.Width), value.Height);
            //Result = Texture.Clone(value, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            if (value.Width > 0)
            {
                for (int j = 0; j < value.Height; j++)
                {
                    for (int i = 0; i < value.Width; i++)
                    {
                        Result.SetPixel(i, j, Texture.GetPixel(value.X + i, value.Y + j));
                    }
                }
            }
            else
            {
                for (int j = 0; j < value.Height; j++)
                {
                    for (int i = 0; i < -value.Width; i++)
                    {
                        Result.SetPixel(i, j, Texture.GetPixel(value.X - i - 1, value.Y + j));
                    }
                }
                TextureRect_.Width *= -1;
            }
        }

        public Vector2f Position = new Vector2f(0f, 0f);
    }

    public class Buff
    {
        public Buff()
        {
            bitmap = new Bitmap(Form1.Picture.Width, Form1.Picture.Height);
        }

        Bitmap bitmap;
        static Color Transparent = Color.FromArgb(0, 0, 0, 0);

        public void Clear()
        {
            bitmap.Dispose();
            bitmap = new Bitmap(Form1.Background);
        }

        public void Draw(Sprite sprite)
        {
            int x = (int)sprite.Position.X;
            int y = (int)sprite.Position.Y;
            int x_, y_;
            for (int j = 0; j < sprite.TextureRect_.Height; j++)
            {
                for (int i = 0; i < sprite.TextureRect_.Width; i++)
                {
                    x_ = Math.Abs(x) + i;
                    y_ = y + j;

                    try
                    {
                        Color Pixel = sprite.Result.GetPixel(i, j);
                        if (Pixel != Transparent)
                        {
                            bitmap.SetPixel(x_, y_, Pixel);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void Display()
        {
            Form1.Picture.Image = bitmap;
        }
    }
}
