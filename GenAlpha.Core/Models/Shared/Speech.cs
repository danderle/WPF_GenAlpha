using System;
using System.Globalization;
using System.Speech.Synthesis;
using System.Threading.Tasks;

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

        #region Properties

        /// <summary>
        /// A flag to let us know if we are currently speaking
        /// </summary>
        public static bool IsSpeaking { get; private set; }

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
            synth.SpeakCompleted += Synth_SpeakCompleted;
        }

        #endregion

        #region Events

        /// <summary>
        /// The even which is triggered when the speech is finished
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            IsSpeaking = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads out the given text in the given language
        /// </summary>
        /// <param name="text"></param>
        /// <param name="culture"></param>
        public static void Speak(string text, string culture = "En-en")
        {
            IsSpeaking = true;
            // Speak word
            var prompt = new PromptBuilder(new CultureInfo(culture));
            prompt.AppendText(text);
            synth.Speak(prompt);
        }

        /// <summary>
        /// Reads out the given text in the given language
        /// </summary>
        /// <param name="text"></param>
        /// <param name="culture"></param>
        public static async Task SpeakAsync(string text, string culture = "En-en")
        {
            IsSpeaking = true;
            // Speak word
            var prompt = new PromptBuilder(new CultureInfo(culture));
            prompt.AppendText(text);
            await Task.Run(() => synth.SpeakAsync(prompt));
        }

        #endregion
    }
}
