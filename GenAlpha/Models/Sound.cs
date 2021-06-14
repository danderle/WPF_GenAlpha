using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GenAlpha
{
    public static class Sound
    {

        private const string CARD_FLIP_SOUND_FILEPATH = @"Resources/Sounds/CardFlip.wav";
        private const string CARD_MATCH_SOUND_FILEPATH = @"Resources/Sounds/Pass.mp3";
        private const string CARD_NOMATCH_SOUND_FILEPATH = @"Resources/Sounds/Fail.wav";
        private const string CARD_SPINOUT_SOUND_FILEPATH = @"Resources/Sounds/CardSpinOut.mp3";

        private static readonly Dictionary<SoundTypes, string> soundPaths = new();
        private static MediaPlayer mediaPlayer = new ();

        static Sound()
        {
            soundPaths.Add(SoundTypes.CardFlip, CARD_FLIP_SOUND_FILEPATH);
            soundPaths.Add(SoundTypes.CardMatch, CARD_MATCH_SOUND_FILEPATH);
            soundPaths.Add(SoundTypes.CardNoMatch, CARD_NOMATCH_SOUND_FILEPATH);
            soundPaths.Add(SoundTypes.CardSpinOut, CARD_SPINOUT_SOUND_FILEPATH);
        }

        public static void Play(SoundTypes sound)
        {
            mediaPlayer.Open(Path(sound));
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }

        private static Uri Path(SoundTypes sound)
        {
            return new Uri(soundPaths[sound], UriKind.Relative);
        }
    }
}
