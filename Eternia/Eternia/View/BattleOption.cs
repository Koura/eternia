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


namespace Eternia.View
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class BattleOption : IOption
    {
        public BattleOption(Game game)
            
        {
            // TODO: Construct any child components here
        }
        private Color colour;

        public Color Colour
        {
            get { return colour; }
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
    }
}
