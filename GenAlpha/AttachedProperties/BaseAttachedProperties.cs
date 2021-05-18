using System;
using System.Windows;

namespace GenAlpha
{
    /// <summary>
    /// A base attached property to replace the vanilla WPF attached property
    /// </summary>
    /// <typeparam name="Parent">The parent class to be attached property</typeparam>
    /// <typeparam name="Property">The type of this attached property</typeparam>
    public abstract class BaseAttachedProperties<Parent, Property>
        where Parent : BaseAttachedProperties<Parent, Property>, new()
    {
        #region Public Events

        /// <summary>
        /// Fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// A singleton instance of our parent class
        /// </summary>
        public static Parent Instance { get; private set; } = new Parent();

        #endregion

        #region Attached Property Definitions

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperties<Parent, Property>), new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">The UI element that has its property changed</param>
        /// <param name="e">The arguments for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Call the parent function
            Instance.OnValueChanged(d, e);

            //call event listeners
            Instance.ValueChanged(d, e);
        }

        /// <summary>
        /// Gets the attached property
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Sets the attached property
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        #endregion

        #region Event Methods

        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        #endregion
    }
}
