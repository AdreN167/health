import "./App.less";
import { Routes, Route, Navigate } from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute/ProtectedRoute";
import Login from "./pages/Login/Login";
import { routes } from "./common/constants";

function App() {
  return (
    <>
      <Routes>
        <Route exact path="/" element={<Navigate to="/auth" />} />
        <Route path="/auth" element={<Login />} />
        {routes.map(({ path, element }) => (
          <Route
            key={path}
            path={path}
            element={<ProtectedRoute>{element()}</ProtectedRoute>}
          />
        ))}
      </Routes>
    </>
  );
}

export default App;
