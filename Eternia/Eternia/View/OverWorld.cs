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
        public OverWorld(Game game)
            : base (game)
        {
            map = new Map("eternia", game);
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
            map.Draw(gameTime);
        }
    }
}
