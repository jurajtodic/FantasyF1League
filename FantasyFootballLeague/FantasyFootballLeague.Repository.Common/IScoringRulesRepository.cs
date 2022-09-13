using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository.Common
{
    public interface IScoringRulesRepository
    {
        Task<ScoringRules> GetScoringRulesByIdAsync(Guid id);
        Task<List<ScoringRules>> GetAllScoringRulesAsync();
        Task<ScoringRules> UpdateScoringRulesByIdAsync(System.Guid id, ScoringRules rules);

    }
}
