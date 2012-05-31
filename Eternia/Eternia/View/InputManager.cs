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

namespace Eternia.View
{
    class InputManager : IinputManager
    {
        private int arrowOnOption;
        private GameState state;

        private String playerOption;

        public String PlayerOption
        {
            get { return playerOption; }
            set { playerOption = value; }
        }

        public int ArrowOnOption
        {
            get { return arrowOnOption; }
            set { arrowOnOption = value; }
        }
        Game game;

        public InputManager(Game game, GameState state)
        {
            this.state = state;
            playerOption = "MainMenu";
            this.game = game;
            arrowOnOption = 0;
        }
        public void ProcessInput(GameTime gameTime)
        {
            switch (state.getState())
            {
                case "MainMenu":
                    {
                        ProcessInputInMainMenu(gameTime);
                        break;
                    }
                case "Battle":
                    {
                        ProcessInputInBattle(gameTime);
                        break;
                    }

            }
        }

        private void ProcessInputInBattle(GameTime gameTime)
        {
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys k in keys)
            {
                if (inputProcessorTimer(gameTime))
                    return;
                switch (k)
                {
                    case Keys.Right:
                        {
                            
                            updateKeyRight();
                            break;
                        }
                    case Keys.Left:
                        {
                            updateKeyLeft();
                            break;
                        }
                        
                }
            }
        }

        private int updateKeyLeft()
        {
            if (ArrowOnOption == 0)
                return ArrowOnOption;

            return ArrowOnOption -= 1;
        }

        private int updateKeyRight()
        {
            if (ArrowOnOption == 3)
                return ArrowOnOption;

            return ArrowOnOption += 1;
        }

        
        public void ProcessInputInMainMenu(GameTime gameTime)
        {
            
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            foreach (Keys k in keys)
            {
                if (inputProcessorTimer(gameTime))
                    return;
                switch (k)
                {
                    case Keys.Up:
                        {

                            updateArrowUp();
                            break;
                        }

                    case Keys.Down:
                        {
                            updateArrowDown();
                            break;
                        }

                    case Keys.Escape:
                        {
                            break;
                        }
                    case Keys.Enter:
                        {
                            // check witch option is currently on
                            checkChosenOption();
                            break;
                        }

                    case Keys.X:
                        game.Exit();
                        break;
                    default:
                        Console.WriteLine(k);
                        break;
                }
                //send message to interface
            }
        }
        private void checkChosenOption()
        {
            int option = ArrowOnOption;
            //option 1 is new game
            if (option == 0)
            {
                PlayerOption = "newGame";
            }
        }

        private int updateArrowDown()
        {
            if (ArrowOnOption == 3)
                return ArrowOnOption;

            return ArrowOnOption += 1;
        }

        private int updateArrowUp()
        {
            if (ArrowOnOption == 0)
                return ArrowOnOption;

            return ArrowOnOption -= 1;
        }

        private bool inputProcessorTimer(GameTime gameTime)
        {
            int time = gameTime.TotalGameTime.Milliseconds / 10;

            if (time % 6 == 0)
                return false;

            return true;
        }
    }
}
