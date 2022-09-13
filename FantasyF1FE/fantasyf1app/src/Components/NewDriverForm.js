import React,{useState,useEffect} from 'react';
import Button from './Button.js';
import { Link} from "react-router-dom";
import DriverService from '../Services/DriverService.js';
import Form from './Form.js';

function NewDriverForm() {

    const initialDriver = {
                            DriverName : "",
                            DriverSurname : "",
                            Age : 0,
                            IsTurboDriver : false,
                            Price : 0,
                            ConstructorId :"",
                            ScoringRulesId : "",
                            }
    const [driver,setDriver] = useState(initialDriver);
    const [constructors,setConstructors] = useState([]);
    const [scoringRules, setScoringRules] = useState([]);

    useEffect(() => {
        fetchConstructors();
        fetchScoringRules();
    },[]);

    const fetchConstructors = () => {
        DriverService.getAllConstructors().then(response => {
            setConstructors(response.data)
        })
        .catch(e=>{
          console.log(e);
        })
    }
    
    const fetchScoringRules = () => {
        DriverService.getAllScoringRules().then(response => {
            setScoringRules(response.data)
        })
        .catch(e=>{
            console.log(e)
        })
    }

    const handleSelectByOnChange = (event) => {
        setDriver({...driver, [event.target.name] : event.target.value });
        console.log(driver);
    }

    const handleOnChange = (event) =>{    
        const prevState = driver
        if((event.target.name === "Age")||(event.target.name === "Price")){
          setDriver(prevState => ({
            ...prevState, [event.target.name] : Number(event.target.value)
          }))
        }
        else{
          setDriver(prevState => ({
            ...prevState, [event.target.name] : event.target.value
          }));
        }
        
    }

    const driverForm = [
        {
          "id" : 1,
          "type":"text",
          "fuction": handleOnChange,
          "labelName" : "Driver Name: ",
          "name" : "DriverName",
          "value" : driver.DriverName
        },
        {
          "id" : 2,
          "type" : "text",
          "fuction" : handleOnChange,
          "labelName" : "Driver Surname: ",
          "name" : "DriverSurname",
          "value" : driver.DriverSurname
        },
        {
            "id" : 3,
          "type" : "number",
          "fuction" : handleOnChange,
          "labelName" : "Age: ",
          "name" : "Age",
          "value" : driver.Age
        },
        {
            "id" : 4,
          "type" : "number",
          "fuction" : handleOnChange,
          "labelName" : "Price: ",
          "name" : "Price",
          "value" : driver.Price
        }
        
    ]

    const handleAddDriver = () => {
        const data = {
            "DriverName" : driver.DriverName,
            "DriverSurname" : driver.DriverSurname,
            "Age" : driver.Age,
            "Price" : driver.Price,
            "ConstructorId" : driver.ConstructorId,
            "ScoringRulesId" : driver.ScoringRulesId
        }
        DriverService.createDriver(data).then(response => {
            window.location.reload(false);
        });
    }

  return (
    <div>
        <h2>ADD A NEW DRIVER</h2>
        <Form list={driverForm}/>
        <div>
            <label>Select constructor:</label>
            <select onChange={handleSelectByOnChange} name="ConstructorId">
                {constructors.map((constructor)=>(
                    <option key={constructor.Id} value={constructor.Id}>{constructor.Name}</option>
                ))}
            </select>
        </div>
        <div>
            <label>Select scoring rules:</label>
            <select onChange={handleSelectByOnChange} name="ScoringRulesId">
                {scoringRules.map((rule)=>(
                    <option key={rule.ScoringRulesId} value={rule.ScoringRulesId}>STANDARD RULE</option>
                ))}
            </select>
        </div>
        <Link to={"/drivers"}> 
          <Button buttonName="Add driver" function={handleAddDriver}/>
        </Link>
    </div>
  )
}

export default NewDriverForm;