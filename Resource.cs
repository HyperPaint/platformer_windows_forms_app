using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    public static class Resource
    {
        public static Sprite sprite = new Sprite( new Bitmap("Texture/block.png") );
        public const int BlockSize = 32;
        private static Rectangle IR = new Rectangle(0, 0, BlockSize, BlockSize);

        public static Sprite GetBlock(int index)
        {
            IR.X = index * BlockSize;
            sprite.TextureRect(IR);
            return sprite;
        }
    }
}
