import React, { useEffect, useState } from 'react'
import FantasyLeagueService from '../../Services/FantasyLeagueService';
import { Link } from "react-router-dom";

const FantasyLeagues = () => {
  const [fantasyLeagues, setFantasyLeagues] = useState([]);
  const [currentFantasyLeague, setCurrentFantasyLeague] = useState(null);
  const [searchstring, setSearchstring] = useState("");

  useEffect(() => {
    retrieveFantasyLeagues();
  }, []);

  const onChangeSearchName = e => {
    const searchstring = e.target.value;
    setSearchstring(searchstring);
  }

  const retrieveFantasyLeagues = () => {
    FantasyLeagueService.getAll().then(response => {
      setFantasyLeagues(response.data);
    })
    .catch(e => {
      console.log(e);
    });
  };

  const setActiveFantasyLeague = (fantasyLeague) => {
    setCurrentFantasyLeague(fantasyLeague);
  }

  const deleteFantasyLeague = () => {
    FantasyLeagueService.remove(currentFantasyLeague.FantasyLeagueId)
      .then(response => {
        console.log(response.data);
        window.location.reload(false);
      })
      .catch(e => {
        console.log(e);
      });
  };

  const findByName = () => {
    FantasyLeagueService.findByTitle(searchstring).then(response => {
      setFantasyLeagues(response.data);
      console.log(response.data);
      setCurrentFantasyLeague();
    })
    .catch(e => {
      console.log(e);
    });
  };

  return (
    <div class="row">

      <div class="col-md-6 mx-4 my-4">

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
                  <th>League Name</th>
                  <th>Budget (mil.)</th>
                  <th>Maximum Teams</th>
                  <th>Maximum Subs</th>
                  <th>Maximum Drivers</th>
              </tr>
            </thead>
            <tbody class="table-group-divider">
            {fantasyLeagues && fantasyLeagues.map((league) => (
              <tr key={league.FantasyLeagueId}>
                <td onClick={() => setActiveFantasyLeague(league)}>{league.FantasyLeagueName}</td>
                <td onClick={() => setActiveFantasyLeague(league)}>{league.Budget}</td>
                <td onClick={() => setActiveFantasyLeague(league)}>{league.MaximumTeams}</td>
                <td onClick={() => setActiveFantasyLeague(league)}>{league.MaximumFreeSubs}</td>
                <td onClick={() => setActiveFantasyLeague(league)}>{league.MaximumDriversPerTeam}</td>
              </tr>
            ))}
            </tbody>
          </table>
        </div>
        <Link to="/newFantasyLeague" class="text-decoration-none text-reset">
          <button type="button" class="btn btn-dark">
              Add new fantasy league
          </button>
        </Link>

      </div>

      <div class="col-md-5 mx-5 my-5">
        {currentFantasyLeague ? (
          <div class="mx-5">
            <h4>{currentFantasyLeague.FantasyLeagueName}</h4>
            <div>
              <label class="fw-bold">Id:</label>{" "}
              {currentFantasyLeague.FantasyLeagueId}
            </div>
            <div>
              <label class="fw-bold">Budget (mil.): </label>{" "}
              {currentFantasyLeague.Budget}
            </div>
            <div>
              <label class="fw-bold">Maximum teams: </label>{" "}
              {currentFantasyLeague.MaximumTeams}
            </div>
            <div>
              <label class="fw-bold">Maximum subs: </label>{" "}
              {currentFantasyLeague.MaximumFreeSubs}
            </div>
            <div>
              <label class="fw-bold">Maximum drivers: </label>{" "}
              {currentFantasyLeague.MaximumDriversPerTeam}
            </div>
            <div>
              <label class="fw-bold">CreationDate:</label>{" "}
              {currentFantasyLeague.CreationDate}
            </div>
            <div class="row my-3 text-center">
              <div class="col-md-2">
              <Link to={"/fantasyLeagues/" + currentFantasyLeague.FantasyLeagueId} state ={{leagueId: currentFantasyLeague.FantasyLeagueId, leagueName: currentFantasyLeague.FantasyLeagueName}} class="text-decoration-none text-reset">
                <button type="button" class="btn btn-success">
                    Join
                </button>
              </Link>
              </div>
              <div class="col-md-2">
                <button type="button" class="btn btn-danger" onClick={deleteFantasyLeague}>
                      Delete
                </button>
              </div>
            </div>
          </div>
        ):(
          <div class="col">
            <br/>
            <p class="fs-4 fw-semibold">Select a fantasy league to join</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default FantasyLeagues