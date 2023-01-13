using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
    protected SpriteBatch _spriteBatch;
    public SpriteClass(Texture2D texture, Vector2 position, float rotation, SpriteBatch spriteBatch, float depth)
	{
        _texture = texture;
        _position = position;
        _origin = new Vector2(texture.Width / 2, texture.Height / 2);
        _color = Color.White;
        _rotation = rotation;
        _scale = Vector2.One;
        _effect = SpriteEffects.None;
        _depth = depth;
        _spriteBatch = spriteBatch;
    }
    public void Draw()
    {
        _spriteBatch.Draw(_texture, _position, null, _color, _rotation, _origin, _scale, _effect, _depth);
    }
}
