using Microsoft.Extensions.DependencyInjection;
using System;

namespace GenAlpha.Core
{
    /// <summary>
    /// The DI container for our application
    /// </summary>
    public static class DI
    {
        #region Public Properties

        public static IServiceProvider Provider { get; private set; }

        public static IServiceCollection Services { get; set; } = new ServiceCollection();

        public static ApplicationViewModel Application => Service<ApplicationViewModel>();


        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// NOTE: Must be calles as soon as application starts up to ensure all services can be found
        /// </summary>
        public static void Setup()
        {
            AddViewModels();
            Build();
        }

        /// <summary>
        /// Binds all the singleton view models
        /// </summary>
        private static void AddViewModels()
        {
            // Bind to a single instance of application view model
            Services.AddSingleton<ApplicationViewModel>();
        }

        #endregion

        /// <summary>
        /// Builds the service collection into a service provider
        /// </summary>
        public static void Build()
        {
            // Use given provider or build it
            Provider = Services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets a service from the DI, of the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Service<T>()
        {
            return Provider.GetService<T>();
        }
    }
}
