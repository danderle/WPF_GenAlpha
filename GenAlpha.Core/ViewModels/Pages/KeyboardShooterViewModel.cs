using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private const string WORDS_TEXT_FILE_PATH = @"Resources\TextFiles\20kWords.txt";

        #endregion

        #region Private Members

        /// <summary>
        /// Flag to let us know if the spawn timer is used
        /// </summary>
        private bool useSpawnTimer;

        /// <summary>
        /// Gets the setting value for word spawning intervals
        /// </summary>
        private int wordSpawnInterval;

        /// <summary>
        /// Word speed setting value
        /// </summary>
        private int wordSpeed;

        /// <summary>
        /// gets the settings value for word length
        /// </summary>
        private int wordLength;

        /// <summary>
        /// The timer to spawn a new <see cref="FallingText"/> item
        /// </summary>
        private readonly Timer spawnFallingTextTimer = new();

        /// <summary>
        /// The timer to move all the <see cref="FallingText"/> and <see cref="Bullets"/> items in the list
        /// </summary>
        private readonly Timer moveTimer = new();

        /// <summary>
        /// A flag for letting us know if the spawn timer is runnning
        /// </summary>
        private bool spwanTimerIsRunning;

        /// <summary>
        /// the list of words to create falling texts from
        /// </summary>
        public List<string> words = new();

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
        public TopBarViewModel TopBar { get; set; } = new TopBarViewModel();

        /// <summary>
        /// The side menu view model
        /// </summary>
        public SideMenuViewModel SideMenu { get; private set; } = new SideMenuViewModel();

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
            Sound.InitializeSounds();

            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Word length", SettingTypes.Increment, 1));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Use spawn timer", SettingTypes.Toggle, 0));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Word spawn time (ms)", SettingTypes.Increment, 5000));
            SideMenu.AddSettingsItems(new SettingsListItemViewModel("Word speed", SettingTypes.Increment, 1));

            TopBar.ToggelSettingsMenu = ToggleSettingsMenu;
            InitializeCommands();
            InitializeTimers();
            RestartGame();
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Opens and closes the settings menu
        /// </summary>
        private void ToggleSettingsMenu()
        {
            SideMenu.ShowSideMenu = !SideMenu.ShowSideMenu;
            if (!SideMenu.ShowSideMenu)
            {
                RestartGame();
                AddItemToCanvas();
            }
            else
            {
                StopTimers();
            }
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
            string letter = obj.ToString();

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
                for(int i = 0; i < FallingTexts.Count; i++)
                {
                    FallingText item = FallingTexts[i];
                    if (item != null && item.Text != string.Empty)
                    {
                        shot = FireBulletIfInputMatches(item, letter[0]);
                        if(shot)
                        {
                            break;
                        }
                    }
                }
            }

            if(shot)
            {
                TopBar.Score++;
                Sound.PlayAsync(SoundTypes.Shot);
            }
            else
            {
                Sound.PlayAsync(SoundTypes.DryFire);
            }
        }

        /// <summary>
        /// Adds <see cref="FallingText"/> items to the list
        /// </summary>
        private void AddItemToCanvas()
        {
            if (!useSpawnTimer)
            {
                SpawnTimer_Elapsed(this, null);
                moveTimer.Start();
            }
            // If timer not running start the timers
            else if (!spwanTimerIsRunning)
            {
                SpawnTimer_Elapsed(this, null);
                spawnFallingTextTimer.Start();
                moveTimer.Start();
                spwanTimerIsRunning = true;
            }
            else if (spwanTimerIsRunning)
            {
                moveTimer.Stop();
                spawnFallingTextTimer.Stop();
                spwanTimerIsRunning = false;
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
            MoveAllTexts();

            textHit = MoveAllBulletsAndCheckForHit();
            MoveItems = !MoveItems;
            if (textHit)
            {
                Sound.PlayAsync(SoundTypes.Hit);
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
            var randomNum = random.Next(0, CanvasWidth);
            FallingTexts.Add(new FallingText(words.Last(), randomNum, wordSpeed));
            words.RemoveAt(words.Count - 1);
            if(words.Count == 0)
            {
                words = GetAllWordsWithLength();
            }
            FallingTextItemSpawned ^= true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the current game settings
        /// </summary>
        private void GetGameSettings()
        {
            foreach(var setting in SideMenu.SettingsList.SettingItems)
            {
                if(setting.Name.Contains("Word length"))
                {
                    wordLength = setting.CurrentValue;
                }
                else if(setting.Name.Contains("Use spawn"))
                {
                    useSpawnTimer = setting.CurrentValue > 0;
                }
                else if (setting.Name.Contains("Word spawn time"))
                {
                    wordSpawnInterval = setting.CurrentValue;
                }
                else if (setting.Name.Contains("Word speed"))
                {
                    wordSpeed = setting.CurrentValue;
                }
            }
        }

        /// <summary>
        /// Gets a list of words with the defined word length
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllWordsWithLength()
        {
            var sortedWords = new List<string>();
            foreach (var word in File.ReadAllLines(WORDS_TEXT_FILE_PATH).ToList())
            {
                if (wordLength >= word.Length)
                {
                    sortedWords.Add(word.ToUpper());
                }
            }
            return sortedWords.Shuffle();
        }

        /// <summary>
        /// Restarts game
        /// </summary>
        private void RestartGame()
        {
            CurrentTextTarget = string.Empty;
            Bullets.Clear();
            FallingTexts.Clear();
            GetGameSettings();
            if (File.Exists(WORDS_TEXT_FILE_PATH))
            {
                words = GetAllWordsWithLength();
            }
            moveTimer.Interval = 100;
            spawnFallingTextTimer.Interval = wordSpawnInterval;
        }

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
                    item.Targeted = true;
                    item.Text = item.Text.Substring(1);
                    CurrentTextTarget = item.Text;
                }
                else
                {
                    item.Text = string.Empty;
                    CurrentTextTarget = string.Empty;
                    if(!useSpawnTimer)
                    {
                        SpawnTimer_Elapsed(this, null);
                    }
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
                    }
                    Bullets.Remove(bullet);
                    i--;
                    textHit = true;
                    MoveItems ^= true;
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
                if (item == null)
                {
                    continue;
                }

                item.Move();
                if (item.Yposition > CanvasHeight)
                {
                    if(CurrentTextTarget == item.Text)
                    {
                        CurrentTextTarget = string.Empty;
                    }
                    FallingTexts.RemoveAt(i);
                    --i;
                }
            }
        }

        /// <summary>
        /// Stops all runnning timers
        /// </summary>
        private void StopTimers()
        {
            spawnFallingTextTimer.Stop();
            moveTimer.Stop();
            spwanTimerIsRunning = false;
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
            moveTimer.Elapsed += MoveTimer_Elapsed;
            spawnFallingTextTimer.Elapsed += SpawnTimer_Elapsed;
        }

        #endregion
    }
}
