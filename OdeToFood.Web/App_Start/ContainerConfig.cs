using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OdeToFood.Data.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace OdeToFood.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {

            //build my IoC Builder
            var builder = new ContainerBuilder();

            //Tell this builder about differents abstractions in my app
            //Tell it what assembly contain the controllers for my app
            //MvcApplication is the class that represent this app, See global asax.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            //Tell this builder about the specific services I have
            //I want to use InMemoeryRestaurantData whenever something ask for an object that implements IRestaurantData
            //In future I can change InMemoryRestauranData for something like A conctext class that connnects to database
            //and I just need to change it here, Once!
            builder.RegisterType<SqlRestaurantData>()
                .As<IRestaurantData>()
               .InstancePerRequest();//This specifie when you want to use what I said above. 
            builder.RegisterType<OdeToFoodDbContext>().InstancePerRequest();

            ///Create my container
            var container = builder.Build();
            //Tell to MVC Framework whic container use to resolve dependencies.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //Web Api contreollers are in other library so you need another dependency resolver for that.
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);



        }
    }
}