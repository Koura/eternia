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
    class BattleMenu : Screen, IObserver
    {
        
        Texture2D battlePanel;
        Texture2D timeBarTexture;
        Battle battle;
        float[] optionsXpos;
        float optionY;
        SpriteFont font;
        int selectionValue = 0;

        List<Being> fighters;
        private List<MenuOption> menuoptions = new List<MenuOption>();
        private float timeBarValue;
        Dictionary<String, float> timeBar;
        public float TimeBarValue
        {
            get { return timeBarValue; }
            set { timeBarValue = value; }
        }

        public BattleMenu(Game game)
            : base(game)
        {
            timeBar = new Dictionary<string, float>();
            fighters = new List<Being>();
            battle = new Battle();
            
            Party party = new Party();
            party.addCompany(new Hero("Taistelu Jaska"));
            battle.setUpHeroes(party.Heroes);
            battle.setUpBattle();
            
        }

        public override void Initialize()
        {
            
            
            optionsXpos = new float[4];
            optionY = game.GraphicsDevice.Viewport.Height / 10 + 500;
            optionsXpos[0] = game.GraphicsDevice.Viewport.Width / 5 + 100;
            optionsXpos[1] = game.GraphicsDevice.Viewport.Width / 5 + 250;
            optionsXpos[2] = game.GraphicsDevice.Viewport.Width / 5 + 400;
            optionsXpos[3] = game.GraphicsDevice.Viewport.Width / 5 + 550;
            
            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            base.LoadContent();
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            font = game.Content.Load<SpriteFont>("fonts/battleFighters");
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
           
            drawFighterStats(fighters);
            
            drawTimeBars(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawFighterStats(List<Being> fighters)
        {
            this.fighters = fighters;
            int i = 0;
            foreach(Being being in fighters)
            {
                spriteBatch.DrawString(font, being.Name +
                "\n Health: " + being.CurrentHealth, new Vector2(600, 20 + (i++ * 50)), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            }
           

        }

        private void drawTimeBars(GameTime gameTime)
        {
            int x = 150;
            int y = 5;
            int index = 0;
            foreach (Being b in this.fighters)
            {                
                int calcY = y + index * 20;
                float timeBarLength;
                timeBar.TryGetValue(b.Name,out timeBarLength);
                
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
            spriteBatch.DrawString(font, "Your turn!", new Vector2(x + 210, calcY), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
        }

        private void drawfighterNames(int x, int calcY, Being being)
        {
            spriteBatch.DrawString(font, being.Name, new Vector2(x, calcY), Color.White, 0, new Vector2(0,0),0.5f, SpriteEffects.None,0);
        }

        private void drawMenuOptions()
        {

            foreach (MenuOption option in menuoptions)
            {
                option.Colour = Color.Tomato;
            }
            menuoptions.ElementAt(selectionValue).Colour = Color.Black;
            foreach (MenuOption option in menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }

        }

        

        public override void Update(GameTime gameTime)
        {
            fighters = battle.Fighters;
            timeBar = battle.TimeBar;
            battle.fight();
            base.Update(gameTime);
            
        }

        protected override void ProcessInput(String message)
        {
            if (message.Equals("left"))
            {
                if (selectionValue > 0)
                {
                    selectionValue--;
                }
            }
            if (message.Equals("right"))
            {
                if (selectionValue < 3)
                {
                    selectionValue++;
                }
            }
            if (message.Equals("accept"))
            {
                interpretAccept();
            }
        }

        private void interpretAccept()
        {
            // 
            throw new NotImplementedException();
        }


        // IObserver interface update
        public void update()
        {
            
        }

    }
}
