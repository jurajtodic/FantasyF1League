using Autofac;
using AutoMapper;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FantasyFootballLeague.WebAPI.Controllers.FantasyLeagueController;
using static FantasyFootballLeague.WebAPI.Controllers.FantasyTeamController;

namespace FantasyFootballLeague.WebAPI.Models
{
    public class DIAutoMapper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //primjer registriranja za automapper
            // builder.RegisterType<Prvi>.AsSelf();
            // builder.RegisterType<Drugi>.AsSelf();
            builder.RegisterType<ScoringRules>().AsSelf();
            builder.RegisterType<ScoringRulesRest>().AsSelf();
            builder.RegisterType<List<ScoringRules>>().AsSelf();
            builder.RegisterType<List<ScoringRulesRest>>().AsSelf();

            builder.RegisterType<FantasyLeague>().AsSelf();
            builder.RegisterType<FantasyLeagueRest>().AsSelf();
            builder.RegisterType<List<FantasyLeague>>().AsSelf();
            builder.RegisterType<List<FantasyLeagueRest>>().AsSelf();

            builder.RegisterType<FantasyTeam>().AsSelf();
            builder.RegisterType<FantasyTeamRest>().AsSelf();
            builder.RegisterType<List<FantasyTeam>>().AsSelf();
            builder.RegisterType<List<FantasyTeamRest>>().AsSelf();


            builder.RegisterType<List<Driver>>().AsSelf();
            builder.RegisterType<List<DriverRest>>().AsSelf();
            builder.RegisterType<Driver>().AsSelf();
            builder.RegisterType<DriverRest>().AsSelf();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //primjer kreiranja mape
                cfg.CreateMap<FantasyLeague,FantasyLeagueRest>();
                cfg.CreateMap<FantasyTeam, FantasyTeamRest>();
                cfg.CreateMap<FantasyTeamRest, FantasyTeam>();

                // cfg.CreateMap<Drugi, Prvi>();
                cfg.CreateMap<ScoringRules,ScoringRulesRest>();    
               
                cfg.CreateMap<ScoringRules,ScoringRulesRest>();
                cfg.CreateMap<Driver, DriverRest>();
                cfg.CreateMap<DriverRest, Driver>();

            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);


            }).As<IMapper>().InstancePerLifetimeScope();



        }
    }
}