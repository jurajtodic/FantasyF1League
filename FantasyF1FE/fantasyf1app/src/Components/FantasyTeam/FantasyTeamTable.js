import React, { useState, useEffect} from 'react'
import FantasyTeamService from '../../Services/FantasyTeamService';
import { Link, useLocation } from "react-router-dom";


const FantasyTeamTable = () => {
    const [fantasyTeams, setFantasyTeams] = useState([]);
    const [searchstring, setSearchstring] = useState("");

    const location = useLocation();
    const {leagueId}  = location.state;
    const {leagueName} = location.state;
    
    useEffect(() => {
        retrieveFantasyTeams(leagueId)
    }, []);

    const onChangeSearchName = (e) => {
        setSearchstring(e.target.value);
    }

    const retrieveFantasyTeams = (fantasyLeagueId) => {
        FantasyTeamService.getAllFantasyTeamsFromLeague(fantasyLeagueId).then(response => {
            setFantasyTeams(response.data);
            console.log(response.data);
        })
        .catch((e) => {
            console.log(e);
        });
    };

    const deleteFantasyTeam = (fantasyTeamId) => {
        FantasyTeamService.remove(fantasyTeamId).then(response => {
            window.location.reload(false);
        })
        .catch(e => {
            console.log(e);
        });
    };

    const findByName = () =>{
        FantasyTeamService.findByTitle(searchstring).then((response) => {
            setFantasyTeams(response.data);
        })
        .catch((e) => {
            console.log(e);
        })
    }

    return (
        <div class="mx-4 my-4">
            <div>
                <div class="input-group mb-3">
                    <input type="text" class="form-control" placeholder="Search by name" value={searchstring} onChange={onChangeSearchName}/>
                    <div class="input-group-append">
                    <button class="btn btn-outline-secondary" type="button" onClick={findByName}>Search</button>
                    </div>
                </div>
            </div>
            <div class="my-4">
                <table class="col-md-10 table table-dark table-hover">
                    <thead>
                        <tr>
                            <th>Team name</th>
                            <th>Username</th>
                            <th>Total points</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody class="table-group-divider">
                        {fantasyTeams && fantasyTeams.map((team) => (
                        <tr key={team.FantasyTeamId}>
                            <td>{team.FantasyTeamName}</td>
                            <td>{team.Username}</td>
                            <td>{team.TotalPoints}</td>
                            <td>
                                <div class="row">                                
                                    <div class="col-md-1 mx-1">
                                        <Link to={"/fantasyTeam/" + team.FantasyTeamId} 
                                        state={{teamId:team.FantasyTeamId}}
                                        class="text-decoration-none text-reset">
                                            <button type="button" class="btn btn-success">
                                                    Edit
                                            </button>
                                        </Link>
                                    </div>
                                    <div class="col-md-1 mx-1">
                                        <button type="button" class="btn btn-danger" onClick={()=>deleteFantasyTeam(team.FantasyTeamId)}>
                                            Delete
                                        </button>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <Link to="/newFantasyTeam" class="text-decoration-none text-reset" state = {{leagueId : leagueId,leagueName:leagueName}}>
                <button type="button" class="btn btn-dark">
                    Add new team
                </button>
            </Link>
        </div>
    )
};

export default FantasyTeamTable