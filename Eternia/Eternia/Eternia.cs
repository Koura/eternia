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
    /// This is the main type for your game
    /// </summary>
    public class Eternia : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        GameState gameState;

        public GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }
        ScreenManager view;
        AudioManager audio;
        ScreenDelegator delegator;
        CommandHandler commandHandler;
        LogicManager logicUnit;

        private const string gameTitle = "Last Dreams of Eternia";

        public Eternia()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            device = graphics.GraphicsDevice;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            // AudioManager is a Iobserver. Give a Isubject as parameter in constructor. 
            this.GameState = new GameState(this);
            this.audio = new AudioManager(this.GameState);
            this.GameState.attachObserver(audio);
            view = new ScreenManager(this);
            delegator = new ScreenDelegator(view, this, this.GameState);
            this.GameState.attachObserver(delegator);
            this.logicUnit = new LogicManager(this.GameState);
            this.commandHandler = new CommandHandler(this.view, this.logicUnit);
            view.attachObserver(this.commandHandler);
            this.GameState.setState("MainMenu");
            Window.Title = gameTitle;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Song menuSong = Content.Load<Song>(@"audios\maintheme");
            Song battle = Content.Load<Song>(@"audios\battletheme1");
            Song overworld = Content.Load<Song>(@"audios\overworld");
            SoundEffect rollEffect = Content.Load<SoundEffect>(@"audios\roll");
            SoundEffect laughEffect = Content.Load<SoundEffect>(@"audios\laugh");
            audio.addNewSong("MainMenu", menuSong);
            audio.addNewSong("Battle", battle);
            audio.addNewSong("Options", menuSong);
            audio.addNewSong("OverWorld", overworld);
            audio.addNewSoundEffect("roll", rollEffect);
            audio.addNewSoundEffect("laugh", laughEffect); 
            audio.playSong(this.GameState.getState());
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputManager.instance().interpretInput(gameTime);            
            base.Update(gameTime);
        }
         
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            device.RasterizerState = rs;
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
