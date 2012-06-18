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
    public class Camera : ISubject
    {
        #region Properties
        public Matrix viewMatrix { get; set; }
        public Matrix projectionMatrix { get; set; }
        public Vector3 cameraPos { get; set; }
        private List<IObserver> observers;
        #endregion

        public const float nearClip = 1.0f;
        public const float farClip = 200.0f;

        /// <summary>
        /// Sets up the camera
        /// </summary>
        /// <param name="device"></param>

        public Camera()
        {
            observers = new List<IObserver>();
        }

        public void SetUpCamera(GraphicsDevice device)
        {
           cameraPos = new Vector3(-40, 20, -30);
          // viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0, 2, -12), new Vector3(0, 2, 0));
           viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0, 0, 0), new Vector3(0, 1, 0));
           projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, nearClip, farClip);
        }

        public void moveCamPos(Vector3 moveVector)
        {
            cameraPos += moveVector;
            viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            notify();
        }
        public void Draw(Effect effect)
        { 
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
           
        }

        public void attachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void detachObserver(IObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
        public void notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.update();
            }
        }

    }
}
