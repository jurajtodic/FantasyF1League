using AutoMapper;
using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Service;
using FantasyFootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static FantasyFootballLeague.WebAPI.Controllers.FantasyTeamController;

namespace FantasyFootballLeague.WebAPI.Controllers
{
    public class FantasyLeagueController : ApiController
    {
        protected IMapper Mapper;
        protected IFantasyLeagueService Service;
        public FantasyLeagueController(IFantasyLeagueService service, IMapper mapper)
        {
            this.Service = service;
            this.Mapper = mapper;
        }

        // GET: /get_all_leagues
        [HttpGet]
        [Route("get_all_leagues")]
        public async Task<HttpResponseMessage> GetAllAsync(string searchstring = null, double? lowerbudget=null, double? higherbudget = null,
            int? lowerfreesubs=null, int? higherfreesubs=null, string orderby= "FantasyLeagueName", string sortorder="ASC", int rpp=10, int pagenumber=1)
        {
            FantasyLeagueFilter filter = new FantasyLeagueFilter(searchstring, lowerbudget, higherbudget, lowerfreesubs, higherfreesubs);
            FantasyLeagueSorting sorting = new FantasyLeagueSorting(orderby, sortorder);
            FantasyLeaguePaging paging = new FantasyLeaguePaging(rpp, pagenumber);

            List<FantasyLeague> fantasyLeagues = new List<FantasyLeague>();
            fantasyLeagues = await Service.GetFantasyLeagueDataAsync(filter, paging, sorting);

            if (fantasyLeagues == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Database is empty.");
            }
            else
            {
                var fantasyLeagueRest = Mapper.Map<List<FantasyLeague>, List<FantasyLeagueRest>>(fantasyLeagues);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyLeagueRest);
            }
        }
        
        // GET: api/FantasyLeague/5
        public async Task<HttpResponseMessage> GetByIdAsync(System.Guid id)
        {
            FantasyLeague fantasyLeague = await Service.GetFantasyLeagueDataByIdAsync(id);

            if (fantasyLeague == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Fantasy league not found");
            }
            else
            {
                var fantasyLeagueRest = Mapper.Map<FantasyLeague, FantasyLeagueRest>(fantasyLeague);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyLeagueRest);
            }
        }

        // POST: api/FantasyLeague
        public async Task<HttpResponseMessage> PostFantasyLeagueAsync(FantasyLeague fLeague)    // ako ne radi post -> zbog renaminga
        {
            FantasyLeague fantasyLeague = await Service.PostFantasyLeagueDataAsync(fLeague);

            if (fantasyLeague == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Fantasy league");
            }
            else
            {
                var fantasyLeagueRest = Mapper.Map<FantasyLeague, FantasyLeagueRest>(fantasyLeague);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyLeagueRest);
            }
        }

        // DELETE: api/FantasyLeague/5
        public async Task<HttpResponseMessage> DeleteFantasyLeagueDataAsync(Guid id)
        {
            bool result = await Service.DeleteFantasyLeagueDataAsync(id);

            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Fantasy League id not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }

        [HttpGet]
        [Route("get_fantasy_teams_from_league")]
        public async Task<HttpResponseMessage> GetFantasyTeamsFromLeague(Guid fantasyLeagueId, string searchstring = null, int? lowerTotalPoints = null, int? higherTotalPoints = null, string orderBy = "TotalPoints", string sortOrder = "DESC", int rpp = 10, int pageNumber = 1)
        {
            FantasyTeamFilter filter = new FantasyTeamFilter(searchstring, lowerTotalPoints, higherTotalPoints);
            FantasyTeamSorting sorting = new FantasyTeamSorting(orderBy, sortOrder);
            FantasyTeamPaging paging = new FantasyTeamPaging(rpp, pageNumber);

            List<FantasyTeam> fantasyTeams = await Service.GetFantasyTeamsFromLeague(fantasyLeagueId, filter, paging, sorting);

            if (fantasyTeams == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No fantasy teams found in given league ");
            }
            else
            {
                var fantasyTeamRest = Mapper.Map<List<FantasyTeam>, List<FantasyTeamRest>>(fantasyTeams);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyTeamRest);
            }
        }

        [HttpPost]
        [Route("add_fantasy_team_to_league")]
        public async Task<HttpResponseMessage> AddFantasyTeamToLeague(Guid id,[FromBody] FantasyTeamRest team)
        {

            var restTeamToTeam = Mapper.Map<FantasyTeamRest, FantasyTeam>(team);

            FantasyTeam fantasyTeam = await Service.AddFantasyTeamToLeague(id, restTeamToTeam);

            if (fantasyTeam == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Couldn't create fantasy team in fantasy league");
            }
            else
            {              
                return Request.CreateResponse(HttpStatusCode.OK, fantasyTeam);
            }
        }

        public class FantasyLeagueRest
        {
            public System.Guid FantasyLeagueId { get; set; }
            public string FantasyLeagueName { get; set; }
            public double Budget { get; set; }
            public int MaximumTeams { get; set; }
            public int MaximumFreeSubs { get; set; }
            public int MaximumDriversPerTeam { get; set; }
            public DateTime CreationDate { get; set; }

            public FantasyLeagueRest() { }

        }

    }
}
