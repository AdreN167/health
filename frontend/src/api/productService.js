import useApi from "../hooks/useApi";

export const getProducts = async (token) => {
  const api = useApi(token);
  const response = await api.get("/api/Product");
  return response.data.data;
};
