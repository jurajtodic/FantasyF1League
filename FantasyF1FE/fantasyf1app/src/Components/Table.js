import '../Services/DriverService.js';
import  Table from 'react-bootstrap/esm/Table.js';
import Button from './Button.js';
import '../Styles/Table.css';
import {Link} from 'react-router-dom';
import './Drivers.css';

function TableDriver(props) {

  return (
    <div id="table-content">
        <Table striped bordered hover variant="dark">
        <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Surname</th>
                        <th>Price (mil.)</th>
                        <th>Total Points</th>
                        <th>Age</th>
                        <th>ConstructorId</th>
                        <th>Action</th>
                    </tr>
            </thead>
            <tbody>
            {props.driverList.map( (driver) =>(
                    <tr key={driver.DriverId}>
                        <td>{driver.DriverId}</td>
                        <td>{driver.DriverName}</td>
                        <td>{driver.DriverSurname}</td>
                        <td>{driver.Price}</td>
                        <td>{driver.TotalPoints}</td>
                        <td>{driver.Age}</td>
                        <td>{driver.ConstructorId}</td>
                        <td id="table-data-button">
                            {props.driverButtons.map((button) => (

                                button.hasLink === true ? (<Link key={button.id} to={"/driver/" + driver.DriverId} state={{driverId: driver.DriverId,
                                                                         driverName: driver.DriverName, driverSurname:driver.DriverSurname,driverConstructor: driver.ConstructorId}}>
                                                                <Button key={button.id} function={button.function} buttonName={button.name} hasALink={button.hasLink}/>
                                                            </Link> ) : 
                                                        (<Button key={button.id} function={()=>button.function(driver.DriverId)} buttonName={button.name} 
                                                                 shasALink={button.hasLink}/>)                                
                                                                                              
                            ))}
                        </td>
                    </tr>
            ))}
            </tbody>
        </Table>
        <div>                           
            {props.list.map( (button) => (
                <Button key={button.id} function={button.function} buttonName={button.name}/>
            ))}
        </div>
    </div>
 )
}

export default TableDriver;