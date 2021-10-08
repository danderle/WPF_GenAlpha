using System.Globalization;
using System.Speech.Synthesis;

namespace GenAlpha.Core
{
    /// <summary>
    /// Static class to read out words in the specified language
    /// </summary>
    public static class Speech
    {
        #region Fields

        /// <summary>
        /// The speech Synthesizer to use
        /// </summary>
        private static SpeechSynthesizer synth;

        #endregion

        #region static constructor

        /// <summary>
        /// Static constructor
        /// </summary>
        static Speech()
        {
            // init speech snythesizer
            synth = new SpeechSynthesizer();
            // Configure the audio output.   
            synth.SetOutputToDefaultAudioDevice();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads out the given text in the given language
        /// </summary>
        /// <param name="text"></param>
        /// <param name="culture"></param>
        public static void Speak(string text, string culture)
        {
            // Speak word
            var prompt = new PromptBuilder(new CultureInfo(culture));
            prompt.AppendText(text);
            synth.SpeakAsync(prompt);

        }

        #endregion
    }
}
