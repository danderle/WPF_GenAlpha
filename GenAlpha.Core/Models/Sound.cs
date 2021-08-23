using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GenAlpha.Core
{
    /// <summary>
    /// The static class to play preloaded sounds
    /// </summary>
    public static class Sound
    {
        #region Sound file paths

        private const string CARD_FLIP_SOUND_FILEPATH = @"Resources/Sounds/CardFlip.wav";
        private const string CARD_MATCH_SOUND_FILEPATH = @"Resources/Sounds/Pass.mp3";
        private const string CARD_NOMATCH_SOUND_FILEPATH = @"Resources/Sounds/Fail.wav";
        private const string CARD_SPINOUT_SOUND_FILEPATH = @"Resources/Sounds/CardSpinOut.mp3";
        private const string SHOT_SOUND_FILEPATH = @"Resources/Sounds/Shot.wav";
        private const string HIT_SOUND_FILEPATH = @"Resources/Sounds/Hit.wav";
        private const string DRYFIRE_SOUND_FILEPATH = @"Resources/Sounds/DryFire.wav";

        #endregion

        #region Fields

        private static bool initialized = false;
        private static readonly Dictionary<SoundTypes, string> soundPaths = new();
        private static readonly Dictionary<SoundTypes, MediaPlayer> mediaPlayers = new();

        #endregion

        #region Constructor

        /// <summary>
        /// Default static constructor
        /// </summary>
        static Sound()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Preloads all the sound files and creates a mediaplayer for each file to play independantly
        /// </summary>
        public static void InitializeSounds()
        {
            if (!initialized)
            {
                soundPaths.Add(SoundTypes.CardFlip, CARD_FLIP_SOUND_FILEPATH);
                soundPaths.Add(SoundTypes.CardMatch, CARD_MATCH_SOUND_FILEPATH);
                soundPaths.Add(SoundTypes.CardNoMatch, CARD_NOMATCH_SOUND_FILEPATH);
                soundPaths.Add(SoundTypes.CardSpinOut, CARD_SPINOUT_SOUND_FILEPATH);
                soundPaths.Add(SoundTypes.Hit, HIT_SOUND_FILEPATH);
                soundPaths.Add(SoundTypes.Shot, SHOT_SOUND_FILEPATH);
                soundPaths.Add(SoundTypes.DryFire, DRYFIRE_SOUND_FILEPATH);
                foreach (var sound in soundPaths)
                {
                    var mediaPlayer = new MediaPlayer();
                    mediaPlayers.Add(sound.Key, mediaPlayer);
                }
                initialized = true;
            }
        }

        /// <summary>
        /// Plays the sound files asyncronously
        /// </summary>
        /// <param name="sound">the <see cref="SoundTypes"/> type of sound to play</param>
        /// <returns></returns>
        public static async void PlayAsync(SoundTypes sound)
        {
            MediaPlayer mediaPlayer;
            int milliseconds = 1;
            mediaPlayers.TryGetValue(sound, out mediaPlayer);
            mediaPlayer.Dispatcher.Invoke(() =>
            {
                mediaPlayer.Open(Path(sound));
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Play();
                if(mediaPlayer.NaturalDuration.HasTimeSpan)
                    milliseconds = Convert.ToInt32(mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds);
                else
                {
                    milliseconds = 250;
                }
            });
            await Task.Delay(milliseconds);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Returns a URI path of a sound files
        /// </summary>
        /// <param name="sound"><see cref="SoundTypes"/> to get</param>
        /// <returns></returns>
        private static Uri Path(SoundTypes sound)
        {
            return new Uri(soundPaths[sound], UriKind.Relative);
        } 

        #endregion
    }
}
