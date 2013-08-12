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
        //checks which screen to push into screen manager.
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
                case "PartyMenu" :
                    return new PartyMenu(game, this.gameState);
                case "Defeated" :
                    return new Defeated(game);
            }
            return null;
        }

        //gets notified by an ISubject and pops the old screen from the screen manager and pushes a new one on the top.
        //If an utility screen like Party menu is pushed on the on the stack then the recently active screen is not popped out.
        public void update() 
        {
            Console.WriteLine(this.gameState.getState());
            Screen screen = chooseScreen(this.gameState.getState());
            if (screen != null)
            {
                Console.WriteLine("Got through" + screen.GetType());
                if (screen.UtilityScreen)
                {
                    this.manager.pushScreen(screen);
                }
                else
                {
                    this.manager.popScreen();
                    Console.WriteLine(screen.GetType() + " : " + ScreenExists(screen));
                    if (!ScreenExists(screen))
                    {
                        this.manager.pushScreen(screen);
                    }
                }
            }
            Console.WriteLine("# of screens: " + this.manager.numOfScreens());
        }
        //attaches an observer to look for changes in the gamestate and to tell the current screen the new updates.
        private Screen newWorld()
        {
            OverWorld world = new OverWorld(game);
            observer = new OverWorldObserver(gameState, world);
            gameState.Camera.attachObserver(observer);
            return world;
        }

        private bool ScreenExists(Screen screen) {
            return this.manager.topScreen(screen);
        }
    }
}
