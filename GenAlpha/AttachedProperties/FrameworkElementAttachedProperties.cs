using System.Windows;

namespace GenAlpha
{
    #region Animation Properties

    /// <summary>
    /// A base class to run any animation method when a boolean is set to true
    /// and a reverse animation when set to false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperties<Parent, bool>
        where Parent : BaseAttachedProperties<Parent, bool>, new()
    {

        #region Public Properties

        /// <summary>
        /// A flag indicating if this is the first time this property has been loaded
        /// </summary>
        public bool FirstLoad { get; set; } = true;

        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Get the framework element
            if(!(sender is FrameworkElement element))
            {
                return;
            }

            // Dont fire if the value doesnt change
            if(sender.GetValue(ValueProperty) == value && !FirstLoad)
            {
                return;
            }

            //On first load..
            if(FirstLoad)
            {
                //Create a single self-unhookable event
                // for the elements Loaded events
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    //Unhook the event
                    element.Loaded -= onLoaded;

                    //Do desired animtion
                    DoAnimation(element, (bool)value);

                    //No longer in first load
                    FirstLoad = false;
                };

                // Hook into the loaded event of the element
                element.Loaded += onLoaded;
            }
            else
            {
                //Do desired animtion
                DoAnimation(element, (bool)value);
            }
        }

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="value">The value</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }

    /// <summary>
    /// Animates elements in from the right and back out to the right
    /// </summary>
    public class AnimateSlideInFromRightProperty : AnimateBaseProperty<AnimateSlideInFromRightProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
            {
                // Animte in
                await element.SlideInFromRightAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
                // Animate out
                await element.SlideOutToRightAsync(FirstLoad ? 0 : 0.3f, false);
        }
    }

    #endregion

    #region Height and width properties

    /// <summary>
    /// Gets the actual height after element is loaded
    /// </summary>
    public class ActualHeightProperty : BaseAttachedProperties<ActualHeightProperty, double>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                element.Loaded += Element_Loaded;
            }
        }

        private void Element_Loaded(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            SetValue(element, element.Height);
        }
    }

    /// <summary>
    /// Gets the actual width after element is loaded
    /// </summary>
    public class ActualWidthProperty : BaseAttachedProperties<ActualWidthProperty, double>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                element.Loaded += Element_Loaded;
            }
        }

        private void Element_Loaded(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            SetValue(element, element.Width);
        }
    } 
    #endregion
}
