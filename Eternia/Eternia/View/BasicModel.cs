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
    public class BasicModel
    {
        public Model model { get; set; }
        protected Matrix world = Matrix.Identity;
        private Matrix rotation = Matrix.Identity;

        private bool isAlive;

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        public BasicModel(Model model, Vector3 position)
        {
            isAlive = true;
            this.model = model;
            this.world = Matrix.CreateTranslation(position);
        }
        public void setPosition(Vector3 position)
        {
            this.world = Matrix.CreateTranslation(position);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update()
        {
            // TODO: Add your update code here
        }
        public void Draw(Camera camera)
        {
            if (!IsAlive)
                return;

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = camera.projectionMatrix;
                    effect.View = camera.viewMatrix;
                    effect.World = GetWorld() * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        public virtual Matrix GetWorld()
        {
            return world * rotation;
        }
        public void rotateModelOnX(float degrees)
        {
            rotation *= Matrix.CreateRotationX(MathHelper.ToRadians(degrees));
        }
        public void rotateModelOnY(float degrees)
        {
            rotation *= Matrix.CreateRotationY(MathHelper.ToRadians(degrees));
        }
        public void rotateModelOnZ(float degrees)
        {
            rotation *= Matrix.CreateRotationZ(MathHelper.ToRadians(degrees));
        }
    }
}
