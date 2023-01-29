using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class AppleClass : SpriteClass
{
    int gridSize;
    int count;
    Random rnd = new Random();
    public AppleClass(Texture2D texture, Vector2 position , float rotation, float depth) : base(texture, position, rotation, depth)
    {
        gridSize = 32;
        count = 0;
    }

    public Vector2 PickUpCheck(Vector2 position, float epsilon)
    {
        if (Math.Abs(this.Position.X - position.X) < epsilon &&
                Math.Abs(this.Position.Y - position.Y) < epsilon)
        {
            this.Position = new Vector2((rnd.Next(0, 20) * this.gridSize) + (this.gridSize / 2), (rnd.Next(0, 15) * this.gridSize) + (this.gridSize / 2));
            count++;
        }
        return this.Position;
    }
}
