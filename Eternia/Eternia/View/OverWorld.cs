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
        List<Hero> heroes;
        BasicModel model;
        Random battleProbability;

        public OverWorld(Game game)
            : base (game)
        {
            effect = game.Content.Load<Effect>("EterniaEffects");
            this.heroes = new List<Hero>();
        }

        //processes the messages gotten from the player and acts accordingly.
        protected override void ProcessInput(String message)
        {
            battleProbability = new Random();
            if (message.Equals("up"))
            {
                StateChanged("moveUp");
                if (battleProbability.NextDouble() < 0.03)
                {
                    StateChanged("Battle");
                }
            }
            if (message.Equals("down"))
            {
                StateChanged("moveDown");
                if (battleProbability.NextDouble() < 0.03)
                {
                    StateChanged("Battle");
                }
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
            if (message.Equals("menu"))
            {
                StateChanged("PartyMenu");
            }
        }
        //Determines how to react to pressing A button in a given situation
        private void interpretAccept()
        {
            StateChanged("Battle");
        }

        //Gets called by the observer when changes have happened and gets the new relevant data.
        public void receiveChanges(Map map, Camera camera, List<Hero> heroes, BasicModel model)
        {
            this.map = map;
            this.camera = camera;
            this.heroes = heroes;
            this.model = model;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //tells the camera, terrain, model and water to draw themselves.
        public override void Draw(GameTime gameTime)
        {
            float time = (float)gameTime.TotalGameTime.TotalMilliseconds / 100.0f;
            base.Draw(gameTime);
            map.Draw(gameTime, camera);
            model.Draw(camera, effect);
            map.DrawWater(time);
        }


    }
}
