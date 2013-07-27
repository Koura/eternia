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
        private GraphicsDevice device;
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
        
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// Sets up the camera
        /// </summary>
        /// <param name="device"></param>

        public Camera(GraphicsDevice device)
        {
            observers = new List<IObserver>();
            this.device = device;
        }

        public void SetUpCamera(Vector3 target, Quaternion rotation)
        {
            cameraPos = new Vector3(0, 0.7f, -2f);
            cameraPos = Vector3.Transform(cameraPos, Matrix.CreateFromQuaternion(rotation));
            cameraPos += target;
            Vector3 camup = new Vector3(0, 1, 0);
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(rotation));
            viewMatrix = Matrix.CreateLookAt(cameraPos, target, camup);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, nearClip, farClip);
        }

        public void moveCamPos(Vector3 moveVector, Quaternion rotation)
        {
            cameraPos = new Vector3(0, 0.7f, -2f);
            cameraPos = Vector3.Transform(cameraPos, Matrix.CreateFromQuaternion(rotation));
            cameraPos += moveVector;
            Vector3 camup = new Vector3(0, 1, 0);
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(rotation));
            viewMatrix = Matrix.CreateLookAt(cameraPos, moveVector, camup);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, nearClip, farClip);
            notify();
        }
 
        public void Draw(Effect effect)
        {
           

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