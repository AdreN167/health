import { createContext, useState } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const tryGetToken = () => {
    let token = localStorage.getItem("token");
    console.log(token);
    return token;
  };

  const [token, setToken] = useState(tryGetToken());
  const [isAuthenticated, setIsAuthenticated] = useState(
    token !== null && token !== undefined
  );

  const login = (newToken) => {
    setIsAuthenticated(true);
    setToken(newToken);
    localStorage.setItem("token", newToken);
  };

  const logout = () => {
    setIsAuthenticated(false);
    setToken(null);
    try {
      localStorage.removeItem("token");
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
