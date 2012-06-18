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
    public class OverWorld : Screen
    {

        Map map;
        Camera camera;
        Effect effect;

        public OverWorld(Game game)
            : base (game)
        {
            camera = new Camera();
            camera.SetUpCamera(game.GraphicsDevice);
            effect = game.Content.Load<Effect>("EterniaEffects");
        }




        protected override void ProcessInput(String message)
        {
            if (message.Equals("up"))
            {
                StateChanged("moveUp");
            }
            if (message.Equals("down"))
            {
                StateChanged("moveDown");
            }
            if (message.Equals("left"))
            {
                StateChanged("moveLeft");
            }
            if (message.Equals("right"))
                StateChanged("moveRight");
            {
            }
            if (message.Equals("accept"))
            {
                interpretAccept();
            }
        }

        private void interpretAccept()
        {
            StateChanged("Battle");
        }

        public void receiveChanges(Map map, Camera camera)
        {
            this.map = map;
            this.camera = camera;
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            camera.Draw(effect);
            map.DrawSkyDome(camera);
            map.Draw(gameTime);
        }


    }
}
