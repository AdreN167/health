import useApi from "../hooks/useApi";

export const signIn = async (credentials) => {
  const api = useApi();
  const response = await api.post("/signIn", credentials);
  return response.data.data;
};

export const signUp = async (credentials) => {
  const api = useApi();
  const response = await api.post("/signUp", credentials);
  return response.data.data;
};
