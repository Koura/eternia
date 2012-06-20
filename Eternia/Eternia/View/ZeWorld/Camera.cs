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
        #endregion

        public const float nearClip = 1.0f;
        public const float farClip = 200.0f;
        private Matrix view;
        private Matrix projection;

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }
        private Vector3 position;

        public Vector3 Target
        {
            get { return target; }
            set { target = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private GraphicsDevice device;
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
        public void setcamera(Vector3 position, Vector3 target, Vector3 up)
        {
            this.Position = position;
            this.target = target;
            viewMatrix = Matrix.CreateLookAt(position, target, up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)device.Viewport.Width / (float)device.Viewport.Height, 1, 3000); 
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