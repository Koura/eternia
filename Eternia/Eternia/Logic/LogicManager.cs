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
                stateUpdate(newState);
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
                gameState.Party.Position += new Vector3(0,0,1);
            }
            if (update.Equals("moveDown"))
            {
                gameState.Camera.moveCamPos(new Vector3(0,0,-1));
            }
            if (update.Equals("moveLeft"))
            {
                gameState.Camera.moveCamPos(new Vector3(-1, 0, 0));
            }
            if (update.Equals("moveRight"))
            {
                gameState.Camera.moveCamPos(new Vector3(1, 0, 0));
            }
        }
    }
}
