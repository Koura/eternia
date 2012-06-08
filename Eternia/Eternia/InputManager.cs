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
/*
 *This is a singleton class. Not the final version but rather a first try-out to see that input actually
 *works in a desirable manner.
*/
namespace Eternia
{
    public class InputManager : Microsoft.Xna.Framework.Game
    {
        private long keyboard;
        private static InputManager inst;
        double currentTimeUp;
        double currentTimeDown;
        Boolean moveable = false;
 
        public static InputManager instance()
        {
            if (inst == null)
            {
                inst = new InputManager();
            }
            return inst;
        }

        private InputManager()
        {
            keyboard = 0;
        }


        public long getKeyboard() 
        {
            return keyboard;
        }

        private void setKeyDown(Boolean isDown, int bit)
        {
            if (isDown)
            {
                keyboard |= (UInt32)(1 << bit);
            }
            else
            {
                keyboard &=  (~(1 << bit));
            }
        }

        public void interpretInput(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                setKeyDown(true, 0);
                if (currentTimeUp == 0.0f)
                {
                    currentTimeUp = gameTime.TotalGameTime.TotalMilliseconds;
                    moveable = true;
                    //Send Message('KeyDown', Key)
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds - currentTimeUp > 400)
                {
                    //Send Message('KeyDown', Key)
                    currentTimeUp += 300;
                    moveable = true;
                }
            }
            else
            {
                setKeyDown(false, 0);
                //Send Message('KeyUp', Key)
                currentTimeUp = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                setKeyDown(true, 1);
                if (currentTimeDown == 0.0f)
                {
                    currentTimeDown = gameTime.TotalGameTime.TotalMilliseconds;
                    moveable = true;
                    //Send Message('KeyDown', Key)
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds - currentTimeDown > 400)
                {
                    //Send Message('KeyDown', Key)
                    currentTimeDown += 300;
                    moveable = true;
                }            
            }
            else
            {
                setKeyDown(false, 1);
                currentTimeDown = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                setKeyDown(true, 5);

            }
            else
            {
                setKeyDown(false, 5);
            }
        }

        public Boolean getMove()
        {
            return moveable;
        }

        public void falsify()
        {
            moveable = false;
        }
    }
}
