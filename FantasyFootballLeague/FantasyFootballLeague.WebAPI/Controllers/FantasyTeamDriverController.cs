using AutoMapper;
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
    public class FantasyTeamDriverController : ApiController
    {
        protected IMapper Mapper;
        protected IFantasyTeamDriverService Service;
        public FantasyTeamDriverController(IFantasyTeamDriverService service, IMapper mapper)
        {
            this.Service = service;
            this.Mapper = mapper;
        }

        // GET: api/FantasyTeamDriver
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            List<FantasyTeamDriver> fantasyTeamDriverList = new List<FantasyTeamDriver>();

            fantasyTeamDriverList = await Service.GetFantasyTeamDriversAsync();

            if (fantasyTeamDriverList == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No fantasy teams");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, fantasyTeamDriverList);
            }
        }

        // GET: api/FantasyTeamDriver/5

        // POST: api/FantasyTeamDriver
        public async Task<HttpResponseMessage> PostFantasyTeamDriverDataAsync(FantasyTeamDriver fantasyTeamDriver)
        {
            FantasyTeamDriver fTeamDriver = await Service.PostFantasyTeamDriverDataAsync(fantasyTeamDriver);

            if (fTeamDriver == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No driver in fantasy team ");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, fTeamDriver);
            }
        }

        // DELETE: api/FantasyTeamDriver/5
        public async Task<HttpResponseMessage> DeleteFantasyTeamDriverDataAsync(Guid id)
        {
            bool result = await Service.DeleteFantasyTeamDriverDataAsync(id);

            if (result == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Driver id not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        [Route("Get_number_of_drivers_in_team")]
        public async Task<HttpResponseMessage> GetNumberOfDriversInTeam(Guid fantasyTeamId)
        {
            int result = await Service.GetNumberOfDriversInTeam(fantasyTeamId);
            if (result == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No drivers in this fantasy team ");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }

    }
}
