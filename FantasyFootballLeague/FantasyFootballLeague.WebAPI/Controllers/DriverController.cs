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
    public class DriverController : ApiController
    {
        protected IDriverService DriverService;
        protected IMapper Mapper;

        public DriverController(IDriverService driverService, IMapper mapper)
        {
            this.DriverService = driverService;
            this.Mapper = mapper;
        }

        // GET: api/Driver
        #region getAllDriversWithFilter
        [HttpGet]
        [Route("driver_filter_list")]
        public async Task<HttpResponseMessage> GetAllDriversAsync([FromUri]int pageNumber = 1,
                                                                           int itemsByPage = 10,
                                                                           string orderBy = "Price",
                                                                           string sortBy = "DESC",
                                                                           int? totalPoint = null,
                                                                           int? totalPointsIsHigher = null,
                                                                           int? totalPointsIsLower = null,
                                                                           double? totalPrice = null, 
                                                                           double? totalPriceIsHigher = null,
                                                                           double? totalPriceIsLower= null)
        {

            Paging page = new Paging(pageNumber, itemsByPage);
            Sorting sort = new Sorting(orderBy, sortBy);
            DriverFilter filter = new DriverFilter(totalPoint, totalPointsIsHigher, totalPointsIsLower, totalPrice,
                                            totalPriceIsHigher, totalPriceIsLower);

            try
            {
                List<Driver> driverList = new List<Driver>();
                driverList = await DriverService.GetAllDriversAsync(page, sort, filter);
                //return Request.CreateResponse(HttpStatusCode.OK, scoringRulesList);                
                if (driverList == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Database is empty.");
                }
                else
                {
                    var listToRest = Mapper.Map<List<Driver>, List<Driver>>(driverList);
                    return Request.CreateResponse(HttpStatusCode.OK, listToRest);
                }
            }

            catch (Exception e)
            {
                throw new Exception(string.Format("Sql error {0}", e.Message));
            }
        }
        #endregion datenegledam
        // GET: api/Driver/5
        [HttpGet]
        [Route("driver_By_Id_list")]
        public async Task<HttpResponseMessage> GetDriverById(Guid id)
        {
            try
            {
                Driver driverById = new Driver();
                driverById = await DriverService.GetDriverById(id);
                //return Request.CreateResponse(HttpStatusCode.OK, scoringRulesList);                
                if (driverById == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Database is empty.");
                }
                else
                {
                    var driverIdToRest = Mapper.Map<Driver, DriverRest>(driverById);
                    return Request.CreateResponse(HttpStatusCode.OK, driverIdToRest);
                }
            }

            catch (Exception e)
            {
                throw new Exception(string.Format("Sql error {0}", e.Message));
            }

        }

        // POST: api/Driver
        [HttpPost]
        [Route("add_driver")]
        public async Task<HttpResponseMessage> Post([FromBody]DriverRest driver)
        {

            /*DriverRest newDriver = new DriverRest();
            newDriver.DriverName = driver.DriverName;
            newDriver.DriverSurname = driver.DriverSurname;
            newDriver.Age = driver.Age;
            newDriver.IsTurboDriver = driver.IsTurboDriver;
            newDriver.Price = driver.Price;
            newDriver.ConstructorId = driver.ConstructorId;
            newDriver.ScoringRulesId = driver.ScoringRulesId;*/

            var restDriverToDriver = Mapper.Map<DriverRest, Driver>(driver);

            try
            {
                var result = await DriverService.AddDriver(restDriverToDriver);
                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Couldn't put new author in database.");
                }

                else return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sql error {0}", e.Message));
            }
            
        }

        // PUT: api/Driver/5
        [HttpPut]
        [Route("update_driver")]
        public async Task<HttpResponseMessage> Put([FromUri]Guid id, [FromBody]DriverRest driver)
        {
            try
            {
                var fromDriverRestToDriver = Mapper.Map<DriverRest, Driver>(driver);
                var result = await DriverService.UpdateDriver(id, fromDriverRestToDriver);
                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No one with that ID in database.");
                }

                else return Request.CreateResponse(HttpStatusCode.OK, "Driver updated.");
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sql error {0}", e.Message));
            }

        }

        // DELETE: api/Driver/5
        [HttpDelete]
        [Route("delete_driver")]
        public async Task<HttpResponseMessage> DeleteDriver([FromUri]Guid id)
        {
            try
            {
                var result = await DriverService.DeleteDriver(id);
                if (result == false)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No one with that ID in database.");
                }

                else return Request.CreateResponse(HttpStatusCode.OK, "Driver deleted.");
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sql error {0}", e.Message));
            }
        }

        [HttpPut]
        [Route("add_total_points_by_driver_id")]
        public async Task<HttpResponseMessage> UpdateDriverTotalPoints([FromUri]Guid id, int position)
        {
            try
            {
                var result = await DriverService.UpdateDriverTotalPoints(id, position);
                if (result == false)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Couldn't update total score.");
                }

                else return Request.CreateResponse(HttpStatusCode.OK, "Driver total score updated.");
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sql error {0}", e.Message));
            }
        }
    }

    public class DriverRest
    {
        public Guid DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
        public int Age { get; set; }
        public bool IsTurboDriver { get; set; }
        public double Price { get; set; }
        public int TotalPoints { get; set; }
        public Guid ConstructorId { get; set; }
        public Guid ScoringRulesId { get; set; }

        public DriverRest() { }

    }
}
