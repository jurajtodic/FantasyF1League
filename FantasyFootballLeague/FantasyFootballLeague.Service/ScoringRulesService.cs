using FantasyFootballLeague.Model;
using FantasyFootballLeague.Repository.Common;
using FantasyFootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Service
{
    public class ScoringRulesService : IScoringRulesService
    {
        protected IScoringRulesRepository ScoringRulesRepository;

        public ScoringRulesService(IScoringRulesRepository scoringRulesRepository)
        {
            this.ScoringRulesRepository = scoringRulesRepository;
        }

        public async Task<ScoringRules> GetScoringRulesByIdAsync(Guid id)
        {
            return await ScoringRulesRepository.GetScoringRulesByIdAsync(id);
        }

        public async Task<List<ScoringRules>> GetAllScoringRulesAsync()
        {
            return await ScoringRulesRepository.GetAllScoringRulesAsync();
        }

        public async Task<ScoringRules> UpdateScoringRulesByIdAsync(System.Guid id, ScoringRules rules)
        {
            return await ScoringRulesRepository.UpdateScoringRulesByIdAsync(id, rules);
        }
    }

}

