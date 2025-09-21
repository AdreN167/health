import useApi from "../hooks/useApi";

export const getGoals = async (token) => {
  const api = useApi(token);
  const response = await api.get("/api/Goal");
  return response.data.data;
};

export const getGoalsByUserEmail = async (token, email) => {
  const api = useApi(token);
  const response = await api.get("/api/Goal/email/" + email);
  return response.data.data;
};

export const getGoalById = async (token, id) => {
  const api = useApi(token);
  const response = await api.get("/api/Goal/" + id);
  return response.data.data;
};

export const createGoal = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/Goal", body);
  return response.data.data;
};

export const updateGoal = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Goal", body);
  return response.data.data;
};

export const deleteGoal = async (token, id) => {
  const api = useApi(token);
  const response = await api.delete("/api/Goal/" + id);
  return response.data.data;
};
