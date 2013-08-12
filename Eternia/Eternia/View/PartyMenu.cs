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
    class PartyMenu : Screen
    {
        Rectangle arrowposi;
        Texture2D menuarrow;
        Texture2D background;
        private List<Texture2D> avatars;
        SpriteFont font;
        private GameState gameState;
        int selection = 1;
        float[] optionsYpos;
        float optionX;
        private List<MenuOption> menuoptions;
        private String menuState;

        public PartyMenu(Game game, IGameState gameState)
            : base(game)
        {
            this.gameState = (GameState)gameState;
            base.UtilityScreen = true;
            this.menuoptions = new List<MenuOption>();
            avatars = new List<Texture2D>();
        }

        public override void Initialize()
        {
            base.Initialize();
            menuState = "default";
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            background = game.Content.Load<Texture2D>("images/partyBG");
            optionsYpos = new float[2];
            optionsYpos[0] = GraphicsDevice.Viewport.Height / 5;
            optionsYpos[1] = GraphicsDevice.Viewport.Height / 5 + game.GraphicsDevice.Viewport.Height / 15;
            optionX = GraphicsDevice.Viewport.Width / 20 + 100;
            menuoptions.Add(new MenuOption(new Vector2(optionX, optionsYpos[0]), "Items", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(optionX, optionsYpos[1]), "Close", font, Color.White));
            arrowposi = new Rectangle(game.GraphicsDevice.Viewport.Width / 20, game.GraphicsDevice.Viewport.Height / 5-21, menuarrow.Width / 16, menuarrow.Height / 16);
            foreach (Hero hero in this.gameState.Party.Heroes)
            {
                avatars.Add(game.Content.Load<Texture2D>("images/"+hero.Name));
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void ProcessInput(String message)
        {
           switch(menuState)
           {
               case "items":
                   {
                       interpretCommandsItems(message);
                       break;
                   }
               case "default":
                   {
                       interpretCommandsDefault(message);
                       break;
                   }
           }
        }

        private void interpretCommandsDefault(String message)
        {
            switch (message)
            {
                case "up":
                    {
                        if (selection > 1)
                        {
                            arrowposi.Y -= game.GraphicsDevice.Viewport.Height / 15;
                            selection--;
                        }
                        break;
                    }
                case "down":
                    {
                        if (selection < 2)
                        {
                            arrowposi.Y += game.GraphicsDevice.Viewport.Height / 15;
                            selection++;
                        }
                        break;
                    }
                case "accept":
                    {
                        interpretAcceptDefault();
                        break;
                    }
                case "decline":
                    {
                        StateChanged("OverWorld");
                        break;
                    }
            }
        }
        private void interpretAcceptDefault()
        {
            switch (selection)
            {
                case 1:
                    {
                        menuState = "items";
                        break;
                    }
                case 2:
                    {
                        StateChanged("OverWorld");
                        break;
                    }
            }
        }

        private void interpretCommandsItems(String message)
        {
            switch (message)
            {
                case "up":
                    {
                        break;
                    }
                case "down":
                    {
                        break;
                    }
                case "menu":
                    {
                        StateChanged("OverWorld");
                        break;
                    }
                case "accept":
                    {
                        break;
                    }
                case "decline":
                    {
                        menuState = "default";
                        break;
                    }
            }
        }
 
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(menuarrow, arrowposi, Color.White);
            foreach (MenuOption option in menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            } 
            switch (menuState)
            {
                case "items":
                    {
                        drawItems();
                        break;
                    }
                case "default":
                    {
                        drawDefault();
                        break;
                    }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawDefault()
        {
            int i = 0;         
            foreach(Hero hero in this.gameState.Party.Heroes)
            {
                spriteBatch.Draw(avatars[i], new Rectangle(game.GraphicsDevice.Viewport.Width / 5 * 2, GraphicsDevice.Viewport.Height / 5 + (i*(game.GraphicsDevice.Viewport.Height/6+10)), game.GraphicsDevice.Viewport.Width/7, game.GraphicsDevice.Viewport.Height/6), Color.White);
                spriteBatch.DrawString(font, hero.Name + "\nHP:    " + hero.CurrentHealth + "  /  " + hero.MaxHealth + "\nMP:    " + hero.CurrentMana + "  /  " + hero.MaxMana + "\nEXP:    " + hero.Experience,
                    new Vector2(game.GraphicsDevice.Viewport.Width/5*2+game.GraphicsDevice.Viewport.Width/8*1.5f, game.GraphicsDevice.Viewport.Height/5), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                i++;
            }
        }

        private void drawItems()
        {
            int i = 0;
            foreach (KeyValuePair<String, int> entry in this.gameState.Party.getItemList())
            {
                spriteBatch.DrawString(font, entry.Key + "   x   " + entry.Value, new Vector2(game.GraphicsDevice.Viewport.Width / 5 * 2, GraphicsDevice.Viewport.Height / 5 + (i * (game.GraphicsDevice.Viewport.Height / 15))), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                i++;
            }
        }
    }

     
}
