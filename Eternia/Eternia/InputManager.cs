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
            cooldowns.Add("left", 0);
            cooldowns.Add("right", 0);
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
            CheckCooldown("up", Keys.Up, gameTime);
            CheckCooldown("down", Keys.Down, gameTime);
            CheckCooldown("left", Keys.Left, gameTime);
            CheckCooldown("right", Keys.Right, gameTime);
            CheckCooldown("accept", Keys.A, gameTime);
        }

        public void InputReceived(String input)
        {
            if (InputGiven != null)
            {
                InputGiven(this, input);
            }
        }

        public void CheckCooldown(String key, Keys button, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(button))
            {
                setKeyDown(true, 1);
                if (cooldowns[key] == 0.0f)
                {
                    cooldowns[key] = gameTime.TotalGameTime.TotalMilliseconds;
                    InputReceived(key);
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds - cooldowns[key] > 400)
                {
                    cooldowns[key] = cooldowns[key] + 300;
                    InputReceived(key);
                }
            }
            else
            {
                setKeyDown(false, 1);
                cooldowns[key] = 0;
            }
        }
    }
}
