using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="KeyboardShooterPage"/>
    /// </summary>
    public class KeyboardShooterViewModel : BaseViewModel
    {
        #region Private Members

        private readonly object FallingTextsLock = new object();

        public class FallingText
        {
            public FallingText(string text, int xPosition)
            {
                Text = text;
                Xposition = xPosition;
                Yposition = 0;
            }
            public string Text { get; set; }
            public int Xposition { get; private set; }
            public int Yposition { get; set; }
        }

        private Timer SpawnTimer = new();
        private Timer MoveTimer = new();
        private bool spwanTimerIsRunning;

        #endregion

        #region Properties

        public bool ItemSpawned { get; set; }
        public bool MoveItems { get; set; }
        public bool TextShot { get; set; }
        public int CanvasHeight { get; set; } = 100;
        public int CanvasWidth { get; set; } = 100;
        public string CurrentTextTarget { get; set; } = string.Empty;

        public List<FallingText> FallingTexts{ get; set; } = new List<FallingText>();

        /// <summary>
        /// The view model for the top bar
        /// </summary>
        public TopBarViewModel TopBar { get; set; } = new();

        #endregion

        #region Commands

        public ICommand AddItemToCanvasCommand { get; set; }

        public ICommand KeyboardInputCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public KeyboardShooterViewModel()
        {
            MoveTimer.Interval = 50;
            MoveTimer.Elapsed += MoveTimer_Elapsed;
            SpawnTimer.Elapsed += SpawnTimer_Elapsed;
            SpawnTimer.Interval = 500;
            AddItemToCanvasCommand = new RelayCommand(AddItemToCanvas);
            KeyboardInputCommand = new RelayParameterizedCommand(KeyboardInput);
        }

        #endregion

        #region Command Methods

        private void KeyboardInput(object obj)
        {
            var letter = obj.ToString();
            if (CurrentTextTarget != string.Empty)
            {

            }
            else
            {
                lock(FallingTextsLock)
                {
                    foreach (FallingText item in FallingTexts)
                    {
                        if (item.Text[0] == letter[0])
                        {
                            if (item.Text.Length > 1)
                            {
                                item.Text = item.Text.Substring(1);
                                CurrentTextTarget = item.Text;
                            }
                            else
                            {
                                FallingTexts.Remove(item);
                                CurrentTextTarget = string.Empty;
                            }
                            TextShot ^= true;
                            break;
                        }
                    } 
                }
            }
        }

        #endregion

        #region Event Methods

        private void MoveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (FallingTextsLock)
            {
                for (int i = 0; i < FallingTexts.Count; i++)
                {
                    var item = FallingTexts[i];
                    item.Yposition++;
                    if (item.Yposition > CanvasHeight)
                    {
                        FallingTexts.RemoveAt(i);
                        --i;
                    }
                } 
            }
            MoveItems ^= true;
        }

        private void SpawnTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var random = new Random();
            var randomNum = random.Next(0, CanvasWidth);

            FallingTexts.Add(new FallingText("A", randomNum));
            ItemSpawned ^= true;
        }

        #endregion

        #region Private Methods

        private void AddItemToCanvas()
        {
            if (!spwanTimerIsRunning)
            {
                FallingTexts.Add(new FallingText("A", 40));
                ItemSpawned ^= true;
                SpawnTimer.Start();
                MoveTimer.Start();
                spwanTimerIsRunning = true;
            }
            else
            {
                MoveTimer.Stop();
                SpawnTimer.Stop();
                spwanTimerIsRunning = false;
            }
        }

        #endregion
    }
}
