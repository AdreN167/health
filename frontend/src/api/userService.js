import useApi from "../hooks/useApi";

export const getUsersInfo = async (token, email) => {
  const api = useApi(token);
  const response = await api.get("/api/User?email=" + email);
  return response.data.data;
};

export const updateUsersInfo = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/User", body);
  return response.data.data;
};
