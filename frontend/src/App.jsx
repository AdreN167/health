import { lazy, Suspense, useContext, useEffect } from "react";
import "./App.less";
import AuthContext from "./store/AuthContext";
import { getProducts } from "./api/productService";
import { Link, Routes, Route, useLocation } from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute/ProtectedRoute";
import ProductPage from "./pages/ProductPage/ProductPage";
import Login from "./pages/Login/Login";
import { routes } from "./common/constants";

function App() {
  const { isAuthenticated } = useContext(AuthContext);
  return (
    <>
      <Routes>
        <Route exact path="/auth" element={<Login />} />

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
