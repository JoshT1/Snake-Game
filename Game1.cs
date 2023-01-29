using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SnakeProject
{
    public class Game1 : Game
    {
        // Declare asset variables
        Texture2D Background;
        Texture2D snakeHead;
        Texture2D snakeBody;
        Texture2D snakeTail;
        Texture2D snakeBodyTurn;
        Texture2D Apple;

        Vector2 headPosition;
        Vector2 bodyPosition;
        Vector2 applePosition;
        Vector2 velocity;

        float rotation;

        int turnIndex;
        int snakeSpeed;

        Song song;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch SpriteBatch;

        private SnakeHeadClass headOb;
        private SnakeBodyClass bodyOb;
        private SnakeBodyClass tailOb;
        private AppleClass AppleOb;

        List<SnakeBodyClass> snakeList = new List<SnakeBodyClass>();



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content/Asset/";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            headPosition = new Vector2((_graphics.PreferredBackBufferWidth / 2) - 176,
            _graphics.PreferredBackBufferHeight / 2);
            bodyPosition = headPosition + new Vector2(-32, 0);
            applePosition = new Vector2((_graphics.PreferredBackBufferWidth / 2) + 176,
            _graphics.PreferredBackBufferHeight / 2);
            snakeSpeed = 100;
            velocity = new Vector2(snakeSpeed, 0);
            rotation = (float)Math.PI / 2;
            turnIndex = 0;
            MediaPlayer.Volume = 0.1f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Background = Content.Load<Texture2D>("Background");
            snakeHead = Content.Load<Texture2D>("Snake-Head");
            snakeBody = Content.Load<Texture2D>("Snake-Body");
            snakeBodyTurn = Content.Load<Texture2D>("Snake-Body-Turn");
            snakeTail = Content.Load<Texture2D>("Snake-Tail");
            Apple = Content.Load<Texture2D>("Apple");
            song = Content.Load<Song>("Tasty");

            //Create sprite objects
            headOb = new SnakeHeadClass(snakeHead, headPosition, rotation);
            bodyOb = new SnakeBodyClass(snakeBody, bodyPosition, rotation);
            tailOb = new SnakeBodyClass(snakeTail, bodyPosition + new Vector2(-32, 0), rotation);
            AppleOb = new AppleClass(Apple, applePosition, 0);

            //fill initial snake list
            snakeList.Add(bodyOb);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                for (int i = 0; i < snakeList.Count; i++)
                {
                    Debug.WriteLine(i.ToString() + " " + snakeList[i].ToString());
                    Debug.WriteLine("---");
                }

                Exit();

            }

            // TODO: Add your update logic here

            headOb.CurrentNode();
            snakeList.ForEach(i => i.CurrentNode());
            tailOb.CurrentNode();

            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W) && (velocity.Y != snakeSpeed))
            {
                turnIndex = 1;
            }

            if (kstate.IsKeyDown(Keys.S) && (velocity.Y != -snakeSpeed))
            {
                turnIndex = 2;
            }

            if (kstate.IsKeyDown(Keys.A) && (velocity.X != snakeSpeed))
            {
                turnIndex = 3;

            }

            if (kstate.IsKeyDown(Keys.D) && (velocity.X != -snakeSpeed))
            {
                turnIndex = 4;
            }
            if (headOb.IsCentered())
            {

                switch (turnIndex)
                {
                    case 1:
                        velocity = new Vector2(0, -snakeSpeed);
                        headOb.Rotation = 0;
                        turnIndex = 0;
                        break;
                    case 2:
                        velocity = new Vector2(0, snakeSpeed);
                        headOb.Rotation = (float)Math.PI;
                        turnIndex = 0;
                        break;
                    case 3:
                        velocity = new Vector2(-snakeSpeed, 0);
                        headOb.Rotation = (float)Math.PI * 1.5f;
                        turnIndex = 0;
                        break;
                    case 4:
                        velocity = new Vector2(snakeSpeed, 0);
                        headOb.Rotation = (float)Math.PI / 2;
                        turnIndex = 0;
                        break;
                    default:
                        break;
                }
                headOb.RotList.Add(headOb.Rotation);
                snakeList.ForEach(i => i.RotList.Add(i.Rotation));
                tailOb.RotList.Add(tailOb.Rotation);
            }
            //Center the sprites along the grid
            headOb.CenterSprite();
            snakeList.ForEach(i => i.CenterSprite());
            tailOb.CenterSprite();

            // Update the position of the parts of the snake relative to the head.
            headOb.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (SnakeBodyClass ob in snakeList)
            {
                if(snakeList.IndexOf(ob) == 0) 
                {
                    ob.FollowPath(headOb, snakeBodyTurn, snakeBody);
                }
                else
                {
                    ob.FollowPath(snakeList[snakeList.IndexOf(ob) - 1], snakeBodyTurn, snakeBody);
                }
            }
            tailOb.FollowPath(snakeList[^1], snakeBodyTurn, snakeTail);

            //Does not allow the snake to exceed the boundaries. TODO: Game over if boundaries are exceeded.
            headOb.BoundCheck(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

        //If the snake head center is within epsilon of the apple center, set the apples location to the center of a random node.
        AppleOb.PickUpCheck(headOb.Position,snakeList,tailOb, snakeBody);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();
            SpriteBatch.Draw(Background, new Rectangle(0, 0, 640, 480), Color.White);
            SpriteBatch.End();

            AppleOb.Draw(SpriteBatch);
            tailOb.Draw(SpriteBatch);
            snakeList.ForEach(i => i.Draw(SpriteBatch));
            headOb.Draw(SpriteBatch);

            base.Draw(gameTime);
        }
    }
}