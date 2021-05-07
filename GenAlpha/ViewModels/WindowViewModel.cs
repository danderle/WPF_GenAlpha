using System.Windows;
using System.Windows.Input;

namespace GenAlpha
{
    /// <summary>
    /// The view model for the custom window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// the window this viewmodel controls
        /// </summary>
        private Window _window;

        private int _outerMarginSize = 10;

        private int _windowRadius = 4;

        #endregion

        #region Properties

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize => _window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get { return _window.WindowState == WindowState.Maximized ? 0 : _windowRadius; }
            set { _outerMarginSize = value; }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius => _window.WindowState == WindowState.Maximized ? new CornerRadius(0) : new CornerRadius(WindowRadius);

        /// <summary>
        /// The height of the title bar
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// The height of the title bar
        /// </summary>
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to open the system menu the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            _window = window;

            //Listen out for the window resizing
            _window.StateChanged += _window_StateChanged;

            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(_window, GetMousePosition()));
        }



        #endregion

        #region Events

        private void _window_StateChanged(object sender, System.EventArgs e)
        {
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            /// mouse position relative to the window
            var position = Mouse.GetPosition(_window);
            return new Point(position.X + _window.Left, position.Y + _window.Top);
        }

        #endregion

    }
}
