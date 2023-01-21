using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

abstract class SpriteClass
{
    protected Texture2D _texture;
    protected Vector2 _position;
    protected Vector2 _origin;
    protected Color _color;
    protected float _rotation;
    protected Vector2 _scale;
    protected SpriteEffects _effect;
    protected float _depth;
    protected List<float> _rotList;
    protected List<Vector2> _pathList;
    protected int _gridSize;
    protected Vector2 _node;

    public SpriteClass(Texture2D texture, Vector2 position, float rotation, float depth)
    {
        _texture = texture;
        _position = position;
        _pathList = new List<Vector2>() { };
        _origin = new Vector2(texture.Width / 2, texture.Height / 2);
        _color = Color.White;
        _rotation = rotation;
        _rotList = new List<float>() { this.Rotation, this.Rotation };
        _scale = Vector2.One;
        _effect = SpriteEffects.None;
        _depth = depth;
        _gridSize = 32;
    }
    public Texture2D Texture
    {
        get { return _texture; }
        set { _texture = value; }
    }
    public Vector2 Position
    {
        get { return _position; }
        set { _position = value; }
    }
    public List<Vector2> PathList
    {
        get { return _pathList; }
        set { _pathList = value; }
    }
    public float Rotation
    {
        get { return _rotation; }
        set { _rotation = value; }
    }
    public List<float> RotList
    {
        get { return _rotList; }
        set { _rotList = value; }
    }
    public SpriteEffects SpriteEffect
    {
        get { return _effect; }
        set { _effect = value; }
    }
    public int GridSize
    {
        get { return _gridSize; }
        set { _gridSize = value; }
    }
        public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(_texture, _position, null, _color, _rotation, _origin, _scale, this._effect, _depth);
        spriteBatch.End();
    }
    public void CurrentNode()
    {
        // Calculate the x and y grid coordinates for the current position of the snake head.
        // If the grid node is new, append current node to pathList.
        _node = new Vector2((int)Math.Floor(this.Position.X / _gridSize), (int)Math.Floor(this.Position.Y / _gridSize));
        if (Math.Abs(this.Position.X % _gridSize - _gridSize / 2) < 1f && Math.Abs(this.Position.Y % _gridSize - _gridSize / 2) < 1f)
        {
            if (this.PathList.Count == 0)
            {
                this.PathList.Add(_node);
            }
            else if (this.PathList.Last() != _node)
            {
                this.PathList.Add(_node);
            }
        }
    }
    public void CenterSprite()
    {
        if (this.Rotation == (float)Math.PI * 1.5f || this.Rotation == (float)Math.PI / 2)
        {
            this.Position = new Vector2(this.Position.X, ((int)Math.Floor(this.Position.Y / _gridSize) * _gridSize) + _gridSize / 2);
        }
        if (this.Rotation == 0 || this.Rotation == (float)Math.PI)
        {
            this.Position = new Vector2(((int)Math.Floor(this.Position.X / _gridSize) * _gridSize) + _gridSize / 2, this.Position.Y);
        }
    }
    public Boolean IsCentered()
    {
        // Calculate the center position of the grid cell at the x and y indices
        Vector2 gridCenter = new Vector2((this.PathList[^1].X * _gridSize) + (_gridSize / 2), (this.PathList[^1].Y * _gridSize) + (_gridSize / 2));

        // Calculate the distance between the center of the grid cell and the current position of the sprite
        float distance = Vector2.Distance(this.Position, gridCenter);

        if (distance < 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
