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
        private IGameState gameState;
       
        Game game;

        public InputManager(Game game, IGameState gameState)
        {
            this.game = game;
            this.gameState = gameState;
            arrowOnOption = gameState.getArrowOnOptionState();
        }
        public void ProcessInput(GameTime gameTime)
        {
            switch (gameState.getState())
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

        private void updateKeyLeft()
        {
            if (arrowOnOption == 0)
                return;

            arrowOnOption -= 1;
        }

        private void updateKeyRight()
        {
            if (arrowOnOption == 3)
                return;
            Console.WriteLine("InputManager -  moving to right");
           arrowOnOption += 1;
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
                            checkChosenOptionInMainMenu();
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
            
            gameState.setArrowOnOptionState(arrowOnOption);
        }
        private void checkChosenOptionInMainMenu()
        {
            gameState.setArrowOnOptionState(arrowOnOption);
            //option 1 is new battle
            gameState.setState("Battle");
        }

        private void updateArrowDown()
        {
            if (arrowOnOption == 3)
                return;

            arrowOnOption += 1;
        }

        private void updateArrowUp()
        {
            if (arrowOnOption == 0)
                return;

            arrowOnOption -= 1;
        }

        private bool inputProcessorTimer(GameTime gameTime)
        {
            int time = gameTime.TotalGameTime.Milliseconds / 10;

            if (time % 6 == 0)
                return false;

            return true;
        }

        public void update()
        {
            gameState.getState();
        }
    }
}
