using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eternia
{
    class LogicManager
    {
        GameState gameState;

        public LogicManager(GameState gameState)
        {
            this.gameState = gameState;
        }

        public void StateChange(String newState)
        {
            if (newState.Contains("move"))
            {
            }
            else
            {
                gameState.setState(newState);
            }
        }

        public void stateUpdate(String update)
        {
            if (update.Equals("moveUp"))
            {
                gameState.Party.setPosi(new Vector3(0,0,3));
            }
            if (update.Equals("moveDown"))
            {
                gameState.Party.setPosi(new Vector3(0,0,-3));
            }
            if (update.Equals("moveLeft"))
            {
                gameState.Party.setPosi(new Vector3(3, 0, 0));
            }
            if (update.Equals("moveRight"))
            {
                gameState.Party.setPosi(new Vector3(-3, 0, 0));
            }
        }
    }
}
