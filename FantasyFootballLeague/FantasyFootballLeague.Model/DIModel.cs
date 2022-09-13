using Autofac;
using FantasyFootballLeague.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model
{
    public class DIModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //primjer dodavanja modula za DI
            //builder.RegisterType<BookService>().As<IBookService>();
            //builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<Driver>().As<IDriver>();
            builder.RegisterType<FantasyLeague>().As<IFantasyLeague>();
            builder.RegisterType<FantasyTeam>().As<IFantasyTeam>();
            builder.RegisterType<FantasyTeamDriver>().As<IFantasyTeamDriver>();
            builder.RegisterType<ScoringRules>().As<IScoringRules>();
            builder.RegisterType<Team>().As<ITeam>();
            base.Load(builder);
        }
    }
}
