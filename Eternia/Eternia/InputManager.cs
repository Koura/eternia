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
    //our own event that send a source object and a string representation of the input
    public delegate void InputEventHandler(object source, String input);


    public class InputManager : Microsoft.Xna.Framework.Game
    {
        private static InputManager inst;
        private Dictionary<String, double> cooldowns;
             
        //Defines an event called inputgiven that is the typo of inputeventhandler.
        public event InputEventHandler InputGiven;
        
        //Implemented as a singleton so that there is only one instance of the inputmanager roaming free.
        //Returns an instance or creates one if there isn't one yet.
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
            cooldowns = new Dictionary<String, double>();
            addMappings();
        }

        //Adds string messages representing certain keys to the dictionary
        private void addMappings()
        {
            cooldowns.Add("up", 0);
            cooldowns.Add("down", 0);
            cooldowns.Add("left", 0);
            cooldowns.Add("right", 0);
            cooldowns.Add("accept", 0);
            cooldowns.Add("decline", 0);
            cooldowns.Add("menu", 0);
        }

        //Does the checkcooldown method for the given key
        public void interpretInput(GameTime gameTime)
        {
            CheckCooldown("up", Keys.Up, gameTime);
            CheckCooldown("down", Keys.Down, gameTime);
            CheckCooldown("left", Keys.Left, gameTime);
            CheckCooldown("right", Keys.Right, gameTime);
            CheckCooldown("accept", Keys.A, gameTime);
            CheckCooldown("decline", Keys.D, gameTime);
            CheckCooldown("menu", Keys.Enter, gameTime);
        }

        //When we have validated that the player has pressed a key we call all the methods subscribed to InputGiven if any.
        public void InputReceived(String input)
        {
            if (InputGiven != null)
            {
                InputGiven(this, input);
            }
        }

        //Checks if a certain key is pressed down or if it's up. Also checks the button cooldown and determines if an inputevent can be
        //triggered for the same key again.
        public void CheckCooldown(String key, Keys button, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(button))
            {
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
                cooldowns[key] = 0;
            }
        }
    }
}
