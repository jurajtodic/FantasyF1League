import React, { useEffect , useState} from 'react'
import { useLocation } from 'react-router-dom';
import FantasyTeamService from '../../Services/FantasyTeamService';
import DriverTableinTeam from '../Drivers/DriverTableinTeam';
import DriverService from '../../Services/DriverService';
import SearchBy from '../SearchBy';
import SelectBy from '../SelectBy';


function FantasyTeamDetails() {
  const [drivers, setDrivers] = useState([]);
  const [fantasyTeam, setFantasyTeam] = useState([]);
  const [allDrivers,setAllDrivers] = useState([]);
  const [filter,setFilter] = useState({ page:{name:"pageNumber", value:1},
                                        totalPriceIsHigher : {name:"totalPriceIsHigher", value:0},
                                        orderBy : {name:"orderBy", value:"Price"}
                                      });
  
  const location = useLocation();
  const {teamId} = location.state;

  useEffect(() => {
    retrieveDriversFromTeam(teamId);
    getTeamData(teamId);
    fetchAllDrivers();
  }, [filter.page,filter.totalPriceIsHigher,filter.orderBy]);

  const fetchAllDrivers = () => {
    DriverService.findDriverByFilter(filter).then(response => {
      setAllDrivers(response.data)
    })
    .catch(e =>{
      console.log(e);
    })
  }

  const handleNextButton = () => {
    if(Object.keys(filter.page)===0){
      setFilter({...filter, page : {name:"pageNumber",pageNumber:1}});
    }
    else if(allDrivers.length<10){
      console.log("No more items in database");
    }
    else {
      setFilter((prevState) => {return {...prevState, page : {...prevState.page, value:prevState.page.value+1}}});
    }   
  };

  const handleBackButton = () => {  
    if(filter.page.value <= 1){
      console.log("First page, can't go back.");
    }
    else{
      const prevState = filter
    var pageNmbr = prevState.page.value - 1;
    setFilter(prevState => ({...prevState, page : {name:"pageNumber",value:pageNmbr}}));
    }
  };

  const handleOnChange = (event) => {
    const prevState = filter;
    if(filter.page.value!==1){      
      setFilter(prevState => ({...prevState, page : {...prevState.page,value:1}}));
    }
    else{
      setFilter({
        ...filter,
        [event.target.name] : {
          ...filter[event.target.name],
          value:Number(event.target.value),
        },
      });
    }
  };

  const handleSelectByOnChange = (event) => {
    const prevState = filter;
    setFilter(prevState => ({
      ...prevState, orderBy : {...prevState.orderBy, value:event.target.value}
    }))
    console.log(filter);
  }

  function addDriverToFantasyTeam(fantasyTeamId, driverId) {
    FantasyTeamService.addDriverToFantasyTeam(fantasyTeamId, driverId).then(response => {
      console.log("hello from add driver to fantasy team");
      window.location.reload(false);     
      //setDrivers([...drivers, response.data])
      //setDrivers(response.data)
    })
    .catch((e) => {
      console.log(e);
    });
  };

  const driverListBackNextButton = [{
    "id" : 1,
    "name" : "Back",
    "function" : handleBackButton
  },
  {
    "id": 2,
    "name" : "Next",
    "function" : handleNextButton
  }]

  const driverAddButton = [{
    "id" : 1,
    "name" : "Add to team",
    "function" : addDriverToFantasyTeam,
    "hasLink" : false
    //"link" : "/editDriver" 
  }]

  const selectByOptionList = [{
    "id" : 1,
    "name" : "orderBy",
    "value" : "Price",
    "text" : "Price"
  },
  {
    "id" : 2,
    "name" : "orderBy",
    "value" : "TotalPoints",
    "text" : "Total Points"
  },
  {
    "id" : 3,
    "name" : "orderBy",
    "value" : "DriverSurname",
    "text" : "Surname"
  }]  


  const retrieveDriversFromTeam = (fantasyTeamId) => {
    FantasyTeamService.getDriversFromTeam(fantasyTeamId).then(response => {
      setDrivers(response.data);
      console.log(response.data);
    })
    .catch((e) => {
      console.log(e);
    });
  };

  function deleteDriverFromTeam (fantasyTeamId, driverId) {
    FantasyTeamService.removeDriverFromFantasyTeam(fantasyTeamId, driverId).then(response => {
      window.location.reload(false);
    })
    .catch(e => {
      console.log(e);
    });
  }

  function getTeamData(fantasyTeamId) {
    FantasyTeamService.get(fantasyTeamId).then(response => {
      setFantasyTeam(response.data);
      console.log("hello from get team data");
      console.log(response.data);
    })
    .catch((e) => {
      console.log(e);
    });
  };

  return (
    <div>  
      <div class="row mx-3 mt-3">

        <div class="col-md-5 mx-4 mt-4">
          <h1>{fantasyTeam.FantasyTeamName}</h1>
          <div>
            <div>
              <label class="fw-bold">Total points:</label>{" "}
              {fantasyTeam.TotalPoints}
            </div>
            <div>
              <label class="fw-bold">Subs left:</label>{" "}
              {fantasyTeam.FreeSubsLeft}
            </div>
            <div>
              <label class="fw-bold">Remaining budget (mil.):</label>{" "}
              {Math.round((fantasyTeam.RemainingBudget + Number.EPSILON) * 10000) / 10000}
            </div>
          </div>
        </div>

        <div class="col-md-6 mx-5 my-2)">
          <div class="row">
            <div class="col-md-6 mt-5 pt-5">
              <SearchBy text="the higher price" type="number" function={handleOnChange} value={filter.totalPriceIsHigher.value} name={filter.totalPriceIsHigher.name}/>
            </div>
            <div class="col-md-6 mt-5 pt-5">
              <SelectBy list={selectByOptionList} function={handleSelectByOnChange}/>
            </div>
          </div>
        </div>
      </div>
        
      <div class="row">

        <div class="col-md-5 mx-4 py-5">
          <table class="table table-dark table-hover">
            <thead>
              <tr>
                  <th>Surname</th>
                  <th>Name</th>
                  <th>Price (mil.)</th>
                  <th>Points</th>
                  <th>Action</th>
              </tr>
            </thead>
            <tbody class="table-group-divider">
            {drivers && drivers.map((driver) => (
              <tr key={driver.DriverId}>
                <td>{driver.DriverSurname}</td>
                <td>{driver.DriverName}</td>
                <td>{driver.Price}</td>
                <td>{driver.TotalPoints}</td>
                <td>
                  <button type="button" class="btn btn-danger" onClick={()=>deleteDriverFromTeam(teamId, driver.DriverId)}>
                    Delete
                  </button>
                </td>
              </tr>
            ))}
            </tbody>
          </table>
        </div>
        
        <div class="col-md-6 mx-4">
          <DriverTableinTeam driverList={allDrivers} list={driverListBackNextButton} driverButtons = {driverAddButton} fantasyTeamId = {teamId} />
        </div>
      </div>

    </div>
  )
}

export default FantasyTeamDetails