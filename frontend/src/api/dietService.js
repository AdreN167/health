import useApi from "../hooks/useApi";

export const getDiets = async (token, id) => {
  const api = useApi(token);
  const response = await api.get("/api/Diet/byGoal/" + id);
  return response.data.data;
};

export const createDiet = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/Diet", body);
  return response.data.data;
};

export const updateDiet = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Diet", body);
  return response.data.data;
};

export const updateListOfProductsAndDishes = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Diet/update/updateListOfFood", body);
  return response.data.data;
};

export const deleteDiet = async (token, id) => {
  const api = useApi(token);
  const response = await api.delete("/api/Diet/" + id);
  return response.data.data;
};
