// Terrain textures were taking from http://opengameart.org/textures/ just for testing purposes.
// Characters and Monsters texture were taking from http://untamed.wild-refuge.net/rmxpresources.php?characters just for testing purposes.
// Logo by Phillip Fowler

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        Credits,
        GameOver
    }

    //Enum for seperate states that run along side game states
    public enum CoState
    {
        
    }

    //Enum for who's turn it is
    public enum TurnState
    {
        Player,
        NPC
    }

    //Enum for unit selection
    public enum SelectState
    {
        Selected,
        NotSelected
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Map
        GridOccupant[,] map;

        //class declarations
        Level scenario;
        GamePlay gamePlay;
        GameVariables gameVars;
        CharacterSheet characterSheet;
        Cursor cursor;
        PlayerUnit selectedUnit;
        List<PlayerUnit> characterList;
        Drawer screenDrawer;

        //States
        GameState currentState;
        TurnState currentTurn;
        SelectState selected;

        //Keyboard states to track keys
        KeyboardState keyState;
        KeyboardState previousKeyState;

        //keep track of frame for animations
        int frameNum;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //set window size(need to settle on a size)
            graphics.PreferredBackBufferHeight = 780;
            graphics.PreferredBackBufferWidth = 1200;

            //set content directory
            Content.RootDirectory = "Content";

            //setup variable storage class
            gameVars = new GameVariables(this.Content);

            //set initial game state
            currentState = GameState.Menu;
            selected = SelectState.NotSelected;

            //set the cursor
            cursor = new Cursor();

            // Set up the draw class
            screenDrawer = new Drawer(gameVars, spriteBatch, GraphicsDevice);
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
            
            
            
            //set initial frame to zero
            frameNum = 0;
            gamePlay = new GamePlay();
            characterSheet = new CharacterSheet();
            characterSheet.Hide();
            
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
            //load images
            gameVars.setImages();

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
            //get current keyboard state
            keyState = Keyboard.GetState();

            //update certian elements based on gamestate
            switch (currentState)
            {
                case GameState.Intro:
                    {
                        if (keyState.IsKeyDown(Keys.Enter)) //if enter is pressed move to playing state
                        {
                            if (SingleKeyPress(keyState, previousKeyState, Keys.Enter))
                            {
                                currentState = GameState.Menu;
                                
                            }
                        }
                        if (keyState.IsKeyDown(Keys.Escape))
                        {
                            currentState = GameState.Menu;
                        }
                        break;
                    }
                case GameState.Menu:
                    {
                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            if (SingleKeyPress(keyState, previousKeyState, Keys.Enter))
                            {
                                currentState = GameState.Playing;
                                //setup level
                                SetupLevel("map.txt", "players.txt", "enemies.txt");
                                currentTurn = TurnState.Player;
                            }
                        }
                        if (keyState.IsKeyDown(Keys.Escape)) //if escape is pressed close game, this is a quick exit for testing 
                        {
                            Exit();
                        }
                        break;
                    }
                case GameState.Playing:
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        //players turn
                        if (currentTurn == TurnState.Player)
                        {
                            cursor.isVisible = true;
                            
                            //check for cursor movement
                            if (keyState.IsKeyDown(Keys.Up))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.Up))
                                {   //move the cursor up 1 grid space
                                    if (cursor.cursorPos.Y > 0)
                                    {
                                        cursor.cursorPos.Y -= 1;
                                    }
                                }
                            }

                            if (keyState.IsKeyDown(Keys.Down))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.Down))
                                {
                                    if (cursor.cursorPos.Y < 10)
                                    {
                                        cursor.cursorPos.Y += 1;
                                    }
                                }
                            }

                            if (keyState.IsKeyDown(Keys.Left))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.Left))
                                {   //move the cursor left 1 grid space
                                    if (cursor.cursorPos.X > 0)
                                    {
                                        cursor.cursorPos.X -= 1;
                                    }
                                }
                            }

                            if (keyState.IsKeyDown(Keys.Right))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.Right))
                                {   //move the cursor right 1 grid space
                                    if (cursor.cursorPos.X < 19)
                                    {
                                        cursor.cursorPos.X += 1;
                                    }
                                }
                            }

                            // Check if they selected a unit
                            if (keyState.IsKeyDown(Keys.Z))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.Z))
                                {
                                    int cursorX = cursor.cursorPos.X;
                                    int cursorY = cursor.cursorPos.Y;
                                    if (selected == SelectState.NotSelected)    // If we haven't selected a unit
                                    {
                                        if (map[cursorX, cursorY] is PlayerUnit)
                                        {
                                            selectedUnit = (PlayerUnit)map[cursorX, cursorY]; // Cast the unit
                                            selected = SelectState.Selected;
                                        }
                                    }
                                    else                                        // We have a unit selected, now we want to move
                                    {
                                        if (map[cursorX, cursorY] is Terrain)   // They aren't moving into another unit
                                            if (MovementValid(cursorX, cursorY, selectedUnit.X, selectedUnit.Y) == true)
                                            {
                                                Terrain tempHolder = (Terrain)map[cursorX, cursorY];
                                                if (tempHolder.Impassable == false) // If they *can* move there
                                                {
                                                    // Re-insert the terrain they're on right now
                                                    map[selectedUnit.X, selectedUnit.Y] = selectedUnit.OccupiedSpace;

                                                    // Assign the space they're moving into to occupiedSpace
                                                    selectedUnit.OccupiedSpace = (Terrain)map[cursorX, cursorY];

                                                    // Move the unit
                                                    map[cursorX, cursorY] = selectedUnit;
                                                    selectedUnit.X = cursorX;
                                                    selectedUnit.Y = cursorY;

                                                    // Deselect the unit, they've moved
                                                    selected = SelectState.NotSelected;
                                                }
                                            }
                                    }//End of else
                                }//End of singleKeyPress
                            }

                            if (keyState.IsKeyDown(Keys.X))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.X))
                                {
                                    if (selected == SelectState.Selected)
                                    {
                                        selected = SelectState.NotSelected;
                                    }
                                }
                            }

                            if(keyState.IsKeyDown(Keys.C))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.C))
                                {
                                    if(characterSheet.IsOpen())
                                    {
                                        gamePlay.CloseCharacterSheet(characterSheet);
                                    }
                                    else if (!characterSheet.IsOpen())
                                    {
                                        gamePlay.OpenCharacterSheet(characterSheet);
                                    }
                                }
                            }


                            if (Keyboard.GetState().GetPressedKeys().Length > 0)
                            {
                                gamePlay.MoveCursor(keyState);
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //npc's turn
                        if(currentTurn == TurnState.NPC)
                        {
                            //read through list of enemy npcs and perform actions
                            {

                            }
                        }
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////

                        if (keyState.IsKeyDown(Keys.Escape)) //if escape is pressed close game, this is a quick exit for testing 
                        {
                            Exit();
                        }
                        break;
                    }
                case GameState.Won:
                    {
                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            if (SingleKeyPress(keyState, previousKeyState, Keys.Enter))
                            {

                                currentState = GameState.Credits;
                            }
                        }
                        if (keyState.IsKeyDown(Keys.Escape)) //if escape is pressed close game, this is a quick exit for testing 
                        {
                            Exit();
                        }
                        break;
                    }
                case GameState.GameOver:
                    {
                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            if (SingleKeyPress(keyState, previousKeyState, Keys.Enter))
                            {
                                
                                currentState = GameState.Menu;
                            }
                        }
                        if (keyState.IsKeyDown(Keys.Escape)) //if escape is pressed close game, this is a quick exit for testing 
                        {
                            Exit();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }//end gamestate switch

            previousKeyState = keyState;//set previous state for next update
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
            

            switch(currentState)
            {
                case GameState.Intro:
                    {
                        GraphicsDevice.Clear(Color.White);

                        screenDrawer.DrawIntro(); //draws the game intro

                        break;
                    }
                case GameState.Menu:
                    {   //stubs for menu code, will need graphics and such here
                        GraphicsDevice.Clear(Color.Black);

                        screenDrawer.DrawMenu(); //draws the game menu

                        break;
                    }
                case GameState.Playing:
                    {
                        screenDrawer.DrawMap(map);
                        cursor.Draw(spriteBatch, gameVars.cursor);
                        break;
                    }
                case GameState.Credits:
                    {   //stubs for menu code, will need graphics and such here
                        GraphicsDevice.Clear(Color.Black);

                        screenDrawer.DrawCredits(); //draws the game menu

                        break;
                    }
            }//end switch
            spriteBatch.End();
            base.Draw(gameTime);
        }



        /// <summary>
        /// Will need information passed in to determine which level to setup.
        /// Currently only contains test code.
        /// </summary>
        protected void SetupLevel(string mapName, string pListName, string eListName)
        {
            // Load map
            scenario = new Level(mapName, pListName, eListName, gameVars);
            characterList = scenario.PlayerList;
            map = scenario.MapGrid;
            
        }//end setup level


        /// <summary>
        /// check to see if a key has been pressed only once
        /// </summary>
        /// <param name="current"></param>
        /// <param name="previous"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool SingleKeyPress(KeyboardState current, KeyboardState previous, Keys key)
        {
            if (current.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"> cursor.CursorPos.X </param>
        /// <param name="cY"> cursor.CursorPos.Y </param>
        /// <param name="sX"> selectedUnit.X </param>
        /// <param name="sY"> selectedUnit.Y </param>
        /// <returns></returns>
        private bool MovementValid(int cX, int cY, int sX, int sY)
        {
            bool resultFlag = false;

            if ((cX == (sX - 1) && cY == sY) || (cX == (sX + 1) && cY == sY) || (cY == (sY - 1) && cX == sX) || (cY == (sY + 1) && cX == sX))
                resultFlag = true;

            return resultFlag;
        }

    }//end class
}
