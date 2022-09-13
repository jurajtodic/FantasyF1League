using FantasyFootballLeague.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model
{
    public class Team : ITeam
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }

        public Team(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Team()
        {
        }
    }
}
