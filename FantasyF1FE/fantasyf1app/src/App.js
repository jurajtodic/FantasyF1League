import NavBar from './Components/NavBar.js';
import Info from './Components/Info.js';
import 'bootstrap/dist/css/bootstrap.min.css';
import "@fortawesome/fontawesome-free/css/all.css";
import "@fortawesome/fontawesome-free/js/all.js";
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import Drivers from './Components/Drivers.js';
import FantasyLeagues from './Components/FantasyLeague/FantasyLeagues.js';
import AddFantasyLeague from './Components/FantasyLeague/AddFantasyLeague.js';
import FantasyTeamTable from './Components/FantasyTeam/FantasyTeamTable.js';
import FantasyTeamDetails from './Components/FantasyTeam/FantasyTeamDetails';
import NewFantasyTeam from './Components/FantasyTeam/NewFantasyTeam.js';
import EditDriver from './Components/EditDriver.js';
import NewDriverForm from './Components/NewDriverForm.js';

function App() {
  return (
    <Router>
      <div className="App">
        <NavBar />
        <Routes>
          <Route path="/" element={<FantasyLeagues/>}/>
          <Route path="/fantasyLeagues" element={<FantasyLeagues/>}/>
          <Route path="/newFantasyLeague" element={<AddFantasyLeague/>}/>
          <Route exact path="/drivers" element={<Drivers/>}/>
          <Route path="/driver/:id" element={<EditDriver/>}/>
          {/* <Route path="/info" element={<Info/>}/> */}
          <Route path="/fantasyLeagues/:id" element={<FantasyTeamTable/>}/>
          <Route path="/fantasyTeam/:id" element={<FantasyTeamDetails/>}/>
          <Route path="/newFantasyTeam" element={<NewFantasyTeam/>}/>
          <Route path="/newDriverForm" element={<NewDriverForm/>}/>
        </Routes>
      </div>
    </Router>
  );
}

export default App;
