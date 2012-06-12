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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Dictionary<String,BasicModel> models = new Dictionary<String,BasicModel>();
        public ModelManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }
        protected override void LoadContent()
        {
            BasicModel hero = new BasicModel(Game.Content.Load<Model>(@"models/fighter"), new Vector3(0, 0, 0));
            BasicModel enemy1 = new BasicModel(Game.Content.Load<Model>(@"models/enemy"), new Vector3(0, 0, -10));

            models.Add("hero",hero);
            models.Add("enemy",enemy1);
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<String, BasicModel> model in models)
            {
                model.Value.Update();
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (KeyValuePair<String, BasicModel> model in models)
            {
                model.Value.Draw(((Eternia)Game).Camera);
            }
            base.Draw(gameTime);
        }
        public void setModelAlive(String modelName)
        {
            BasicModel model = null;

            if(models.TryGetValue(modelName, out model))
            {
                model.IsAlive = true;
            }

        }
        public void setAllModelsAlive()
        {
            foreach (KeyValuePair<String, BasicModel> model in models)
            {
                model.Value.IsAlive = true;
            }
        }
        public void setModelDead(String modelName)
        {
            BasicModel model = null;

            if(models.TryGetValue(modelName, out model))
            {
                model.IsAlive = false;
            }
        }
    }
}
