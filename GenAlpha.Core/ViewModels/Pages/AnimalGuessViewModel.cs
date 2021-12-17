using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for the <see cref="AnimalGuessPage"/>
    /// </summary>
    public class AnimalGuessViewModel : BaseViewModel
    {
        #region Fields

        private const int TIMER_INTERVAL = 1000;

        private Timer stopwatchTimer = new Timer(TIMER_INTERVAL);

        private double lastRadius = 10;

        private bool insideWindow = false;

        private Dictionary<string, string> images;

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag to show the start button
        /// </summary>
        public bool ShowStartButton { get; set; } = true;

        /// <summary>
        /// A flag to let us know if the images are found
        /// </summary>
        public bool NoImagesFound { get; set; }

        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        public int ElapsedTime { get; set; } = 0;

        /// <summary>
        /// The radius of the spy circle
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The current mouse position
        /// </summary>
        public Point MousePosition { get; set; }

        /// <summary>
        /// The top bar for this view model
        /// </summary>
        public BaseTopBarViewModel TopBar { get; set; } = new BaseTopBarViewModel();

        #endregion

        #region Commands

        /// <summary>
        /// The mouse move command
        /// </summary>
        public ICommand MouseMoveCommand { get; set; }

        /// <summary>
        /// The mouse enter command
        /// </summary>
        public ICommand MouseEnterCommand { get; set; }

        /// <summary>
        /// The mouse leave command
        /// </summary>
        public ICommand MouseLeaveCommand { get; set; }

        /// <summary>
        /// The command to start the game
        /// </summary>
        public ICommand StartCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AnimalGuessViewModel()
        {
            stopwatchTimer.Elapsed += StopwatchTimer_Elapsed;
            IntializeCommands();
            InitializeProperties();
        }

        #endregion

        #region Events methods

        /// <summary>
        /// the stopwatch event to add a second to the elapsed time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopwatchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ElapsedTime += 1;
            if (insideWindow)
            {
                Radius += 1;
            }
            else
            {
                lastRadius += 1;
            }
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// Start the game
        /// </summary>
        private void Start()
        {
            ShowStartButton = false;
            stopwatchTimer.Start();

        }

        /// <summary>
        /// Sets the current mouse position
        /// </summary>
        /// <param name="obj"></param>
        private void MouseMoved(object obj)
        {
            string position = obj.ToString();
            var coo = position.Split(";");
            MousePosition = new Point(Convert.ToDouble(coo[0]), Convert.ToDouble(coo[1]));
        }

        /// <summary>
        /// On entering set the radius
        /// </summary>
        private void MouseEnter()
        {
            insideWindow = true;
            Radius = lastRadius;
        }

        /// <summary>
        /// On leave set the radius to zero
        /// </summary>
        private void MouseLeave()
        {
            insideWindow = false;
            lastRadius = Radius;
            Radius = 0;
        }

        #endregion

        #region Private helpers

        /// <summary>
        /// Initializes the commands
        /// </summary>
        private void IntializeCommands()
        {
            MouseLeaveCommand = new RelayCommand(MouseLeave);
            MouseEnterCommand = new RelayCommand(MouseEnter);
            MouseMoveCommand = new RelayParameterizedCommand(MouseMoved);
            StartCommand = new RelayCommand(Start);
        }

        /// <summary>
        /// Initializes the fields and properties
        /// </summary>
        private void InitializeProperties()
        {
            images = Image.GetAllAnimalImages();
            if (images.Count == 0)
            {
                NoImagesFound = true;
            }
        }

        #endregion
    }
}
