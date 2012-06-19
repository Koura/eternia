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
        Vector3 cameraDirection;
        Vector3 cameraUp;
        #endregion

        public const float nearClip = 1.0f;
        public const float farClip = 200.0f;

        /// <summary>
        /// Sets up the camera
        /// </summary>
        /// <param name="device"></param>
        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
        {
           // build camera view matrix
           cameraPos = pos;
           cameraDirection = target - pos;
           cameraDirection.Normalize();
           cameraUp = up;
           createALookAt();
           
           projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,(float)game.Window.ClientBounds.Width/(float) game.Window.ClientBounds.Height, nearClip, farClip);
        }
        private void createALookAt()
        {
            viewMatrix = Matrix.CreateLookAt(cameraPos, cameraPos + cameraDirection, cameraUp);
        }

        public void Draw(Effect effect)
        {
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);

        }




    }
}
