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
    class ScreenDelegator : IObserver
    {

        ScreenManager manager;
        private Game game;
        GameState gameState;
        OverWorldObserver observer;

        public ScreenDelegator(ScreenManager manager, Game game, GameState gameState)
        {
            this.manager = manager;
            this.game = game;
            this.gameState = gameState;
        }

        private Screen chooseScreen(String choice)
        {
            switch (choice)
            {
                case "OverWorld":             
                    return newWorld();
                case "MainMenu":
                    return new MainMenu(game);
                case "Options" :
                    return new Options(game);
                case "Battle" :
                    return new BattleHandler(this.gameState,new Battle(), new BattleMenu(game)).BattleMenu;
            }
            return null;
        }
        public void update() 
        {
            Screen screen = chooseScreen(this.gameState.getState());
            if (screen != null)
            {
                this.manager.popScreen();
                this.manager.pushScreen(screen);
            }
        }

        public Screen newWorld()
        {
            OverWorld world = new OverWorld(game);
            observer = new OverWorldObserver(gameState, world);
            gameState.Camera.attachObserver(observer);
            return world;
        }
    }
}
