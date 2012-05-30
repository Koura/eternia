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
    public class AudioManager :Microsoft.Xna.Framework.Game, IObserver
    {
        private IGameState state;
        private Dictionary<String, SoundEffect> soundEffects;
        private Dictionary<String, Song> music;
        private Song currentlyPlaying;
        public AudioManager(IGameState gameState)
        {
            this.state = gameState;
            soundEffects = new Dictionary<string, SoundEffect>();
            music = new Dictionary<string, Song>();
        }

        // add new sound effect 
        public void addNewSoundEffect(String effectName, SoundEffect newEffect)
        {
           
            soundEffects.Add(effectName, newEffect);
        }
        public void addNewSong(String state, Song song)
        {
            music.Add(state, song);
        }
        public Song getSong(String state)
        {
            Song song = null;
            if (music.TryGetValue(state, out song))
            {
                return song;
            }
            else
            {
                return null;
            }
        }
        public void playSong(String state)
        {
            Song song = null;
            if (music.TryGetValue(state, out song))
            {
                MediaPlayer.Play(song);
                this.currentlyPlaying = song;
            }
            
        }
        public void update()
        {
            Song song = null;

            if (music.TryGetValue(this.state.getState(), out song))
            {
                if (song.Equals(this.currentlyPlaying))
                    return;
            }
            else if (song != null)
            {
                MediaPlayer.Play(song);
            }
            else
                MediaPlayer.Stop();

                           
            // get state from this ISubject (gameState?)
            // subject -> getState()
            // if state changed, change music.
            // get corresponding state music.
            //playSong(new state)
           
        }
    }
}
