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
        private Vector3 target;
        private GraphicsDevice device;
        #endregion

        public const float nearClip = 1.0f;
        public const float farClip = 200.0f;

        /// <summary>
        /// Sets up the camera
        /// </summary>
        /// <param name="device"></param>

        public Camera(GraphicsDevice device)
        {
            observers = new List<IObserver>();
            this.device = device;
        }

        public void SetUpCamera()
        {
            cameraPos = new Vector3(70, 20, -70);
            viewMatrix = Matrix.CreateLookAt(cameraPos, new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, nearClip, farClip);
        }

        public void moveCamPos(Vector3 moveVector, Quaternion rotation)
        {
            Vector3 cameraPos = new Vector3(0, 2.0f, 2.0f);
            cameraPos = Vector3.Transform(cameraPos, Matrix.CreateFromQuaternion(rotation));

            Vector3 camup = new Vector3(0, 1, 0);
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(rotation));
            viewMatrix = Matrix.CreateLookAt(cameraPos, moveVector, camup);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, nearClip, farClip);
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