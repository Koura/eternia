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
    public class Camera
    {
        #region Properties
        public Matrix viewMatrix { get; set; }
        public Matrix projectionMatrix { get; set; }
        public Vector3 cameraPos { get; set; }
        #endregion

        public const float nearClip = 1.0f;
        public const float farClip = 200.0f;

        /// <summary>
        /// Sets up the camera
        /// </summary>
        /// <param name="device"></param>
        public void SetUpCamera(GraphicsDevice device)
        {
           cameraPos = new Vector3(80, 20, -50);
           viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0, 2, -12), new Vector3(0, 1, 0));
           projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, nearClip, farClip);
        }


        public void Draw(Effect effect)
        { 
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
           
        }




    }
}
