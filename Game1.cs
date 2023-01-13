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
        Vector2 bodyOrient;
        Vector2 bodyPosition;
        Vector2 tailPosition;
        Vector2 applePosition;
        Vector2 velocity;
        Vector2 gridCenter;

        float epsilon;
        float rotation;
        float distance;

        int turnIndex;
        int xIndex;
        int yIndex;
        int gridSize;
        int snakeSpeed;

        Song song;

        Random rnd = new Random();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch SpriteBatch;

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
            gridSize = 32;
            headPosition = new Vector2((_graphics.PreferredBackBufferWidth / 2) - 176,
            _graphics.PreferredBackBufferHeight / 2);
            bodyOrient = new Vector2(gridSize, 0);
            bodyPosition = headPosition - bodyOrient;
            tailPosition = bodyPosition - bodyOrient;
            applePosition = new Vector2((rnd.Next(0, 20) * gridSize) + (gridSize / 2), (rnd.Next(0, 15) * gridSize) + (gridSize / 2));
            snakeSpeed = 200;
            velocity = new Vector2(snakeSpeed, 0);
            rotation = (float)Math.PI / 2;
            turnIndex = 0;
            epsilon = 2f;
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
//
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            headPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculate the x and y grid indices for the current position of the sprite
            xIndex = (int)(headPosition.X / gridSize);
            yIndex = (int)(headPosition.Y / gridSize);
            // Calculate the center position of the grid cell at the x and y indices
            gridCenter = new Vector2((xIndex * gridSize) + (gridSize / 2), (yIndex * gridSize) + (gridSize / 2));

            // Calculate the distance between the center of the grid cell and the current position of the sprite
            distance = Vector2.Distance(headPosition, gridCenter);

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
            if (distance < epsilon)
            {
                switch (turnIndex)
                {
                    case 1:
                        velocity = new Vector2(0, -snakeSpeed);
                        bodyOrient = new Vector2(0, -gridSize);
                        rotation = 0;
                        turnIndex = 0;
                        break;
                    case 2:
                        velocity = new Vector2(0, snakeSpeed);
                        bodyOrient = new Vector2(0, gridSize);
                        rotation = (float)Math.PI;
                        turnIndex = 0;
                        break;
                    case 3:
                        velocity = new Vector2(-snakeSpeed, 0);
                        bodyOrient = new Vector2(-gridSize, 0);
                        rotation = (float)Math.PI * 1.5f;
                        turnIndex = 0;
                        break;
                    case 4:
                        velocity = new Vector2(snakeSpeed, 0);
                        bodyOrient = new Vector2(gridSize, 0);
                        rotation = (float)Math.PI / 2;
                        turnIndex = 0;
                        break;
                }
            }
            // Update the position of the parts of the snake relative to the head.
            bodyPosition = headPosition - bodyOrient;
            tailPosition = bodyPosition - bodyOrient;

            //Does not allow the snake to exceed the boundaries. TODO: Game over if boundaries are exceeded.
            if (headPosition.X > _graphics.PreferredBackBufferWidth - snakeHead.Width / 2)
            {
                headPosition.X = _graphics.PreferredBackBufferWidth - snakeHead.Width / 2;
            }
            else if (headPosition.X < snakeHead.Width / 2)
            {
                headPosition.X = snakeHead.Width / 2;
            }

            if (headPosition.Y > _graphics.PreferredBackBufferHeight - snakeHead.Height / 2)
            {
                headPosition.Y = _graphics.PreferredBackBufferHeight - snakeHead.Height / 2;
            }
            else if (headPosition.Y < snakeHead.Height / 2)
            {
                headPosition.Y = snakeHead.Height / 2;
            }
            //If the snake head center is within epsilon of the apple center, set the apples location to the center of a random grid.
            if (Math.Abs(applePosition.X - headPosition.X) < epsilon &&
                Math.Abs(applePosition.Y - headPosition.Y) < epsilon)
            {
                applePosition = new Vector2((rnd.Next(0, 20) * gridSize) + (gridSize / 2), (rnd.Next(0, 15) * gridSize) + (gridSize / 2));

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();
            SpriteBatch.Draw(Background, new Rectangle(0, 0, 640, 480), Color.White);

            SnakeHeadClass headOb = new(snakeHead, headPosition, rotation, SpriteBatch, 0f);
            headOb.Draw();

            SnakeBodyClass bodyOb = new(snakeBody, bodyPosition, rotation, SpriteBatch, 0f);
            bodyOb.Draw();

            SnakeTailClass tailOb = new(snakeTail, tailPosition, rotation, SpriteBatch, 0f);
            tailOb.Draw();

            AppleClass AppleOb = new(Apple, applePosition, 0, SpriteBatch, 0f);
            AppleOb.Draw();

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}