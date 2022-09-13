﻿using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository.Common
{
    public interface ITeamRepository
    {
        Task<Team> GetAsync(Guid id);
        Task<List<Team>> GetAllTeamsAsync();
    }
}
