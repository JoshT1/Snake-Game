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
            this.PathList.Add(new Vector2(3, 7));
        }
        public void FollowPath(SpriteClass Ob, float sec)
        {
        Vector2 velocity = new Vector2(0, 0);
        if (Ob.PathList[^2].X > this.PathList[^1].X)
        {
            this.Position = new Vector2(((this.PathList[^1].X + 1) * this.GridSize) + (this.GridSize / 2), (this.PathList[^1].Y * this.GridSize) + (this.GridSize / 2));

        }
        if (Ob.PathList[^2].X < this.PathList[^1].X)
        {
            this.Position = new Vector2(((this.PathList[^1].X - 1) * this.GridSize) + (this.GridSize / 2), (this.PathList[^1].Y * this.GridSize) + (this.GridSize / 2));
        }
        if (Ob.PathList[^2].Y > this.PathList[^1].Y)
        {
           this.Position = new Vector2((this.PathList[^1].X * this.GridSize) + (this.GridSize / 2), ((this.PathList[^1].Y + 1) * this.GridSize) + (this.GridSize / 2));
        }
        if (Ob.PathList[^2].Y < this.PathList[^1].Y)
        {
            this.Position = new Vector2((this.PathList[^1].X * this.GridSize) + (this.GridSize / 2), ((this.PathList[^1].Y - 1) * this.GridSize) + (this.GridSize / 2));
        }
        this.Rotation = Ob.RotList[^1];
    }

}