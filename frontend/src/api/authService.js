import { useContext } from "react";
import useApi from "../hooks/useApi";
import AuthContext from "../store/AuthContext";

export const signIn = async (credentials) => {
  const api = useApi();
  const response = await api.post("/signIn", credentials);
  return response.data.data;
};
