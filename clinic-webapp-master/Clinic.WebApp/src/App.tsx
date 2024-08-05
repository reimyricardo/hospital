import { Route, Routes } from "react-router-dom";
import "./App.css";
import Navbar from "./shared/components/Navbar";
import { router } from "./shared/constants/routes";

function App() {
  return (
    <>
      <Navbar />
      <Routes>
        {router.map(({ path, element }, index) => (
          <Route path={path} element={element} key={index} />
        ))}
      </Routes>
    </>
  );
}

export default App;
