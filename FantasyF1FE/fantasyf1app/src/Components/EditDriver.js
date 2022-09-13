import React,{useState,useEffect} from 'react'
import {Link,useLocation} from "react-router-dom";
import Form from './Form.js';
import DriverService from '../Services/DriverService.js';

function EditDriver() {

  const location = useLocation();
  const {driverName} = location.state;
  const {driverSurname} = location.state;
  const {driverConstructor} = location.state;
  const {driverId} = location.state;

  const initialDriver = {
    DriverId: "", 
    DriverName: "", 
    DriverSurname: "", 
    Age: 0, 
    IsTurboDriver: false, 
    Price: 0, 
    TotalPoints: 0, 
    LastUpdated: "", 
    ConstructorId: driverConstructor, 
    ScoringRulesId: ""
  }

  const [constructors,setConstructor] = useState([]);
  const [constructorOption, setConstructorOption] = useState();
  const [driver,setDriver] = useState(initialDriver);
  const [position, setPosition] = useState(20);
  
  

  useEffect(() => {
    fetchConstructors();
    fetchDriver(driverId);
  },[]);

  const handleSelectByOnChange = (event) => {
    const prevState = driver;
    setDriver(prevState => ({
      ...prevState, ConstructorId : event.target.value
    }))
    setConstructorOption(event.target.value);
  }

  const handleOnChangePosition = (event) => {
    console.log("Došlo od promjene pozicije");
    setPosition(Number(event.target.value));
  }

  const handleOnChange = (event) =>{    
    const prevState = driver
    if((event.target.name === "Age")||(event.target.name === "Price")){
      setDriver(prevState => ({
        ...prevState, [event.target.name] : Number(event.target.value), ConstructorId:driverConstructor
      }))
    }
    else{
      setDriver(prevState => ({
        ...prevState, [event.target.name] : event.target.value
      }));
    }
    
  }

  const fetchConstructors = () => {
    DriverService.getAllConstructors().then(response => {
      setConstructor(response.data)
    })
    .catch(e=>{
      console.log(e);
    })
  }

  const fetchDriver = ( (driverId) => {
    DriverService.getDriver(driverId).then(response => {
      setDriver(response.data)
    })
    .catch(e=>{
      console.log(e);
    })
  })

  const updateDriverData = () =>{
    var data = {
      DriverName : driver.DriverName,
      DriverSurname : driver.DriverSurname,
      Age : driver.Age,
      IsTurboDriver : driver.IsTurboDriver,
      Price : driver.Price,
      ConstructorId : driver.ConstructorId,
      ScoringRulesId : driver.ScoringRulesId
    };
    DriverService.updateDriver(driverId,data).then(response => {
      console.log(response.data);
      window.location.reload(false);
    })
    console.log(driver)
    console.log(constructors);
  }

  const updateNewDriverPoints = () => {
    console.log(driverId)
    console.log(position)
    DriverService.updateDriverPoints(driverId,position).then(response => {
      console.log(response.data);
      window.location.reload(false);
    })
    console.log(driver);
  }

  const forms = [
                {
                  "id":1,
                  "type":"text",
                  "fuction":handleOnChange,
                  "labelName":"Name",
                  "name":"DriverName",
                  "value": driver.DriverName
                },
                {
                  "id":2,
                  "type":"text",
                  "fuction":handleOnChange,
                  "labelName":"Surname",
                  "name":"DriverSurname",
                  "value": driver.DriverSurname
                },
                {
                  "id":3,
                  "type":"number",
                  "fuction":handleOnChange,
                  "labelName":"Age",
                  "name":"Age",
                  "value": driver.Age
                },
                {
                  "id":4,
                  "type":"number",
                  "fuction":handleOnChange,
                  "labelName":"Price",
                  "name":"Price",
                  "value": driver.Price
                }
  ]

  const points = [
                  {
                    "id" : 1,
                    "type":"number",
                    "fuction": handleOnChangePosition,
                    "labelName" : "Position",
                    "name" : "Position",
                    "value" : position.value
                  }

  ]

  return (
    <div className="d-flex flex-column">
      <h1 class="p-2">Updating {driverName} {driverSurname}</h1>
      <div class="p-2 justify-content-sm-center">
        <Form list={forms}/>
        <select onChange={handleSelectByOnChange}>
          {constructors.map(constructor =>(
            <option key={constructor.Id} value={constructor.Id}>{constructor.Name}</option>
          ))}
        </select>
        <Link to="/drivers">
          <button onClick={updateDriverData}>Update Driver Info</button>
        </Link>
      </div>
      <div class="p-2 justify-content-center">
        <h2>Update race results:</h2>
        <Form list={points}/>
        <Link to="/drivers">
          <button onClick={updateNewDriverPoints}>Update Driver Points</button>
        </Link>
      </div>
    </div>
  )
}

export default EditDriver;
