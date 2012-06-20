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

    public class Eternia : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        Camera camera;

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }
        GameState gameState;
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

        protected override void Initialize()
        {
            device = graphics.GraphicsDevice;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Camera = new Camera(device);
            // AudioManager is a Iobserver. Give a Isubject as parameter in constructor. 
            this.gameState = new GameState(this);
            this.audio = new AudioManager(this.gameState);
            this.gameState.attachObserver(audio);
            view = new ScreenManager(this);
            delegator = new ScreenDelegator(view, this, this.gameState);
            this.gameState.attachObserver(delegator);
            this.logicUnit = new LogicManager(this.gameState);
            this.commandHandler = new CommandHandler(this.view, this.logicUnit);
            view.attachObserver(this.commandHandler);
            this.gameState.setState("MainMenu");
            Window.Title = gameTitle;

            base.Initialize();
        }

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
            audio.playSong(this.gameState.getState());
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.instance().interpretInput(gameTime);            
            base.Update(gameTime);
        }
         

        protected override void Draw(GameTime gameTime)
        {
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            device.RasterizerState = rs;
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            base.Draw(gameTime);
        }
    }
}
