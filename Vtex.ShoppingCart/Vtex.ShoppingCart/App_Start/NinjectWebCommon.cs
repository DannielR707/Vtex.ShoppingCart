[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Vtex.ShoppingCart.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Vtex.ShoppingCart.App_Start.NinjectWebCommon), "Stop")]

namespace Vtex.ShoppingCart.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Net.Http.Headers;
    using Service;
    using Service.Interface;
    using Octokit;
    using Repo.Interface;
    using Repo;
    using Octokit.Internal;
    using System.Configuration;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {

            //Initialization of basic authentication for Accessing GitHub API
            Credentials credentials = new Credentials(ConfigurationManager.AppSettings["GitHubAPIToken"]);
            ICredentialStore credentialStore = new InMemoryCredentialStore(credentials);
            Octokit.ProductHeaderValue productHeader = new Octokit.ProductHeaderValue("VtexShoopingCart");

            //kernel.Bind<ICredentialStore>().ToMethod(context => new InMemoryCredentialStore(credentials));
            //kernel.Bind<IGitHubClient>().To<GitHubClient>().InRequestScope();

            kernel.Bind<IGitHubClient>().ToMethod(context => new GitHubClient(productHeader, credentialStore));
            kernel.Bind<IPriceRepository>().To<PriceRepository>().InRequestScope();
            kernel.Bind<IPriceService>().To<PriceService>().InRequestScope();
            kernel.Bind<IDeveloperService>().To<DeveloperService>().InRequestScope();
        }        
    }
}
