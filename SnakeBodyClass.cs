using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using static System.Net.Mime.MediaTypeNames;

class SnakeBodyClass : SpriteClass
{
    public SnakeBodyClass(Texture2D texture, Vector2 position, float rotation) : base(texture, position, rotation)
    {
    }
    public void FollowPath(SpriteClass Ob, Texture2D turn, Texture2D stra)
    {
        if (Ob.RotList[^2] == Ob.Rotation)
        {
            if (Ob.PathList[^1].X - this.PathList[^1].X == 1 && Ob.PathList[^1].Y == this.PathList[^1].Y)
            {
                this.Position = Ob.Position + new Vector2(-32, 0);
            }
            if (Ob.PathList[^1].X - this.PathList[^1].X == -1 && Ob.PathList[^1].Y == this.PathList[^1].Y)
            {
                this.Position = Ob.Position + new Vector2(32, 0);
            }
            if (Ob.PathList[^1].Y - this.PathList[^1].Y == 1 && Ob.PathList[^1].X == this.PathList[^1].X)
            {
                this.Position = Ob.Position + new Vector2(0, -32);
            }
            if (Ob.PathList[^1].Y - this.PathList[^1].Y == -1 && Ob.PathList[^1].X == this.PathList[^1].X)
            {
                this.Position = Ob.Position + new Vector2(0, 32);
            }
            this.Texture = stra;
            this.Rotation = Ob.RotList[^2];
            this.SpriteEffect = SpriteEffects.None;
        }
        if (this.IsCentered() && Ob.RotList[^2] != Ob.Rotation)
        {
            this.Position = new Vector2(Ob.PathList[^1].X * _gridSize + 16, Ob.PathList[^1].Y * _gridSize + 16);
            this.Texture = turn;
            this.PathList.Add(Ob.PathList[^2]);

            // Clockwise
            if (Math.Abs(Ob.RotList[^2] - Ob.Rotation - (-(float)Math.PI / 2)) < .1f || (Math.Abs(Ob.RotList[^2] - Ob.Rotation - (1.5f * (float)Math.PI))) < .1f)
            {
                this.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            // Counter clockwise
            if (Math.Abs(Ob.RotList[^2] - Ob.Rotation - (float)Math.PI / 2) < .1f || (Math.Abs(Ob.RotList[^2] - Ob.Rotation - (-1.5f * (float)Math.PI))) < .1f)
            {
                this.SpriteEffect = SpriteEffects.None;
            }
            this.Rotation = Ob.RotList[^2];
        }
        if (this.RotList[^1] != this.Rotation)
        {
            this.RotList.Add(this.Rotation);
        }
    }
}