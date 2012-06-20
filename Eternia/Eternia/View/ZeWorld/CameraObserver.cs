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


namespace Eternia
{

    //Observes party movement and rotation and then reports changes to camera and overworld model.
    public class CameraObserver : IObserver
    {
        GameState gameState;
        Vector3 position;
        Quaternion rotation;

        public CameraObserver(GameState gameState)
        {
            this.gameState = gameState;
        }

        public void update()
        {
            getUpdates();
            pushUpdates();
        }

        private void getUpdates()
        {
            this.position = gameState.Party.Position;
            this.rotation = gameState.Party.PartyRotation;
        }

        private void pushUpdates()
        {
            //gameState.Camera.moveCamPos(position, rotation);
            gameState.WorldModel.setPosition(position, rotation);
        }
    }
}
