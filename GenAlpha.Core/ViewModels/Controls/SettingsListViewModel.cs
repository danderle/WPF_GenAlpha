﻿using System.Collections.ObjectModel;

namespace GenAlpha.Core
{
    /// <summary>
    /// The view model for a settings list
    /// </summary>
    public class SettingsListViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// The list of setting items
        /// </summary>
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
