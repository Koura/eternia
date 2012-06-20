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
using Eternia.View;

namespace Eternia
{

    //It's a "clickable" menu option used in the menu. Has a lot of getters and setters so nothing to comment in here.
    //So move along. Now for something completely different  ->

    //checks who trained the moose
    //public IMoose trainMoose() {
    //}
    class MenuOption: IOption
    {
        private Color colour;

        public Color Colour
        {
            get { return colour;  }
            set { colour = value; }
        }
        private Vector2 position;
        
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }


        private float rotation;

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        private SpriteFont font;

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }
        private Vector2 size;

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public MenuOption(Vector2 position,string text,SpriteFont font,Color colour)
        {
            Position = position;
            Text = text;
            Font = font;
            Colour = colour;

            Scale = 1.0f;
            Rotation = 0.0f;           
            Size = font.MeasureString(Text);
        }
    }
}
