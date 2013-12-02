using Caliburn.Micro;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryDemo
{
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        private IUnityContainer container;

        protected override void Configure()
        {
            container = new UnityContainer();
            container.RegisterType<IWindowManager, WindowManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            container.RegisterInstance(container);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return container.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {

            return container.ResolveAll(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

    } 
}
