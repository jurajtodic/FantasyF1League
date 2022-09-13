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
    public class TeamController : ApiController
    {
        protected ITeamService TeamService;
        protected IMapper mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            this.TeamService = teamService;
        }

        [HttpGet]
        [Route("team_list")] 
        public async Task<HttpResponseMessage> GetAllTeamsAsync()
        {

            List<Team> teams = new List<Team>();

            try
            {                
                teams = await TeamService.GetAllTeamsAsync();
                if (teams == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Database is empty.");
                }
                else
                {
                    /*foreach (Team team in teams.ToList())
                    {
                        teams.Add(new Team(team.Id, team.Name));
                    }*/
                    return Request.CreateResponse(HttpStatusCode.OK, teams);
                }
            }

            catch (SqlException e)
            {
                throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
            }


        }

        [HttpGet]
        [Route("team_by_id")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Guid id)
        {          
            try
            {
                var result = await TeamService.GetAsync(id);
                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No book with that ID in database.");
                }

                else
                {
                    Team teamById = new Team(result.Id, result.Name);
                    return Request.CreateResponse(HttpStatusCode.OK, teamById);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sql error {0}", e.Message));
            }
        }

       
    }
}
