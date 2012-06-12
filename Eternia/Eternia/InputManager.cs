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

    public delegate void InputEventHandler(object source, String input);

    public class InputManager : Microsoft.Xna.Framework.Game
    {
        private long keyboard;
        private static InputManager inst;
        private Dictionary<String, double> cooldowns;
             
        public event InputEventHandler InputGiven;
        
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
            cooldowns = new Dictionary<String, double>();
            addMappings();
        }

        private void addMappings()
        {
            cooldowns.Add("up", 0);
            cooldowns.Add("down", 0);
            cooldowns.Add("accept", 0);
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
                if (cooldowns["up"] == 0.0f)
                {
                    cooldowns["up"] = gameTime.TotalGameTime.TotalMilliseconds;
                    InputReceived("up");
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds - cooldowns["up"] > 400)
                {                   
                    cooldowns["up"] = cooldowns["up"] + 300;
                    InputReceived("up");
                }
            }
            else
            {
                setKeyDown(false, 0);
                cooldowns["up"] = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                setKeyDown(true, 1);
                if (cooldowns["down"] == 0.0f)
                {
                    cooldowns["down"] = gameTime.TotalGameTime.TotalMilliseconds;
                    InputReceived("down");
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds - cooldowns["down"] > 400)
                {
                    cooldowns["down"] = cooldowns["down"] + 300;
                    InputReceived("down");
                }            
            }
            else
            {
                setKeyDown(false, 1);
                cooldowns["down"] = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                setKeyDown(true, 5);
                if (cooldowns["accept"] == 0.0f)
                {
                    cooldowns["accept"] = gameTime.TotalGameTime.TotalMilliseconds;
                    InputReceived("accept");
                }

                else if (gameTime.TotalGameTime.TotalMilliseconds - cooldowns["accept"] > 400)
                {
                    cooldowns["accept"] = cooldowns["accept"] + 300;
                    InputReceived("accept");
                }
            }
            else
            {
                setKeyDown(false, 5);
                cooldowns["accept"] = 0;
            }
        }

        public void InputReceived(String input)
        {
            if (InputGiven != null)
            {
                InputGiven(this, input);
            }
        }
    }
}
