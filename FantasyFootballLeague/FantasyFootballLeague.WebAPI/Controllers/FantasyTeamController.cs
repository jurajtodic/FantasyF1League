using AutoMapper;
using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FantasyFootballLeague.WebAPI.Controllers
{
    public class FantasyTeamController : ApiController
    {

        protected IMapper Mapper;
        protected IFantasyTeamService Service;
        protected IFantasyLeagueService FantasyLeagueService;
        public FantasyTeamController(IFantasyTeamService service, IMapper mapper, IFantasyLeagueService fantasyLeagueService)
        {
            this.Service = service;
            this.Mapper = mapper;
            this.FantasyLeagueService = fantasyLeagueService;
        }

        // GET: /get_all_teams
        [HttpGet]
        [Route("get_all_teams")]
        public async Task<HttpResponseMessage> GetAllAsync(string searchstring = null, int? lowerTotalPoints=null, int? higherTotalPoints=null, string orderBy= "FantasyTeamName", string sortOrder="ASC", int rpp=10, int pageNumber=1)
        {
            FantasyTeamFilter filter = new FantasyTeamFilter(searchstring, lowerTotalPoints, higherTotalPoints);
            FantasyTeamSorting sorting = new FantasyTeamSorting(orderBy, sortOrder);
            FantasyTeamPaging paging = new FantasyTeamPaging(rpp, pageNumber);

            List<FantasyTeam> fantasyTeams = new List<FantasyTeam>();
            fantasyTeams = await Service.GetFantasyTeamDataAsync(filter, paging, sorting);

            if (fantasyTeams == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No fantasy teams");
            }
            else
            {
                var fantasyTeamRest = Mapper.Map<List<FantasyTeam>, List<FantasyTeamRest>>(fantasyTeams);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyTeamRest);
            }

        }

        // GET: api/FantasyTeam/5
        public async Task<HttpResponseMessage> GetByIdAsync(System.Guid id)
        {
            FantasyTeam fTeam = await Service.GetFantasyTeamDataByIdAsync(id);

            if (fTeam == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Fantasy team not found");
            }
            else
            {
                var fantasyTeamRest = Mapper.Map<FantasyTeam, FantasyTeamRest>(fTeam);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyTeamRest);
            }
        }
        
        // POST: api/FantasyTeam
        public async Task<HttpResponseMessage> PostFantasyTeamDataAsync(FantasyTeam fantasyTeam)
        {
            FantasyTeam fTeam = await Service.PostFantasyTeamDataAsync(fantasyTeam);

            if (fantasyTeam == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Fantasy team");
            }
            else
            {
                var fantasyTeamRest = Mapper.Map<FantasyTeam, FantasyTeamRest>(fTeam);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyTeamRest);
            }
        }


        // PUT: api/FantasyTeam/5
        public async Task<HttpResponseMessage> PutFantasyTeamDataAsync(Guid id, FantasyTeam fTeam)
        {
            FantasyTeam fantasyTeam = await Service.PutFantasyTeamDataAsync(id, fTeam);

            if (fantasyTeam == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ID not found");
            }
            else
            {
                var fantasyTeamRest = Mapper.Map<FantasyTeam, FantasyTeamRest>(fantasyTeam);
                return Request.CreateResponse(HttpStatusCode.OK, fantasyTeamRest);
            }
        }

        // DELETE: api/FantasyTeam/5
        public async Task<HttpResponseMessage> DeleteFantasyTeamDataAsync(Guid id)
        {
            bool result = await Service.DeleteFantasyTeamDataAsync(id);

            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Fantasy team id not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }

        [HttpGet]
        [Route("get_drivers_from_team")]
        public async Task<HttpResponseMessage> GetDriversFromFantasyTeam(Guid fantasyTeamId)
        {
            List<Driver> drivers = await Service.GetDriversFromFantasyTeam(fantasyTeamId);

            if (drivers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No drivers in selected team");
            }
            else
            {
                var driversRest = Mapper.Map<List<Driver>, List<DriverRest>>(drivers);
                return Request.CreateResponse(HttpStatusCode.OK, driversRest);
            }
        }

        [HttpPost]
        [Route("add_driver_to_fantasy_team")]
        public async Task<HttpResponseMessage> AddDriverToFantasyTeam(Guid fantasyTeamId, Guid driverId)
        {
            FantasyTeamDriver newFantasyTeamDriver = await Service.AddDriverToFantasyTeam(fantasyTeamId, driverId);
            if (newFantasyTeamDriver == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cannot add driver to fantasy team");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, newFantasyTeamDriver);
            }
        }

        [HttpDelete]
        [Route("remove_driver_from_fantasy_team")]
        public async Task<HttpResponseMessage> RemoveDriverFromFantasyTeam(Guid fantasyTeamId, Guid driverId)
        {
            FantasyTeam newFantasyTeam = await Service.RemoveDriverFromFantasyTeam(fantasyTeamId, driverId);
            if (newFantasyTeam == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cannot remove driver from fantasy team");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, newFantasyTeam);
            }
        }

        public class FantasyTeamRest
        {
            public Guid FantasyTeamId { get; set; }
            public string FantasyTeamName { get; set; }
            public string Username { get; set; }
            public int TotalPoints { get; set; }
            public int FreeSubsLeft { get; set; }
            public double RemainingBudget { get; set; }
            public Guid FantasyLeagueId { get; set; }

        public FantasyTeamRest() { }
        }
    }
}
