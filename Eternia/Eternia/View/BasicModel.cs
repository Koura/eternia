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
        private Quaternion rotation = Quaternion.Identity;
        Effect effect;
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
        }
        public void setPosition(Vector3 position, Quaternion rotation)
        {
            this.rotation = rotation;
            this.position = position;
        }

        public virtual void Update()
        {
        }

        public void Draw(Camera camera, Effect effect)
        {
            if (!IsAlive)
            {
                return;
            }
            Matrix worldMatrix = Matrix.CreateScale(0.05f, 0.05f, 0.05f) * Matrix.CreateRotationY(MathHelper.Pi) *
                Matrix.CreateFromQuaternion(this.rotation) * Matrix.CreateTranslation(this.position);
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    currentEffect.CurrentTechnique = currentEffect.Techniques["Textured"];
                    currentEffect.Parameters["xWorld"].SetValue(transforms[mesh.ParentBone.Index] * worldMatrix);
                    currentEffect.Parameters["xView"].SetValue(camera.viewMatrix);
                    currentEffect.Parameters["xProjection"].SetValue(camera.projectionMatrix);
                    currentEffect.Parameters["xEnableLighting"].SetValue(true);
                    currentEffect.Parameters["xLightDirection"].SetValue(new Vector3(-0.5f, -1, -0.5f));
                    currentEffect.Parameters["xAmbient"].SetValue(0.4f);
                }
                mesh.Draw();
            }
        }
    }
}
