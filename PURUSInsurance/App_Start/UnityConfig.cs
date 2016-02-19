using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Services;

namespace PURUSInsurance
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

                      
            container.RegisterType<IQuote, svcQuote>();
            container.RegisterType<ICustomer, svcCustomer>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}