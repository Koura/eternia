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
        Texture2D timeBarTexture;
        int arrowXpos;
        int arrowYpos;
        Battle battle;
        int arrowXoffset;
        float[] optionsXpos;
        float optionY;
        SpriteFont font;
        SpriteFont fightersNamesFont;
        private List<MenuOption> menuoptions = new List<MenuOption>();
        private float timeBarValue;

        public float TimeBarValue
        {
            get { return timeBarValue; }
            set { timeBarValue = value; }
        }

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
            
            optionsXpos = new float[4];
            optionY = game.GraphicsDevice.Viewport.Height / 10 + 500;
            arrowYpos = (int)optionY;
            optionsXpos[0] = game.GraphicsDevice.Viewport.Width / 5 + 100;
            optionsXpos[1] = game.GraphicsDevice.Viewport.Width / 5 + 250;
            optionsXpos[2] = game.GraphicsDevice.Viewport.Width / 5 + 400;
            optionsXpos[3] = game.GraphicsDevice.Viewport.Width / 5 + 550;
            arrowXoffset = -100;
            fightIsOn = true;
            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            fightersNamesFont = game.Content.Load<SpriteFont>("fonts/battleFighters");
            battlePanel = game.Content.Load<Texture2D>("images/battleImg1");
            timeBarTexture = game.Content.Load<Texture2D>("images/timeBar");
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
            drawFighterStats();
            
            if(fightIsOn)
                drawTimeBars(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawFighterStats()
        {
            List<Being> fighters = battle.Fighters;
            int i = 0;
            foreach(Being being in fighters)
            {
                spriteBatch.DrawString(fightersNamesFont, being.Name +
                "\n Health: " + being.CurrentHealth, new Vector2(600, 20 + (i++ * 50)), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
           

        }

        private void drawTimeBars(GameTime gameTime)
        {
            int x = 150;
            int y = 5;
            int index = 0;
            foreach (Being b in battle.Fighters)
            {                
                int calcY = y + index * 20;
                float timeBarLength;
                battle.TimeBar.TryGetValue(b.Name,out timeBarLength);
                
                spriteBatch.Draw(timeBarTexture, new Rectangle(x, calcY, timeBarTexture.Width + 2, timeBarTexture.Height / 2 + 2), Color.Red);
                spriteBatch.Draw(timeBarTexture, new Rectangle(x, calcY, (int)timeBarLength, timeBarTexture.Height / 2), Color.White);
                if (timeBarLength == 0.0)
                {
                    drawPlayerToTakeActionNext(x, calcY);
                }
                drawfighterNames(x, calcY, b);
                index++;
            }
            
        }

        private void drawPlayerToTakeActionNext(int x, int calcY)
        {
            spriteBatch.DrawString(fightersNamesFont, "Your turn!", new Vector2(x + 210, calcY), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
        }

        private void drawfighterNames(int x, int calcY, Being being)
        {
            spriteBatch.DrawString(fightersNamesFont, being.Name, new Vector2(x, calcY), Color.White, 0, new Vector2(0,0),0.5f, SpriteEffects.None,0);
        }

        private void drawMenuOptions()
        {

            foreach (MenuOption option in Menuoptions)
            {
                option.Colour = Color.Tomato;
            }
            foreach (MenuOption option in Menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }
        }

        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }

        protected override void ProcessInput(String message)
        {
        }


        // IObserver interface update
        public void update()
        {
        }

    }
}
