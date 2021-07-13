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
        #region Constants

        private const int SPAWN_TIMER_INTERVAL = 2000;

        private const int MOVE_TIMER_INTERVAL = 25;

        private int maxTextLength = 50;

        #endregion

        #region Private Members

        /// <summary>
        /// Locks the <see cref="FallingText"/> and <see cref="Bullets"/> lists
        /// </summary>
        private readonly object MovelLock = new ();

        /// <summary>
        /// Locks the <see cref="FallingText"/> and <see cref="Bullets"/> lists
        /// </summary>
        private readonly object InputLock = new();

        /// <summary>
        /// The timer to spawn a new <see cref="FallingText"/> item
        /// </summary>
        private Timer SpawnFallingTextTimer = new();

        /// <summary>
        /// The timer to move all the <see cref="FallingText"/> and <see cref="Bullets"/> items in the list
        /// </summary>
        private Timer MoveTimer = new();

        /// <summary>
        /// A flag for letting us know if the spawn timer is runnning
        /// </summary>
        private bool spwanTimerIsRunning;

        #endregion

        #region Properties

        /// <summary>
        /// A flag for letting us know if a new <see cref="FallingText"/> item has spawned
        /// </summary>
        public bool FallingTextItemSpawned { get; set; }

        /// <summary>
        /// A flag for letting us know if the <see cref="FallingText"/> and <see cref="Bullets"/> items have moved
        /// </summary>
        public bool MoveItems { get; set; }

        /// <summary>
        /// A flag for letting us know if a <see cref="FallingText"/> has been shot
        /// </summary>
        public bool TextShot { get; set; }

        /// <summary>
        /// The current canvas height
        /// </summary>
        public int CanvasHeight { get; set; } = 100;

        /// <summary>
        /// The current canvas width
        /// </summary>
        public int CanvasWidth { get; set; } = 100;

        /// <summary>
        /// The current targeted text, which is not typed to completion
        /// </summary>
        public string CurrentTextTarget { get; set; } = string.Empty;

        /// <summary>
        /// All of the falling text items
        /// </summary>
        public List<FallingText> FallingTexts { get; set; } = new List<FallingText>();

        /// <summary>
        /// All bullets currently live
        /// </summary>
        public List<Bullet> Bullets { get; set; } = new List<Bullet>();

        /// <summary>
        /// The view model for the top bar
        /// </summary>
        public TopBarViewModel TopBar { get; set; } = new();

        #endregion

        #region Commands

        /// <summary>
        /// The command to start the game
        /// </summary>
        public ICommand AddItemToCanvasCommand { get; set; }

        /// <summary>
        /// The command to handle keyboard input
        /// </summary>
        public ICommand KeyboardInputCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public KeyboardShooterViewModel()
        {
            InitializeTimers();
            InitializeCommands();
            Sound.InitializeSounds();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// The method gets executed when a key on keyboard is hit
        /// </summary>
        /// <param name="obj"></param>
        private void KeyboardInput(object obj)
        {
            bool shot = false;
            var letter = obj.ToString();
            if (CurrentTextTarget != string.Empty)
            {
                var item = FallingTexts.Find(t => t.Text == CurrentTextTarget);
                if(item != null)
                {
                    shot = FireBulletIfInputMatches(item, letter[0]);
                }
            }
            else
            {
                lock(InputLock)
                {
                    for(int i = 0; i < FallingTexts.Count; i++)
                    {
                        var item = FallingTexts[i];
                        if (item.Text != string.Empty)
                        {
                            shot = FireBulletIfInputMatches(item, letter[0]);
                            break;
                        }
                    } 
                }
            }

            if(shot)
            {
                PlaySoundAsync(SoundTypes.Shot);
            }
            else
            {
                PlaySoundAsync(SoundTypes.DryFire);
            }
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// The event method gets executed when the move timer has elapsed.
        /// This moves the falling texts downward
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool textHit;
            lock (MovelLock)
            {
                MoveAllTexts();

                textHit = MoveAllBulletsAndCheckForHit();
            }
            MoveItems ^= true;
            if (textHit)
            {
                PlaySoundAsync(SoundTypes.Hit);
            }
        }

        /// <summary>
        /// The event method gets executed when the spawn timer has elapsed.
        /// This will spawn a new text item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpawnTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var random = new Random();
            var randomNum = random.Next(0, CanvasWidth-maxTextLength);

            FallingTexts.Add(new FallingText("AB", randomNum));
            FallingTextItemSpawned ^= true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fires a bullet if input letter matches item letter
        /// </summary>
        /// <param name="item"></param>
        /// <param name="inputLetter"></param>
        /// <returns></returns>
        private bool FireBulletIfInputMatches(FallingText item, char inputLetter)
        {
            bool shot = item.Text[0] == inputLetter;
            if (shot)
            {
                Bullets.Add(new Bullet(item.Xposition, CanvasHeight - 40, item));
                if (item.Text.Length > 1)
                {
                    item.Text = item.Text.Substring(1);
                    CurrentTextTarget = item.Text;
                }
                else
                {
                    item.Text = string.Empty;
                }
            }
            return shot;
        }

        /// <summary>
        /// Moves all bullets upwards and return true if text has been hit
        /// </summary>
        /// <returns></returns>
        private bool MoveAllBulletsAndCheckForHit()
        {
            bool textHit = false;
            for (int i = 0; i < Bullets.Count; i++)
            {
                var bullet = Bullets[i];
                bullet.Move();
                var item = FallingTexts.Find(t => t == bullet.Target);
                if (item == null)
                    continue;
                if (item.Yposition >= bullet.Yposition)
                {
                    if (item.DisplayedText.Length > 1)
                    {
                        item.DisplayedText = item.DisplayedText.Substring(1);
                    }
                    else
                    {
                        FallingTexts.Remove(item);
                        CurrentTextTarget = string.Empty;
                    }
                    Bullets.Remove(bullet);
                    i--;
                    TextShot ^= true;
                    textHit = true;
                }
            }
            return textHit;
        }

        /// <summary>
        /// Moves all the texts from top to bottom
        /// </summary>
        private void MoveAllTexts()
        {
            for (int i = 0; i < FallingTexts.Count; i++)
            {
                var item = FallingTexts[i];
                item.Move();
                if (item.Yposition > CanvasHeight)
                {
                    CurrentTextTarget = string.Empty;
                    FallingTexts.RemoveAt(i);
                    --i;
                }
            }
        }

        /// <summary>
        /// Plays sounds asynchronously
        /// </summary>
        /// <param name="sound"></param>
        private async void PlaySoundAsync(SoundTypes sound)
        {
            await Sound.PlayAsync(sound);
        }

        /// <summary>
        /// Adds <see cref="FallingText"/> items to the list
        /// </summary>
        private void AddItemToCanvas()
        {
            // If timer not running start the timers
            if (!spwanTimerIsRunning)
            {
                SpawnFallingTextTimer.Start();
                MoveTimer.Start();
                spwanTimerIsRunning = true;
            }
            else
            {
                MoveTimer.Stop();
                SpawnFallingTextTimer.Stop();
                spwanTimerIsRunning = false;
            }
        }

        /// <summary>
        /// Initialized the commands
        /// </summary>
        private void InitializeCommands()
        {
            AddItemToCanvasCommand = new RelayCommand(AddItemToCanvas);
            KeyboardInputCommand = new RelayParameterizedCommand(KeyboardInput);
        }

        /// <summary>
        /// Initialized the timers
        /// </summary>
        private void InitializeTimers()
        {
            MoveTimer.Interval = MOVE_TIMER_INTERVAL;
            MoveTimer.Elapsed += MoveTimer_Elapsed;
            SpawnFallingTextTimer.Elapsed += SpawnTimer_Elapsed;
            SpawnFallingTextTimer.Interval = SPAWN_TIMER_INTERVAL;
        }

        #endregion
    }
}
