using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GenAlpha
{
    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        #region Private Fields

        /// <summary>
        /// The View model for this page
        /// </summary>
        private VM _viewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The animation to play when the pages is first loaded
        /// </summary>
        public PageAnimationTypes PageLoadAnimation { get; set; } = PageAnimationTypes.SlideAndFadeInFromRight; 
        
        /// <summary>
        /// The animation to play when the page is unloaded
        /// </summary>
        public PageAnimationTypes PageUnloadAnimation { get; set; } = PageAnimationTypes.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time to complete the slide animation
        /// </summary>
        public float SlidesSeconds { get; set; } = 0.8f;

        /// <summary>
        /// The View model for this page
        /// </summary>
        public VM ViewModel 
        { 
            get => _viewModel;
            set
            {
                //if value changed
                if(_viewModel != value)
                {
                    //Set view model
                    _viewModel = value;
                    //Bind the viewmodel to this page
                    DataContext = _viewModel;
                }
            }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            //Hide at begin of animatioon
            if(PageLoadAnimation != PageAnimationTypes.None)
            {
                Visibility = Visibility.Collapsed;
            }

            //listen out for the page loading
            Loaded += BasePage_Loaded;

            //Create a default view model
            ViewModel = new VM();
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any set animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await AnimateInAsync();
        }

        /// <summary>
        /// Animates the page in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            if (PageLoadAnimation == PageAnimationTypes.None)
                return;

            switch(PageLoadAnimation)
            {
                case PageAnimationTypes.SlideAndFadeInFromRight:
                    await this.SlideAndFadeInFromRight(SlidesSeconds);
                    break;
            }
        }
        
        /// <summary>
        /// Animates the page out
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync()
        {
            if (PageLoadAnimation == PageAnimationTypes.None)
                return;

            switch(PageUnloadAnimation)
            {
                case PageAnimationTypes.SlideAndFadeOutToLeft:
                    await this.SlideAndFadeOutToLeft(SlidesSeconds);
                    break;
            }
        }

        #endregion
    }
}
