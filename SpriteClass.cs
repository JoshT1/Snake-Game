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
    protected List<Vector2> _velList;
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
        _velList = new List<Vector2>() { new Vector2(50, 0), new Vector2(50, 0) };
        _gridSize = 32;
        _node = new Vector2((int)Math.Floor(this.Position.X / _gridSize), (int)Math.Floor(this.Position.Y / _gridSize));
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(_texture, _position, null, _color, _rotation, _origin, _scale, this._effect, _depth);
        spriteBatch.End();
    }
    public void CurrentNode()
    {
        _node = new Vector2((int)Math.Floor(this.Position.X / _gridSize), (int)Math.Floor(this.Position.Y / _gridSize));
        if (Math.Abs(this.Position.X % _gridSize - _gridSize / 2) < 2f && Math.Abs(this.Position.Y % _gridSize - _gridSize / 2) < 2f)
        {
            if (this.PathList.Count == 1)
            {
                this.PathList.Add(_node);
            }
            else if (this.PathList.Last() != _node)
            {
                this.PathList.Add(_node);
            }
        }
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
    public List<Vector2> VelList
    {
        get { return _velList; }
        set { _velList = value; }
    }
    public int GridSize
    {
        get { return _gridSize; }
        set { _gridSize = value; }
    }
}
