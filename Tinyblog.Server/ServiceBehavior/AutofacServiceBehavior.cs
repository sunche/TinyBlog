using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Autofac;

namespace Tinyblog.Server.ServiceBehavior
{
    public class AutofacServiceBehavior : IServiceBehavior
    {
        private readonly AutofacInstanceProvider instanceProvider;

        public AutofacServiceBehavior(IContainer container)
        {
            instanceProvider = new AutofacInstanceProvider(container);
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                if (dispatcherBase is ChannelDispatcher channelDispatcher)
                {
                    foreach (EndpointDispatcher dispatcher in channelDispatcher.Endpoints)
                    {
                        instanceProvider.ServiceType = serviceDescription.ServiceType;
                        dispatcher.DispatchRuntime.InstanceProvider = instanceProvider;
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
