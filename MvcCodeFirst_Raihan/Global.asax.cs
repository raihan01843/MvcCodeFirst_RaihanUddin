using AutoMapper;
using MvcCodeFirst_Raihan.Models;
using MvcCodeFirst_Raihan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcCodeFirst_Raihan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(config =>
            {
                config.CreateMap<StudentVM, Student>();
                config.CreateMap<Student, StudentVM>();

                config.CreateMap<ProductVM, Product>();
                config.CreateMap<Product, ProductVM>();
            });
        }
    }
}
