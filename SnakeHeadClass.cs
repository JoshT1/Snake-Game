using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

class SnakeHeadClass : SpriteClass
{
    int gridSize;
    Vector2 node;
    public List<Vector2> pathList = new List<Vector2>();
    public List<float> rotList = new List<float>();
    public SnakeHeadClass(Texture2D texture, Vector2 position, float rotation, float depth ) : base(texture, position, rotation, depth)
    {
        gridSize = 32;
    }
    public Vector2 CurrentNode()
    {
        node = new Vector2((int)Math.Floor(this.Position.X / gridSize), (int)Math.Floor(this.Position.Y / gridSize));

        if (pathList.Count == 0)
        {
            pathList.Add(node);
            rotList.Add(this.Rotation);
        }
        else if (pathList.Last() != node)
        {
            pathList.Add(node);
            rotList.Add(this.Rotation);
        }
        //returns a new vector as an easy way to have a paired list to store the snakes movement history
        return node;
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
