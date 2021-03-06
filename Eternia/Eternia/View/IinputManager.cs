﻿using System;
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
    interface IinputManager : IObserver
    {
        bool EnterPressed { get; set; }

        void ProcessInput(GameTime gameTime);

        bool inputProcessorTimer(GameTime gameTime);

        int getChosenOption();

         
    }
}
