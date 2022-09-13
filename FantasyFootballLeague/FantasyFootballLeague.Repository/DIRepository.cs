using Autofac;
using FantasyFootballLeague.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository
{
    public class DIRepository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //primjer dodavanja modula za DI
            //builder.RegisterType<BookService>().As<IBookService>();
            //builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<DriverRepository>().As<IDriverRepository>();
            builder.RegisterType<FantasyLeagueRepository>().As<IFantasyLeagueRepository>();
            builder.RegisterType<FantasyTeamRepository>().As<IFantasyTeamRepository>();
            builder.RegisterType<FantasyTeamDriverRepository>().As<IFantasyTeamDriverRepository>();
            builder.RegisterType<ScoringRulesRepository>().As<IScoringRulesRepository>();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>();
            base.Load(builder);
        }
    }
}
