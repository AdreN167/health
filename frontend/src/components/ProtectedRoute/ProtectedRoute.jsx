import { useContext } from "react";
import AuthContext from "../../store/AuthContext";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children, ...rest }) => {
  const { isAuthenticated } = useContext(AuthContext);
  return isAuthenticated ? children : <Navigate to="/auth" />;
};

export default ProtectedRoute;
