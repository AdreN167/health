import useApi from "../hooks/useApi";

export const getWorkouts = async (token, id) => {
  const api = useApi(token);
  const response = await api.get("/api/Workout/byGoal/" + id);
  return response.data.data;
};

export const createWorkout = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/Workout", body);
  return response.data.data;
};

export const updateWorkout = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Workout", body);
  return response.data.data;
};

export const updateListOfExercises = async (token, body) => {
  const api = useApi(token);
  const response = await api.put(
    "/api/Workout/update/updateListOfExercises",
    body
  );
  return response.data.data;
};

export const deleteWorkout = async (token, id) => {
  const api = useApi(token);
  const response = await api.delete("/api/Workout/" + id);
  return response.data.data;
};
