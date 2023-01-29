using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

class AppleClass : SpriteClass
{
    int gridSize;
    int count;
    Random rnd = new Random();
    public AppleClass(Texture2D texture, Vector2 position , float rotation) : base(texture, position, rotation)
    {
        gridSize = 32;
        count = 0;
    }

    public Vector2 PickUpCheck(Vector2 position, List<SnakeBodyClass> sl, SnakeBodyClass tail, Texture2D stra)
    {
        if (Math.Abs(this.Position.X - position.X) < 1f &&
                Math.Abs(this.Position.Y - position.Y) < 1f)
        {
            this.Position = new Vector2((rnd.Next(0, 20) * this.gridSize) + (this.gridSize / 2), (rnd.Next(0, 15) * this.gridSize) + (this.gridSize / 2));
            count++;
            /*
            SnakeBodyClass newBody = tail;
            newBody.Texture = stra;
            sl.Add(newBody);
            tail.Position = new Vector2(tail.PathList[^2].X * _gridSize + 16, tail.PathList[^2].Y * _gridSize + 16);
            */

        }
        return this.Position;
    }
}
