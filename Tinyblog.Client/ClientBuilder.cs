using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Tinyblog.Client.Services;
using Tinyblog.Client.ViewModels;

namespace Tinyblog.Client
{
    public class ClientBuilder
    {
        private IContainer container;
        public MainViewModel CreateApplication()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TinyblogModule());
            container = builder.Build();
            return container.Resolve<MainViewModel>();
        }
    }
}
