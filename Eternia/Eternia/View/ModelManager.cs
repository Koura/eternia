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
    public class ModelManager: DrawableGameComponent
    {

        private Model enemyModel;
        private Model heroModel;
        private static ModelManager inst;
        public Dictionary<String,BasicModel> models;
        Effect effect;
        private ModelManager(Game game)
            : base(game)
        {
           models = new Dictionary<String,BasicModel>();
           LoadContent();
        }

        public void setEffect(Effect effect)
        {
            this.effect = effect;
        }
         public static ModelManager instance(Game game)
        {
            if (inst == null)
            {
                inst = new ModelManager(game);
            }
            return inst;
        } 
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
        }
        protected override void LoadContent()
        {
             heroModel = Game.Content.Load<Model>(@"models/fighter");
            enemyModel = Game.Content.Load<Model>(@"models/enemy");
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
        /*
         * Draws each model on model list.
         */
        public override void Draw(GameTime gameTime)
        {
            foreach (KeyValuePair<String, BasicModel> model in models)
            {
                model.Value.Draw(((Eternia)Game).Camera);
            }
        }        
        /*
         * Set given list of enemys to modelManager's model list to draw. Method uses enemyModel that is initialized on loadContent method.
         * To change enemy model change different model on loadContent.
         */
        internal void setEnemies(List<Being> enemies)
        {
            foreach (Being being in enemies)
            {
                models.Add(being.Name, new BasicModel(enemyModel, new Vector3(being.Position.X, being.Position.Y, being.Position.Z)));
            }
           
                
        }
        /*
         * Set given list of heros to modelManager's model list to draw. Method uses heroModel that is initialized on loadContent method.
         * To change hero model change different model on loadContent.
         */
        internal void setHeros(List<Being> heroes)
        {
            foreach (Being being in heroes)
            {
                if (!models.ContainsKey(being.Name))
                {
                    BasicModel newBasicModel = new BasicModel(heroModel, new Vector3(being.Position.X, being.Position.Y, being.Position.Z));
                    models.Add(being.Name, newBasicModel);
                }
            }
        }

        /*
         * removes given being's model from model list. Removed model won't be drawn anymore.
         */
        internal void removeModel(Being model)
        {
            models.Remove(model.Name);
        }
    }
}
