using Autofac;
using FantasyFootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Service
{
    public class DIService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Primjer dodavanja modula po layeru
            //builder.RegisterType<BookService>().As<IBookService>();
            //builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<DriverService>().As<IDriverService>();
            builder.RegisterType<FantasyLeagueService>().As<IFantasyLeagueService>();
            builder.RegisterType<FantasyTeamService>().As<IFantasyTeamService>();
            builder.RegisterType<FantasyTeamDriverService>().As<IFantasyTeamDriverService>();
            builder.RegisterType<ScoringRulesService>().As<IScoringRulesService>();
            builder.RegisterType<TeamService>().As<ITeamService>();
            base.Load(builder);
        }
    }
}
