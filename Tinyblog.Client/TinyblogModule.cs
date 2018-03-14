using System;
using Autofac;
using Tinyblog.Client.Services;
using Tinyblog.Client.Services.Implementations;
using Tinyblog.Client.ViewModels;
using Tinyblog.Common.Log.Implementations;
using Tinyblog.Contracts.Services;
using Tinyblog.Proxies.Shared;

namespace Tinyblog.Client
{
    /// <summary>
    /// Configuration module for containers.
    /// </summary>
    /// <seealso cref="Autofac.Module"/>
    public class TinyblogModule : Module
    {
        /// <summary>
        /// Gets or sets the name of the endpoint.
        /// </summary>
        public string EndpointName { get; set; } = "tcp";

        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">
        /// The builder through which components can be
        /// registered.
        /// </param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new TinyblogClient(EndpointName)).As<ITinyblogService>();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<MainViewModel>().AsSelf().UsingConstructor(typeof(IArticleService));
            builder.RegisterType<SerilogLogger>().AsImplementedInterfaces();
        }
    }
}
