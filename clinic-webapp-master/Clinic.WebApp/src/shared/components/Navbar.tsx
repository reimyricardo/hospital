import { Link } from "react-router-dom";
import { routes } from "../constants/routes";

function Navbar(): React.JSX.Element {
  return (
    <nav className="navbar navbar-expand-sm navbar-dark bg-dark mb-4">
      <div className="container-fluid">
        <Link to={"/"} className="navbar-brand mb-0 h1">
          <img
            src="https://cache.clinic.co/assets/img/logo.png"
            alt="clinic-logo"
            className="me-2"
            height={60}
            width={60}
          />
          Clinic Web App
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div
          className="collapse navbar-collapse text-white"
          id="navbarSupportedContent"
        >
          <ul className="navbar-nav me-auto mb-3 mb-lg-0">
            {Object.entries(routes).map(([parentRoute, childRoute], index) => (
              <li key={index} className="nav-item dropdown">
                <button
                  className="nav-link dropdown-toggle text-white fs-5 fw-semibold"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  {parentRoute ?? "Unknonw father pathName"}
                </button>
                <ul className="dropdown-menu">
                  {childRoute.map(({ path, pathChildName }, index) => (
                    <li key={index}>
                      <Link to={path} className="dropdown-item">
                        {pathChildName ?? "Unknown child pathName"}
                      </Link>
                    </li>
                  ))}
                </ul>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
