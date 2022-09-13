import React, { useState } from 'react';
import FantasyLeagueService from '../../Services/FantasyLeagueService';
import { Link } from "react-router-dom";



const AddFantasyLeague = () => {
  const initialFantasyLeagueState = {
    FantasyLeagueId: null,
    FantasyLeagueName : "",
    Budget : "",
    MaximumTeams : "",
    MaximumFreeSubs : "",
    MaximumDriversPerTeam : "",
  };

  const [fantasyLeague, setFantasyLeague] = useState(initialFantasyLeagueState);
  const [submitted, setSubmitted] = useState(false);

  const handleInputChange = event => {
    setFantasyLeague({...fantasyLeague, [event.target.name]: event.target.value});
  };

  const saveFantasyLeague = () => {
    var data = {
      FantasyLeagueName : fantasyLeague.FantasyLeagueName,
      Budget : fantasyLeague.Budget,
      MaximumTeams : fantasyLeague.MaximumTeams,
      MaximumFreeSubs : fantasyLeague.MaximumFreeSubs,
      MaximumDriversPerTeam : fantasyLeague.MaximumDriversPerTeam,
    };

    FantasyLeagueService.create(data).then(response => {
      setFantasyLeague({
        FantasyLeagueId : response.data.FantasyLeagueId,
        FantasyLeagueName: response.data.FantasyLeagueName,
        Budget: response.data.Budget,
        MaximumTeams : response.data.MaximumTeams,
        MaximumFreeSubs : response.data.MaximumFreeSubs,
        MaximumDriversPerTeam : response.data.MaximumDriversPerTeam,
      });
      setSubmitted(true);
      console.log(response.data);
    })

    .catch(e => {
      console.log(e);
    });
  };

  const newFantasyLeague = () => {
    setFantasyLeague(initialFantasyLeagueState);
    setSubmitted(false);
  };

  return (
    <div className="submit-form">
      { submitted ? (
        <div>
          <h4>You submitted succesfully!</h4>
          <button className="btn btn-success" onClick={newFantasyLeague}>Add</button>
          <button className="btn btn-dark"><Link to="/fantasyLeagues" class="text-decoration-none text-reset">Go back</Link></button>
        </div>
      ) : (
        <div>
          <div className="form-group">
            <label htmlFor="title">Fantasy league name</label>
            <input type="text" className="form-control" id="fantasyLeagueName" required value={fantasyLeague.FantasyLeagueName} onChange={handleInputChange} name="FantasyLeagueName"/>
          </div>
          <div className="form-group">
            <label htmlFor="title">Budget (mil.)</label>
            <input type="number" className="form-control" id="budget" required value={fantasyLeague.Budget} onChange={handleInputChange} name="Budget"/>
          </div>
          <div className="form-group">
            <label htmlFor="title">Maximum teams</label>
            <input type="number" className="form-control" id="maximumTeams" required value={fantasyLeague.MaximumTeams} onChange={handleInputChange} name="MaximumTeams"/>
          </div>
          <div className="form-group">
            <label htmlFor="title">Maximum free subs</label>
            <input type="number" className="form-control" id="maximumFreeSubs" required value={fantasyLeague.MaximumFreeSubs} onChange={handleInputChange} name="MaximumFreeSubs"/>
          </div>
          <div className="form-group">
            <label htmlFor="title">Maximum drivers per team</label>
            <input type="number" className="form-control" id="maximumDriversPerTeam" required value={fantasyLeague.MaximumDriversPerTeam} onChange={handleInputChange} name="MaximumDriversPerTeam"/>
          </div>
          <button onClick={saveFantasyLeague} className="btn btn-success">Submit</button>
        </div>
      )
      }
    </div>
  );
};

export default AddFantasyLeague