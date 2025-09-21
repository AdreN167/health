import { createContext, useState } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const tryGetToken = () => {
    let token = localStorage.getItem("token");
    return token;
  };
  const tryGetRole = () => {
    let role = localStorage.getItem("role");
    return role;
  };
  const tryGetUserEmail = () => {
    let email = localStorage.getItem("email");
    return email;
  };

  const [token, setToken] = useState(tryGetToken());
  const [role, setRole] = useState(tryGetRole());
  const [email, setEmail] = useState(tryGetUserEmail());
  const [isAuthenticated, setIsAuthenticated] = useState(
    token !== null &&
      token !== undefined &&
      role !== null &&
      role !== undefined &&
      email !== null &&
      email !== undefined
  );

  const login = (newToken, role, email) => {
    setIsAuthenticated(true);
    setToken(newToken);
    setRole(role);
    setEmail(email);
    localStorage.setItem("token", newToken);
    localStorage.setItem("role", role);
    localStorage.setItem("email", email);
  };

  const logout = () => {
    setIsAuthenticated(false);
    setToken(null);
    setRole(null);
    setEmail(null);
    try {
      localStorage.removeItem("token");
      localStorage.removeItem("role");
      localStorage.removeItem("email");
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <AuthContext.Provider
      value={{ isAuthenticated, token, role, email, login, logout }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
