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

namespace Eternia.View
{
    interface IOption
    {
         Color Colour { get; set; }
        SpriteFont Font { get; set; }
         Vector2 Position { get; set; }
         float Rotation { get; set; }
         float Scale { get; set; }
         Vector2 Size { get; set; }
         String Text { get; set; }
    }
}
