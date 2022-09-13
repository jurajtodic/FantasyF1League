import React,{useState,useEffect} from 'react'
import Form from '../Form.js';
import Button from '../Button.js';
import FantasyLeagueService from '../../Services/FantasyLeagueService.js';
import { Link, useLocation } from "react-router-dom";

function NewFantasyTeam() {

  const initialValue = {
                          "FantasyTeamName" : "",
                           "Username" : "",
                        }

  const [team,setTeam] = useState(initialValue);
  const location = useLocation();
  const {leagueId}  = location.state;
  const {leagueName} = location.state;

  const handleOnChange = (event) => {
    const prevState = team;
    setTeam(prevState => ({
      ...prevState, [event.target.name] : event.target.value
    }))
    console.log(team);
  }

  const teamForm = [
    {
      "id" : 1,
      "type":"text",
      "fuction": handleOnChange,
      "labelName" : "Fantasy Team Name: ",
      "name" : "FantasyTeamName",
      "value" : team.FantasyTeamName
    },
    {
      "id" : 2,
      "type" : "text",
      "fuction" : handleOnChange,
      "labelName" : "Username: ",
      "name" : "Username",
      "value" : team.Username
    }
  ]

  const handleAddTeam = () => {
    const data = {
      FantasyTeamName : team.FantasyTeamName,
      Username : team.Username
    }
    FantasyLeagueService.addTeamToLeague(leagueId,data).then(response => {
      window.location.reload(false);
    })
    console.log("Button pressed")
    console.log(leagueId + data);
  }

  return (
    <div>
        <h2>
        ADD A TEAM IN {leagueName} LEAGUE
        </h2>
        <Form list={teamForm}/>
        <Link to={"/fantasyLeagues/" + leagueId} state={{leagueId:leagueId}}> 
          <Button buttonName="Add a team" function={handleAddTeam}/>
        </Link>
    </div>
  )
}

export default NewFantasyTeam