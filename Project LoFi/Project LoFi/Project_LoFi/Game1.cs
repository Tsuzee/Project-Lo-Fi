// Terrain textures were taking from http://opengameart.org/textures/ just for testing purposes.
// Characters and Monsters texture were taking from http://untamed.wild-refuge.net/rmxpresources.php?characters just for testing purposes.
/*
Credits: 
Songs: 
James Opie 
http://jamesopiecomposer.com/
Alone By The Fire

Braiton
http://braiton.newgrounds.com/
Heart of Fire


Leifthrasir
http://leifthrasir.newgrounds.com/
Shui Xian Hua

TravisGladue
http://travisgladue.newgrounds.com/
TLOZ Minish - Game Over

alertG
http://alertg.newgrounds.com/
Nature Adventure

ZykiaZyoki
http://zykiazyoki.newgrounds.com/
gameOver


SoundEffects:
http://www.freesound.org
*/
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
        Drawer screenDrawer;
        EnemyAI enemyAI;

        //States
        GameState currentState;
        TurnState currentTurn;
        SelectState selected;

        //Keyboard states to track keys
        KeyboardState keyState;
        KeyboardState previousKeyState;

        //Lists
        List<PlayerUnit> characterList;
        List<EnemyUnit> enemyList;
        List<PlayerUnit> activePList;
        
        //Arrays
        // 0:Number of turns, 1:did player move, 2:player name, 3: did enemy move, 4:enemy name, 5:was attack called, 6:dmg delt
        // 7:Attacker, 8:Target
        string[] textLog; 
        


        //variables
        int numOfTurns;
        double timer;
        int enemyNum;
        float timeToMove;
        int frameNum;
        int level;
        int maxLevel = 3;
        bool winSound = false;

        bool songStarted = false;
        private Random rand;

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
            screenDrawer = new Drawer();

            //set number of turns to 6
            numOfTurns = 6;
            enemyNum = 0;
            timeToMove = 2;

            //set level to 1
            level = 1;
            //setup the textLog array
            textLog = new string[10]{numOfTurns.ToString(), "false", "", "false", "", "false", "", "", "", ""};
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
            
            gamePlay = new GamePlay();
            characterSheet = new CharacterSheet();
            characterSheet.Hide();
            rand = new Random();
            frameNum = 0;
            
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

            gameVars.setSounds();

            cursor.setCursorTextures(gameVars.cursor, gameVars.selCursor);
            
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

            timer += gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here

            //get current keyboard state
            keyState = Keyboard.GetState();


            // Play Songs at different stages 
            if (currentState == GameState.Menu)
            {
                if (!songStarted)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(gameVars.inGame[5]);
                    songStarted = true;
                }
                if (keyState.IsKeyDown(Keys.Enter))
                {
                    MediaPlayer.IsRepeating = false;
                    songStarted = false;
                }
            }
            else if (currentState == GameState.Credits)
            {
                if (!songStarted)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(gameVars.inGame[3]);
                    songStarted = true;
                }
                if (keyState.IsKeyDown(Keys.Enter))
                {
                    MediaPlayer.IsRepeating = false;
                    songStarted = false;
                }
            }
            else if (currentState == GameState.GameOver)
            {
                if (!songStarted)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(gameVars.inGame[4]);
                    songStarted = true;
                }
                if (keyState.IsKeyDown(Keys.Enter))
                {
                    MediaPlayer.IsRepeating = false;
                    songStarted = false;
                }
            }



            //update certain elements based on gamestate
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
                                if(level > 3)
                                {
                                    ResetGameForNextLevel();
                                    level = 1;
                                }
                                currentState = GameState.Playing;

                                //setup level
                                //level = 3; //test code to force a level not 1 to start first
                                SetupLevel(level, "ItemDatabase.txt", "players.LPF", "MonsterDatabase.txt");

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
                        
                        enemyList = scenario.enemyList;

                        if (frameNum % 30 == 0)
                        {
                            characterSheet.UpdateCharSheet(characterList);
                            
                            if (frameNum > 3000) { frameNum = 0; }
                        }
                        frameNum++;

                        //screenDrawer.enemyList = enemyList;
                        /////////////////////////////////////////////////////////////////////////////////////////////////////
                        //players turn
                        if (currentTurn == TurnState.Player)
                        {
                            screenDrawer.HighlightCurrentEnemy(false, null);
                            textLog[0] = numOfTurns.ToString();
                            if (numOfTurns == 0)
                            {
                                numOfTurns = 6;
                                textLog[3] = "false";
                                textLog[1] = "false";
                                
                            }
                            cursor.isVisible = true;
                            screenDrawer.updateTextLog(textLog);
                            
                            
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
                                            cursor.Selected = true;
                                            selectedUnit = (PlayerUnit)map[cursorX, cursorY]; // Cast the unit
                                            //screenDrawer.SelectedUnit = selectedUnit;
                                            selected = SelectState.Selected;
                                        }
                                    }
                                    else                                        // We have a unit selected, now we want to move
                                    {
                                        if (map[cursorX, cursorY] is Terrain)   // They aren't moving into another unit
                                        {
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

                                                    //update text log info to be drawn to screen
                                                    textLog[1] = "true";
                                                    textLog[2] = selectedUnit.Name;
                                                    textLog[5] = "false";
                                                    textLog[6] = "";
                                                    textLog[7] = "";
                                                    textLog[8] = "";
                                                    screenDrawer.updateTextLog(textLog);

                                                    // Deselect the unit, they've moved
                                                    selected = SelectState.NotSelected;
                                                    cursor.Selected = false;

                                                    //change cursor back to unselected and decrement their number of turns
                                                    //screenDrawer.SelectedUnit = null;
                                                    selectedUnit = null;
                                                    numOfTurns--;
                                                    
                                                }
                                            }
                                        }
                                        else if (map[cursorX, cursorY] is EnemyUnit)
                                        {
                                            if (MovementValid(cursorX, cursorY, selectedUnit.X, selectedUnit.Y) == true)
                                            {
                                                EnemyUnit target = (EnemyUnit)map[cursorX, cursorY];
                                                //pop up pre-attack info here (e.g. tell them the expected result)
                                                //Will also need to mess with adding more states again, to see if
                                                //they press Z to confirm the attack or X to cancel

                                                //update text log info to be drawn to screen
                                                textLog[1] = "false";
                                                textLog[7] = selectedUnit.Name;
                                                textLog[8] = target.Name;
                                                textLog[5] = "true";
                                                screenDrawer.updateTextLog(textLog);

                                                textLog[6] = selectedUnit.Attack(target).ToString();

                                                //attack sounds
                                                int soundNum = DetermineCharcaterAttackSoundAccordingToJesse(selectedUnit);

                                                if (soundNum == 1)
                                                    gameVars.attackWithSword.Play();
                                                else if (soundNum == 2)
                                                    gameVars.rogueAttack.Play();
                                                else if (soundNum == 3)
                                                    gameVars.mageAttack.Play();


                                                if (target.IsDead() == true)
                                                {
                                                    //remove monster corpse
                                                    target.RemoveCorpse(map);

                                                    enemyList.Remove(target);
                                                    if (target.IsBoss)
                                                    {
                                                        currentState = GameState.Won;
                                                        winSound = true;
                                                    }
                                                }

                                                // Deselect the unit, they've attacked
                                                selected = SelectState.NotSelected;
                                                cursor.Selected = false;

                                                //change cursor back to unselected and decrement their number of turns
                                                selectedUnit = null;
                                                numOfTurns--;
                                            }
                                        }
                                    }//End of else
                                }//End of singleKeyPress
                            }

                            if (keyState.IsKeyDown(Keys.X))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.X))
                                {
                                    cursor.Selected = false;
                                    //screenDrawer.SelectedUnit = null;
                                    if (selected == SelectState.Selected)
                                    {
                                        selected = SelectState.NotSelected;
                                        selectedUnit = null;
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

                            //if player uses all their turns change game state
                            if(numOfTurns == 0)
                            {
                                currentTurn = TurnState.NPC;
                                timer = 0;
                                enemyNum = 0;
                                timeToMove = 2;
                                cursor.isVisible = false;
                            }
                        }
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //npc's turn
                        if(currentTurn == TurnState.NPC)
                        {
                            if (keyState.IsKeyDown(Keys.C))
                            {
                                if (SingleKeyPress(keyState, previousKeyState, Keys.C))
                                {
                                    if (characterSheet.IsOpen())
                                    {
                                        gamePlay.CloseCharacterSheet(characterSheet);
                                    }
                                    else if (!characterSheet.IsOpen())
                                    {
                                        gamePlay.OpenCharacterSheet(characterSheet);
                                    }
                                }
                            }

                            textLog[0] = numOfTurns.ToString();
                            textLog[1] = "false";
                            screenDrawer.updateTextLog(textLog);

                            //read through list of enemy npcs and perform actions
                            EnemyUnit enemy;

                            if( timer > timeToMove)
                            {
                                enemy = enemyList[enemyNum];
                                EnemyLogic(enemy);
                                if(enemyNum < enemyList.Count())
                                {
                                    enemyNum++;
                                }
                                timeToMove += 2;
                            }

                            //change back to players state
                            if(enemyNum == enemyList.Count())
                            {
                                currentTurn = TurnState.Player;
                                timer = 0;
                            }
                            
                            
                        }//end NPC turns
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////

                        if (keyState.IsKeyDown(Keys.Escape)) //if escape is pressed close game, this is a quick exit for testing 
                        {
                            Exit();
                        }

                        foreach (PlayerUnit scrub in characterList)
                        {
                            if (scrub.CurrentExp >= scrub.Level * 8) 
                            { 
                                scrub.CurrentExp -= scrub.Level * 8;
                                scrub.Level += 1;
                                scrub.Dexterity += 2;
                                scrub.Magic += 2;
                                scrub.Strength += 2;
                                scrub.setStats();
                            }
                        }


                        //secert key to auto win level
                        if (keyState.IsKeyDown(Keys.NumLock)) //if escape is pressed close game, this is a quick exit for testing 
                        {
                            currentState = GameState.Won;
                            winSound = true;
                        }
                        break;
                    }
                case GameState.Won:
                    {
                      
                        if( winSound )
                        {
                            gameVars.levelComplete.Play();
                            winSound = false;
                        }

                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            if (SingleKeyPress(keyState, previousKeyState, Keys.Enter))
                            {
                                level++;

                                if(level >= 4)
                                {
                                    currentState = GameState.Credits;
                                }
                                else
                                {
                                    currentState = GameState.Playing;
                                    //setup level
                                    SetupLevel(level, "ItemDatabase.txt", "players.LPF", "MonsterDatabase.txt");

                                    currentTurn = TurnState.Player;
                                }
                            }
                        }
                        if (keyState.IsKeyDown(Keys.Escape)) //if escape is pressed close game, this is a quick exit for testing 
                        {
                            Exit();
                        }
                        break;
                    }
                case GameState.Credits:
                    {
                        if (keyState.IsKeyDown(Keys.Enter))
                        {
                            if (SingleKeyPress(keyState, previousKeyState, Keys.Enter))
                            {
                                currentState = GameState.Menu;
                            }
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
                        if(frameNum > 299)
                        {
                            currentState = GameState.Menu;
                            break;
                        }
                        GraphicsDevice.Clear(Color.White);

                        screenDrawer.DrawIntro(gameVars, spriteBatch, GraphicsDevice); //draws the game intro

                        frameNum++;
                        break;
                    }
                case GameState.Menu:
                    {   //stubs for menu code, will need graphics and such here
                        GraphicsDevice.Clear(Color.Black);

                        //screenDrawer.DrawMenu(gameVars, spriteBatch, GraphicsDevice); //draws the game menu
                        screenDrawer.DrawStory(gameVars, spriteBatch, GraphicsDevice);
                        frameNum = 0;
                        break;
                    }
                case GameState.Playing:
                    {
                        
                        screenDrawer.DrawMap(map, spriteBatch, gameVars);
                        if (selectedUnit != null)
                            screenDrawer.DrawHighlighter(map, selectedUnit, gameVars, spriteBatch);
                        cursor.Draw(spriteBatch, map, gameVars);
                        screenDrawer.DrawGameInfo(gameVars, spriteBatch, GraphicsDevice, currentTurn);
                        break;
                    }
                case GameState.Won:
                    {
                        screenDrawer.DrawWon(gameVars, spriteBatch, GraphicsDevice, level, maxLevel);
                        break;
                    }
                case GameState.GameOver:
                    {
                        screenDrawer.DrawGameOver(gameVars, spriteBatch, GraphicsDevice);
                        break;
                    }
                case GameState.Credits:
                    {   //stubs for menu code, will need graphics and such here
                        GraphicsDevice.Clear(Color.Black);

                        screenDrawer.DrawCredits(gameVars, spriteBatch, GraphicsDevice); //draws the game menu

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
        protected void SetupLevel(int level, string itemListName, string pListName, string eListName)
        {
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;
                // Load map
                switch (level)
                {
                    case 1:
                        {
                            MediaPlayer.Play(gameVars.inGame[0]);
                            scenario = new Level("map1alt.txt", itemListName, pListName, eListName, gameVars);
                            textLog[9] = "Defeat the Red Devil";
                            screenDrawer.updateTextLog(textLog);
                            characterList = scenario.PlayerList;
                            activePList = characterList;
                            break;
                        }
                    case 2:
                        {
                            ResetGameForNextLevel();
                            //scenario.ResetGameForNextLevel();
                            MediaPlayer.Play(gameVars.inGame[1]);
                            scenario.ConstructMap("map2alt.txt");
                            textLog[9] = "Defeat the Rock Golem";
                            screenDrawer.updateTextLog(textLog);
                            enemyList = scenario.enemyList;
                            break;
                        }
                    case 3:
                        {
                            ResetGameForNextLevel();
                            //scenario.ResetGameForNextLevel();
                            MediaPlayer.Play(gameVars.inGame[2]);
                            scenario.ConstructMap("map3alt.txt");
                            textLog[9] = "Defeat the evil master Dead Beard";
                            screenDrawer.updateTextLog(textLog);
                            enemyList = scenario.enemyList;
                            break;
                        }
                    default:
                        {
                            currentState = GameState.Menu;
                            break;
                        }
                }

            map = scenario.MapGrid;

            //setup AI for enemies
            enemyAI = new EnemyAI(characterList, enemyList);
        }//end setup level

        /// <summary>
        /// Alternate method for new map format
        /// Will need information passed in to determine which level to setup.
        /// Currently only contains test code.
        /// </summary>
        protected void ResetGameForNextLevel()
        {
            selected = SelectState.NotSelected;

            //set number of turns to 6
            numOfTurns = 6;
            enemyNum = 0;
            timeToMove = 2;

            //setup the textLog array
            textLog = new string[10] { numOfTurns.ToString(), "false", "", "false", "", "false", "", "", "", "" };

        }//end reset

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

        /// <summary>
        /// Decides what the enemy will do on their move
        /// </summary>
        /// <param name="enemy"></param>
        private void EnemyLogic(EnemyUnit enemy)
        {
            PlayerUnit player;
            player = enemyAI.ClosestPlayer(enemy); //get the player closest to the enemy
            if(player == null && characterList.Count < 1 )
            {
                currentState = GameState.GameOver;
                return;
            }
            screenDrawer.HighlightCurrentEnemy(true, enemy);

            if (enemyAI.IsPlayerVisible(enemy, player)) //can the enemy see/hear that player
            {

                if (enemyAI.IsNextTo(enemy, player)) //is the player next to them
                {
                    if (enemyAI.AttackPlayer(enemy, player)) //check to see if they should attack
                    {
                        textLog[6] = enemy.Attack(player).ToString();
                        textLog[2] = "";
                        textLog[3] = "false";
                        textLog[5] = "true";
                        textLog[7] = enemy.Name;
                        textLog[8] = player.Name;
                        screenDrawer.updateTextLog(textLog);
                        if (enemy.Name == "Devil Scorpion"
                            || enemy.Name == "Mauler"
                            || enemy.Name == "Tornado Lizard"
                            || enemy.Name == "Ant Lion"
                            || enemy.Name == "Zombie"
                            || enemy.Name == "Wyvern Chick"
                            || enemy.Name == "Nightmare"
                            || enemy.Name == "Foul Mummy"
                            )
                            gameVars.enemyBite.Play();
                        else if (enemy.Name == "Bone Fighter"
                            || enemy.Name == "Living Armor"
                            || enemy.Name == "Sentinel"
                            || enemy.Name == "Dead Beard"
                            || enemy.Name == "Evil Armor"
                            || enemy.Name == "White Knight"
                            || enemy.Name == "Satrage")
                            gameVars.enemySwordAttack.Play();
                        else if (enemy.Name == "Rock Golem"
                            || enemy.Name == "Red Devil")
                            gameVars.punch.Play();
                        if(player.IsDead())
                        {
                            player.RemoveCorpse(map);
                            characterList.Remove(player);
                            if(characterList.Count < 1)
                            {
                                currentState = GameState.GameOver;
                            }
                        }
                    }
                    else if (enemyAI.RunAway(enemy, player)) //should they run away
                    {
                        switch (enemyAI.WhichSide(enemy, player))
                        {
                            case 0: { enemy.Move(map, 1); break; }
                            case 1: { enemy.Move(map, 0); break; }
                            case 2: { enemy.Move(map, 3); break; }
                            case 3: { enemy.Move(map, 2); break; }
                            case 4: { /* No Movement */   break; }
                            default: { /* No Movement */  break; }
                        }//end switch

                        //update draw information
                        textLog[2] = "";
                        textLog[3] = "true";
                        textLog[4] = enemy.Name;
                        textLog[5] = "false";
                        screenDrawer.updateTextLog(textLog);
                    }//end run away move
                }//end if next to
                else
                {
                    if (enemyAI.MoveTowardsPlayer(enemy, player)) //if a playe is nearby and visible, maybe move towards them
                    {
                        switch (enemyAI.WhichSide(enemy, player))
                        {
                            case 0: { enemy.Move(map, 0); break; }
                            case 1: { enemy.Move(map, 1); break; }
                            case 2: { enemy.Move(map, 2); break; }
                            case 3: { enemy.Move(map, 3); break; }
                            case 4: { /* No Movement */   break; }
                            default: { /* No Movement */  break; }
                        }//end switch

                        //update draw information
                        textLog[2] = "";
                        textLog[3] = "true";
                        textLog[4] = enemy.Name;
                        textLog[5] = "false";
                        screenDrawer.updateTextLog(textLog);
                    }
                }
            }//end is player visible logic
            else if((rand.Next(1,101) % 2) == 0)
            {
                enemy.Move(map, rand.Next(0, 5));
            }
        }

        protected int DetermineCharcaterAttackSoundAccordingToJesse(Unit selectedUnit)
        {
            if (selectedUnit.Strength > selectedUnit.Dexterity && selectedUnit.Strength > selectedUnit.Magic)
            {
                return 1;
            }

            if (selectedUnit.Dexterity > selectedUnit.Strength && selectedUnit.Dexterity > selectedUnit.Magic)
            {
                return 2;
            }

            if (selectedUnit.Magic > selectedUnit.Strength && selectedUnit.Magic > selectedUnit.Dexterity)
            {
                return 3;
            }

            return 1;
        }
    }//end class
}
