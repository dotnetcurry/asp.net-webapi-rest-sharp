using System.Web.Http;
using Unity;
using Unity.WebApi;
using REST_Api.Models;
using REST_Api.Repositories;
namespace REST_Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType(typeof(AppDataEntities));
            container.RegisterType(typeof(IRepository<EmployeeInfo, int>), typeof(EmpRepository));
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}