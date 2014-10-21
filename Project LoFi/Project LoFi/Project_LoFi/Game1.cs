using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project_LoFi
{
    //Enum for Game States
    public enum GameState
    {
        Intro,
        Menu,
        Playing,
        Won,
        GameOver
    }

    //Enum for seperate states that run along side game states
    public enum CoState
    {

    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameVariables gameVars;

        //Game State
        GameState currentState;

        //Keyboard states to track keys
        KeyboardState keyState;
        KeyboardState previousKeySate;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //set window size(need to settle on a size)
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            //set content directory
            Content.RootDirectory = "Content";

            gameVars = new GameVariables(Content);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //load fonts
            gameVars.setFont();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.DrawString(gameVars.Font1Bold, "Try Again Studios Presents",
                            new Vector2((GraphicsDevice.Viewport.Width / 2) - 390, (GraphicsDevice.Viewport.Height / 2) - 150), Color.DarkRed, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0.0f);

            spriteBatch.DrawString(gameVars.Font1Bold, "presents",
                            new Vector2((GraphicsDevice.Viewport.Width / 2) - 180, (GraphicsDevice.Viewport.Height / 2) - 50), Color.DarkRed, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.0f);

            spriteBatch.DrawString(gameVars.Font1Bold, "PROJECT LO-FI",
                            new Vector2((GraphicsDevice.Viewport.Width / 2) - 375, (GraphicsDevice.Viewport.Height / 2)), Color.DarkRed, 0.0f, Vector2.Zero, 5.0f, SpriteEffects.None, 0.0f);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
