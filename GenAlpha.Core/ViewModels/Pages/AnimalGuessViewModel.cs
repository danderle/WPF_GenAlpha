using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        #region Constants

        private const int TIMER_INTERVAL = 1000;

        private const int START_RADIUS = 10;

        #endregion

        #region Fields

        private Timer stopwatchTimer = new Timer(TIMER_INTERVAL);

        private double lastRadius = 10;

        private bool insideWindow = false;

        private List<string> imagePaths;

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
        /// A flag to let us know if the image is covered
        /// </summary>
        public bool ImageCovered { get; set; } = true;

        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        public int ElapsedTime { get; set; } = 0;

        /// <summary>
        /// The radius of the spy circle
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The path of the image to identify
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// The current mouse position
        /// </summary>
        public Point MousePosition { get; set; }

        /// <summary>
        /// The list of possible choices
        /// </summary>
        public ObservableCollection<AnimalChoiceViewModel> AnimalChoices { get; set; } = new();

        /// <summary>
        /// The top bar for this view model
        /// </summary>
        public BaseTopBarViewModel TopBar { get; set; } = new ();

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

        /// <summary>
        /// The command to check if the right animal is found
        /// </summary>
        public ICommand AnimalFoundCommand { get; set; }

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
            GetRandomImage();
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

        /// <summary>
        /// The command method which checks if the animal was correctly found
        /// </summary>
        /// <param name="obj"></param>
        private void AnimalFound(object obj)
        {
            if ((bool)obj)
            {
                ImageCovered = false;
                stopwatchTimer.Stop();
                Radius = START_RADIUS;
                lastRadius = START_RADIUS;
            }
        }

        #endregion

        #region Private helpers

        /// <summary>
        /// Gets a random image and fills the list of possible animal choices and then shuffles the list
        /// </summary>
        private void GetRandomImage()
        {
            List<AnimalChoiceViewModel> list = new ();
            for (int i = 0; i < 4; i++)
            {
                Random random = new Random();
                int index = random.Next(imagePaths.Count);
                AnimalChoiceViewModel possibleChoice = new AnimalChoiceViewModel(Path.GetFileNameWithoutExtension(imagePaths[index]), AnimalFoundCommand);
                if (i == 0)
                {
                    ImagePath = imagePaths[index];
                    possibleChoice.IsHiddenAnimal = true;
                }
                list.Add(possibleChoice);
            }
            list.Shuffle();
            AnimalChoices = new ObservableCollection<AnimalChoiceViewModel>(list);
        }

        /// <summary>
        /// Initializes the commands
        /// </summary>
        private void IntializeCommands()
        {
            MouseLeaveCommand = new RelayCommand(MouseLeave);
            MouseEnterCommand = new RelayCommand(MouseEnter);
            MouseMoveCommand = new RelayParameterizedCommand(MouseMoved);
            StartCommand = new RelayCommand(Start);
            AnimalFoundCommand = new RelayParameterizedCommand(AnimalFound);
        }

        /// <summary>
        /// Initializes the fields and properties
        /// </summary>
        private void InitializeProperties()
        {
            imagePaths = Image.GetAllAnimalImagePaths();
            if (imagePaths.Count == 0)
            {
                NoImagesFound = true;
                return;
            }
        }

        #endregion
    }
}
