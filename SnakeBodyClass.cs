using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using static System.Net.Mime.MediaTypeNames;

class SnakeBodyClass : SpriteClass
{
    public SnakeBodyClass(Texture2D texture, Vector2 position, float rotation, float depth) : base(texture, position, rotation, depth)
    {
    }
    public void FollowPath(SpriteClass Ob, float sec, Texture2D turn, Texture2D stra)
    {
        Vector2 velocity = new Vector2(0, 0);
        if (Ob.PathList.Count > 1 && Ob.RotList[^2] == Ob.RotList[^1])
        {
            if (Ob.PathList[^1].X - this.PathList[^1].X == 1 && Ob.PathList[^1].Y == this.PathList[^1].Y)
            {
                //velocity = new Vector2(100, 0);
                this.Position = Ob.Position + new Vector2(-32, 0);
            }
            if (Ob.PathList[^1].X - this.PathList[^1].X == -1 && Ob.PathList[^1].Y == this.PathList[^1].Y)
            {
                //velocity = new Vector2(-100, 0);
                this.Position = Ob.Position + new Vector2(32, 0);
            }
            if (Ob.PathList[^1].Y - this.PathList[^1].Y == 1 && Ob.PathList[^1].X == this.PathList[^1].X)
            {
                //velocity = new Vector2(0, 100);
                this.Position = Ob.Position + new Vector2(0, -32);
            }
            if (Ob.PathList[^1].Y - this.PathList[^1].Y == -1 && Ob.PathList[^1].X == this.PathList[^1].X)
            {
                //velocity = new Vector2(0, -100);
                this.Position = Ob.Position + new Vector2(0, 32);
            }
            this.Texture = stra;
            this.Rotation = Ob.RotList[^1];
        }
        if (Ob.RotList[^2] != Ob.RotList[^1])
        {
            if (this.IsCentered())
            {
                this.Position = new Vector2(Ob.PathList[^1].X * _gridSize + 16 , Ob.PathList[^1].Y * _gridSize + 16);
                this.Texture = turn;
                this.PathList.Add(Ob.PathList[^1]);
            }
        }
        this.Position += velocity * sec;

    }

}