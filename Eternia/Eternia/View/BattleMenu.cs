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
        Texture2D timeBarTexture;
        Texture2D hpBarTexture;
        float[] optionsXpos;
        float optionY;
        SpriteFont font;
        int selectionValue = 0;
        List<Being> fighters;
        public Info info;
        public Info casualtiesInfo;
        private bool playerTurn;
        public ModelManager modelManager;
        Camera camera;
        List<BasicModel> battledudes;

        public bool PlayerTurn
        {
            get { return playerTurn; }
            set { playerTurn = value; }
        }
        private TimeSpan currentTime;
        public TimeSpan CurrentTime
        {
            get { return this.currentTime; }
        }
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
            modelManager = ModelManager.instance(game);
            camera = new Camera(game.GraphicsDevice);
            camera.SetUpCamera();
            Dictionary<String, BasicModel> temp = ModelManager.instance(game).models;
            battledudes = temp.Values.ToList();
            battledudes[0].setPosition(new Vector3(200, -120, 200), Quaternion.Identity);
            Console.WriteLine(battledudes[0].Position);
        }
        /*
         * Method will initialize vector's X value for fonts that are drawn as options on battleMenu. BattleState is set for BattleMenu.
         * PlayerTurn and actionMade is set false.
         */
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
        
        /*
         * Method will set all images and different option's fonts for the menu. 
         */
        protected override void LoadContent()
        {
            base.LoadContent();
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            timeBarTexture = game.Content.Load<Texture2D>("images/timeBar");
            hpBarTexture = game.Content.Load<Texture2D>("images/hpbar");
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[0], optionY), "Attack", font, Color.Black));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[1], optionY), "Magic", font, Color.Black));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[2], optionY), "Skills", font, Color.Black));
            menuoptions.Add(new MenuOption(new Vector2(optionsXpos[3], optionY), "Items", font, Color.Black));
            
        }

        protected override void UnloadContent()
        {

        }
        /*
         * Method will draw all components used in the menu.
         */
        public override void Draw(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime;
            spriteBatch.Begin();
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
            drawInfo();
            drawInfo(gameTime);
            spriteBatch.End();
            foreach(BasicModel dude in battledudes)
            {
                dude.Draw(camera);
            }

            base.Draw(gameTime);

            

        }      

        /*
         * Method will draw, if player has made an attack action, font for player to chose right target.
         */
        private void drawTargetSelection()
        {
            if (actionMade) return;
            int x = 150;
            int y = 5;
            int index = target;
            int calcY = y + index * 20;
            spriteBatch.DrawString(font, "<<< Target", new Vector2(x + 210, calcY), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            info = new Info("Attack (A)", new Vector2(200, 200), currentTime);
        }

        /*
         * Method will draw a message if there is some information to be displayed on screen i.e if any party member has been killed or enemy is defeaded.
         */
        private void drawInfo()
        {
            if (info.text != null && (info.startTime.TotalMilliseconds + 1200.0f > currentTime.TotalMilliseconds))
                spriteBatch.DrawString(font, info.text, info.position, Color.White);

            if (casualtiesInfo.text != null && (casualtiesInfo.startTime.TotalMilliseconds + 1200.0f > currentTime.TotalMilliseconds))
                spriteBatch.DrawString(font, casualtiesInfo.text, casualtiesInfo.position, Color.White);
        }
         
        private void drawInfo(GameTime gameTime)
        {
            if (info.text != null && (info.startTime.TotalMilliseconds + 1200.0f > gameTime.TotalGameTime.TotalMilliseconds))
                spriteBatch.DrawString(font, info.text, info.position, Color.White);
        }
        /*
         * Method will draw all fighters current statistics relevant for the battle. 
         */
        private void drawFighterStats()
        {
            int i = 0;
            int j = 0;
            Color color = Color.Linen;
            foreach(Being being in fighters)
            {
                if (being.GetType() == typeof(Hero))
                {
                    spriteBatch.DrawString(font, being.Name + "    HP: " + being.CurrentHealth + "/" + being.MaxHealth + "    MP: " + ((Hero)being).CurrentMana + "/" + ((Hero)being).MaxMana ,
                        new Vector2(game.GraphicsDevice.Viewport.Width/10*6, game.GraphicsDevice.Viewport.Height/10*6.6f), Color.White, 0, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0);
                    if (being.CurrentHealth > being.MaxHealth * 0.5)
                    {
                        color = Color.Yellow;
                    }
                    else if (being.CurrentHealth > being.MaxHealth * 0.2)
                    {
                        color = Color.Orange;
                    }
                    else
                    {
                        color = Color.Magenta;
                    }
                    spriteBatch.Draw(hpBarTexture, new Rectangle((int)(game.GraphicsDevice.Viewport.Width / 10 * 6), (int)(game.GraphicsDevice.Viewport.Height / 10 * 6.6f + game.GraphicsDevice.Viewport.Height / 29),
                           (int)(game.GraphicsDevice.Viewport.Width / 10 * 3.5f), (int)(game.GraphicsDevice.Viewport.Height / 43)), Color.SlateBlue);
                    spriteBatch.Draw(hpBarTexture, new Rectangle((int)(game.GraphicsDevice.Viewport.Width / 10 * 6), (int)(game.GraphicsDevice.Viewport.Height / 10 * 6.6f + game.GraphicsDevice.Viewport.Height / 29),
                           (int)((game.GraphicsDevice.Viewport.Width / 10 * 3.5f) * ((double)being.CurrentHealth / being.MaxHealth)), (int)(game.GraphicsDevice.Viewport.Height / 45)), color);
                }
                else
                {
                    spriteBatch.DrawString(font, being.Name +
                    "\n Health: " + being.CurrentHealth, new Vector2(600, 20 + (i++ * 50)), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                }
            }
           

        }
        /*
         * Method will draw fighter's timebars to show who is next take action.
         */
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
        /*
         * Method will draw font next to fighters timebar to point next fighter to take action.
         */
        private void drawPlayerToTakeActionNext(int x, int calcY)
        {
            spriteBatch.DrawString(font, "Your turn!", new Vector2(x + 210, calcY), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);

        }
        /*
         * Method will draw all fighters name's on the timebars.
         */
        private void drawfighterNames(int x, int calcY, Being being)
        {
            spriteBatch.DrawString(font, being.Name, new Vector2(x, calcY), Color.White, 0, new Vector2(0,0),0.5f, SpriteEffects.None,0);
        }

        /*
         * If it is player's turn to take action method will draw options for player to choose next action.
         */
        private void drawMenuOptions()
        {
            if (!playerTurn) return;
            foreach (MenuOption option in menuoptions)
            {
                option.Colour = Color.White;
            }
            menuoptions.ElementAt(selectionValue).Colour = Color.Tomato;
            foreach (MenuOption option in menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }

        }

        

        public override void Update(GameTime gameTime)
        {
            notify();
            base.Update(gameTime);
            
        }

        /*
         * Method will process player's input on menu. If battleState is set on BattleMenu method ProcessInputInBattleMenu is called. 
         */
        protected override void ProcessInput(String message)
        {
            if (!playerTurn) return;

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
        /*
         * Process input on attack state. Target is selected by moving "target" font on the side of timebars by icreasing or decreasing the
         * value of target.
         */
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
        /*
         * Process input on battleMenu. Selection value is updated as player moves to left or right.
         */
        private void ProcessInputInBattleMenu(string message)
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
        }
        /*
         * Method will iterpret the action that player has chosen, determined by battleState, interpretAccecptInBattleMenu or
         * interpretAccecptInAttackOption method is called to take right action for player.
         */
        private void interpretAccept()
        {
            switch (battleState)
            {
                case "BattleMenu":
                    {
                        /*
                         * iterpret what player is choosing in main battle menu
                         * | attack | magic | skills | items
                         */
                        interpretAccecptInBattleMenu();
                        break;
                    }
                case "Attack":
                    {
                        // interpret players action in attack. Who's player targeting.
                        interpretAccecptInAttackOption();
                        break;
                    }
            }

            
        }
        /*
         * Method will interpret player action on attack state.
         */
        private void interpretAccecptInAttackOption()
        {
            info = new Info("Attacking!", new Vector2(200, 200), currentTime);
            actionMade = true;
            battleState = "BattleMenu";
        }
        /*
         Method will interpret player action on battle menu state*/
        private void interpretAccecptInBattleMenu()
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
        }
        /*
         * Method will set battle state and playerAction on "attack".
         * Notifies player to choose the target.
         */
        private void playerAttacking()
        {
            playerAction = "Attack";
            battleState = "Attack";
            info = new Info("Select target", new Vector2(200, 200), currentTime);
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
