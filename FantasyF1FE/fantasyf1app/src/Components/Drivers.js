import Table from './Table.js';
import React,{useState,useEffect} from 'react';
import DriverService from '../Services/DriverService.js';
import SearchBy from './SearchBy.js';
import SelectBy from './SelectBy.js';
import './Drivers.css';
import Button from './Button.js';
import {Link} from 'react-router-dom';

function Drivers() {
  const [drivers,setDrivers] = useState([]);
  const [filter,setFilter] = useState({ page:{name:"pageNumber", value:1},
                                        totalPriceIsHigher : {name:"totalPriceIsHigher", value:0},
                                        orderBy : {name:"orderBy", value:"Price"}
                                      });
  const handleNextButton = () => {
    if(Object.keys(filter.page)===0){
      setFilter({...filter, page : {name:"pageNumber",pageNumber:1}});
    }
    else if(drivers.length<10){
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

  const handleDriverEditButton = () =>{    
    console.log("Edit button pressed");
  }

  const handleDriverDeleteButton = (id) =>{
    DriverService.deleteDriver(id).then(response =>{
      window.location.reload(false);
    })
    .catch(e => {
      console.log(e);
    })
    console.log("Delete button pressed");
  }

  const handleGoToAddDriverForm = () => {

  }


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

  const driverEditDeleteButton = [{
                              "id" : 1,
                              "name" : "Edit",
                              "function" : handleDriverEditButton,
                              "hasLink" : true,
                              "link" : "/editDriver"  
                            },
                            {
                              "id" : 2,
                              "name" : "Delete",
                              "function" : handleDriverDeleteButton,
                              "hasLink" : false,
                              "link" : ""
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
                              }
                            ]                          

  useEffect(() => {
    fetchDrivers();
  },[filter.page,filter.totalPriceIsHigher,filter.orderBy]);
 
  const fetchDrivers = () => {
    DriverService.findDriverByFilter(filter).then(response => {
      setDrivers(response.data)
    })
    .catch(e =>{
      console.log(e);
    })
  }


    return (
      <div className="Drivers" id="driver-page">
        <h1>Drivers</h1>
        <Table driverList={drivers} list={driverListBackNextButton} driverButtons={driverEditDeleteButton}/>
        <div class="mt-2">
          <Link to="/newDriverForm">
            <Button key="1" function={handleGoToAddDriverForm} buttonName="Add new driver"/>
          </Link>
        </div>
        <div class="mt-2">
          <SearchBy text="the higher price" type="number" function={handleOnChange} value={filter.totalPriceIsHigher.value} name={filter.totalPriceIsHigher.name}/>
        </div>
        <div class="mt-2">
          <SelectBy list={selectByOptionList} function={handleSelectByOnChange}/>
        </div>
      </div>
    );
  }
  
  export default Drivers;