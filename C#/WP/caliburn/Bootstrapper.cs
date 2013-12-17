using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
namespace Demo01
{
   public class Bootstrapper : PhoneBootstrapper
    {
       public PhoneContainer container { get; set; }
       protected override void Configure()
        {
 	        container = new PhoneContainer();
           
           container.RegisterPhoneServices(RootFrame);
           container.PerRequest<MainPageViewModel>();
           container.PerRequest<Page2ViewModel>();

           container.PerRequest<PivotViewModel>();
           container.PerRequest<PivotItem1ViewModel>();
           container.PerRequest<PivotItem2ViewModel>();
           AddCustomConventions();
        }

       static void AddCustomConventions()
       {

       }

           protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }
 
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }
 
        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
        }
}
