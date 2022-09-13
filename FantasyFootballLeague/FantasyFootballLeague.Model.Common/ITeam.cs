using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model.Common
{
    public interface ITeam
    {
        System.Guid Id { get; set; }
        string Name { get; set; }
    }
}
