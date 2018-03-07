using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Autofac;

namespace Tinyblog.Server.ServiceBehavior
{
    public class AutofacInstanceProvider : IInstanceProvider
    {
        private readonly IContainer container;

        public AutofacInstanceProvider(IContainer container)
        {
            this.container = container;
        }

        public Type ServiceType { private get; set; }

        public object GetInstance(InstanceContext instanceContext)
        {
            return container.Resolve(ServiceType);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return container.Resolve(ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}
