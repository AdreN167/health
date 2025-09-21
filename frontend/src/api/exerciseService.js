import useApi from "../hooks/useApi";

export const getExercises = async (token) => {
  const api = useApi(token);
  const response = await api.get("/api/Exercise");
  return response.data.data;
};

export const createExercise = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/Exercise", body);
  return response.data.data;
};

export const updateExercise = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Exercise", body);
  return response.data.data;
};

export const deleteExercise = async (token, id) => {
  const api = useApi(token);
  const response = await api.delete("/api/Exercise/" + id);
  return response.data.data;
};
