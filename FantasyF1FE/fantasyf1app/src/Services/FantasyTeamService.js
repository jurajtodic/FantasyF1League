import http from './http-common.js';

const getAllTeams = () => {
    return http.get(`/get_all_teams`);
  };

const get = id => {
  return http.get(`/api/FantasyTeam/${id}`);
};

const create = data => {
  return http.post("/api/FantasyTeam", data);
};

const update = (id, data) => {
  return http.put(`/api/FantasyTeam/${id}`, data);
};
const remove = id => {
  return http.delete(`/api/FantasyTeam/${id}`);
};

function findByTitle (fantasyLeagueId, searchstring) {
  return http.get(`/get_fantasy_teams_from_league?fantasyLeagueId=${fantasyLeagueId}&searchstring=${searchstring}`);
};

const getAllFantasyTeamsFromLeague = fantasyLeagueId => {
    return http.get(`/get_fantasy_teams_from_league?fantasyLeagueId=${fantasyLeagueId}`);
};

const getDriversFromTeam = fantasyTeamId => {
  return http.get(`/get_drivers_from_team?fantasyTeamId=${fantasyTeamId}`)
};

function removeDriverFromFantasyTeam (fantasyTeamId, driverId) {
  return http.delete(`/remove_driver_from_fantasy_team?fantasyTeamId=${fantasyTeamId}&driverId=${driverId}`)
} 

function addDriverToFantasyTeam (fantasyTeamId, driverId) {
  return http.post(`/add_driver_to_fantasy_team?fantasyTeamId=${fantasyTeamId}&driverId=${driverId}`)
}

const FantasyTeamService = {
    getAllTeams,
    get,
    create,
    update,
    remove,
    findByTitle,
    getAllFantasyTeamsFromLeague,
    getDriversFromTeam,
    removeDriverFromFantasyTeam,
    addDriverToFantasyTeam
};

export default FantasyTeamService;