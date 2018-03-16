using System;
using System.Configuration;
using System.ServiceModel;
using Autofac;
using Tinyblog.Common.Log.Implementations;
using Tinyblog.DataLayer.Core.Implementations;
using Tinyblog.DataLayer.Repository.Implementations.NHibernate;
using Tinyblog.Server.Infrastructure;
using Tinyblog.Server.ServiceBehavior;
using Tinyblog.Services;
using Tinyblog.Services.Processors.Implementations;

namespace Tinyblog.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = ResolveDependencies();
            using (var host = new ServiceHost(typeof(TinyblogController)))
            {
                host.Description.Behaviors.Add(new AutofacServiceBehavior(container));
                host.Open();
                Console.WriteLine("Tinyblog service is running.");
                Console.WriteLine("Press any key to close service.");
                Console.ReadKey();
                host.Close();
                container.Dispose();
            }
        }

        private static IContainer ResolveDependencies()
        {
            var builder = new ContainerBuilder();
            var connectionString = ConfigurationManager.ConnectionStrings["TinyblogPostgre"].ConnectionString;

            builder.RegisterType<NhArticleRepository>().AsImplementedInterfaces();
            builder.RegisterType<NhCommentRepository>().AsImplementedInterfaces();
            builder.RegisterType<ArticleProcessorUoW>().AsImplementedInterfaces();
            builder.RegisterType<AutofacRepositoryResolver>().AsImplementedInterfaces();
            builder.RegisterType<SerilogLogger>().AsImplementedInterfaces();
            builder.RegisterType<NHibernateUnitOfWorkFactory>()
                .WithParameter(new TypedParameter(typeof(string), connectionString))
                .AsImplementedInterfaces();
            builder.RegisterType<TinyblogController>().AsSelf();
            return builder.Build();
        }
    }
}
