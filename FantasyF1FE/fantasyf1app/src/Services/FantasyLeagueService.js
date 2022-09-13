import http from './http-common.js';

const getAll = () => {
  return http.get("/get_all_leagues");
};

const get = id => {
  return http.get(`/api/FantasyLeague/${id}`);
};

const create = data => {
  return http.post("/api/FantasyLeague", data);
};

const update = (id, data) => {
  return http.put(`/api/FantasyLeague/${id}`, data);
};
const remove = id => {
  return http.delete(`/api/FantasyLeague/${id}`);
};

const findByTitle = searchstring => {
  return http.get(`/get_all_leagues?searchstring=${searchstring}`);
};

const addTeamToLeague = (id,data) => {
  return http.post(`/add_fantasy_team_to_league?id=${id}`,data);
}

const FantasyLeagueService = {
  getAll,
  get,
  create,
  update,
  remove,
  findByTitle,
  addTeamToLeague
};

export default FantasyLeagueService;