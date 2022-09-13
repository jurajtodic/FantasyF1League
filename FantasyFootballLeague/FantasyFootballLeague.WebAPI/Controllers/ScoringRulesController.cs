using AutoMapper;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FantasyFootballLeague.WebAPI.Controllers
{
    public class ScoringRulesController : ApiController
    {

        protected IScoringRulesService ScoringRulesService;
        protected IMapper Mapper;

        public ScoringRulesController(IScoringRulesService scoringRulesService, IMapper mapper)
        {
            this.ScoringRulesService = scoringRulesService;
            this.Mapper = mapper;
        }


        // GET: api/ScoringRules
        [HttpGet]
        [Route("scoringrules_list")]
        public async Task<HttpResponseMessage> GetAllTeamsAsync()
        {            

            try
            {
                List<ScoringRules> scoringRulesList = new List<ScoringRules>();
                scoringRulesList = await ScoringRulesService.GetAllScoringRulesAsync();
                //return Request.CreateResponse(HttpStatusCode.OK, scoringRulesList);                
                if (scoringRulesList == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Database is empty.");
                }
                else
                {
                    var listToRest = Mapper.Map<List<ScoringRules>, List<ScoringRulesRest>>(scoringRulesList);
                    return Request.CreateResponse(HttpStatusCode.OK, listToRest);
                }
            }

            catch (SqlException e)
            {
                throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
            }
        }

        // GET: api/ScoringRules/5
        [HttpGet]
        [Route("scoringRulesById_list")]
        public async Task<HttpResponseMessage> GetScoringRulesByIdAsync([FromUri]Guid id)
        {
            try
            {
                var result = await ScoringRulesService.GetScoringRulesByIdAsync(id);
                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No book with that ID in database.");
                }

                else
                {
                    ScoringRules scoringRulesById = new ScoringRules(result.ScoringRulesId,
                        result.PositionOne,
                        result.PositionTwo,
                        result.PositionThree,
                        result.PositionFour,
                        result.PositionFive,
                        result.PositionSix,
                        result.PositionSeven,
                        result.PositionEight,
                        result.PositionNine,
                        result.PositionTen,
                        result.CreationDate,
                        result.LastUpdated);
                    return Request.CreateResponse(HttpStatusCode.OK, scoringRulesById);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
            }
        }

        // PUT: api/ScoringRules/5
        [HttpPut]
        [Route("update_scoringRulesById_list")]
        public async Task<HttpResponseMessage> UpdateScoringRulesByIdAsync([FromUri]System.Guid id,[FromBody] ScoringRules rules)
        {
            var result = await ScoringRulesService.UpdateScoringRulesByIdAsync(id, rules);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No one with that ID in database.");
            }

            else 
            {
                var scoringRulesToScoringRulesRest = Mapper.Map<ScoringRules, ScoringRulesRest>(result);
                return Request.CreateResponse(HttpStatusCode.OK, scoringRulesToScoringRulesRest); 
            }
        }        
    }

    public class ScoringRulesRest
    {

        public Guid ScoringRulesId { get; set; }
        public int PositionOne { get; set; }
        public int PositionTwo { get; set; }
        public int PositionThree { get; set; }
        public int PositionFour { get; set; }
        public int PositionFive { get; set; }
        public int PositionSix { get; set; }
        public int PositionSeven { get; set; }
        public int PositionEight { get; set; }
        public int PositionNine { get; set; }
        public int PositionTen { get; set; }

        public ScoringRulesRest()
        {          
        }


    }
}
