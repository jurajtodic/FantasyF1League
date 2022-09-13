using Autofac;
using Autofac.Integration.WebApi;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Repository;
using FantasyFootballLeague.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace FantasyFootballLeague.WebAPI.Models
{
    public class DIBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<DIAutoMapper>();
            builder.RegisterModule<DIService>();
            builder.RegisterModule<DIRepository>();
            builder.RegisterModule<DIModel>();

            /*primjer dodavanja Modula*/
            /*builder.RegisterModule<DIModuleService>();*/
            return builder.Build();            
            

        }




    }
}