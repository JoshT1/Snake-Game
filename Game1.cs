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
        int gridSize;
        int snakeSpeed;
        //
        Vector2 a = new Vector2(0, 0);
        //

        Song song;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch SpriteBatch;

        private SnakeHeadClass headOb;
        private SnakeBodyClass bodyOb;
        private SnakeTailClass tailOb;
        private AppleClass AppleOb;

        bool ifStatementTriggered;



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
            applePosition = new Vector2((_graphics.PreferredBackBufferWidth / 2) + 176,
            _graphics.PreferredBackBufferHeight / 2);
            snakeSpeed = 50;
            velocity = new Vector2(snakeSpeed, 0);
            rotation = (float)Math.PI / 2;
            turnIndex = 0;
            epsilon = 2f;
            MediaPlayer.Volume = 0.1f;
            ifStatementTriggered = false;
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
            headOb = new SnakeHeadClass(snakeHead, headPosition, rotation, 0f);
            bodyOb = new SnakeBodyClass(snakeBody, bodyPosition, rotation, 0f);
            tailOb = new SnakeTailClass(snakeTail, tailPosition, rotation, 0f);
            AppleOb = new AppleClass(Apple, applePosition, 0, 0f);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            headOb.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;



            // Calculate the x and y grid indices for the current position of the snake head.
            // If the grid node is new, append current node to pathList.
            Vector2 test = headOb.CurrentNode();

            // Calculate the center position of the grid cell at the x and y indices
            gridCenter = new Vector2((test.X * gridSize) + (gridSize / 2), (test.Y * gridSize) + (gridSize / 2));

            // Calculate the distance between the center of the grid cell and the current position of the sprite
            distance = Vector2.Distance(headOb.Position, gridCenter);

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
                        headOb.Rotation = 0;
                        turnIndex = 0;
                        break;
                    case 2:
                        velocity = new Vector2(0, snakeSpeed);
                        bodyOrient = new Vector2(0, gridSize);
                        headOb.Rotation = (float)Math.PI;
                        turnIndex = 0;
                        break;
                    case 3:
                        velocity = new Vector2(-snakeSpeed, 0);
                        bodyOrient = new Vector2(-gridSize, 0);
                        headOb.Rotation = (float)Math.PI * 1.5f;
                        turnIndex = 0;
                        break;
                    case 4:
                        velocity = new Vector2(snakeSpeed, 0);
                        bodyOrient = new Vector2(gridSize, 0);
                        headOb.Rotation = (float)Math.PI / 2;
                        turnIndex = 0;
                        break;
                }

                // Update the position of the parts of the snake relative to the head.
                /*
                if (headOb.pathList.Count > 2 && a != headOb.pathList.Last())
                {
                    tailPosition = new Vector2((headOb.pathList[^3].X * gridSize) + (gridSize / 2), (headOb.pathList[^3].Y * gridSize) + (gridSize / 2));
                    rotation = headOb.Rotation;
                    
                }
                */
                if (headOb.pathList.Count > 1 && a != headOb.pathList.Last())
                {
                    bodyOb.Position = new Vector2((headOb.pathList[^2].X * gridSize) + (gridSize / 2), (headOb.pathList[^2].Y * gridSize) + (gridSize / 2));
                    if ((headOb.rotList[^2] == (float)Math.PI / 2 || headOb.rotList[^2] == (float)Math.PI * 1.5f) && Math.Abs(headOb.pathList[^2].Y - headOb.pathList[^1].Y) > 0)
                    {
                        bodyOb.Rotation = headOb.rotList[^2];
                        bodyOb.Texture = snakeBodyTurn;
                        ifStatementTriggered = true;
                    }
                    else if ((headOb.rotList[^2] == (float)Math.PI|| headOb.rotList[^2] == 0f) && Math.Abs(headOb.pathList[^2].X - headOb.pathList[^1].X) > 0)
                    {
                        bodyOb.Rotation = headOb.rotList[^2];
                        bodyOb.Texture = snakeBodyTurn;
                        ifStatementTriggered = true;
                    }
                    else if (ifStatementTriggered)
                    {
                        ifStatementTriggered = false;
                        bodyOb.Rotation = headOb.rotList[^1];
                        bodyOb.Texture = snakeBody;
                    }
                    a = headOb.pathList.Last();
                }
            }

            bodyOb.Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Does not allow the snake to exceed the boundaries. TODO: Game over if boundaries are exceeded.
            headOb.BoundCheck(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            //If the snake head center is within epsilon of the apple center, set the apples location to the center of a random node.
            AppleOb.PickUpCheck(headOb.Position, epsilon);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();
            SpriteBatch.Draw(Background, new Rectangle(0, 0, 640, 480), Color.White);
            SpriteBatch.End();

            headOb.Draw(SpriteBatch);
            bodyOb.Draw(SpriteBatch);
            tailOb.Draw(SpriteBatch);
            AppleOb.Draw(SpriteBatch);

            base.Draw(gameTime);
        }
    }
}