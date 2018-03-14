using System;
using System.Configuration;
using System.ServiceModel;
using Autofac;
using Tinyblog.Common.Log.Implementations;
using Tinyblog.DataLayer.Repository.Implementations;
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
            builder.Register(x => new ArticleRepository(connectionString)).AsImplementedInterfaces();
            builder.Register(x => new CommentRepository(connectionString)).AsImplementedInterfaces();
            builder.RegisterType<ArticleProcessor>().AsImplementedInterfaces();
            builder.RegisterType<SerilogLogger>().AsImplementedInterfaces();
            builder.RegisterType<TinyblogController>().AsSelf();
            return builder.Build();
        }
    }
}
