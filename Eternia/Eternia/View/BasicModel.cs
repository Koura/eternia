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
    public class BasicModel
    {
        public Model model { get; set; }
        Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        protected Matrix world = Matrix.Identity;
        private Quaternion rotation = Quaternion.Identity;

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
            this.position = position;
            this.world = Matrix.CreateScale(0.5f, 0.5f, 0.5f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateFromQuaternion(this.rotation) * Matrix.CreateTranslation(this.position);
        }
        public void setPosition(Vector3 position, Quaternion rotation)
        {
            this.rotation = rotation;
            this.position = position;
            this.world = Matrix.CreateScale(0.5f, 0.5f, 0.5f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(position);
        }

        public virtual void Update()
        {
        }

        public void Draw(Camera camera)
        {
            if (!IsAlive)
            {
                return;
            }
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = camera.projectionMatrix;
                    effect.View = camera.viewMatrix;
                    effect.World = world * mesh.ParentBone.Transform;              
                }
                mesh.Draw();
            }
        }
    }
}
