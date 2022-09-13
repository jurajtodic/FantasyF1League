import '../../Services/DriverService';
import  Table from 'react-bootstrap/esm/Table.js';
import Button from '../Button';
import '../../Styles/Table.css';
import {Link} from 'react-router-dom';

function DriverTableinTeam(props) {

  return (
    <div id="table-content">
        <Table striped bordered hover variant="dark">
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
                {props.driverList.map( (driver) =>(
                    <tr key={driver.DriverId}>
                        <td>{driver.DriverSurname}</td>
                        <td>{driver.DriverName}</td>
                        <td>{driver.Price}</td>
                        <td>{driver.TotalPoints}</td>
                        <td id="table-data-button">
                            {props.driverButtons.map((button) => (
                                button.hasLink === true ? (<Link key={button.id} to={"/driver/" + driver.DriverId} state={{driverId: driver.DriverId, driverName: driver.DriverName, driverSurname:driver.DriverSurname}}>
                                                                <Button key={button.id} function={button.function} buttonName={button.name} hasALink={button.hasLink}/>
                                                            </Link> ) : 
                                                        (<Button key={button.id} function={()=>button.function(props.fantasyTeamId, driver.DriverId)} buttonName={button.name} 
                                                                 shasALink={button.hasLink}/>)                                
                                                                                              
                            ))}
                        </td>
                    </tr>
                ))}
            </tbody>
        </Table>

        {props.list.map( (button) => (
            <Button key={button.id} function={button.function} buttonName={button.name} />
        ))}
    </div>
 )
}

export default DriverTableinTeam;