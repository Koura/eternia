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
    public class OverWorld : Screen
    {

        Map map;
        Camera camera;
        Effect effect;
        public OverWorld(Game game)
            : base (game)
        {
            map = new Map("eternia", game);
            camera = new Camera(game, new Vector3(0, 20, 20), new Vector3(0, 0, -20), Vector3.Up);
            effect = game.Content.Load<Effect>("EterniaEffects");
        }

        public void updateMap(Map map)
        {
            this.map = map;
        }
        protected override void ProcessInput(String message)
        {
            if (message.Equals("accept"))
            {
                interpretAccept();
            }
        }

        private void interpretAccept()
        {
            StateChanged("Battle");
        }      

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            camera.Draw(effect);
            map.Draw(gameTime);
        }
    }
}
