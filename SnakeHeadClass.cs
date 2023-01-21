using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

class SnakeHeadClass : SpriteClass
{
    public SnakeHeadClass(Texture2D texture, Vector2 position, float rotation, float depth ) : base(texture, position, rotation, depth)
    {
    }
    public void BoundCheck(int width, int height)
    {
        if (this.Position.X > width - this.Texture.Width / 2)
        {
            this.Position = new Vector2(width - this.Texture.Width / 2, this.Position.Y);
        }
        else if (this.Position.X < this.Texture.Width / 2)
        {
            this.Position = new Vector2(this.Texture.Width / 2, this.Position.Y);
        }

        if (this.Position.Y > height - this.Texture.Height / 2)
        {
            this.Position = new Vector2(this.Position.X, height - this.Texture.Height / 2);

        }
        else if (this.Position.Y < this.Texture.Height / 2)
        {
            this.Position = new Vector2(this.Position.X, this.Texture.Height / 2);
        }
    }
}
