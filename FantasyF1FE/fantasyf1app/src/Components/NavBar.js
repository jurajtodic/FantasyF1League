import {Link} from 'react-router-dom';

function NavBar() {
    return (
      <div className="NavBar">
        <nav className="navbar navbar-expand navbar-dark bg-dark">
            <a href="/" className='navbar-brand mx-3'><i class="bi-speedometer2 mx-1 fs-4"></i></a>
            <div className='navbar-nav mr-auto'>
              <li className='nav-item mx-3 fs-5'><Link to="/fantasyLeagues" className='nav-link'>Fantasy Leagues</Link></li>
              <li className='nav-item mx-3 fs-5'><Link to="/drivers" className='nav-link'>Drivers</Link></li>
              {/* <li className='nav-item mx-3 fs-5'><Link to="/info" className='nav-link'>Info</Link></li> */}
            </div>
        </nav>
      </div>
    );
  }
  
  export default NavBar;