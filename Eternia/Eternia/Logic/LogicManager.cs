﻿using System;
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

        //If the newstate contains the word move it doesn't do anything. Otherwise tells the gamestate to change the state to the newstate.
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

        //Changes objects in the gamestate according to the message
        public void stateUpdate(String update)
        {
            if (update.Equals("moveUp"))
            {
                gameState.Party.setPosi(new Vector3(0,0,-5));
            }
            if (update.Equals("moveDown"))
            {
                gameState.Party.setPosi(new Vector3(0,0,5));
            }
            if (update.Equals("moveLeft"))
            {
                 Quaternion additionalRot = Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), 0.5f)*Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), 0.0f);
                gameState.Party.setRota(additionalRot);
            }
            if (update.Equals("moveRight"))
            {
                Quaternion additionalRot = Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), -0.5f)*Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), 0.0f);
                gameState.Party.setRota(additionalRot);
            }
        }
    }
}
