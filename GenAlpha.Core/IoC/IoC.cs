using Ninject;

namespace GenAlpha.Core
{
    /// <summary>
    /// The IoC container for our application
    /// </summary>
    public static class IoC
    {
        #region Public Properties

        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// NOTE: Must be calles as soon as application starts up to ensure all services can be found
        /// </summary>
        public static void Setup()
        {
            BindViewModels();
        }

        /// <summary>
        /// Binds all the singleton ciew models
        /// </summary>
        private static void BindViewModels()
        {
            // Bind to a single instance of application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        #endregion

        /// <summary>
        /// Gets a service from the ioc, of thew specidifed type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
