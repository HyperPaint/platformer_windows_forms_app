using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class Person : Entity
    {
        public Person(Vector2f SpawnPoint, int EntityNumber = 0, float dx = 1, float dy = 1) : base(SpawnPoint, EntityNumber, dx, dy)
        {
            isMoving = true;
        }

        public override Sprite Draw()
        {
            Process();

            if (FacingRight)
            {
                if (isMoving)
                {
                    Rectangle IR = new Rectangle();
                    IR.X = Frame % 64 / 16 * 32;
                    IR.Y = EntityNumber * 48;
                    IR.Width = 32;
                    IR.Height = 48;
                    Sprite.TextureRect(IR);
                }
                else
                {
                    Rectangle IR = new Rectangle();
                    IR.X = 0;
                    IR.Y = EntityNumber * 48;
                    IR.Width = 32;
                    IR.Height = 48;
                    Sprite.TextureRect(IR);
                }
            }
            else
            {
                if (isMoving)
                {
                    Rectangle IR = new Rectangle();
                    IR.X = Frame % 64 / 16 * 32 + 32;
                    IR.Y = EntityNumber * 48;
                    IR.Width = -32;
                    IR.Height = 48;
                    Sprite.TextureRect(IR);
                }
                else
                {
                    Rectangle IR = new Rectangle();
                    IR.X = 32;
                    IR.Y = EntityNumber * 48;
                    IR.Width = -32;
                    IR.Height = 48;
                    Sprite.TextureRect(IR);
                }
            }

            Sprite.Position = new Vector2f(GetPosition.X - Form1.MyEntity.GetPosition.X + Entity.OffsetX, GetPosition.Y - Form1.MyEntity.GetPosition.Y + Entity.OffsetY);

            Frame++;
            return Sprite;
        }

        protected override void Process()
        {
            base.Process();
            if (new Random().Next(1, 500) == 1)
            {
                FacingRight = !FacingRight;
            }
            else if (new Random().Next(1, 500) == 1)
            {
                Jump();
            }
            else if (new Random().Next(1, 500) == 1)
            {
                isMoving = !isMoving;
            }
        }
    }
}
