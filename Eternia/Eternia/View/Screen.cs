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
    public delegate void StateChangeEventHandler(object source, String state);

    public abstract class Screen : Microsoft.Xna.Framework.DrawableGameComponent
    {

        protected SpriteFont textFont;
        protected SpriteBatch spriteBatch;
        protected Game game;

        public event StateChangeEventHandler stateChange;

         public Screen(Game game)
            : base(game)
        {
            this.game = game;
            //subscribe into inputmanagers Input Event.
            InputManager.instance().InputGiven += new InputEventHandler(OnInput);
        }
         
        protected override void LoadContent()
         {
             spriteBatch = new SpriteBatch(game.GraphicsDevice);
         }

        //On user/player input we call an abstract method that is supposed to be implemented by every screen.
        public void OnInput(object sender, String message)
        {
            if (this.Enabled)
            {
                ProcessInput(message);
            }
        }

        protected override void UnloadContent()
        {
        }
        //Method that reacts to an input in a chosen way
        protected abstract void ProcessInput(String message);

        public override void Update(GameTime gameTime)
        {
 

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        //Calls every method subscribed to the event (if any) with parametres Object sender and String message. 
        public void StateChanged(String newState)
        {
            if (stateChange != null)
            {
                stateChange(this, newState);
            }
        }
    }
}
