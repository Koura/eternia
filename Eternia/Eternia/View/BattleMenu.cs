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
    class BattleMenu : Screen
    {
        Texture2D menuarrow;
        int arrowXpos;
        int arrowYpos;

        int arrowXoffset;
        int arrowYoffset;
        float[] optionsXpos;
        float optionY;
        SpriteFont font;
        private List<MenuOption> menuoptions = new List<MenuOption>();

        internal List<MenuOption> Menuoptions
        {
            get { return menuoptions; }
            set { menuoptions = value; }
        }

        public BattleMenu(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
           
            arrowXpos = game.GraphicsDevice.Viewport.Width / 5 + 100 + arrowXoffset;
            ArrowOnOption = 0;
            optionsXpos = new float[4];
            arrowXpos = (int)optionsXpos[ArrowOnOption];
            optionY = game.GraphicsDevice.Viewport.Height / 10 + 500;
            arrowYpos = (int)optionY;
            optionsXpos[0] = game.GraphicsDevice.Viewport.Width / 5 + 100;
            optionsXpos[1] = game.GraphicsDevice.Viewport.Width / 5 + 250;
            optionsXpos[2] = game.GraphicsDevice.Viewport.Width / 5 + 400;
            optionsXpos[3] = game.GraphicsDevice.Viewport.Width / 5 + 550;
            arrowXoffset = -100;
            arrowYoffset = -20;
            base.Initialize();
            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[0], optionY), "Attack", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[1], optionY), "Magic", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[2], optionY), "Skills", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[3], optionY), "Items", font, Color.White));
        }

        protected override void UnloadContent()
        {

        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();


            spriteBatch.Draw(menuarrow, new Rectangle((int)optionsXpos[ArrowOnOption] + arrowXoffset,arrowYpos +arrowYoffset , menuarrow.Width / 16, menuarrow.Height / 16), Color.White);

            foreach (MenuOption option in Menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
