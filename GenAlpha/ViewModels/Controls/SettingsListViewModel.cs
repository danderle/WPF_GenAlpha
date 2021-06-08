using System.Collections.ObjectModel;

namespace GenAlpha
{
    /// <summary>
    /// The view model for a settings list
    /// </summary>
    public class SettingsListViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<SettingsListItemViewModel> SettingItems { get; set; } = new ObservableCollection<SettingsListItemViewModel>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsListViewModel()
        {

        }

        #endregion

    }
}
