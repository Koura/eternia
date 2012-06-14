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
    class BattleMenu : Screen, ISubject
    {
        List<IObserver> observers = new List<IObserver>();
        
        Texture2D battlePanel;
        Texture2D timeBarTexture;
        float[] optionsXpos;
        float optionY;
        SpriteFont font;
        int selectionValue = 0;
        List<Being> fighters;
        public Info info;
        private bool playerTurn;
        GameTime gameTime;
        String battleState;
        int target = 0;
        private int heroCount;
        public int HeroCount
        {
            get {return heroCount;}
            set {heroCount = value;}
        }
        public int Target
        {
            get { return this.target; }
            set { target = value; }

        }
        public bool PlayerTurn
        {
            set { this.playerTurn = value; }
        }

        private bool actionMade;

        public bool ActionMade
        {
            get { return this.actionMade; }
            set { this.actionMade = value; }
        }
        private String playerAction = "";

        public String PlayerAction
        {
            get { return this.playerAction; }
            set { this.playerAction = value; }
        }
        public List<Being> Fighters
        {
            get { return this.fighters; }
            set { this.fighters = value; }
        }

        Dictionary<String, float> timeBar;

        public Dictionary<String, float> TimeBar
        {
            get { return this.timeBar; }
            set { this.timeBar = value; }
        }

        TimeSpan currentTime;
        private List<MenuOption> menuoptions = new List<MenuOption>();
        private float timeBarValue;
        private int casualties = 0;

        public int Casualties
        {
            get { return casualties; }
            set { casualties = value; }
        }

            
            public float TimeBarValue
        {
            get { return timeBarValue; }
            set { timeBarValue = value; }
        }
        public BattleMenu(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            optionsXpos = new float[4];
            optionY = game.GraphicsDevice.Viewport.Height / 10 + 500;
            optionsXpos[0] = game.GraphicsDevice.Viewport.Width / 5 + 100;
            optionsXpos[1] = game.GraphicsDevice.Viewport.Width / 5 + 250;
            optionsXpos[2] = game.GraphicsDevice.Viewport.Width / 5 + 400;
            optionsXpos[3] = game.GraphicsDevice.Viewport.Width / 5 + 550;
            battleState = "BattleMenu";
            playerTurn = false;
            actionMade = false;
            base.Initialize();
        }
        

        protected override void LoadContent()
        {
            base.LoadContent();
            font = game.Content.Load<SpriteFont>("fonts/menufont");
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
            this.gameTime = gameTime;
            spriteBatch.Begin();
            
            spriteBatch.Draw(battlePanel, new Rectangle(0,0, battlePanel.Width, battlePanel.Height), Color.White);
            
           
            drawFighterStats();
            
            drawTimeBars(gameTime);

            

            if (playerAction.Equals("Attack"))
            {
                drawTargetSelection();
            }
            else
            {
                drawMenuOptions();
               
            }
            drawCasualties();
            drawInfo();
            Console.WriteLine(info.text);
            spriteBatch.End();
                
            base.Draw(gameTime);

            

        }

        private void drawCasualties()
        {
            String infoText ="";
            switch (casualties)
            {
                case 4:
                    {
                        infoText  = "All enemies defeated.";                        
                        break;
                    }
                case 1:
                    {
                        infoText = "Enemy killed.";
                        break;
                    }
                case -1:
                    {
                        infoText = "Party member has been killed.";
                        break;
                    }
                case -4:
                    {
                        infoText = "All party member has been killed.\n"+
                        "GAME OVER";
                        break;
                    }
            }            
            info = new Info(infoText, new Vector2(200, 200), gameTime.TotalGameTime);
            casualties = 0;
        }
            

        private void drawTargetSelection()
        {
            if (actionMade) return;
            int x = 150;
            int y = 5;
            int index = target;
            int calcY = y + index * 20;
            spriteBatch.DrawString(font, "<<< Target", new Vector2(x + 210, calcY), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            info = new Info("Attack (A)", new Vector2(200, 200), gameTime.TotalGameTime);
        }

        private void drawInfo()
        {
            if (info.text != null && (info.startTime.TotalMilliseconds + 1200.0f > gameTime.TotalGameTime.TotalMilliseconds))
                spriteBatch.DrawString(font, info.text, info.position, Color.White);
            
        }

        private void drawFighterStats()
        {
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
                    if(!playerAction.Equals("Attack"))
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
            if (!playerTurn) return;
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
            currentTime = gameTime.TotalGameTime;
            notify();
            
            base.Update(gameTime);
            
        }

        protected override void ProcessInput(String message)
        {
            switch (battleState)
            {
                case "BattleMenu":
                    {
                        ProcessInputInBattleMenu(message);
                        break;
                    }
                case "Attack":
                    {
                        ProcessInputInAttack(message);
                        break;
                    }
            }
            if (message.Equals("accept"))
            {
                interpretAccept();
            }
        }

        private void ProcessInputInAttack(string message)
        {
            if (message.Equals("down"))
            {
                if (target < fighters.Count - 1)
                {
                    target++;
                }
            }
            if (message.Equals("up"))
            {
                if (target > 0)
                {
                    target--;
                }
            }
        }

        private void ProcessInputInBattleMenu(string message)
        {
            if (!playerTurn) return;
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
        }

        private void interpretAccept()
        {
            switch (battleState)
            {
                case "BattleMenu":
                    {
                        // if selectionValue is 0 player is about to attack
                        if (selectionValue == 0)
                        {
                            playerAttacking();
                        }
                        // if selectionValue is 1 players is about use magic
                        if (selectionValue == 1)
                        {
                            actionMade = true;
                        }
                        // if selectionValue is 2 player is about to check items
                        if (selectionValue == 2)
                        {
                            actionMade = true;
                        }
                        break;
                    }
                case "Attack":
                    {

                        info = new Info("Attacking!", new Vector2(200, 200), gameTime.TotalGameTime);
                        actionMade = true;
                        battleState = "BattleMenu";
                        break;
                    }
            }

            
        }

        private void playerAttacking()
        {
            playerAction = "Attack";
            battleState = "Attack";
            // need to choose target
            info = new Info("Select target", new Vector2(200, 200), gameTime.TotalGameTime);
        }

        public void attachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void detachObserver(IObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        public void notify()
        {
            foreach (IObserver obs in observers)
            {
                obs.update();
            }
        }
    }
}
