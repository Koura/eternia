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
    class BattleMenuInputManager: IinputManager
    {
        private int arrowOnOption;
        private IGameState gameState;
        private bool enterPressed;

        Game game;

        public BattleMenuInputManager(Game game, IGameState gameState)
        {
            this.game = game;
            this.gameState = gameState;
        }
        public void ProcessInput(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ProcessInputInBattle(gameTime);
        }

        public void update()
        {
            gameState.getState();
        }
        private void ProcessInputInBattle(GameTime gameTime)
        {
            enterPressed = false;
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
                    case Keys.Enter:
                        {
                            checkChosenOptionInBattle();
                            enterPressed = true;
                            break;
                        }

                }
            }
            gameState.setArrowOnOptionState(arrowOnOption);
        }
        public bool inputProcessorTimer(GameTime gameTime)
        {
            int time = gameTime.TotalGameTime.Milliseconds / 10;

            if (time % 6 == 0)
                return false;

            return true;
        }
        
        private void checkChosenOptionInBattle()
        {
            gameState.setArrowOnOptionState(arrowOnOption);
            Console.WriteLine("player is about to chose: " + gameState.getArrowOnOptionState());
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
            arrowOnOption += 1;
        }



        public bool EnterPressed
        {
            get { return enterPressed;}
            set { enterPressed = value; }
        }
    }
}
