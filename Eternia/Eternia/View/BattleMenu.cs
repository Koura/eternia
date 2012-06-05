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
using Eternia.Logic;

namespace Eternia.View
{
    class BattleMenu : Screen, IObserver
    {
        IGameState gameState;
        Texture2D menuarrow;
        Texture2D battlePanel;
        Texture2D timeBars;
        int arrowXpos;
        int arrowYpos;
        Battle battle;
        int arrowXoffset;
        int arrowYoffset;
        float[] optionsXpos;
        float optionY;
        SpriteFont font;
        private List<MenuOption> menuoptions = new List<MenuOption>();
        private float timeBarValue;
        private bool fightIsOn;

        public bool FightIsOn
        {
            get;
            set;
        }

        internal List<MenuOption> Menuoptions
        {
            get { return menuoptions; }
            set { menuoptions = value; }
        }

        public BattleMenu(Game game,GameState gameState, Battle battle)
            : base(game)
        {
            this.battle = battle;
            this.gameState = gameState;
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
            fightIsOn = true;
            base.Initialize();
            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            battlePanel = game.Content.Load<Texture2D>("images/battleImg1");
            timeBars = game.Content.Load<Texture2D>("images/timeBar");
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[0], optionY), "Attack", font, Color.Black));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[1], optionY), "Magic", font, Color.Black));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[2], optionY), "Skills", font, Color.Black));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[3], optionY), "Items", font, Color.Black));
        }

        protected override void UnloadContent()
        {

        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            spriteBatch.Draw(battlePanel, new Rectangle(0,0, battlePanel.Width, battlePanel.Height), Color.White);
            drawMenuOptions();
            
            if(fightIsOn)
                drawTimeBars();
            //spriteBatch.Draw(menuarrow, new Rectangle((int)optionsXpos[ArrowOnOption] + arrowXoffset, arrowYpos + arrowYoffset, menuarrow.Width / 16, menuarrow.Height / 16), Color.White);
            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawTimeBars()
        {
            spriteBatch.Draw(timeBars, new Rectangle(150, 50, timeBars.Width, timeBars.Height), Color.White);
        }

        private void drawMenuOptions()
        {
            MenuOption chosenOption = menuoptions.ElementAt(ArrowOnOption);

            foreach (MenuOption option in Menuoptions)
            {
                option.Colour = Color.Tomato;
            }
            chosenOption.Colour = Color.Black;
            foreach (MenuOption option in Menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }
        }



        public void update()
        {
            
        }
    }
}
