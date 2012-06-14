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

        Model enemyModel;
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
            BasicModel hero = new BasicModel(Game.Content.Load<Model>(@"models/fighter"), new Vector3(5, 0, 0));
            enemyModel = Game.Content.Load<Model>(@"models/enemy");
            models.Add("hero",hero);
            setEnemies(5);
            base.LoadContent();
        }
        public void setEnemies(int amount)
        {
            
            

            for (int i = 1; i <= amount; i++)
            {
                float x = i * 2;
                float z =(float)Math.Sin(MathHelper.ToDegrees(i));
                
                models.Add("enemy_"+i, new BasicModel(enemyModel, new Vector3(x, 0, z)));
                x++;
            }
                
                
                
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
