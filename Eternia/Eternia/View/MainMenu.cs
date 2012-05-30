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


namespace Eternia
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MainMenu : Screen
    {
       
        Texture2D menuarrow;
        SpriteFont font;
        private List<MenuOption> menuoptions = new List<MenuOption>();

        

        public MainMenu(Game game)
            : base(game)
        {         
            // send message "menu"
            // dj.playdatfunkysong("menu"); somewhere else?
        }

        public override void Initialize()

        {
            // Do our MainMenu component creation magicks here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2-40), "New Game", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2), "Load Game", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 + 40), "Options", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 + 80), "Exit Game", font, Color.White));
        }

        protected override void UnloadContent()
        {

        }

        protected override void ProcessInput()
        {  
            foreach (Keys k in Keyboard.GetState().GetPressedKeys())
            {
                switch (k) {
                    case Keys.Up:
                        Console.WriteLine("AAAAAARGH");
                        break;
                    case Keys.Down:
                        Console.WriteLine("ZALGOOOOO");
                        break;
                    case Keys.Escape:
                        Console.WriteLine("OH NOESSS");
                        break;
                    case Keys.X:
                        Game.Exit();
                        break;
                    /*case Keys.M:
                        audio.update();
                        break;*/
                    default:
                        Console.WriteLine(k);
                        break;
                }
            }
            //send message to interface
        }

        /*
         * Can we just leave this like so? Does the gamestate/screenmanager handle things so that only the topmost screen gets to update?
         */
        public override void Update(GameTime gameTime)
        {
            ProcessInput();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();


            spriteBatch.Draw(menuarrow, new Rectangle(game.GraphicsDevice.Viewport.Width/3+10,game.GraphicsDevice.Viewport.Height/2-61,menuarrow.Width/16, menuarrow.Height/16), Color.White);

            foreach (MenuOption option in menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
